using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using FeiniuBus.Grpc.LoadBalancer.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly ServiceDiscoveryConsulOptions _options;
        private readonly IServiceDiscoveryCache _cache;
        private readonly IServiceChangeTokenManager _manager;
        private readonly ILogger _logger;

        public ConsulServiceDiscovery(IOptions<ServiceDiscoveryConsulOptions> options, IServiceDiscoveryCache cache,
            IServiceChangeTokenManager manager, ILogger<ConsulServiceDiscovery> logger)
        {
            _options = options.Value;
            _cache = cache;
            _manager = manager;
            _logger = logger;
        }

        public IEnumerable<ServiceEndPoint> FindServiceEndpoint(string name)
        {
            return _cache.GetOrAdd(name, RemoteServiceEndpoint);
        }

        private IEnumerable<ServiceEndPoint> RemoteServiceEndpoint(string name)
        {
            var client = ConsulClientFactory.CreateConsulClient(_options);
            var queryOptions = QueryOptions.Default;
            queryOptions.Consistency = ConsistencyMode.Stale;
            if (!string.IsNullOrEmpty(_options.Token))
            {
                queryOptions.Token = _options.Token;
            }

            var queryResult = client.Health.Service(name, "", true, queryOptions).ConfigureAwait(false).GetAwaiter()
                .GetResult();

            if (!_manager.Contains(name))
            {
                var token = new ServiceChangeToken(name, queryResult.LastIndex);
                if (_manager.TryAdd(name, token))
                {
                    ThreadPool.QueueUserWorkItem(Watch, token);
                    ChangeToken.OnChange(() => token, InvokeChanged, token.Name);
                }
            }
            
            var list = new List<ServiceEndPoint>();
            foreach (var entry in queryResult.Response)
            {
                var endpoint = new ServiceEndPoint
                {
                    Id = entry.Service.ID,
                    Address = entry.Service.Address,
                    Port = entry.Service.Port,
                    Weight = 1,
                    EffectiveWeight = 1,
                    CurrentWeight = 0
                };
                
                list.Add(endpoint);
            }
            return list;
        }

        private void Watch(object state)
        {
            var token = (ServiceChangeToken) state;
            var client = new ConsulClient(c =>
            {
                c.Address = ConsulClientFactory.BuildConsulUri(_options);
                c.Datacenter = _options.Datacenter;
                c.WaitTime = c.WaitTime;
            });
            
            var queryOptions = QueryOptions.Default;
            if (!string.IsNullOrEmpty(_options.Token))
            {
                queryOptions.Token = _options.Token;
            }

            while (true)
            {
                queryOptions.WaitIndex = token.LastIndex;
                try
                {
                    var result = client.Health.Service(token.Name, "", true, queryOptions).ConfigureAwait(false)
                        .GetAwaiter().GetResult();

                    if (result.LastIndex != token.LastIndex)
                    {
                        token.LastIndex = result.LastIndex;
                        token.OnChange();
                    }
                }
                catch (TaskCanceledException)
                {
                }
                catch (Exception e)
                {
                    _logger.LogError(0, e, "监控远程服务时出错");
                }
            }
        }

        private void InvokeChanged(string name)
        {
            _cache.TryRemove(name);
            _cache.GetOrAdd(name, RemoteServiceEndpoint);
        }
    }
}
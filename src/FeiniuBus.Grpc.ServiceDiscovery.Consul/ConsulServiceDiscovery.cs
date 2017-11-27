using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Consul;
using FeiniuBus.Grpc.LoadBalancer.Abstractions;
using Microsoft.Extensions.Options;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly ServiceDiscoveryConsulOptions _options;

        public ConsulServiceDiscovery(IOptions<ServiceDiscoveryConsulOptions> options)
        {
            _options = options.Value;
        }

        public async Task<ServiceQueryResult> FindServiceEndpointAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var client = ConsulClientFactory.CreateConsulClient(_options);
            var queryOptions = QueryOptions.Default;
            queryOptions.Consistency = ConsistencyMode.Stale;
            if (!string.IsNullOrEmpty(_options.Token))
            {
                queryOptions.Token = _options.Token;
            }
            
            var queryResult = await client.Health.Service(name, "", true, queryOptions).ConfigureAwait(false);
            var result = new ServiceQueryResult
            {
                LastIndex = queryResult.LastIndex,
                EndPoints = new List<ServiceEndPoint>()
            };
            
            var list = new List<ServiceEndPoint>();
            foreach (var entry in queryResult.Response)
            {
                var endpoint = new ServiceEndPoint
                {
                    Id = entry.Service.ID,
                    Address = entry.Service.Address,
                    Port = entry.Service.Port
                };
                
                list.Add(endpoint);
            }

            result.EndPoints = list.AsReadOnly();
            return result;
        }
    }
}
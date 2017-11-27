using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<IEnumerable<ServiceEndPoint>> FindServiceEndpointAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            throw new NotImplementedException();
        }
    }
}
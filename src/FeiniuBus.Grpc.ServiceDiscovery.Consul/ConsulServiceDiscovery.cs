using System.Collections.Generic;
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
        
        public IEnumerable<ServiceEndPoint> FindServiceEndpoint(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
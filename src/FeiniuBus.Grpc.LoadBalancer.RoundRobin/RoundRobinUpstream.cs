using FeiniuBus.Grpc.LoadBalancer.Abstractions;

namespace FeiniuBus.Grpc.LoadBalancer.RoundRobin
{
    public class RoundRobinUpstream : IUpstream
    {
        private readonly IServiceDiscovery _serviceDiscovery;

        public RoundRobinUpstream(IServiceDiscovery serviceDiscovery)
        {
            _serviceDiscovery = serviceDiscovery;
        }
        
        public ServiceEndPoint Peer(string serviceName)
        {
            throw new System.NotImplementedException();
        }
    }
}
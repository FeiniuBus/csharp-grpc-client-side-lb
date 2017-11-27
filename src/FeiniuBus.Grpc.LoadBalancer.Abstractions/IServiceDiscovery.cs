using System.Collections.Generic;

namespace FeiniuBus.Grpc.LoadBalancer.Abstractions
{
    public interface IServiceDiscovery
    {
        IEnumerable<ServiceEndPoint> FindServiceEndpoint(string name);
    }
}
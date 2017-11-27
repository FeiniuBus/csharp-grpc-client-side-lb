using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeiniuBus.Grpc.LoadBalancer.Abstractions
{
    public interface IServiceDiscovery
    {
        Task<ServiceQueryResult> FindServiceEndpointAsync(string name);
    }
}
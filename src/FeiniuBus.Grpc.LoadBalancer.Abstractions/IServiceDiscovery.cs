using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeiniuBus.Grpc.LoadBalancer.Abstractions
{
    public interface IServiceDiscovery
    {
        Task<IEnumerable<ServiceEndPoint>> FindServiceEndpointAsync(string name);
    }
}
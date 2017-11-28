using System.Linq;
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
            var entries = _serviceDiscovery.FindServiceEndpoint(serviceName).ToList();
            if (entries.Count == 0)
            {
                return null;
            }

            if (entries.Count == 1)
            {
                return entries[0];
            }

            int total = 0;
            ServiceEndPoint best = null;
            
            foreach (var entry in entries)
            {
                entry.CurrentWeight += entry.EffectiveWeight;
                total += entry.EffectiveWeight;

                if (entry.EffectiveWeight < entry.Weight)
                {
                    entry.EffectiveWeight++;
                }

                if (best == null || entry.CurrentWeight > best.CurrentWeight)
                {
                    best = entry;
                }
            }

            if (best == null)
            {
                return null;
            }

            best.CurrentWeight -= total;
            return best;
        }
    }
}
using System;
using System.Collections.Generic;
using FeiniuBus.Grpc.LoadBalancer.Abstractions;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public interface IServiceDiscoveryCache
    {
        IEnumerable<ServiceEndPoint> GetOrAdd(string name, Func<string, IEnumerable<ServiceEndPoint>> createEntries);

        bool TryAdd(string name, IEnumerable<ServiceEndPoint> entries);

        bool TryRemove(string name);

        void Clear();
    }
}
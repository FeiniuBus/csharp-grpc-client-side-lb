using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FeiniuBus.Grpc.LoadBalancer.Abstractions;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public class ServiceDiscoveryCache : IServiceDiscoveryCache
    {
        private readonly ConcurrentDictionary<string, IEnumerable<ServiceEndPoint>> _cache =
            new ConcurrentDictionary<string, IEnumerable<ServiceEndPoint>>();

        public IEnumerable<ServiceEndPoint> GetOrAdd(string name, Func<string, IEnumerable<ServiceEndPoint>> createEntries)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (createEntries == null)
            {
                throw new ArgumentNullException(nameof(createEntries));
            }

            return _cache.GetOrAdd(name, createEntries);
        }

        public bool TryAdd(string name, IEnumerable<ServiceEndPoint> entries)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (entries == null)
            {
                throw new ArgumentNullException(nameof(entries));
            }

            return _cache.TryAdd(name, entries);
        }

        public bool TryRemove(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _cache.TryRemove(name, out var _);
        }

        public void Clear() => _cache.Clear();
    }
}
using System;
using System.Collections.Concurrent;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public class ServiceChangeTokenManager : IServiceChangeTokenManager
    {
        private readonly ConcurrentDictionary<string, ServiceChangeToken> _tokens =
            new ConcurrentDictionary<string, ServiceChangeToken>();


        public bool Contains(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _tokens.ContainsKey(name);
        }

        public bool TryAdd(string name, ServiceChangeToken token)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return _tokens.TryAdd(name, token);
        }

        public bool TryRemove(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _tokens.TryRemove(name, out var _);
        }

        public void Clear()
        {
            _tokens.Clear();
        }
    }
}
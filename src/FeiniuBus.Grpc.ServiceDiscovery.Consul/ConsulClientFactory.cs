using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Consul;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    internal static class ConsulClientFactory
    {
        private static readonly IDictionary<string, IConsulClient> ConsulClientCache =
            new ConcurrentDictionary<string, IConsulClient>();

        private static readonly ReaderWriterLockSlim ConsulClientCacheLock = new ReaderWriterLockSlim();
        
        public static IConsulClient CreateConsulClient(ServiceDiscoveryConsulOptions options)
        {
            IConsulClient client;
            bool found;
            var uniqueString = CreateOptionUniqueString(options);
            ConsulClientCacheLock.EnterReadLock();

            try
            {
                found = ConsulClientCache.TryGetValue(uniqueString, out client);
            }
            finally
            {
                ConsulClientCacheLock.ExitReadLock();
            }

            if (found)
            {
                return client;
            }
            
            ConsulClientCacheLock.EnterWriteLock();
            try
            {
                found = ConsulClientCache.TryGetValue(uniqueString, out client);
                if (found)
                {
                    return client;
                }

                client = NewConsulClient(options);
                ConsulClientCache[uniqueString] = client;
                return client;
            }
            finally
            {
                ConsulClientCacheLock.ExitWriteLock();
            }
        }

        private static IConsulClient NewConsulClient(ServiceDiscoveryConsulOptions options)
        {
            var client = new ConsulClient(c =>
            {
                var builder = new UriBuilder
                {
                    Host = options.Address,
                    Port = options.Port,
                    Scheme = options.Scheme
                };

                c.Address = builder.Uri;
                c.Datacenter = options.Datacenter;
                c.WaitTime = options.WaitTime;
            });

            return client;
        }

        private static string CreateOptionUniqueString(ServiceDiscoveryConsulOptions options)
        {
            var uniqueString = string.Format("{0}-{1}-{2}-{3}", options.Scheme, options.Address, options.Port,
                options.Datacenter);
            
            if (options.WaitTime.HasValue)
            {
                uniqueString = string.Concat(uniqueString, $"-{options.WaitTime.Value.ToString()}");
            }

            return uniqueString;
        }
    }
}
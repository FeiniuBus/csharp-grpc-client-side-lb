using System;
using FeiniuBus.Grpc.LoadBalancer.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddConsulServiceDiscovery(this IServiceCollection services,
            Action<ServiceDiscoveryConsulOptions> configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddOptions();
            services.TryAdd(ServiceDescriptor
                .Singleton<IPostConfigureOptions<ServiceDiscoveryConsulOptions>,
                    PostConfigureServiceDiscoveryConsulOptions>());

            services.TryAdd(ServiceDescriptor.Singleton<IServiceDiscoveryCache, ServiceDiscoveryCache>());
            services.TryAdd(ServiceDescriptor.Singleton<IServiceChangeTokenManager, ServiceChangeTokenManager>());
            services.TryAdd(ServiceDescriptor.Singleton<IServiceDiscovery, ConsulServiceDiscovery>());

            return services;
        }
    }
}
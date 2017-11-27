using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace FeiniuBus.Grpc.LoadBalancer.RoundRobin
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRoundRobin(this IServiceCollection services,
            Action<RoundRobinOptions> configuration)
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
                .Singleton<IPostConfigureOptions<RoundRobinOptions>, PostConfigureRoundRobinOptions>());
            
            return services;
        }
    }
}
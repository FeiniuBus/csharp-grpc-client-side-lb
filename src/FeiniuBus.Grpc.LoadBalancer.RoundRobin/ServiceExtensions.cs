using System;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.Grpc.LoadBalancer.RoundRobin
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRoundRobin(this IServiceCollection services,
            Action<RoundRobinOptions> configuration)
        {
            return services;
        }
    }
}
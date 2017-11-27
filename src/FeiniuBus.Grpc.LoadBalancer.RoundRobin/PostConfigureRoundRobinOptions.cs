using Microsoft.Extensions.Options;

namespace FeiniuBus.Grpc.LoadBalancer.RoundRobin
{
    public class PostConfigureRoundRobinOptions : IPostConfigureOptions<RoundRobinOptions>
    {
        public void PostConfigure(string name, RoundRobinOptions options)
        {
            if (string.IsNullOrEmpty(options.ConsulAddress))
            {
                options.ConsulAddress = "localhost";
            }

            if (options.ConsulPort == 0)
            {
                options.ConsulPort = 8500;
            }

            if (string.IsNullOrEmpty(options.ConsulDatacenter))
            {
                options.ConsulDatacenter = "dc1";
            }
        }
    }
}
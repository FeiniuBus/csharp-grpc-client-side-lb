using Microsoft.Extensions.Options;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public class PostConfigureServiceDiscoveryConsulOptions : IPostConfigureOptions<ServiceDiscoveryConsulOptions>
    {
        public void PostConfigure(string name, ServiceDiscoveryConsulOptions options)
        {
            if (string.IsNullOrEmpty(options.Address))
            {
                options.Address = "127.0.0.1";
            }

            if (options.Port == 0)
            {
                options.Port = 8500;
            }

            if (string.IsNullOrEmpty(options.Datacenter))
            {
                options.Datacenter = "dc1";
            }

            if (string.IsNullOrEmpty(options.Scheme))
            {
                options.Scheme = "http";
            }
        }
    }
}
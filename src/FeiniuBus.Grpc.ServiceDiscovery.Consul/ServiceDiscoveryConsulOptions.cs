using System;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public class ServiceDiscoveryConsulOptions
    {
        public string Address { get; set; }
        
        public int Port { get; set; }
        
        public string Datacenter { get; set; }
        
        public TimeSpan? WaitTime { get; set; }
        
        public string Token { get; set; }
        
        public string Scheme { get; set; }
    }
}
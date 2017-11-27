using System.Collections.Generic;

namespace FeiniuBus.Grpc.LoadBalancer.Abstractions
{
    public class ServiceQueryResult
    {
        public ulong LastIndex { get; set; }
        
        public IReadOnlyCollection<ServiceEndPoint> EndPoints { get; set; }
    }
    
    public class ServiceEndPoint
    {
        public string Id { get; set; }
        
        public string Address { get; set; }
        
        public int Port { get; set; }
    }
}
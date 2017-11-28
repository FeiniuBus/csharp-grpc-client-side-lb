namespace FeiniuBus.Grpc.LoadBalancer.Abstractions
{   
    public class ServiceEndPoint
    {
        public string Id { get; set; }
        
        public string Address { get; set; }
        
        public int Port { get; set; }
        
        public int Weight { get; set; }
        
        public int EffectiveWeight { get; set; }
        
        public int CurrentWeight { get; set; }
    }
}
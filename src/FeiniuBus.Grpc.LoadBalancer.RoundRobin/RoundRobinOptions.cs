namespace FeiniuBus.Grpc.LoadBalancer.RoundRobin
{
    public class RoundRobinOptions
    {
        public string ConsulAddress { get; set; }
        
        public int ConsulPort { get; set; }
        
        public string ConsulDatacenter { get; set; }
    }
}
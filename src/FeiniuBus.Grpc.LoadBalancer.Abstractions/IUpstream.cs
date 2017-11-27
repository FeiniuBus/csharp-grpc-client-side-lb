namespace FeiniuBus.Grpc.LoadBalancer.Abstractions
{
    public interface IUpstream
    {
        ServiceEndPoint Peer(string serviceName);
    }
}
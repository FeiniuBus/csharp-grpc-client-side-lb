namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public interface IServiceChangeTokenManager
    {
        bool Contains(string name);
        
        bool TryAdd(string name, ServiceChangeToken token);

        bool TryRemove(string name);

        void Clear();
    }
}
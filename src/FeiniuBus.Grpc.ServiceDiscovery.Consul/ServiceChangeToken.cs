using System;
using System.Threading;
using Microsoft.Extensions.Primitives;

namespace FeiniuBus.Grpc.ServiceDiscovery.Consul
{
    public class ServiceChangeToken : IChangeToken
    {
        public ServiceChangeToken(string name, ulong index)
        {
            Name = name;
            LastIndex = index;
        }
        
        public string Name { get; set; }
        
        public ulong LastIndex { get; set; }
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) =>
            _cts.Token.Register(callback, state);

        public bool HasChanged => _cts.IsCancellationRequested;
        
        public bool ActiveChangeCallbacks => true;

        public void OnChange() => _cts.Cancel();
    }
}
using Autofac;

namespace FileTransferApi.Provider
{
    public class TransferServiceFactory : ITransferServiceFactory
    {
        private readonly ILifetimeScope _scope;

        public TransferServiceFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IProviderSettings GetPlatformSettings(string providerType)
        {
            return _scope.ResolveKeyed<IProviderSettings>(providerType);
        }

        public IProviderSettingsValidator GetPlatformSettingsValidator(string providerType)
        {
            return _scope.ResolveKeyed<IProviderSettingsValidator>(providerType);
        }

        public ITransferService GetTransferService(string providerType)
        {
            return _scope.ResolveKeyed<ITransferService>(providerType);
        }
    }
}

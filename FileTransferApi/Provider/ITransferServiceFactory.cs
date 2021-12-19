
namespace FileTransferApi.Provider
{
    public interface ITransferServiceFactory
    {
        ITransferService GetTransferService(string providerType);
        IProviderSettings GetPlatformSettings(string providerType);
        IProviderSettingsValidator GetPlatformSettingsValidator(string providerType);
    }
}

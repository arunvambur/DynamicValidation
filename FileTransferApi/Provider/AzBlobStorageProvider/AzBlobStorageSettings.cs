

using FileTransferApi.Provider;

namespace FileTransferApi.Provider.AzBlobStorageProvider
{
    public class AzBlobStorageSettings : IProviderSettings
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string ObjectName { get; set; }
    }
}

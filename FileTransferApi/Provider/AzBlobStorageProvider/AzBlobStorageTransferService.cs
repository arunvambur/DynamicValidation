using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using FileTransferApi.Provider.AzBlobStorageProvider;
using FileTransferApi.Model;
using FileTransferApi.Provider;
using FileTransferApi.Provider.DataStream;
using System;

namespace FileTransferApi.Provider.AzBlobStorageProvider
{
    public class AzBlobStorageTransferService : ITransferService
    {
        public string GetMountPath(Node node)
        {
            throw new System.NotImplementedException();
        }

        public async Task Pull(Node node, IDataStream stream, CancellationToken cancellationToken = default)
        {
            string settingsJson = Convert.ToString(node.Settings).Replace(Environment.NewLine, string.Empty);
            AzBlobStorageSettings settings = JsonConvert.DeserializeObject<AzBlobStorageSettings>(settingsJson);
            AzBlobStorageManager azBlobStorageManager = new AzBlobStorageManager(settings, node.ObjectName);
            await azBlobStorageManager.Read(stream.GetStream());
        }

        public async Task Push(Node node, IDataStream stream, CancellationToken cancellationToken = default)
        {
            string settingsJson = Convert.ToString(node.Settings).Replace(Environment.NewLine, string.Empty);
            AzBlobStorageSettings settings = JsonConvert.DeserializeObject<AzBlobStorageSettings>(settingsJson);
            AzBlobStorageManager azBlobStorageManager = new AzBlobStorageManager(settings, node.ObjectName);
            await azBlobStorageManager.Write(stream.GetStream());
        }
    }
}

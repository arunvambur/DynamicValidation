using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

using FileTransferApi;

namespace FileTransferApi.Provider.AzBlobStorageProvider
{
    public class AzBlobStorageManager
    {
        private readonly AzBlobStorageSettings _settings;
        BlobServiceClient blobService;
        string _objectname;

        public AzBlobStorageManager(AzBlobStorageSettings settings, string objectName)
        {
            _settings = settings;
            _objectname = objectName;
            blobService = new BlobServiceClient(settings.ConnectionString);

        }

        public async Task<BlobDownloadInfo> Read(Stream stream)
        {
            try
            {
                BlobContainerClient containerClient = blobService.GetBlobContainerClient(_settings.ContainerName);

                BlobClient blobClient = containerClient.GetBlobClient(_objectname);

                BlobDownloadInfo download = await blobClient.DownloadAsync();


                await download.Content.CopyToAsync(stream);

                return download;
            }
            catch (Exception ex)
            {
                throw new FileTransferException($"An error occurred with the message '{ex.Message}' when reading an object", ex);
            }

        }

        public async Task<BlobContentInfo> Write(Stream stream)
        {
            try
            {
                BlobContainerClient containerClient = blobService.GetBlobContainerClient(_settings.ContainerName);


                BlobClient blobClient = containerClient.GetBlobClient(_objectname);

                

                stream.Position = 0;
                BlobContentInfo blobContentInfo = await blobClient.UploadAsync(stream);

                return blobContentInfo;
            }
            catch(Exception ex)
            {
                throw new FileTransferException($"An error occurred with the message '{ex.Message}' when writing an object", ex);
            }
        }
    }
}

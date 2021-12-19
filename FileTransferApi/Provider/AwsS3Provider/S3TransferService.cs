using Amazon.Runtime;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using FileTransferApi.Model;
using FileTransferApi.Provider;
using FileTransferApi.Provider.DataStream;
using System;

namespace FileTransferApi.Provider.AwsS3Provider
{
    public class S3TransferService : ITransferService
    {
        public async Task Pull(Node node, IDataStream stream,  CancellationToken cancellationToken = default)
        {
            string settingsJson = Convert.ToString(node.Settings).Replace(Environment.NewLine, string.Empty);
            AwsS3Settings settings = JsonConvert.DeserializeObject<AwsS3Settings>(settingsJson);
            AwsS3Manager awsS3Manager = new AwsS3Manager(settings, node.ObjectName);
            await awsS3Manager.Read(stream.GetStream());
        }

        public async Task Push(Node node, IDataStream stream, CancellationToken cancellationToken = default)
        {
            string settingsJson = Convert.ToString(node.Settings).Replace(Environment.NewLine, string.Empty);
            AwsS3Settings settings = JsonConvert.DeserializeObject<AwsS3Settings>(settingsJson);
            AwsS3Manager awsS3Manager = new AwsS3Manager(settings, node.ObjectName);
            await awsS3Manager.Write(stream.GetStream());
            
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

using FileTransferApi.Model;
using FileTransferApi.Provider.DataStream;


namespace FileTransferApi.Provider
{
    //public delegate void StatusUpdate(DataTransferStatus dataTransferStatus);

    /// <summary>
    /// Transfer service interface for providers, it provide the interfaces for pull or push the files to the nodes
    /// </summary>
    public interface ITransferService
    {
        /// <summary>
        /// Pull the data from the node and write it to stream
        /// </summary>
        /// <param name="node">Source node</param>
        /// <param name="stream">Data stream where the file data is written</param>
        /// <returns></returns>
        Task Pull(Node node, IDataStream stream, CancellationToken cancellationToken = default);
        

        /// <summary>
        /// Push the stream data to the destination node
        /// </summary>
        /// <param name="node">Destination node</param>
        /// <param name="stream">Data stream where the file data is written</param>
        /// <returns></returns>
        Task Push(Node node, IDataStream stream, CancellationToken cancellationToken = default);
    }
}

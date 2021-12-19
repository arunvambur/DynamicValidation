using System;
using System.IO;
using System.Threading.Tasks;

namespace FileTransferApi.Provider.DataStream
{
    public interface IDataStream : IDisposable
    {
        Task<int> Read(byte[] data);
        Task Write(byte[] data);

        Stream GetStream();
    }
}

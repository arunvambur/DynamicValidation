using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileTransferApi.Provider.DataStream
{
    public class DataStream : IDataStream, IDisposable
    {
        MemoryStream _memoryStream;

        public DataStream()
        {
            _memoryStream = new MemoryStream();
        }

        public void Dispose()
        {
            if (_memoryStream != null) _memoryStream.Dispose();
        }

        public Stream GetStream()
        {
            return _memoryStream;
        }

        public async Task<int> Read(byte[] data)
        {
            return await _memoryStream.ReadAsync(data);
        }

        public async Task Write(byte[] data)
        {
            await _memoryStream.WriteAsync(data);
        }
    }
}

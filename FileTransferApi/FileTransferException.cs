using System;

namespace FileTransferApi
{
    public class FileTransferException : Exception
    {
        public FileTransferException()
        { }

        public FileTransferException(string message)
            : base(message)
        { }

        public FileTransferException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}

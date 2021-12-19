using System;

namespace FileTransferApi.Model
{
    public class TransferRequest
    {
        /// <summary>
        /// Source node where the file to be copied from
        /// </summary>
        public Node Source { get; set; }

        /// <summary>
        /// Destination node where the file copied to
        /// </summary>
        public Node Destination { get; set; }

       
    }

   
}

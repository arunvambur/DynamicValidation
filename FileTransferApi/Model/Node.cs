using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransferApi.Model
{
    public class Node
    {
        /// <summary>
        /// Name of the file
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Type of the cloud service
        /// </summary>
        public string ProviderType { get; set; }

        /// <summary>
        /// Settings for the respective cloud
        /// </summary>
        public dynamic Settings { get; set; }

    }
}

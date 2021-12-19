using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransferApi.Model
{
    public class ProviderType : Enumeration
    {
        public static ProviderType AWS_S3 = new ProviderType(1, "aws-s3");
        public static ProviderType AZ_BLOB_STORAGE = new ProviderType(1, "az-blob-storage");

        public ProviderType(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<ProviderType> List() =>
           new[] { AWS_S3, AZ_BLOB_STORAGE };

        public static ProviderType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for TransferStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ProviderType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for TransferStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}

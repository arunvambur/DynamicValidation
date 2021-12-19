using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;

using FileTransferApi;

namespace FileTransferApi.Provider.AwsS3Provider
{
    public class AwsS3Manager
    {
        readonly IAmazonS3 _amazonS3;
        readonly AwsS3Settings _awsS3Settings;
        readonly string _objectName;

        public AwsS3Manager(AwsS3Settings awsS3Settings, string objectName)
        {
            _awsS3Settings = awsS3Settings;
            _objectName = objectName;

            AmazonS3Config amazonS3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.EnumerableAllRegions.Where(t => t.SystemName == _awsS3Settings.RegionEndPoint).FirstOrDefault()
            };
            _amazonS3 = new AmazonS3Client(_awsS3Settings.AccessKeyID, _awsS3Settings.SecretKey, amazonS3Config);
        }



        public async Task<GetObjectResponse> Read(Stream stream)
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest()
                {
                    BucketName = _awsS3Settings.BucketName,
                    Key = _objectName
                };

                GetObjectResponse response = await _amazonS3.GetObjectAsync(request);
                string title = response.Metadata["x-amz-meta-title"];

                using (Stream st = response.ResponseStream)
                {
                    await st.CopyToAsync(stream);
                }
                
                return response;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new FileTransferException("Please check the provided AWS Credentials.", amazonS3Exception);

                }
                else
                {
                    throw new FileTransferException($"An error occurred with the message '{amazonS3Exception.Message}' when reading an object", amazonS3Exception);
                }
            }
        }

        public async Task<PutObjectResponse> Write(Stream stream)
        {
            try
            {
                // simple object put
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = stream,
                    BucketName = _awsS3Settings.BucketName,
                    Key = _objectName
                };

                request.Metadata.Add("title", "the title");

                PutObjectResponse response = await _amazonS3.PutObjectAsync(request);

                return response;

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new FileTransferException("Please check the provided AWS Credentials.", amazonS3Exception);
                    
                }
                else
                {
                    throw new FileTransferException($"An error occurred with the message '{amazonS3Exception.Message}' when writing an object", amazonS3Exception);
                }
            }
        }
    }
}

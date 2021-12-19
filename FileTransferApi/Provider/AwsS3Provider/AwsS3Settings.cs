

namespace FileTransferApi.Provider.AwsS3Provider
{
    public class AwsS3Settings : IProviderSettings
    {

        public string RegionEndPoint { get; set; }
        public string BucketName { get; set; }
        public string AccessKeyID { get; set; }
        public string SecretKey { get; set; }
        public string SessionToken { get; set; }
    }
}

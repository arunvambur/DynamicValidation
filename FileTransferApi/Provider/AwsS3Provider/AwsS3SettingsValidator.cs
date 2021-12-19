using System;
using System.Linq;
using Amazon;
using FluentValidation;


namespace FileTransferApi.Provider.AwsS3Provider
{
    public class AwsS3SettingsValidator : ProviderSettingsValidator<AwsS3Settings>
    {
        public AwsS3SettingsValidator()
        {
            RuleFor(t => t.RegionEndPoint).NotEmpty()
                .Must(IsRegionEndpointValid).WithMessage($"The region endpoint is not valid. Possible values are ({string.Join(", ", RegionEndpoint.EnumerableAllRegions.Select(t => t.SystemName.ToLowerInvariant()))})");
            RuleFor(t => t.BucketName).NotEmpty();
            RuleFor(t => t.AccessKeyID).NotEmpty().Matches("(?<![A-Z0-9])[A-Z0-9]{20}(?![A-Z0-9])");
            RuleFor(t => t.SecretKey).NotEmpty();
            //RuleFor(t => t.SessionToken).NotEmpty();
        }

        private bool IsRegionEndpointValid(string regionEndPoint)
        {
            if (string.IsNullOrEmpty(regionEndPoint)) return false;
            var names = RegionEndpoint.EnumerableAllRegions.Select(t=>t.SystemName.ToLowerInvariant()).ToArray();
            return names.Contains(regionEndPoint.ToLowerInvariant());
        }
    }
}

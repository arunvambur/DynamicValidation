using System;
using System.Collections.Generic;
using System.Text;
using FileTransferApi.Provider;
using FluentValidation;


namespace FileTransferApi.Provider.AzBlobStorageProvider
{
    public class AzBlobStorageSettingsValidator : ProviderSettingsValidator<AzBlobStorageSettings>
    {
        public AzBlobStorageSettingsValidator()
        {
            RuleFor(t => t.ConnectionString).NotEmpty();
            RuleFor(t => t.ContainerName).NotEmpty();
        }
    }
}

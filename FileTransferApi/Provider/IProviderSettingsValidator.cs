using FluentValidation.Results;

namespace FileTransferApi.Provider
{
    public interface IProviderSettingsValidator
    {
        ValidationResult Validate(string settingsJson, string prefix);
    }
}

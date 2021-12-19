using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace FileTransferApi.Provider
{
    public class ProviderSettingsValidator<T> : AbstractValidator<T>, IProviderSettingsValidator where T : IProviderSettings
    {
        public ProviderSettingsValidator()
        {
        }

        public ValidationResult Validate(string settingsJson, string prefix)
        {
            var settings = JsonConvert.DeserializeObject<T>(settingsJson);
            var vr = this.Validate(settings);
            foreach(var vf in vr.Errors)
            {
                vf.ErrorMessage = $"{prefix} {vf.ErrorMessage}";
            }
            return vr;
        }
    }
}

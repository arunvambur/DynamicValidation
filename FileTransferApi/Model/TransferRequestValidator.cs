using FluentValidation;
using System.Linq;


namespace FileTransferApi.Model
{
    public class TransferRequestValidator : AbstractValidator<TransferRequest>
    {
        private const string providerTypeErrorMessage = "$The provider type is not valid.";

        public TransferRequestValidator()
        {

            RuleFor(t => t.Source).NotNull();
            When(t => t.Source != null, () =>
            {
                RuleFor(t => t.Source.ObjectName).NotEmpty();
                RuleFor(t => t.Source.Settings).NotNull();
                RuleFor(t => t.Source.ProviderType)
                .NotEmpty()
                .Must(ValidateProviderType).WithMessage(providerTypeErrorMessage);
            });


            RuleFor(t => t.Destination).NotNull();
            When(t => t.Destination != null, () =>
            {
                RuleFor(t => t.Destination.ObjectName).NotEmpty();
                RuleFor(t => t.Destination.Settings).NotNull();
                RuleFor(t => t.Destination.ProviderType)
                .NotEmpty()
                .Must(ValidateProviderType).WithMessage(providerTypeErrorMessage);
            });
        }

        private bool ValidateProviderType(string providerType)
        {
            return ProviderType.GetAll<ProviderType>().Any(t => t.Name == providerType.ToLower()) ;
        }
    }
}

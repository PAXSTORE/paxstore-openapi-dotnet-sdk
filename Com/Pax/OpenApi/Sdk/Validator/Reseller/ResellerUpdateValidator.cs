using Com.Pax.OpenApi.Sdk.Dto.Reseller;
using FluentValidation;

namespace Com.Pax.OpenApi.Sdk.Validator.Reseller
{
    public class ResellerUpdateValidator: AbstractValidator<ResellerUpdateRequest>
    {
        public ResellerUpdateValidator() {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Contact).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Phone).MaximumLength(32);
            RuleFor(x => x.Postcode).MaximumLength(16);
            RuleFor(x => x.Address).MaximumLength(255);
            RuleFor(x => x.Company).MaximumLength(255);
            RuleFor(x => x.ParentResellerName).MaximumLength(64);
        }
    }
}
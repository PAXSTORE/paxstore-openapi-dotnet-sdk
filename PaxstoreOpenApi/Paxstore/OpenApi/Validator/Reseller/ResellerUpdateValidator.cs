using FluentValidation;
using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Validator.Reseller
{
    public class ResellerUpdateValidator: AbstractValidator<ResellerUpdateRequest>
    {
        public ResellerUpdateValidator() {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Contact).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(32);
            RuleFor(x => x.Postcode).MaximumLength(16);
            RuleFor(x => x.Address).MaximumLength(255);
            RuleFor(x => x.Company).MaximumLength(255);
            RuleFor(x => x.ParentResellerName).MaximumLength(64);
        }
    }
}
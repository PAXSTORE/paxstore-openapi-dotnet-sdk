using FluentValidation;
using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Validator.Merchant
{
    public class MerchantUpdateValidator: AbstractValidator<MerchantUpdateRequest>
    {
        public MerchantUpdateValidator() {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Email).MaximumLength(255);
            RuleFor(x => x.ResellerName).MaximumLength(64);
            RuleFor(x => x.Contact).MaximumLength(64);
            RuleFor(x => x.Country).MaximumLength(64);
            RuleFor(x => x.Phone).MaximumLength(32);
            RuleFor(x => x.Postcode).MaximumLength(16);
            RuleFor(x => x.Address).MaximumLength(255);
            RuleFor(x => x.Description).MaximumLength(3000);
            RuleFor(x => x.City).MaximumLength(255);
            RuleFor(x => x.Province).MaximumLength(64);
        }
    }
}
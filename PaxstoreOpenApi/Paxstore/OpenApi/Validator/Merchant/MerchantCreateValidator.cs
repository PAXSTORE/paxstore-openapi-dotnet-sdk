using FluentValidation;
using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Validator.Merchant
{
    public class MerchantCreateValidator:  AbstractValidator<MerchantCreateRequest>
    {
        public MerchantCreateValidator() {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x => x.ResellerName).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Contact).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(32);
            RuleFor(x => x.Postcode).MaximumLength(16);
            RuleFor(x => x.Address).MaximumLength(255);
            RuleFor(x => x.Description).MaximumLength(3000);
            RuleFor(x => x.City).MaximumLength(255);
            RuleFor(x => x.Province).MaximumLength(64);
        }
    }
}
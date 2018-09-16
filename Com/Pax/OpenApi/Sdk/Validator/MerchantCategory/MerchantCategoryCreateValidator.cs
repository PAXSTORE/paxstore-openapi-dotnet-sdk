using Com.Pax.OpenApi.Sdk.Dto.MerchantCategory;
using FluentValidation;

namespace Com.Pax.OpenApi.Sdk.Validator.MerchantCategory
{
    public class MerchantCategoryCreateValidator: AbstractValidator<MerchantCategoryCreateRequest>
    {
        public MerchantCategoryCreateValidator(){
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Remarks).MaximumLength(255);
        }
    }
}
using Com.Pax.OpenApi.Sdk.Dto.MerchantCategory;
using FluentValidation;

namespace Com.Pax.OpenApi.Sdk.Validator.MerchantCategory
{
    public class MerchantCategoryUpdateValidator: AbstractValidator<MerchantCategoryUpdateRequest>
    {
        public MerchantCategoryUpdateValidator(){
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Remarks).MaximumLength(255);
        }
    }
}
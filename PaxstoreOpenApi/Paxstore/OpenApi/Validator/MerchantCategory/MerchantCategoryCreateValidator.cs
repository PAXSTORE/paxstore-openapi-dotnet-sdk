using FluentValidation;
using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Validator.MerchantCategory
{
    public class MerchantCategoryCreateValidator: AbstractValidator<MerchantCategoryCreateRequest>
    {
        public MerchantCategoryCreateValidator(){
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Remarks).MaximumLength(255);
        }
    }
}
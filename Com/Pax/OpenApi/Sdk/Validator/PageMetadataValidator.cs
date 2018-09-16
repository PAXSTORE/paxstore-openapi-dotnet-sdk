using Com.Pax.OpenApi.Sdk.Base.Dto;
using FluentValidation;

namespace Com.Pax.OpenApi.Sdk.Validator
{
    public class PageMetadataValidator: AbstractValidator<PageMetadata>
    {
        public PageMetadataValidator(){
            RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(Constants.MAX_PAGE_SIZE);
            RuleFor(x => x.PageNo).GreaterThan(0);
        }
    }
}
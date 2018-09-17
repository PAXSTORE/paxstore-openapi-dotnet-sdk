using FluentValidation;
using Paxstore.OpenApi.Base.Dto;

namespace Paxstore.OpenApi.Validator
{
    public class PageMetadataValidator: AbstractValidator<PageMetadata>
    {
        public PageMetadataValidator(){
            RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(Constants.MAX_PAGE_SIZE);
            RuleFor(x => x.PageNo).GreaterThan(0);
        }
    }
}
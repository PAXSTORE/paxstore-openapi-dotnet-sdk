using FluentValidation;
using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Validator.Terminal
{
    public class TerminalUpdateValidator: AbstractValidator<TerminalUpdateRequest>
    {
        public TerminalUpdateValidator(){
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.TID).MinimumLength(8).MaximumLength(16);
            RuleFor(x => x.SerialNo).MaximumLength(32);
            RuleFor(x => x.MerchantName).MaximumLength(64);
            RuleFor(x => x.ResellerName).NotEmpty().MaximumLength(64);
            RuleFor(x => x.ModelName).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Location).MaximumLength(32);
        }
    }
}
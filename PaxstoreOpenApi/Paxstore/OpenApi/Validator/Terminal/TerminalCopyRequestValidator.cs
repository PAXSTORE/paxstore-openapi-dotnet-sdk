using FluentValidation;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.Terminal
{
    public class TerminalCopyRequestValidator : AbstractValidator<TerminalCopyRequest>
    {
        public TerminalCopyRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Tid).MinimumLength(8).MaximumLength(16);
            RuleFor(x => x.SerialNo).MaximumLength(32);
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}

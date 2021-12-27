using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Validator.TerminlGroupRki
{
    public class CreateTerminalGroupRkiTaskValidator : AbstractValidator<CreateTerminalGroupRkiTaskRequest>
    {
        public CreateTerminalGroupRkiTaskValidator() {
            RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("Parameter GroupId must grate than 0!");
            RuleFor(x => x.RkiKey).NotEmpty().WithMessage("Parameter RkiKey is mandatory!");
        }
    }
}

using FluentValidation;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.Reseller
{
    class ReplaceResellerEmailValidator: AbstractValidator<ReplaceResellerEmailModel>
    {
        public ReplaceResellerEmailValidator() {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(255).EmailAddress();
        }
    }
}

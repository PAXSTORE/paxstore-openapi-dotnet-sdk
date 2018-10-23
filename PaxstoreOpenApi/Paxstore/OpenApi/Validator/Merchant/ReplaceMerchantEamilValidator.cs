using FluentValidation;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.Merchant
{
    class ReplaceMerchantEamilValidator : AbstractValidator<ReplaceMerchantEmailModel>
    {
        public ReplaceMerchantEamilValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(255).EmailAddress();
        }
    }
}

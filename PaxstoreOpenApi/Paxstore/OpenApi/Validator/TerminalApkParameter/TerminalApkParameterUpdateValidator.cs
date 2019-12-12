using FluentValidation;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.TerminalApkParameter
{
    class TerminalApkParameterUpdateValidator: AbstractValidator<UpdateApkParameterRequest>
    {
        public TerminalApkParameterUpdateValidator()
        {
            RuleFor(x => x.ParamTemplateName).NotEmpty();
            RuleFor(x => x.TID).MinimumLength(8).MaximumLength(16);
            RuleFor(x => x.SerialNo).MaximumLength(32);
            RuleFor(x => x.MerchantName).MaximumLength(64);
            RuleFor(x => x.ResellerName).NotEmpty().MaximumLength(64);
            RuleFor(x => x.ModelName).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Location).MaximumLength(32);
            RuleFor(x => x.Status).Must(BeValidStatus).WithMessage("'Status' must be 'A' or 'P'.");
        }

        private bool BeValidStatus(string status)
        {
            if ("A".Equals(status) || "P".Equals(status) || status == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

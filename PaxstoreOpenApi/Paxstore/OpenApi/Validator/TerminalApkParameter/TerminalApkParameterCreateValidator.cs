using FluentValidation;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.TerminalApkParameter
{
    class TerminalApkParameterCreateValidator: AbstractValidator<CreateApkParameterRequest>
    {
        public TerminalApkParameterCreateValidator()
        {
            RuleFor(x => x.PackageName).NotEmpty();
            RuleFor(x => x.Version).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ParamTemplateName).NotEmpty();
        }
    }
}

using FluentValidation;
using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Validator.TerminalApk
{
    public class TerminalApkCreateValidator: AbstractValidator<CreateTerminalApkRequest>
    {
        public TerminalApkCreateValidator(){
            RuleFor(x => x.PackageName).NotEmpty();
        }
    }
}
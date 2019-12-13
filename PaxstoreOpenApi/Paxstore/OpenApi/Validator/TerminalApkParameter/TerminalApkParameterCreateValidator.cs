using FluentValidation;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.TerminalApkParameter;
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
            RuleFor(x => x.Base64FileParameters).Must(validateParameterFilesLength).WithMessage("Max Base64FileParameters count is 10");
            RuleFor(x => x.Base64FileParameters).Must(validateParameterFileSize).WithMessage("Max size of each parameter file is 500k");
        }

        private bool validateParameterFilesLength(List<FileParameter> base64FileParameters)
        {
            if (base64FileParameters != null) {
                if (base64FileParameters.Count > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private bool validateParameterFileSize(List<FileParameter> base64FileParameters) {
            if (base64FileParameters != null)
            {
                for (int i = 0; i < base64FileParameters.Count; i++) {
                    if (Base64FileUtil.GetBase64FileSizeKB(base64FileParameters[i].FileData) > 500) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

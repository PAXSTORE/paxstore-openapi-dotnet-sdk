using FluentValidation;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.TerminalApkParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.GroupApk
{
    public class CreateGroupApkRequestValidator: AbstractValidator<CreateTerminalGroupApkRequest>
    {
        public const int MAX_FILE_TYPE_PARAMETER_COUNTER = 10;
        public const int MAX_FILE_TYPE_PARAMETER_SIZE = 500;

        public CreateGroupApkRequestValidator()
        {
            RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.Base64FileParameters).Must(validateParameterFilesLength).WithMessage("Exceed max counter (10) of file type parameters!");
            RuleFor(x => x.Base64FileParameters).Must(validateParameterFileSize).WithMessage("Exceed max size (500kb) per file type parameters!");

        }

        private bool validateParameterFilesLength(List<FileParameter> base64FileParameters)
        {
            if (base64FileParameters != null)
            {
                if (base64FileParameters.Count > MAX_FILE_TYPE_PARAMETER_COUNTER)
                {
                    return false;
                }
            }
            return true;
        }

        private bool validateParameterFileSize(List<FileParameter> base64FileParameters)
        {
            if (base64FileParameters != null)
            {
                for (int i = 0; i < base64FileParameters.Count; i++)
                {
                    if (Base64FileUtil.GetBase64FileSizeKB(base64FileParameters[i].FileData) > MAX_FILE_TYPE_PARAMETER_SIZE)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

using FluentValidation;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.MerchantVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Validator.MerchantVariable
{
    public class MerchantVariableCreateValidator : AbstractValidator<MerchantVariableCreateRequest>
    {
        public MerchantVariableCreateValidator()
        {
            RuleFor(x => x.VariableList).Must(validateVariableList).WithMessage("Merchant variable is mandatory!");
        }

        private bool validateVariableList(List<ParameterVariable> variables) {
            if (variables == null || variables.Count() == 0)
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model.MerchantVariable
{
    public class MerchantVariableCreateRequest
    {
        [JsonProperty("merchantId")]
        public long MerchantId { get; set; }

        [JsonProperty("variableList")]
        public List<ParameterVariable> VariableList { get; set; }
    }
}

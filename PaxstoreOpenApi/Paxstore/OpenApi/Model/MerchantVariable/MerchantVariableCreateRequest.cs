using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paxstore.OpenApi.Model
{
    public class MerchantVariableCreateRequest
    {
        [JsonProperty("merchantId")]
        public long MerchantId { get; set; }

        [JsonProperty("variableList")]
        public IList<ParameterVariable> VariableList;
    }
}

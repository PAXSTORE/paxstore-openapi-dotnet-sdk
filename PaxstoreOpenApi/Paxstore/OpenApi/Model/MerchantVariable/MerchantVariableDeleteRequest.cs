using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paxstore.OpenApi.Model
{
    public class MerchantVariableDeleteRequest
    {
        [JsonProperty("variableIds")]
        public IList<long> VariableIds;
    }
}

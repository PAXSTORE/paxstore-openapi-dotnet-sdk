using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model.MerchantVariable
{
    public class MerchantVariableDeleteRequest
    {
        [JsonProperty("variableIds")]
        public List<long> VariableIds { get; set; }
    }
}

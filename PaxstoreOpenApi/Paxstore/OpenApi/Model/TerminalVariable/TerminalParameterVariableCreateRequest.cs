using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalParameterVariableCreateRequest
    {
        [JsonProperty("tid")]
        public string TID { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("variableList")]
        public Dictionary<TerminalParameterVariable> VariableList { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class CreateTerminalGroupRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("modelName")]
        public string ModelName { get; set; }

        [JsonProperty("resellerName")]
        public string ResellerName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("dynamic")]
        public Nullable<bool> Dynamic { get; set; }

        [JsonProperty("containSubResellerTerminal")]
        public Nullable<bool> ContainSubResellerTerminal { get; set; }

        [JsonProperty("merchantNameList")]
        public List<string> MerchantNameList { get; set; }
    }
}

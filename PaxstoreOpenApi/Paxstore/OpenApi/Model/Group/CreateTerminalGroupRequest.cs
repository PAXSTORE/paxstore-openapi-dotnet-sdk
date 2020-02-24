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

        [JsonProperty("modelId")]
        public long ModelId { get; set; }

        [JsonProperty("resellerId")]
        public long ResellerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("dynamic")]
        public Boolean Dynamic { get; set; } = false;

        [JsonProperty("containSubResellerTerminal")]
        public Boolean ContainSubResellerTerminal { get; set; } = false;

        [JsonProperty("merchantIds")]
        public string MerchantIds { get; set; }
    }
}

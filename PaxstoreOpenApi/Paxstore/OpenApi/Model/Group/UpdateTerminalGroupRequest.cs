using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class UpdateTerminalGroupRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("modelId")]
        public long ModelId { get; set; }

        [JsonProperty("resellerId")]
        public long ResellerId { get; set; }

        [JsonProperty("merchantIds")]
        public string MerchantIds { get; set; }
    }
}

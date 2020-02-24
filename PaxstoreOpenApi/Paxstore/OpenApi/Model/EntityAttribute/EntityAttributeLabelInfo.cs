using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class EntityAttributeLabelInfo
    {
        [JsonProperty("entityAttributeId")]
        public long EntityAttributeId { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class EntityAttributeUpdateRequest
    {
        [JsonProperty("inputType")]
        public string InputType { get; set; }

        [JsonProperty("minLength")]
        public int MinLength { get; set; }

        [JsonProperty("maxLength")]
        public int MaxLength { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("regex")]
        public string Regex { get; set; }

        [JsonProperty("selector")]
        public string Selector { get; set; }

        [JsonProperty("defaultLabel")]
        public string DefaultLabel { get; set; }
    }
}

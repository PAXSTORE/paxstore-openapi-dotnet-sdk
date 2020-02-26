using Newtonsoft.Json;
using Paxstore.OpenApi.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class EntityAttributeCreateRequest
    {
        [JsonProperty("entityType")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EntityAttributeType EntityType { get; set; }

        [JsonProperty("inputType")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EntityAttributeInputType InputType { get; set; }

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

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("defaultLabel")]
        public string DefaultLabel { get; set; }
    }

    public enum EntityAttributeType
    {
        [EnumValue("Merchant")]
        Merchant,
        [EnumValue("Reseller")]
        Reseller
    }


    public enum EntityAttributeInputType
    {
        [EnumValue("SELECTOR")]
        TEXT,
        [EnumValue("SELECTOR")]
        SELECTOR
    }


}

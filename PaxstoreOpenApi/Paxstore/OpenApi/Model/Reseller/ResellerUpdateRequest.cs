using Newtonsoft.Json;
using System.Collections.Generic;

namespace Paxstore.OpenApi.Model
{
    public class ResellerUpdateRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("entityAttributeValues")]
        public Dictionary<string, string> EntityAttributeValues;
    }
}
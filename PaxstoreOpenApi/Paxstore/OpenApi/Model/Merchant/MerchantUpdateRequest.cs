using Newtonsoft.Json;
using System.Collections.Generic;

namespace Paxstore.OpenApi.Model
{
    public class MerchantUpdateRequest
    {
        [JsonProperty("name")]
        public string Name{get; set;}

        [JsonProperty("email")]
        public string Email{get; set;}

        [JsonProperty("resellerName")]
        public string ResellerName{get; set;}

        [JsonProperty("contact")]
        public string Contact{get; set;}

        [JsonProperty("country")]
        public string Country{get; set;}

        [JsonProperty("phone")]
        public string Phone{get; set;}

        [JsonProperty("postcode")]
        public string Postcode{get; set;}

        [JsonProperty("address")]
        public string Address{get; set;}

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("description")]
        public string Description{get; set;}

        [JsonProperty("createUserFlag")]
        public bool CreateUserFlag { get; set; }

        [JsonProperty("merchantCategoryNames")]
        public List<string> MerchantCategoryNames{get; set;}

        [JsonProperty("entityAttributeValues")]
        public Dictionary<string, string> EntityAttributeValues{get; set;}
    }
}
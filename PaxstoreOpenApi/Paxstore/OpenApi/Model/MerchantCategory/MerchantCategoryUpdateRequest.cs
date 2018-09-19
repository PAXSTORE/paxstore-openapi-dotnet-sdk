using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class MerchantCategoryUpdateRequest
    {
        [JsonProperty("name")]
        public string Name{get; set;}

        [JsonProperty("remarks")]
        public string Remarks{get; set;}
    }
}
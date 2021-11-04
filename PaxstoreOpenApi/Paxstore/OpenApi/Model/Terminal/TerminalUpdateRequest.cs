using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class TerminalUpdateRequest
    {
        [JsonProperty("name")]
        public string Name{get; set;}

        [JsonProperty("tid")]
        public string TID{get; set;}

        [JsonProperty("serialNo")]
        public string SerialNo{get; set;}

        [JsonProperty("merchantName")]
        public string MerchantName{get; set;}

        [JsonProperty("resellerName")]
        public string ResellerName{get; set;}

        [JsonProperty("modelName")]
        public string ModelName{get; set;}

        [JsonProperty("location")]
        public string Location{get; set;}

        [JsonProperty("remark")]
        public string Remark { get; set; }
    }
}
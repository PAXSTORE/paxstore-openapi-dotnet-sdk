
using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class ResellerCreateRequest: ResellerUpdateRequest
    {
        [JsonProperty("parentResellerName")]
        public string ParentResellerName { get; set; }
    }
}
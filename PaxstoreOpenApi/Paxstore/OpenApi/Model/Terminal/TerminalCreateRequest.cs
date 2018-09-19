using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class TerminalCreateRequest: TerminalUpdateRequest
    {
        [JsonProperty("status")]
        public string Status{get; set;}
    }
}
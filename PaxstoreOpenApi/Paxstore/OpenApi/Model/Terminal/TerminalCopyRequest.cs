using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class TerminalCopyRequest
    {
        [JsonProperty("terminalId")]
        public long TerminalId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tid")]
        public string TID { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
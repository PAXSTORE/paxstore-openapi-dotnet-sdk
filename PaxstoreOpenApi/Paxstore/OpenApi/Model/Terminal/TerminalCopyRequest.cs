using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalCopyRequest
    {
        [JsonProperty("terminalId")]
        public long TerminalId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tid")]
        public string Tid { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}

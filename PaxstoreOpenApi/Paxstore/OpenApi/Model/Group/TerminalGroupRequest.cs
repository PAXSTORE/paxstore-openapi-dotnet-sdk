using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalGroupRequest
    {
        [JsonProperty("terminalIds")]
        public HashSet<long> TerminalIds { get; set; }

        [JsonProperty("groupIds")]
        public HashSet<long> GroupIds { get; set; }
    }
}

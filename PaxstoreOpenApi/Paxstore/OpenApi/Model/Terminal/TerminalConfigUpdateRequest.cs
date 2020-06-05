using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalConfigUpdateRequest
    {
        [JsonProperty("allowReplacement")]
        public bool AllowReplacement { get; set; }
    }
}

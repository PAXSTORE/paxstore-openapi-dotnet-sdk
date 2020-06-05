using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalMoveRequest
    {
        [JsonProperty("resellerName")]
        public string ResellerName { get; set; }

        [JsonProperty("merchantName")]
        public string MerchantName { get; set; }
    }
}

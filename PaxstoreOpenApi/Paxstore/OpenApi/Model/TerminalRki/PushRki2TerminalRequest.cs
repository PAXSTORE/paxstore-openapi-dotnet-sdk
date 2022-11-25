using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class PushRki2TerminalRequest
    {
        [JsonProperty("tid")]
        public string Tid { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("rkiKey")]
        public string RkiKey { get; set; }

        [JsonProperty("effectiveTime")]
        public Nullable<DateTime> EffectiveTime { get; set; }

        [JsonProperty("expiredTime")]
        public Nullable<DateTime> ExpiredTime { get; set; }
    }
}

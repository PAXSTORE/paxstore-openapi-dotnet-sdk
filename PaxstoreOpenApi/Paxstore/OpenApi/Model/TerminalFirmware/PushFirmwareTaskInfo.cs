using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class PushFirmwareTaskInfo
    {
        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("terminalSN")]
        public string TerminalSN { get; set; }

        [JsonProperty("fmName")]
        public string FmName { get; set; }

        [JsonProperty("activatedDate")]
        public long ActivatedDate { get; set; }

        [JsonProperty("wifiOnly")]
        public Boolean WifiOnly { get; set; }

        [JsonProperty("effectiveTime")]
        public long EffectiveTime { get; set; }

        [JsonProperty("expiredTime")]
        public long ExpiredTime { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

        [JsonProperty("actionStatus")]
        public int ActionStatus { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
    }
}

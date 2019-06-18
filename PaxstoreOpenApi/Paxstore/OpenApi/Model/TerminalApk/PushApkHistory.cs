using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class PushApkHistory
    {
        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("terminalSN")]
        public string TerminalSN { get; set; }

        [JsonProperty("apkPackageName")]
        public string ApkPackageName { get; set; }

        [JsonProperty("apkVersionCode")]
        public long ApkVersionCode { get; set; }

        [JsonProperty("apkVersionName")]
        public string ApkVersionName { get; set; }

        [JsonProperty("activatedDate")]
        public Nullable<long> ActivatedDate { get; set; }

        [JsonProperty("effectiveTime")]
        public Nullable<long> EffectiveTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("actionStatus")]
        public int ActionStatus { get; set; }

        //[JsonProperty("actionTime")]
        //public Nullable<long> ActionTime { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

    }
}

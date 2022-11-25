using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class CreateTerminalGroupRkiTaskRequest
    {
        [JsonProperty("groupId")]
        public long GroupId { get; set; }

        [JsonProperty("rkiKey")]
        public string RkiKey { get; set; }

        [JsonProperty("effectiveTime")]
        public Nullable<DateTime> EffectiveTime { get; set; }

        [JsonProperty("expiredTime")]
        public Nullable<DateTime> ExpiredTime { get; set; }
    }
}

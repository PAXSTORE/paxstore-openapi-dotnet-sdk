using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class DisablePushRkiTaskRequest
    {
        [JsonProperty("tid")]
        public string Tid { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("rkiKey")]
        public string RkiKey { get; set; }
    }
}

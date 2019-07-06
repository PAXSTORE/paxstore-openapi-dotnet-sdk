using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class ApkPushInfo{

        [JsonProperty("tid")]
        public string TID { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("packageName")]
        public string PackageName { get; set; }

    }
}

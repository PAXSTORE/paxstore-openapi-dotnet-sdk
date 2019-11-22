using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class CreateApkParameterRequest
    {
        [JsonProperty("packageName")]
        public string PackageName { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("paramTemplateName")]
        public string ParamTemplateName { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }
    }
}

using Newtonsoft.Json;
using Paxstore.OpenApi.Model.TerminalApkParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class UpdateApkParameterRequest
    {
        [JsonProperty("paramTemplateName")]
        public string ParamTemplateName { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonProperty("base64FileParameters")]
        public List<FileParameter> Base64FileParameters { get; set; }
    }
}

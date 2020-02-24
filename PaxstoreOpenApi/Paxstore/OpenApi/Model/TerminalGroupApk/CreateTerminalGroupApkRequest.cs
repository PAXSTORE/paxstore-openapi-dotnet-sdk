using Newtonsoft.Json;
using Paxstore.OpenApi.Model.TerminalApkParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class CreateTerminalGroupApkRequest
    {
        [JsonProperty("groupId")]
        public long GroupId { get; set; }

        [JsonProperty("pushTemplateName")]
        public string PushTemplateName { get; set; }

        [JsonProperty("packageName")]
        public string PackageName { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("templateName")]
        public string TemplateName { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonProperty("base64FileParameters")]
        public List<FileParameter> Base64FileParameters { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Paxstore.OpenApi.Model
{
    public class CreateTerminalApkRequest
    {
        [JsonProperty("tid")]
        public string TID{get; set;}

        [JsonProperty("serialNo")]
        public string SerialNo{get; set;}

        [JsonProperty("packageName")]
        public string PackageName{get; set;}

        [JsonProperty("version")]
        public string Version{get; set;}

        [JsonProperty("templateName")]
        public string TemplateName;

        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters;
    }
}
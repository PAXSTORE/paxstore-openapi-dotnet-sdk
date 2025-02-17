using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paxstore.OpenApi.Model
{
    public class TerminalApkParamPushInfo
    {
        [JsonProperty("paramTemplateName")]
        public string ParamTemplateName { get; set; }

        [JsonProperty("actionStatus")]
        public int ActionStatus { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("configuredParameters")]
        public Dictionary<string, string> ConfiguredParameters { get; set; }
    }
}

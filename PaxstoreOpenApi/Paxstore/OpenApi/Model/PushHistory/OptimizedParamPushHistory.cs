using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Paxstore.OpenApi.Paxstore.OpenApi.Help;

namespace Paxstore.OpenApi.Model.PushHistory
{
    public class OptimizedParamPushHistory
    {
        [JsonProperty("terminalId")]
        public long TerminalId { get; set; }

        [JsonProperty("serialNo")]
        public string SerialNo { get; set; }

        [JsonProperty("appName")]
        public string AppName { get; set; }

        [JsonProperty("versionName")]
        public string VersionName { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> PushStartTime { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> AppPushTime { get; set; }

        [JsonProperty("appPushStatus")]
        public string AppPushStatus { get; set; }

        [JsonProperty("appPushError")]
        public string AppPushError { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> ParameterPushTime { get; set; }

        [JsonProperty("parameterPushStatus")]
        public string ParameterPushStatus { get; set; }

        [JsonProperty("parameterPushError")]
        public string ParameterPushError { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonProperty("pushType")]
        public string PushType { get; set; }
    }
}

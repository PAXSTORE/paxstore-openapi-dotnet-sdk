using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Paxstore.OpenApi.Paxstore.OpenApi.Help;

namespace Paxstore.OpenApi.Model.PushHistory
{
    public class OptimizedParamPushHistory
    {
        public long TerminalId { get; set; }

        public string SerialNo { get; set; }

        public string AppName { get; set; }

        public string VersionName { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> PushStartTime { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> AppPushTime { get; set; }

        public string AppPushStatus { get; set; }

        public string AppPushError { get; set; }

        public string ParameterTemplateName { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> ParameterPushTime { get; set; }

        public string ParameterPushStatus { get; set; }

        public string ParameterPushError { get; set; }

        public Dictionary<string, string> Parameters { get; set; }

        public string PushType { get; set; }
    }
}

using Newtonsoft.Json;
using Paxstore.OpenApi.Paxstore.OpenApi.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class ParameterPushHistoryInfo
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

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> ParameterPushTime { get; set; }

        public string ParameterPushStatus { get; set; }

        public string ParameterPushError { get; set; }

        public string ParameterValues { get; set; }

        public string ParameterVariables { get; set; }

        public string PushType { get; set; }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Paxstore.OpenApi.Paxstore.OpenApi.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model.PushHistory
{
    public class AppPushHistoryInfo
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

        [JsonProperty("appPushError")]
        public string AppPushError { get; set; }

        
        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> ParameterPushTime { get; set; }

        [JsonProperty("parameterPushStatus")]
        public string ParameterPushStatus { get; set; }

        [JsonProperty("parameterPushError")]
        public string ParameterPushError { get; set; }

        [JsonProperty("parameterValues")]
        public string ParameterValues { get; set; }

        [JsonProperty("parameterVariables")]
        public string ParameterVariables { get; set; }

        [JsonProperty("pushType")]
        public string PushType { get; set; }
    }
}

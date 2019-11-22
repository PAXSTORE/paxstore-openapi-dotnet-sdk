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
        private string AppName { get; set; }
        private string VersionName { get; set; }
        private DateTime PushStartTime { get; set; }
        private DateTime AppPushTime { get; set; }
        private string AppPushStatus { get; set; }
        private string AppPushError { get; set; }
        private DateTime ParameterPushTime { get; set; }
        private string ParameterPushStatus { get; set; }
        private string ParameterPushError { get; set; }
        private string ParameterValues { get; set; }
        private string ParameterVariables { get; set; }
        private string PushType { get; set; }
    }
}

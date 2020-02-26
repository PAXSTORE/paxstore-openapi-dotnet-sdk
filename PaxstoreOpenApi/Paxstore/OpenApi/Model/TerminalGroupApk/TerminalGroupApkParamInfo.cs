using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalGroupApkParamInfo
    {
        public string ParamTemplateName { get; set; }
        public Dictionary<string, string> configuredParameters { get; set; }
        public Nullable<int> PendingCount { get; set; }
        public Nullable<int> SuccessCount { get; set; }
        public Nullable<int> FailedCount { get; set; }
    }
}

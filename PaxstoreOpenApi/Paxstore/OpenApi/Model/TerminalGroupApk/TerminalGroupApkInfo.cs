using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalGroupApkInfo
    {
        public long Id { get; set; }
        public string ApkPackageName { get; set; }
        public long ApkVersionCode { get; set; }
        public string ApkVersionName { get; set; }
        public long EffectiveTime { get; set; }
        public long ExpiredTime { get; set; }
        public long UpdatedDate { get; set; }
        public int ActionStatus { get; set; }
        public string Status { get; set; }
        public int PendingCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public TerminalGroupApkParamInfo GroupApkParam { get; set; }
    }
}

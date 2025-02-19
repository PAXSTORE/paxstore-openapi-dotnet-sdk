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
        public Nullable<long> ApkVersionCode { get; set; }
        public string ApkVersionName { get; set; }
        public Boolean ForceUpdate { get; set; }
        public Boolean WifiOnly { get; set; }
        public Nullable<long> EffectiveTime { get; set; }
        public Nullable<long> ExpiredTime { get; set; }
        public Nullable<long> UpdatedDate { get; set; }
        public Nullable<int> ActionStatus { get; set; }
        public string Status { get; set; }
        public Nullable<int> PendingCount { get; set; }
        public Nullable<int> SuccessCount { get; set; }
        public Nullable<int> FailedCount { get; set; }
        public Nullable<int> FilteredCount {get; set; }
        public TerminalGroupApkParamInfo GroupApkParam { get; set; }
    }
}

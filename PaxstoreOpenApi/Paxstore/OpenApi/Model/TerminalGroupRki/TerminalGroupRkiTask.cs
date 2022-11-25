using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalGroupRkiTask
    {
        public long Id { get; set; }
        public string RkiKey { get; set; }
        public Nullable<long> ActivatedDate { get; set; }
        public Nullable<long> EffectiveTime { get; set; }
        public Nullable<long> ExpiredTime { get; set; }
        public string Status { get; set; }
        public int ActionStatus { get; set; }
        public int ErrorCode { get; set; }
        public string Remarks { get; set; }

        public int PendingCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public bool Completed { get; set; }
        public int PushLimit { get; set; }

    }
}

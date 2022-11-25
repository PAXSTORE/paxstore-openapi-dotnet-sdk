using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalRkiTaskInfo
    {
        public long Id { get; set; }

        public string TerminalSN { get; set; }

        public string RkiKey { get; set; }

        public long ActivatedDate { get; set; }

        public long EffectiveTime { get; set; }

        public Nullable<long> ExpiredTime { get; set; }

        public string Status { get; set; }

        public int ActionStatus { get; set; }

        public int ErrorCode { get; set; }

        public string Remarks { get; set; }
    }
}

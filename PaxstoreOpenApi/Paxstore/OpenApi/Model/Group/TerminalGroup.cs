using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalGroup
    {
        public long Id { get; set; }

        public ResellerInfo reseller { get; set; }

        public ModelInfo model { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public string description { get; set; }

        public long CreatedByResellerId { get; set; }

        public long CreatedDate { get; set; }

        public long UpdatedDate { get; set; }

        public int TerminalCount { get; set; }

        public Boolean Dynamic { get; set; }

        public Boolean ContainSubResellerTerminal { get; set; }

        public List<MerchantInfo> Merchants { get; set; }
    }
}

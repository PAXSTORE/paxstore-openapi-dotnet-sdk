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

        public string ResellerName { get; set; }

        public string ModelName { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public long CreatedByResellerId { get; set; }

        public long CreatedDate { get; set; }

        public long UpdatedDate { get; set; }

        public int TerminalCount { get; set; }

        public bool Dynamic { get; set; }

        public bool ContainSubResellerTerminal { get; set; }

        public List<string> MerchantNames { get; set; }
    }
}

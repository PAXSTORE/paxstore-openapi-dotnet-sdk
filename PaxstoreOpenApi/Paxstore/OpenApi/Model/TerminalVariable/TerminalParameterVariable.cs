using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalParameterVariable
    {
        public long ID { get; set; }
        public string AppPackageName { get; set; }
        public string Type { get; set; }
        public string AppName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public string Source { get; set; }
        public long CreatedDate { get; set; }
        public long UpdatedDate { get; set; }
    }
}

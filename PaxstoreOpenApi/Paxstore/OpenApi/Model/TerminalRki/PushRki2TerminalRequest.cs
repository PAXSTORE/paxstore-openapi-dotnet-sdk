using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class PushRki2TerminalRequest
    {
        public string Tid { get; set; }

        public string SerialNo { get; set; }
        
        public string RkiKey { get; set; }
    }
}

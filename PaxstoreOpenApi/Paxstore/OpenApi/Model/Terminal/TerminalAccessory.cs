using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalAccessory
    {
        public string RelatedTerminalName { get; set; }
        public List<TerminalDeviceInfo> Basic { get; set; }
        public List<TerminalDeviceInfo> Hardware { get; set; }
        public List<TerminalDeviceInfo> InstallAps { get; set; }
        public List<TerminalDeviceHistory> History { get; set; }

    }
}

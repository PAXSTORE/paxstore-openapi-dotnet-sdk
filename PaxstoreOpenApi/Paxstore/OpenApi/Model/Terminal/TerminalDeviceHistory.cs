using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Paxstore.OpenApi.Paxstore.OpenApi.Help;

namespace Paxstore.OpenApi.Model
{
    public class TerminalDeviceHistory
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> InstallTime { get; set; }
        public Nullable<long> FileSize { get; set; }
        public string FileType { get; set; }
        public string Source { get; set; }
        public string Remarks { get; set; }
    }
}

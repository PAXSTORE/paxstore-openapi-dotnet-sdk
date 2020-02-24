using Newtonsoft.Json;
using Paxstore.OpenApi.Paxstore.OpenApi.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalInstalledApk
    {
        public string AppName { get; set; }
        public string PackageName { get; set; }
        public string VersionName { get; set; }
        public long VersionCode { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        public Nullable<DateTime> InstallTime { get; set; }
    }
}

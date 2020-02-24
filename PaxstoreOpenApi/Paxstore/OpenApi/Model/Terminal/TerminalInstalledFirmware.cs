using Newtonsoft.Json;
using Paxstore.OpenApi.Paxstore.OpenApi.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalInstalledFirmware
    {
        public string FirmwareName { get; set; }

        [JsonConverter(typeof(LongDateTimeConverter))]
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Nullable<DateTime> InstallTime { get; set; }
    }
}

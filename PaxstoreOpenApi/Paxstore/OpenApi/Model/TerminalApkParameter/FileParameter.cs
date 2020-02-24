using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model.TerminalApkParameter
{
    public class FileParameter
    {
        [JsonProperty("pid")]
        public string PID { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileData")]
        public string FileData { get; set; }
    }
}

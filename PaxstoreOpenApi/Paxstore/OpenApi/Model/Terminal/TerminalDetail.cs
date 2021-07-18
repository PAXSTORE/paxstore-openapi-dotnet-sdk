using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class TerminalDetail
    {
        public string PN { get; set; }
        public string OSVersion { get; set; }
        public string IMEI { get; set; }
        public string ScreenResolution { get; set; }
        public string Language { get; set; }
        public string IP { get; set; }
        public string TimeZone { get; set; }
        public string MacAddress { get; set; }
        public string ICCID { get; set; }
        public string CellId { get; set; }
    }
}

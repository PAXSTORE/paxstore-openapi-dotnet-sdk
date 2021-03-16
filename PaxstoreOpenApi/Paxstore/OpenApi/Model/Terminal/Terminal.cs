using System.Collections.Generic;

namespace Paxstore.OpenApi.Model
{
    public class Terminal
    {
        public long ID{get; set;}
    
        public string Name{get; set;}
    
        public string TID{get; set;}
        public string SerialNo{get; set;}
    
        public string Status{get; set;}
    
        public string MerchantName{get; set;}
    
        public string ModelName{get; set;}
    
        public string ResellerName{get; set;}

        public string Location { get; set; }

        public TerminalLocation GeoLocation { get; set; }

        public TerminalInstalledFirmware InstalledFirmware { get; set; }

        public List<TerminalInstalledApk> InstalledApks { get; set; }

        public TerminalDetail TerminalDetail { get; set; }
    }
}
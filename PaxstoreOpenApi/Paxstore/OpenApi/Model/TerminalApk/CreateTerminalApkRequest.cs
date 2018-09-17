using System.Collections.Generic;

namespace Paxstore.OpenApi.Model
{
    public class CreateTerminalApkRequest
    {
        public string TID{get; set;}
	
	    public string SerialNo{get; set;}
        public string PackageName{get; set;}
	
	    public string Version{get; set;}
	
	    public string TemplateName;
	
	    public Dictionary<string, string> Parameters;
    }
}
using System;
using System.Collections.Generic;

namespace Paxstore.OpenApi.Model{
    public class Reseller : PagedReseller{
        private Dictionary<string, string> EntityAttributeValues{get; set;}
	
	    public SimpleReseller Parent{get; set;}

    }
}
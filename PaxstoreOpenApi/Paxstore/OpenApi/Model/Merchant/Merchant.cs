
using System.Collections.Generic;


namespace Paxstore.OpenApi.Model
{
    public class Merchant: PagedMerchant
    {
        public Dictionary<string, string> EntityAttributeValues{get; set;}
	    public List<MerchantCategory> MerchantCategory {get; set;}
    }
}
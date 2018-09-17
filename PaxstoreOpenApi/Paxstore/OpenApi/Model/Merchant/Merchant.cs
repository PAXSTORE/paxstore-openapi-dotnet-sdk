
using System.Collections.Generic;


namespace Paxstore.OpenApi.Model
{
    public class Merchant: PagedMerchant
    {
        Dictionary<string, string> entityAttributeValues{get; set;}
	    List<MerchantCategory> MerchantCategory {get; set;}
    }
}
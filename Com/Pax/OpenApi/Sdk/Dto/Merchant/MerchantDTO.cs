using System.Collections.Generic;
using Com.Pax.OpenApi.Sdk.Dto.MerchantCategory;

namespace Com.Pax.OpenApi.Sdk.Dto.Merchant
{
    public class MerchantDTO: MerchantPageDTO
    {
        Dictionary<string, string> entityAttributeValues{get; set;}
	    List<MerchantCategoryDTO> merchantCategory;
    }
}
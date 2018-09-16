using Com.Pax.OpenApi.Sdk.Dto.Reseller;

namespace Com.Pax.OpenApi.Sdk.Dto.Merchant
{
    public class MerchantPageDTO
    {
        long id{get; set;}
	    string name{get; set;}
	    SimpleResellerDTO reseller{get; set;}
	    string country{get; set;}
	    string postcode{get; set;}
	    string address{get; set;}
	    string contact{get; set;}
	    string email{get; set;}
	    string phone{get; set;}
	    string status{get; set;}
        string description{get; set;}
    }
}
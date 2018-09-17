using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Model
{
    public class PagedMerchant
    {
        long id{get; set;}
	    string name{get; set;}
	    SimpleReseller reseller{get; set;}
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
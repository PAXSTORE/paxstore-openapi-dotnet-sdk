using Paxstore.OpenApi.Model;

namespace Paxstore.OpenApi.Model
{
    public class PagedMerchant
    {
        public long ID{get; set;}
        public string Name {get; set;}
        public SimpleReseller Reseller {get; set;}
        public string Country {get; set;}
        public string Postcode {get; set;}
        public string Address {get; set;}
        public string Contact {get; set;}
        public string Email {get; set;}
        public string Phone {get; set;}
        public string Status {get; set;}
        public string Description {get; set;}
    }
}
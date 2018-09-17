using System.Collections.Generic;

namespace Paxstore.OpenApi.Model
{
    public class MerchantUpdateRequest
    {
        public string Name{get; set;}
        public string Email{get; set;}
        public string ResellerName{get; set;}

        public string Contact{get; set;}

        public string Country{get; set;}

        public string Phone{get; set;}

        public string Postcode{get; set;}

        public string Address{get; set;}

        public string Description{get; set;}

        public List<string> MerchantCategoryNames{get; set;}

        public Dictionary<string, string> EntityAttributeValues{get; set;}
    }
}
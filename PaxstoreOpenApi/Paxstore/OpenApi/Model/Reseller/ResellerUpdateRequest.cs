using System.Collections.Generic;

namespace Paxstore.OpenApi.Model
{
    public class ResellerUpdateRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string ParentResellerName { get; set; }
        public Dictionary<string, string> EntityAttributeValues;
    }
}
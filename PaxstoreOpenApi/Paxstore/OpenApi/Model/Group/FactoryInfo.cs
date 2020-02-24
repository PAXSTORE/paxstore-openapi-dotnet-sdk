using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class FactoryInfo
    {
        public long Id { get; set; }
        public long MarketId { get; set; } = -1L;    // market_id
        public string Name { get; set; }            // name
        public string Contact { get; set; }         // contact
        public string Phone { get; set; }           // phone number
        public string Email { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<ModelInfo> ModelList { get; set; }
        public long CreatedDate { get; set; }
        public long UpdatedDate { get; set; }
    }
}

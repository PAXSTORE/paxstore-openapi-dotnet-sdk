using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class MerchantInfo
    {
        public long Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public ResellerInfo Reseller { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public long CreatedDate { get; set; }
        public long UpdatedDate { get; set; }
        public List<MerchantCategoryInfo> MerchantCategoryList { get; set; }
        public int TerminalCount { get; set; }
    }
}

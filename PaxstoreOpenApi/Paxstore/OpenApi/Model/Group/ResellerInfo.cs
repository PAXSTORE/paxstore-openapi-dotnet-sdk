using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class ResellerInfo
    {
        public long Id { get; set; }
        //public long marketId { get; set; }
        public long parentId { get; set; }
        public string ParentIds { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public string Status { get; set; }
        public long CreatedDate { get; set; }
        public long UpdatedDate { get; set; }
        public ResellerInfo Parent { get; set; }
        public List<ResellerInfo> Children { get; set; }
        public List<MerchantInfo> MerchantList { get; set; }
        public int TerminalCount { get; set; }
        public ResellerRkiInfo ResellerRki { get; set; }
    }
}

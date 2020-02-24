using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class ResellerRkiInfo
    {
        public long ResellerId { get; set; }

        public string Token { get; set; }

        public Boolean AllowChildUse { get; set; }
    }
}

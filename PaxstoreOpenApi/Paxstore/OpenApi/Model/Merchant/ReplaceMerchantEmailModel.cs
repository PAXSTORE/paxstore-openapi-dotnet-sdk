using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    class ReplaceMerchantEmailModel
    {
        public ReplaceMerchantEmailModel(string email, bool createUser) {
            Email = email;
            CreateUser = createUser;
        }


        public string Email { get; set; }

        public bool CreateUser { get; set; }

    }
}

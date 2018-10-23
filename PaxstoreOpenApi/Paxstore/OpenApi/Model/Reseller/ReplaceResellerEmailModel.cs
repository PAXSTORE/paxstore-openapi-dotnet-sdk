using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    class ReplaceResellerEmailModel
    {
        public ReplaceResellerEmailModel(string email) {
            Email = email;
        }

        public string Email { get; set; }
    }
}

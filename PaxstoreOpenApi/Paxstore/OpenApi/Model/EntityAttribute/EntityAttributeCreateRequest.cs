using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class EntityAttributeCreateRequest
    {
        public String entityType;
        public String inputType;
        public int minLength;
        public int maxLength;
        public bool required;
        public string regex;
        public string selector;
        public string key;
        public string defaultLabel;
    }
}

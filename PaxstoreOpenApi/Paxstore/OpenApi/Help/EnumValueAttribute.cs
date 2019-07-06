using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Help
{
    public sealed class EnumValueAttribute : Attribute
    {
        private string value;
        public string EnumValue { get { return value; } }

        public EnumValueAttribute(string value): base()
        {
            this.value = value;
        }
    }
}

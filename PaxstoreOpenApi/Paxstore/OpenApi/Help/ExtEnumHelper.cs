using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Help
{
    class ExtEnumHelper{
        public static string GetEnumValue(Enum target){
            if (target == null)
            {
                return null;
            }
            string enumValue = target.ToString();
            var fieldInfo = target.GetType().GetField(enumValue);
            var attributes =
                (EnumValueAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumValueAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                enumValue = attributes[0].EnumValue;
            }
            return enumValue;
        }
    }
}

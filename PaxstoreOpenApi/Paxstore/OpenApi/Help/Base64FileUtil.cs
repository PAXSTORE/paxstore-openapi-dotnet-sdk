using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Help
{
    public class Base64FileUtil
    {
        public static int GetBase64FileSize(string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str))
            {
                return 0;
            }
            string str = base64Str.Substring(base64Str.LastIndexOf(",", 0) + 1);
            int equalIndex = str.IndexOf("=");
            if (str.IndexOf("=") > 0)
            {
                str = str.Substring(0, equalIndex);
            }
            int strLength = str.Length;
            int size = strLength - (strLength / 8) * 2;
            return size;
        }

        public static int GetBase64FileSizeKB(string base64Str) {
            return GetBase64FileSize(base64Str) / 1024;
        }
    }
}

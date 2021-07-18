
using System.Security.Cryptography;
using System.Text;

namespace Paxstore.OpenApi.Help
{
    public class SecurityHelper
    {

        public static string Get32MD5(string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                string hash = sBuilder.ToString();
                return hash.ToUpper();
            }
        }

    }
}

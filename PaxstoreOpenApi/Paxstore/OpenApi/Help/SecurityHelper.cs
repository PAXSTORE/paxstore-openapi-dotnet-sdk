
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

        public static byte[] GetMD5(string str)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
            byte[] result2 = new byte[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] > 128)
                {
                    int a = result[i] - 256;
                    result2[i] = (byte)(result[i] - 256);
                }
                else
                {
                    result2[i] = result[i];
                }
            }
            return result2;
        }

        public static string ByteToHexStr(byte[] bytes)
        {
            string result = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    result += bytes[i].ToString("X2");
                }
            }
            return result;
        }

        public static string EncryptPasswordParameter(string content, string appSecret)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }
            byte[] md5OfSecret = GetMD5(appSecret);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(content);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = md5OfSecret,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return ByteToHexStr(resultArray);
        }

    }
}

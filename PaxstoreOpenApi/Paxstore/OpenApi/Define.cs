using System;
using System.Security.Cryptography;
using System.Net;
using System.Text;

namespace Paxstore.OpenApi{
    public class Constants{
        public const int DEFAULT_PAGE_SIZE = 10;
        public const string PARAM_NAME_SYSKEY = "sysKey";
        public const string HEADER_NAME_SIGNATURE = "signature";

        public const string PAGINATION_PAGE_NO = "pageNo";
        public const string PAGINATION_PAGE_LIMIT = "limit";

        public const int MAX_PAGE_SIZE = 1000;

        public const string CONTENT_TYPE_JSON = "application/json; charset=utf-8";

        public const string THIRD_PARTY_API_SDK_LANGUAGE = "DOTNET";
        public const string THIRD_PARTY_API_SDK_VERSION = "6.2";
        public const string REQ_HEADER_SDK_LANG = "SDK-Language";
        public const string REQ_HEADER_SDK_VERSION = "SDK-Version";

    }

    public class Utils{
        public static string ByteArr2Hex(byte[] data){
            if(data == null){
                return "";
            }
            var sb = new StringBuilder();
            foreach (var b in data)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static string GenSignature(string secret, string strToSign){
            if (string.IsNullOrEmpty(secret)){
                throw new ArgumentNullException("secret", "Value can't be null or empty");
            }
            if(string.IsNullOrEmpty(strToSign)){
                strToSign = "";
            }
            string signature = ByteArr2Hex(new HMACMD5(Encoding.UTF8.GetBytes(secret)).ComputeHash(
                                            Encoding.UTF8.GetBytes(strToSign))).ToUpper();
            return signature;
        } 
    }
}
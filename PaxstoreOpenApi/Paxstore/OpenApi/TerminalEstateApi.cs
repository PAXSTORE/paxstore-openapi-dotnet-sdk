using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class TerminalEstateApi: BaseApi
    {
        private const string VERIFY_ESTATE_URL = "/v1/3rdsys/estates/verify/{serialNo}";

        public TerminalEstateApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public TerminalEstateApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public TerminalEstateApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public TerminalEstateApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }

        public Result<string> VerifyTerminalEstate(string serialNo)
        {
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(serialNo)) {
                validationErrs.Add(GetMsgByKey("parameterSerialNoMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(VERIFY_ESTATE_URL, Method.Get);
            request.AddUrlSegment("serialNo", serialNo);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
    }
}

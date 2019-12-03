using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class TerminalEstateApi: BaseApi
    {
        private const string VERIFY_ESTATE_URL = "/v1/3rdsys/estates/verify/{serialNo}";

        public TerminalEstateApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
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
            RestRequest request = new RestRequest(VERIFY_ESTATE_URL, Method.GET);
            request.AddUrlSegment("serialNo", serialNo);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
    }
}

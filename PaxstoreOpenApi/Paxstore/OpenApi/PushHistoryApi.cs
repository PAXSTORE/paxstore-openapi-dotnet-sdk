using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.PushHistory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class PushHistoryApi: BaseApi
    {
        private const string SEARCH_APP_PUSH_STATUS_URL = "/v1/3rdsys/parameter/push/history";

        public PushHistoryApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<ParameterPushHistoryInfo> SearchParameterPushHistory(int pageNo, int pageSize, String packageName, String serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime) { 
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrEmpty(packageName)) {
                validationErrs.Add(GetMsgByKey("packageNameMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<ParameterPushHistoryInfo>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_APP_PUSH_STATUS_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());

            if (!string.IsNullOrEmpty(packageName)) {
                request.AddParameter("packageName", packageName);
            }
            if (!string.IsNullOrEmpty(serialNo)) {
                request.AddParameter("serialNo", serialNo);
            }
            if (pushStatus != null) {
                request.AddParameter("pushStatus", (int)pushStatus);
            }
            if (pushTime != null) {
                request.AddParameter("pushTime", pushTime.Value.ToString(Constants.DATE_FORMAT).Remove(23, 1));
            }

            var responseContent = Execute(request);
            ParameterPushHistoryInfoPageResponse pushStatusInfoPageResponse = JsonConvert.DeserializeObject<ParameterPushHistoryInfoPageResponse>(responseContent);
            Result<ParameterPushHistoryInfo> result = new Result<ParameterPushHistoryInfo>(pushStatusInfoPageResponse);
            return result;
        }


        public enum PushHistoryStatus {
            Success=2,
            Failed=3
        }
    }
}

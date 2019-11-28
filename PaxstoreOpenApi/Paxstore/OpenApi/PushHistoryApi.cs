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
        private const string SEARCH_APP_PUSH_STATUS_URL = "/v1/3rdsys/app/push/history";

        public PushHistoryApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<AppPushHistoryInfo> SearchAppPushStatus(int pageNo, int pageSize, Nullable<PushHistorySearchOrderBy> orderBy, string packageName, 
            string snNameTID, Nullable<PushHistoryStatus> appPushStatus, Nullable<PushHistoryStatus> parameterPushStatus){
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<AppPushHistoryInfo>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_APP_PUSH_STATUS_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }

            if (packageName != null) {
                request.AddParameter("packageName", packageName);
            }
            if (snNameTID != null) {
                request.AddParameter("snNameTID", snNameTID);
            }
            if (appPushStatus != null) {
                request.AddParameter("appPushStatus", (int)appPushStatus);
            }
            if (parameterPushStatus != null) {
                request.AddParameter("parameterPushStatus", (int)parameterPushStatus);
            }
            var responseContent = Execute(request);
            AppPushHistoryInfoPageResponse appPushStatusInfoPageResponse = JsonConvert.DeserializeObject<AppPushHistoryInfoPageResponse>(responseContent);
            Result<AppPushHistoryInfo> result = new Result<AppPushHistoryInfo>(appPushStatusInfoPageResponse);
            return result;
        }

        public enum PushHistorySearchOrderBy
        {
            [EnumValue("appPushTime")]
            AppPushTime,

            [EnumValue("serialNo")]
            SerialNo
        }

        public enum PushHistoryStatus {
            Success=2,
            Failed=3
        }
    }
}

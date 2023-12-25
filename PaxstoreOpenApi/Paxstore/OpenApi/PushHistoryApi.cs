using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.PushHistory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class PushHistoryApi: BaseApi
    {
        private const string SEARCH_APP_PUSH_STATUS_URL = "/v1/3rdsys/parameter/push/history";

        public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
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
            var responseContent = searchParameterPushHistory(pageNo, pageSize, packageName, serialNo, pushStatus, pushTime, false, false);

            ParameterPushHistoryInfoPageResponse pushStatusInfoPageResponse = JsonConvert.DeserializeObject<ParameterPushHistoryInfoPageResponse>(responseContent);
            Result<ParameterPushHistoryInfo> result = new Result<ParameterPushHistoryInfo>(pushStatusInfoPageResponse);
            return result;
        }

        public Result<OptimizedParamPushHistory> SearchOptimizedParameterPushHistory(int pageNo, int pageSize, string packageName, string serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrEmpty(packageName))
            {
                validationErrs.Add(GetMsgByKey("packageNameMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<OptimizedParamPushHistory>(validationErrs);
            }
            var responseContent = searchParameterPushHistory(pageNo, pageSize, packageName, serialNo, pushStatus, pushTime, false, true);

            OptimizedParamPushHistoryPageResponse pushStatusInfoPageResponse = JsonConvert.DeserializeObject<OptimizedParamPushHistoryPageResponse>(responseContent);
            Result<OptimizedParamPushHistory> result = new Result<OptimizedParamPushHistory>(pushStatusInfoPageResponse);
            return result;
        }

        public Result<ParameterPushHistoryInfo> SearchLatestParameterPushHistory(int pageNo, int pageSize, string packageName, string serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrEmpty(packageName))
            {
                validationErrs.Add(GetMsgByKey("packageNameMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<ParameterPushHistoryInfo>(validationErrs);
            }
            var responseContent = searchParameterPushHistory(pageNo, pageSize, packageName, serialNo, pushStatus, pushTime, true, false);

            ParameterPushHistoryInfoPageResponse pushStatusInfoPageResponse = JsonConvert.DeserializeObject<ParameterPushHistoryInfoPageResponse>(responseContent);
            Result<ParameterPushHistoryInfo> result = new Result<ParameterPushHistoryInfo>(pushStatusInfoPageResponse);
            return result;
        }

        public Result<OptimizedParamPushHistory> SearchLatestOptimizedParameterPushHistory(int pageNo, int pageSize, string packageName, string serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrEmpty(packageName))
            {
                validationErrs.Add(GetMsgByKey("packageNameMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<OptimizedParamPushHistory>(validationErrs);
            }
            var responseContent = searchParameterPushHistory(pageNo, pageSize, packageName, serialNo, pushStatus, pushTime, true, true);

            OptimizedParamPushHistoryPageResponse pushStatusInfoPageResponse = JsonConvert.DeserializeObject<OptimizedParamPushHistoryPageResponse>(responseContent);
            Result<OptimizedParamPushHistory> result = new Result<OptimizedParamPushHistory>(pushStatusInfoPageResponse);
            return result;
        }

    
        private string searchParameterPushHistory(int pageNo, int pageSize, String packageName, String serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime,
            bool onlyLastPushHistory, bool optimizeParameters)
        {
            RestRequest request = new RestRequest(SEARCH_APP_PUSH_STATUS_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());

            if (!string.IsNullOrEmpty(packageName))
            {
                request.AddParameter("packageName", packageName);
            }
            if (!string.IsNullOrEmpty(serialNo))
            {
                request.AddParameter("serialNo", serialNo);
            }
            if (pushStatus != null)
            {
                request.AddParameter("pushStatus", (int)pushStatus);
            }
            if (pushTime != null)
            {
                request.AddParameter("pushTime", pushTime.Value.ToString(Constants.DATE_FORMAT).Remove(23, 1));
            }
            request.AddParameter("onlyLastPushHistory", onlyLastPushHistory ? "true":"false");
            request.AddParameter("optimizeParameters", optimizeParameters ? "true":"false");

            var responseContent = Execute(request);
            return responseContent;
        }


        public enum PushHistoryStatus {
            Success=2,
            Failed=3
        }
    }
}

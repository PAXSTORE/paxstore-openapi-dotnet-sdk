using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.GroupApk;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class TerminalGroupApkApi:BaseApi
    {
        private const string GET_TERMINAL_GROUP_APK_URL = "/v1/3rdsys/terminalGroupApks/{groupApkId}";
        private const string SEARCH_TERMINAL_GROUP_APK_URL = "/v1/3rdsys/terminalGroupApks";
        private const string CREATE_TERMINAL_GROUP_APK_URL = "/v1/3rdsys/terminalGroupApks";
        private const string SUSPEND_TERMINAL_GROUP_APK_URL = "/v1/3rdsys/terminalGroupApks/{groupApkId}/suspend";
        private const string DELETE_TERMINAL_GROUP_APK_URL = "/v1/3rdsys/terminalGroupApks/{groupApkId}";

        

        public TerminalGroupApkApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public TerminalGroupApkApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public TerminalGroupApkApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public TerminalGroupApkApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }


        public Result<TerminalGroupApkInfo> GetTerminalGroupApk(long groupApkId, List<string> pidList)
        {
            RestRequest request = new RestRequest(GET_TERMINAL_GROUP_APK_URL, Method.Get);
            request.AddUrlSegment("groupApkId", groupApkId.ToString());
            if (pidList != null && pidList.Count > 0) {
                string pids = string.Join(",", pidList);
                request.AddParameter("pidList", pids);
            }
            string responseContent = Execute(request);
            TerminalGroupApkResponse terminalGroupApkResponse = JsonConvert.DeserializeObject<TerminalGroupApkResponse>(responseContent);
            Result<TerminalGroupApkInfo> result = new Result<TerminalGroupApkInfo>(terminalGroupApkResponse);
            return result;
        }

        public Result<TerminalGroupApkInfo> SearchTerminalGroupApk(int pageNo, int pageSize, Nullable<TerminalGroupApkSearchOrderBy> orderBy, long groupId, Nullable<bool> pendingOnly, Nullable<bool> historyOnly, string keyWords)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalGroupApkInfo>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_GROUP_APK_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }
            request.AddParameter("groupId", groupId);
            if (pendingOnly != null) {
                request.AddParameter("pendingOnly", pendingOnly.Value.ToString());
            }
            if (historyOnly != null)
            {
                request.AddParameter("historyOnly", historyOnly.Value.ToString());
            }
            if (!string.IsNullOrEmpty(keyWords)) {
                request.AddParameter("keyWords", keyWords);
            }
            string responseContent = Execute(request);
            TerminalGroupApkPageResponse terminalGroupApkPageResponse = JsonConvert.DeserializeObject<TerminalGroupApkPageResponse>(responseContent);
            Result<TerminalGroupApkInfo> result = new Result<TerminalGroupApkInfo>(terminalGroupApkPageResponse);
            return result;
        }

        public Result<TerminalGroupApkInfo> CreateAndActiveGroupApk(CreateTerminalGroupApkRequest createTerminalGroupApkRequest)
        {
            List<string> validationErrs = ValidateCreate(createTerminalGroupApkRequest, new CreateGroupApkRequestValidator(), "parameterCreateTerminalGroupApkRequestNull");
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalGroupApkInfo>(validationErrs);
            }

            RestRequest request = new RestRequest(CREATE_TERMINAL_GROUP_APK_URL, Method.Post);
            var requestJson = JsonConvert.SerializeObject(createTerminalGroupApkRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContent = Execute(request);
            TerminalGroupApkResponse terminalGroupApkResponse = JsonConvert.DeserializeObject<TerminalGroupApkResponse>(responseContent);
            Result<TerminalGroupApkInfo> result = new Result<TerminalGroupApkInfo>(terminalGroupApkResponse);
            return result;
        }

        


        public Result<TerminalGroupApkInfo> SuspendTerminalGroupApk(long groupApkId)
        {
            RestRequest request = new RestRequest(SUSPEND_TERMINAL_GROUP_APK_URL, Method.Post);
            request.AddUrlSegment("groupApkId", groupApkId.ToString());
            string responseContent = Execute(request);
            TerminalGroupApkResponse terminalGroupApkResponse = JsonConvert.DeserializeObject<TerminalGroupApkResponse>(responseContent);
            Result<TerminalGroupApkInfo> result = new Result<TerminalGroupApkInfo>(terminalGroupApkResponse);
            return result;
        }

        public Result<string> DeleteTerminalGroupApk(long groupApkId)
        {
            RestRequest request = new RestRequest(DELETE_TERMINAL_GROUP_APK_URL, Method.Delete);
            request.AddUrlSegment("groupApkId",groupApkId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
    }

    public enum TerminalGroupApkSearchOrderBy
    {
        [EnumValue("a.created_date DESC")]
        CreatedDate_desc,
        [EnumValue("a.created_date ASC")]
        CreatedDate_asc
    }

}

using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
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
    public class TerminalRkiApi : BaseApi
    {
        private const string CREATE_TERMINAL_RKI_KEY_URL = "/v1/3rdsys/terminalRkis";
        private const string SEARCH_TERMINAL_RKI_KEY_LIST_URL = "/v1/3rdsys/terminalRkis";
        private const string GET_TERMINAL_RKI_KEY_URL = "/v1/3rdsys/terminalRkis/{terminalRkiId}";
        private const string SUSPEND_TERMINAL_RKI_KEY_URL = "/v1/3rdsys/terminalRkis/suspend";

        public TerminalRkiApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public TerminalRkiApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public TerminalRkiApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public TerminalRkiApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }

        public Result<TerminalRkiTaskInfo> PushRkiKey2Terminal(PushRki2TerminalRequest pushRki2TerminalRequest)
        {
            List<string> validationErrs = validateCreateTerminalRkiRequest(pushRki2TerminalRequest);
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalRkiTaskInfo>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_TERMINAL_RKI_KEY_URL, Method.Post);
            var requestBodyJson = JsonConvert.SerializeObject(pushRki2TerminalRequest);

            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestBodyJson, ParameterType.RequestBody);
            var responseContent = Execute(request);

            PushRkiKey2TerminalResponse response = JsonConvert.DeserializeObject<PushRkiKey2TerminalResponse>(responseContent);
            Result<TerminalRkiTaskInfo> result = new Result<TerminalRkiTaskInfo>(response);
            return result;
        }

        string GetOrderValue(SearchOrderBy order)
        {
            switch (order)
            {
                case SearchOrderBy.CreatedDate_asc:
                    return "a.created_date ASC";
                case SearchOrderBy.CreatedDate_desc:
                    return "a.created_date DESC";
            }
            return "a.created_date DESC";
        }

        public Result<TerminalRkiTaskInfo> SearchPushRkiTasks(int pageNo, int pageSize, Nullable<SearchOrderBy> orderBy,
                                                                   string terminalTid, string rkiKey, PushStatus status){
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrEmpty(terminalTid))
            {
                validationErrs.Add(GetMsgByKey("parameterSearchPushRkiTasksTerminalTidEmpty"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalRkiTaskInfo>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_RKI_KEY_LIST_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", GetOrderValue(orderBy.Value));
            }
            request.AddParameter("terminalTid", terminalTid);
            if (!string.IsNullOrEmpty(rkiKey)) {
                request.AddParameter("rkiKey", rkiKey);
            }
            request.AddParameter("status", PushStatusHelper.GetPushStatusVal(status));
            var responseContent = Execute(request);
            TerminalRkiTaskInfoPageResponse terminalRkiTaskInfoPage = JsonConvert.DeserializeObject<TerminalRkiTaskInfoPageResponse>(responseContent);
            Result<TerminalRkiTaskInfo> result = new Result<TerminalRkiTaskInfo>(terminalRkiTaskInfoPage);
            return result;

        }

        public Result<TerminalRkiTaskInfo> GetPushRkiTask(long pushRkiTaskId){
            RestRequest request = new RestRequest(GET_TERMINAL_RKI_KEY_URL, Method.Get);
            request.AddUrlSegment("terminalRkiId", pushRkiTaskId.ToString());
            var responseContent = Execute(request);
            PushRkiKey2TerminalResponse response = JsonConvert.DeserializeObject<PushRkiKey2TerminalResponse>(responseContent);
            Result<TerminalRkiTaskInfo> result = new Result<TerminalRkiTaskInfo>(response);
            return result;
        }

        public Result<string> DisablePushRkiTask(DisablePushRkiTaskRequest disablePushRkiTaskRequest)
        {
            List<string> validationErrs = validateDisablePushRki(disablePushRkiTaskRequest);

            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(SUSPEND_TERMINAL_RKI_KEY_URL, Method.Post);
            var requestBodyJson = JsonConvert.SerializeObject(disablePushRkiTaskRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestBodyJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        private List<string> validateCreateTerminalRkiRequest(PushRki2TerminalRequest request)
        {
            List<string> validationErrs = new List<string>();
            if (request == null)
            {
                validationErrs.Add(GetMsgByKey("parameterPushRki2TerminalRequestNull"));
            }else{
                if (string.IsNullOrEmpty(request.RkiKey)) {
                    validationErrs.Add(GetMsgByKey("rkiKeyMandatory"));
                }
                if (string.IsNullOrEmpty(request.SerialNo) && string.IsNullOrEmpty(request.Tid))
                {
                    validationErrs.Add(GetMsgByKey("snTidMandatory"));
                }
            }
            return validationErrs;
        }

        private List<string> validateDisablePushRki(DisablePushRkiTaskRequest disablePushRkiTaskRequest)
        {
            List<string> validationErrs = new List<string>();
            if (disablePushRkiTaskRequest == null)
            {
                validationErrs.Add(GetMsgByKey("parameterDisablePushRkiTaskRequestNull"));
            } else
            {
                if (string.IsNullOrEmpty(disablePushRkiTaskRequest.RkiKey))
                {
                    validationErrs.Add(GetMsgByKey("rkiKeyMandatory"));
                }
                if (string.IsNullOrEmpty(disablePushRkiTaskRequest.SerialNo) && string.IsNullOrEmpty(disablePushRkiTaskRequest.Tid))
                {
                    validationErrs.Add(GetMsgByKey("snTidMandatory"));
                }
            }
            return validationErrs;
        }
    }
}

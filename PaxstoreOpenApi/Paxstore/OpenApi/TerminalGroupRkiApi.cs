using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.TerminlGroupRki;
using RestSharp;

namespace Paxstore.OpenApi
{
    public class TerminalGroupRkiApi : BaseApi
    {
        private const string GET_TERMINAL_GROUP_RKI_URL = "/v1/3rdsys/terminalGroupRki/{groupRkiId}";
        private const string SEARCH_TERMINAL_GROUP_RKI_URL = "/v1/3rdsys/terminalGroupRki";
        private const string CREATE_TERMINAL_GROUP_RKI_URL = "/v1/3rdsys/terminalGroupRki";
        private const string SUSPEND_TERMINAL_GROUP_RKI_URL = "/v1/3rdsys/terminalGroupRki/{groupRkiId}/suspend";

        public TerminalGroupRkiApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        { }

        public Result<TerminalGroupRkiTask> SearchGroupPushRkiTask(int pageNo, int pageSize, SearchOrderBy orderBy,
            long groupId, Nullable<bool> pendingOnly, Nullable<bool> historyOnly, string keyWords) {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalGroupRkiTask>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_GROUP_RKI_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            request.AddParameter("groupId", groupId);
            if (pendingOnly != null) {
                request.AddParameter("pendingOnly", pendingOnly.Value);
            }
            if (historyOnly != null) {
                request.AddParameter("historyOnly", historyOnly.Value);
            }
            if (!string.IsNullOrEmpty(keyWords)) {
                request.AddParameter("keyWords", keyWords.Trim());
            }
            var responseContent = Execute(request);
            TerminalGroupRkiTaskPageResponse response = JsonConvert.DeserializeObject<TerminalGroupRkiTaskPageResponse>(responseContent);
            Result<TerminalGroupRkiTask> result = new Result<TerminalGroupRkiTask>(response);
            return result;
        }

        public Result<TerminalGroupRkiTask> GetGroupPushRkiTask(long groupRkiPushTaskId) {
            IList<string> validationErrs = ValidateId(groupRkiPushTaskId, "groupRkiPushTaskIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalGroupRkiTask>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_TERMINAL_GROUP_RKI_URL, Method.GET);
            request.AddUrlSegment("groupRkiId", groupRkiPushTaskId);
            var responseContent = Execute(request);
            TerminalGroupRkiTaskResponse response = JsonConvert.DeserializeObject<TerminalGroupRkiTaskResponse>(responseContent);
            Result<TerminalGroupRkiTask> result = new Result<TerminalGroupRkiTask>(response);
            return result;
        }

        public Result<TerminalGroupRkiTask> PushRkiKey2Group(CreateTerminalGroupRkiTaskRequest createTerminalGroupRkiTaskRequest) {
            List<string> validationErrs = ValidateCreate(createTerminalGroupRkiTaskRequest, new CreateTerminalGroupRkiTaskValidator(), "parameterCreateTerminalGroupRkiTaskRequestIsNull");
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalGroupRkiTask>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_TERMINAL_GROUP_RKI_URL, Method.POST);
            var requestJson = JsonConvert.SerializeObject(createTerminalGroupRkiTaskRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            TerminalGroupRkiTaskResponse response = JsonConvert.DeserializeObject<TerminalGroupRkiTaskResponse>(responseContent);
            Result<TerminalGroupRkiTask> result = new Result<TerminalGroupRkiTask>(response);
            return result;
        }

        public Result<TerminalGroupRkiTask> DisableGroupRkiPushTask(long groupRkiPushTaskId) {
            IList<string> validationErrs = ValidateId(groupRkiPushTaskId, "groupRkiPushTaskIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalGroupRkiTask>(validationErrs);
            }
            RestRequest request = new RestRequest(SUSPEND_TERMINAL_GROUP_RKI_URL, Method.POST);
            request.AddUrlSegment("groupRkiId", groupRkiPushTaskId);
            var responseContent = Execute(request);
            TerminalGroupRkiTaskResponse response = JsonConvert.DeserializeObject<TerminalGroupRkiTaskResponse>(responseContent);
            Result<TerminalGroupRkiTask> result = new Result<TerminalGroupRkiTask>(response);
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
    }
}

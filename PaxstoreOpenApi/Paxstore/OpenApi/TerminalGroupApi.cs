using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class TerminalGroupApi: BaseApi
    {


        private const string GET_TERMINAL_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}";
        private const string SEARCH_TERMINAL_GROUP_URL = "/v1/3rdsys/terminalGroups";
        private const string CREATE_TERMINAL_GROUP_URL = "/v1/3rdsys/terminalGroups";
        private const string SEARCH_TERMINAL_URL = "/v1/3rdsys/terminalGroups/terminal";
        private const string UPDATE_TERMINAL_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}";
        private const string ACTIVE_TERMINAL_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}/active";
        private const string DISABLE_TERMINAL_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}/disable";
        private const string DELETE_TERMINAL_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}";
        private const string SEARCH_TERMINAL_IN_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}/terminals";
        private const string ADD_TERMINAL_IN_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}/terminals";
        private const string REMOVE_TERMINAL_OUT_GROUP_URL = "/v1/3rdsys/terminalGroups/{groupId}/terminals";

        public TerminalGroupApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret){

        }

        public Result<TerminalGroup> SearchTerminalGroup(int pageNo, int pageSize, Nullable<TerminalGroupSearchOrderBy> orderBy,
                                                        Nullable<TerminalGroupStatus> status, string name, string resellerNames, string modelNames, Nullable<bool> isDynamic)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0){
                return new Result<TerminalGroup>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_GROUP_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }
            if (status != null){
                request.AddParameter("status", ExtEnumHelper.GetEnumValue(status));
            }
            if (!string.IsNullOrEmpty(name)) {
                request.AddParameter("name", name.Trim());
            }

            if (!string.IsNullOrEmpty(resellerNames)){
                request.AddParameter("resellerNames", resellerNames.Trim());
            }
            if (!string.IsNullOrEmpty(modelNames))
            {
                request.AddParameter("modelNames", modelNames.Trim());
            }
            if (isDynamic != null)
            {
                request.AddParameter("isDynamic", isDynamic);
            }
            string responseContent = Execute(request);
            TerminalGroupPageResponse terminalGroupPageResponse = JsonConvert.DeserializeObject<TerminalGroupPageResponse>(responseContent);
            Result<TerminalGroup> result = new Result<TerminalGroup>(terminalGroupPageResponse);
            return result;
        }

        public Result<TerminalGroup> GetTerminalGroup(long groupId)
        {
            RestRequest request = new RestRequest(GET_TERMINAL_GROUP_URL, Method.GET);
            request.AddUrlSegment("groupId", groupId.ToString());
            string responseContent = Execute(request);
            TerminalGroupResponse resp = JsonConvert.DeserializeObject<TerminalGroupResponse>(responseContent);
            Result<TerminalGroup> result = new Result<TerminalGroup>(resp);
            return result;
        }

        public Result<TerminalGroup> CreateTerminalGroup(CreateTerminalGroupRequest createTerminalGroupRequest)
        {
            List<string> validationErrs = new List<string>();
            if (createTerminalGroupRequest == null) {
                validationErrs.Add(GetMsgByKey("parameterCreateTerminalGroupRequestNull"));
                return new Result<TerminalGroup>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_TERMINAL_GROUP_URL, Method.POST);
            var requestJson = JsonConvert.SerializeObject(createTerminalGroupRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContent = Execute(request);
            TerminalGroupResponse resp = JsonConvert.DeserializeObject<TerminalGroupResponse>(responseContent);
            Result<TerminalGroup> result = new Result<TerminalGroup>(resp);
            return result;
        }

        public Result<Terminal> SearchTerminal(int pageNo, int pageSize, Nullable<TerminalSearchOrderBy> orderBy, Nullable<TerminalStatus> status,
                                             string modelName, string resellerName, string merchantName, String serialNo, Nullable<bool> excludeGroupId)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null)
            {
                request.AddParameter("orderBy", TerminalApi.GetOrderValue(orderBy.Value));
            }
            if (status != null) {
                request.AddParameter("status", ExtEnumHelper.GetEnumValue(status));
            }
            if (!string.IsNullOrEmpty(modelName))
            {
                request.AddParameter("modelName", modelName.Trim());
            }
            if (!string.IsNullOrEmpty(resellerName))
            {
                request.AddParameter("resellerName", resellerName.Trim());
            }
            if (!string.IsNullOrEmpty(merchantName))
            {
                request.AddParameter("merchantName", merchantName.Trim());
            }
            if (!string.IsNullOrEmpty(serialNo))
            {
                request.AddParameter("serialNo", serialNo);
            }
            if (excludeGroupId != null)
            {
                request.AddParameter("excludeGroupId", excludeGroupId);
            }
            string responseContent = Execute(request);
            TerminalPageResponse terminalPageResponse = JsonConvert.DeserializeObject<TerminalPageResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalPageResponse);
            return result;
        }



        public Result<TerminalGroup> UpdateTerminalGroup(long groupId, UpdateTerminalGroupRequest updateTerminalGroupRequest)
        {
            List<string> validationErrs = new List<string>();
            if (updateTerminalGroupRequest == null) {
                validationErrs.Add(GetMsgByKey("parameterUpdateTerminalGroupRequestNull"));
                return new Result<TerminalGroup>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_TERMINAL_GROUP_URL, Method.PUT);
            request.AddUrlSegment("groupId", groupId.ToString());
            var requestJson = JsonConvert.SerializeObject(updateTerminalGroupRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContenet = Execute(request);
            TerminalGroupResponse terminalGroupResponse = JsonConvert.DeserializeObject<TerminalGroupResponse>(responseContenet);
            Result<TerminalGroup> result = new Result<TerminalGroup>(terminalGroupResponse);
            return result;
        }

        public Result<string> ActiveGroup(long groupId)
        {
            RestRequest request = new RestRequest(ACTIVE_TERMINAL_GROUP_URL, Method.POST);
            request.AddUrlSegment("groupId", groupId.ToString());
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> DisableGroup(long groupId){
            RestRequest request = new RestRequest(DISABLE_TERMINAL_GROUP_URL, Method.POST);
            request.AddUrlSegment("groupId", groupId.ToString());
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<String> DeleteGroup(long groupId)
        {
            RestRequest request = new RestRequest(DELETE_TERMINAL_GROUP_URL, Method.DELETE);
            request.AddUrlSegment("groupId", groupId.ToString());
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }


        public Result<Terminal> SearchTerminalsInGroup(int pageNo, int pageSize, Nullable<TerminalSearchOrderBy> orderBy,
                                                           long groupId, string serialNo, string merchantNames)
        {

            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_IN_GROUP_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddUrlSegment("groupId", groupId);
            if (orderBy != null)
            {
                request.AddParameter("orderBy", getTerminalSearchOrderBy(orderBy.Value));
            }
            if (!string.IsNullOrEmpty(serialNo)) {
                request.AddParameter("serialNo", serialNo);
            }
            if (!string.IsNullOrEmpty(merchantNames)) {
                request.AddParameter("merchantNames", merchantNames.Trim());
            }
            string responseContent = Execute(request);
            TerminalPageResponse terminalPageResponse = JsonConvert.DeserializeObject<TerminalPageResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalPageResponse);
            return result;
        }

        string getTerminalSearchOrderBy(TerminalSearchOrderBy orderBy) {
            switch (orderBy)
            {
                case TerminalSearchOrderBy.Name:
                    return "a.name ASC, a.id ASC";
                case TerminalSearchOrderBy.TID:
                    return "a.tid ASC, a.id ASC";
                case TerminalSearchOrderBy.SerialNo:
                    return "a.serial_no ASC, a.id ASC";
            }
            return "a.name ASC, a.id ASC";
        }

        public Result<string> AddTerminalToGroup(long groupId, HashSet<long> terminalIds)
        {
            if (terminalIds == null || terminalIds.Count == 0) {
                List<string> validationErrs = new List<string>();
                validationErrs.Add(GetMsgByKey("parameterTerminalIdsMandatory"));
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(ADD_TERMINAL_IN_GROUP_URL, Method.POST);
            request.AddUrlSegment("groupId", groupId.ToString());
            var requestJson = JsonConvert.SerializeObject(terminalIds);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }


        public Result<string> RemoveTerminalOutGroup(long groupId, HashSet<long> terminalIds)
{
            if (terminalIds == null || terminalIds.Count == 0) {
                List<string> validationErrs = new List<string>();
                validationErrs.Add(GetMsgByKey("parameterTerminalIdsMandatory"));
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(REMOVE_TERMINAL_OUT_GROUP_URL, Method.PUT);
            request.AddUrlSegment("groupId", groupId.ToString());
            var requestJson = JsonConvert.SerializeObject(terminalIds);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
}

    

    public enum TerminalGroupSearchOrderBy{
        [EnumValue("name")]
        Name,

        [EnumValue("createdDate DESC")]
        CreatedDate_desc,

        [EnumValue("createdDate ASC")]
        CreatedDate_asc
    }

    public enum TerminalGroupStatus
    {
        [EnumValue("P")]
        Pending,

        [EnumValue("A")]
        Active,

        [EnumValue("S")]
        Suspend
    }

}

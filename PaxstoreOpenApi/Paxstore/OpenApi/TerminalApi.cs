using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.Terminal;

namespace Paxstore.OpenApi
{
    public class TerminalApi: BaseApi
    {
        private const string SEARCH_TERMINAL_URL = "/v1/3rdsys/terminals";
	    private const string GET_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}";
	    private const string ACTIVE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}/active";
	    private const string DISABLE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}/disable";
	    private const string DELETE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}";
	    private const string CREATE_TERMINAL_URL = "/v1/3rdsys/terminals";
	    private const string UPDATE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}";
        private const string ADD_TERMINAL_TO_GROUP_URL = "/v1/3rdsys/terminals/groups";

        public TerminalApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret){

        }

        public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, string snNameTID) {
            return this.SearchTerminal(pageNo, pageSize, orderBy, status, snNameTID, false, false, false);
        }

        public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, string snNameTID, bool includeGeoLocation, bool includeInstalledApks, bool includeInstalledFirmware) {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            request.AddParameter("snNameTID", snNameTID);
            request.AddParameter("serialNo", snNameTID);
            request.AddParameter("status", GetStatusValue(status));

            request.AddParameter("includeGeoLocation", includeGeoLocation.ToString());
            request.AddParameter("includeInstalledFirmware", includeInstalledFirmware.ToString());
            request.AddParameter("includeInstalledApks", includeInstalledApks.ToString());

            var responseContent = Execute(request);
            TerminalPageResponse resellerPage = JsonConvert.DeserializeObject<TerminalPageResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(resellerPage);
            return result;
        }

        public Result<Terminal> GetTerminal(long terminalId) {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_TERMINAL_URL, Method.GET);
            request.AddUrlSegment("terminalId", terminalId);
            var responseContent = Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalResponse);
            return result;
        }

        public Result<Terminal> CreateTerminal(TerminalCreateRequest terminalCreateRequest){
            List<string> validationErrs = ValidateCreate(terminalCreateRequest, new TerminalCreateValidator(),"terminalCreateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_TERMINAL_URL, Method.POST);
            var terminalJson = JsonConvert.SerializeObject(terminalCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, terminalJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalResponse);
            return result;
        }

        public Result<Terminal> UpdateTerminal(long terminalId,TerminalUpdateRequest terminalUpdateRequest){
            IList<string> validationErrs = ValidateUpdate(terminalId, terminalUpdateRequest, new TerminalUpdateValidator(), "terminalIdInvalid", "terminalUpdateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_TERMINAL_URL, Method.PUT);
            var terminalJson = JsonConvert.SerializeObject(terminalUpdateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, terminalJson, ParameterType.RequestBody);
            request.AddUrlSegment("terminalId",terminalId);
            var responseContent = Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalResponse);
            return result;
        }

        public Result<string> ActivateTerminal(long terminalId) {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(ACTIVE_TERMINAL_URL, Method.PUT);
            request.AddUrlSegment("terminalId",terminalId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
        
        public Result<string> DisableTerminal(long terminalId) {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DISABLE_TERMINAL_URL, Method.PUT);
            request.AddUrlSegment("terminalId",terminalId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
        
        public Result<String> DeleteTerminal(long terminalId) {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DELETE_TERMINAL_URL, Method.DELETE);
            request.AddUrlSegment("terminalId",terminalId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> BatchAddTerminalToGroup(TerminalGroupRequest batchAddTerminalToGroupRequest)
        {
            List<string> validationErrs = new List<string>();
            if (batchAddTerminalToGroupRequest == null) {
                validationErrs.Add(GetMsgByKey("parameterBatchAddTerminalToGroupRequestNull"));
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(ADD_TERMINAL_TO_GROUP_URL, Method.POST);
            var requestJson = JsonConvert.SerializeObject(batchAddTerminalToGroupRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }


        string GetStatusValue(TerminalStatus status)
        {
            switch (status)
            {
                case TerminalStatus.Active:
                    return "A";
                case TerminalStatus.Inactive:
                    return "P";
                case TerminalStatus.Suspend:
                    return "S";
                case TerminalStatus.All:
                    return null;
            }
            return null;
        }

        public static string GetOrderValue(TerminalSearchOrderBy order)
        {
            switch (order)
            {
                case TerminalSearchOrderBy.Name:
                    return "name";
                case TerminalSearchOrderBy.TID:
                    return "tid";
                case TerminalSearchOrderBy.SerialNo:
                    return "serialNo";
            }
            return "name";
        }
    }

    public enum TerminalStatus {
		Active,
		Inactive,
		Suspend,
        All
	}
	
	public enum TerminalSearchOrderBy {
		Name,
		TID,
		SerialNo
	}
}
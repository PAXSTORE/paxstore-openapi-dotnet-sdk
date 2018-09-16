using System;
using RestSharp;
using Newtonsoft.Json;
using Com.Pax.OpenApi.Sdk.Dto.Terminal;
using Com.Pax.OpenApi.Sdk.Base.Dto;
using System.Collections.Generic;
using paxstore_openapi_sdk.Com.Pax.OpenApi.Sdk.Dto.Terminal;
using Com.Pax.OpenApi.Sdk.Validator.Terminal;
using Com.Pax.OpenApi.Sdk.Base;

namespace Com.Pax.OpenApi.Sdk.Api
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

        public TerminalApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret){

        }

        public Result<TerminalDTO> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, string snNameTID) {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            request.AddParameter("snNameTID", snNameTID);
            request.AddParameter("status", GetStatusValue(status));
            var responseContent = Execute(request);
            TerminalPageResponse resellerPageDTO = JsonConvert.DeserializeObject<TerminalPageResponse>(responseContent);
            Result<TerminalDTO> result = new Result<TerminalDTO>(resellerPageDTO);
            return result;
        }

        public Result<TerminalDTO> GetTerminal(long terminalId) {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_TERMINAL_URL, Method.GET);
            request.AddUrlSegment("terminalId", terminalId);
            var responseContent = Execute(request);
            TerminalResponseDTO terminalResponse = JsonConvert.DeserializeObject<TerminalResponseDTO>(responseContent);
            Result<TerminalDTO> result = new Result<TerminalDTO>(terminalResponse);
            return result;
        }

        public Result<TerminalDTO> CreateTerminal(TerminalCreateRequest terminalCreateRequest){
            List<string> validationErrs = ValidateCreate(terminalCreateRequest, new TerminalCreateValidator(),"terminalCreateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<TerminalDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_TERMINAL_URL, Method.POST);
            var terminalJson = request.JsonSerializer.Serialize(terminalCreateRequest);
            request.AddParameter("application/json; charset=utf-8", terminalJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            TerminalResponseDTO terminalResponseDTO = JsonConvert.DeserializeObject<TerminalResponseDTO>(responseContent);
            Result<TerminalDTO> result = new Result<TerminalDTO>(terminalResponseDTO);
            return result;
        }

        public Result<TerminalDTO> UpdateTerminal(long terminalId,TerminalUpdateRequest terminalUpdateRequest){
            IList<string> validationErrs = ValidateUpdate(terminalId, terminalUpdateRequest, new TerminalUpdateValidator(), "terminalIdInvalid", "terminalUpdateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<TerminalDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_TERMINAL_URL, Method.PUT);
            var terminalJson = request.JsonSerializer.Serialize(terminalUpdateRequest);
            request.AddParameter("application/json; charset=utf-8", terminalJson, ParameterType.RequestBody);
            request.AddUrlSegment("terminalId",terminalId);
            var responseContent = Execute(request);
            TerminalResponseDTO terminalResponse = JsonConvert.DeserializeObject<TerminalResponseDTO>(responseContent);
            Result<TerminalDTO> result = new Result<TerminalDTO>(terminalResponse);
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
            Result<String> result = new Result<String>(emptyResponse);
            return result;
        }
        
        public Result<string> disableTerminal(long terminalId) {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DISABLE_TERMINAL_URL, Method.PUT);
            request.AddUrlSegment("terminalId",terminalId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<String> result = new Result<String>(emptyResponse);
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
            Result<String> result = new Result<String>(emptyResponse);
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

        string GetOrderValue(TerminalSearchOrderBy order)
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
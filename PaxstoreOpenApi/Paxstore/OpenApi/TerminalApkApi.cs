using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.TerminalApk;
using RestSharp;

namespace Paxstore.OpenApi {
    public class TerminalApkApi: BaseApi { 
        private const string CREATE_TERMINAL_APK_URL = "/v1/3rdsys/terminalApks";

        private const string SEARCH_TERMINAL_APK_LIST_URL = "/v1/3rdsys/terminalApks";

	    private const string GET_TERMINAL_APK_URL = "/v1/3rdsys/terminalApks/{terminalApkId}";

	    private const string TEMPLATE_NAME_DELIMITER = "|";
	    private const int MAX_TEMPLATE_SIZE = 10;

        public TerminalApkApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<string> CreateTerminalApk(CreateTerminalApkRequest createTerminalApkRequest){
            List<String> validationErrs = ValidateCreateTerminalApk(createTerminalApkRequest);
            
            if(validationErrs.Count>0) {
                return new Result<String>(validationErrs);
            }

            RestRequest request = new RestRequest(CREATE_TERMINAL_APK_URL, Method.POST);

            var terminalApkJson = JsonConvert.SerializeObject(createTerminalApkRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, terminalApkJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<PushApkHistory> SearchPushApkHistory(int pageNo, int pageSize, SearchOrderBy orderBy,
                                                    string terminalTid, string appPackageName, PushStatus status){
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrWhiteSpace(terminalTid)) {
                validationErrs.Add(GetMsgByKey("parameterTerminalTidEmpty"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<PushApkHistory>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_APK_LIST_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("terminalTid", terminalTid);
            request.AddParameter("appPackageName", appPackageName);
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            request.AddParameter("status", GetPushStatusValue(status));
            var responseContent = Execute(request);
            ApkPushHistoryPageResponse apkPushHistoryPageDTO = JsonConvert.DeserializeObject<ApkPushHistoryPageResponse>(responseContent);
            Result<PushApkHistory> result = new Result<PushApkHistory>(apkPushHistoryPageDTO);
            return result;
        }

        public Result<PushApkHistory> GetPushApkHistory(long pushApkId){
            IList<string> validationErrs = ValidateId(pushApkId, "pushApkIdInvalid");

            if (validationErrs.Count > 0)
            {
                return new Result<PushApkHistory>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_TERMINAL_APK_URL, Method.GET);
            request.AddUrlSegment("terminalApkId", pushApkId);
            var responseContent = Execute(request);
            PushApkHistoryResponse apkPushHistoryResponse = JsonConvert.DeserializeObject<PushApkHistoryResponse>(responseContent);
            Result<PushApkHistory> result = new Result<PushApkHistory>(apkPushHistoryResponse);
            return result;
        }

        private List<string> ValidateCreateTerminalApk(CreateTerminalApkRequest createTerminalApkRequest) {
            List<string> validationErrs = new List<string>();
            if(createTerminalApkRequest == null) {
                validationErrs.Add(GetMsgByKey("parameterCreateTerminalApkRequestNull"));
            }else {
                IValidator validator = new TerminalApkCreateValidator();

                ValidationResult results = validator.Validate(createTerminalApkRequest);
                if (!results.IsValid)
                {
                    IList<ValidationFailure> failures = results.Errors;
                    for (int i = 0; i < results.Errors.Count; i++)
                    {
                        validationErrs.Add(results.Errors[i].ErrorMessage);
                    }
                }
                if(string.IsNullOrEmpty(createTerminalApkRequest.SerialNo) && string.IsNullOrEmpty(createTerminalApkRequest.TID)) {
                    validationErrs.Add(GetMsgByKey("parameterCreateTerminalApkRequestSnTidEmpty"));
                }
                if(!string.IsNullOrEmpty(createTerminalApkRequest.TemplateName)) {
                    if(createTerminalApkRequest.TemplateName.Split(new char[1] { '|'}).Length>MAX_TEMPLATE_SIZE) {
                        validationErrs.Add(GetMsgByKey("parameterCreateTerminalApkRequestTemplateNameTooLong"));
                    }
                }
            }
            return validationErrs;
        }

        string GetPushStatusValue(PushStatus status)
        {
            switch (status)
            {
                case PushStatus.Active:
                    return "A";
                case PushStatus.Suspend:
                    return "S";
                case PushStatus.All:
                    return null;
            }
            return null;
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

    public enum SearchOrderBy{
        CreatedDate_desc,
        CreatedDate_asc
    }

    public enum PushStatus{
        Active,
        Suspend,
        All
    }
}
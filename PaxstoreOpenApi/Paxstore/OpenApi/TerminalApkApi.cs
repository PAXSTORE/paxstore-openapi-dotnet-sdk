using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.TerminalApk;
using RestSharp;

namespace Paxstore.OpenApi {
    public class TerminalApkApi: BaseApi { 
        private const string CREATE_TERMINAL_APK_URL = "/v1/3rdsys/terminalApks";

        private const string SEARCH_TERMINAL_APK_LIST_URL = "/v1/3rdsys/terminalApks";

	    private const string GET_TERMINAL_APK_URL = "/v1/3rdsys/terminalApks/{terminalApkId}";

        private const string SUSPEND_TERMINAL_APK_URL = "/v1/3rdsys/terminalApks/suspend";

	    private const string UNINSTALL_TERMINAL_APK_URL = "/v1/3rdsys/terminalApks/uninstall";


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
            request.AddParameter("status", PushStatusHelper.GetPushStatusVal(status));
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

        public Result<string> DisableApkPushByTidAndPackageName(string tid, string packageName){
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(tid)) {
                validationErrs.Add(GetMsgByKey("parameterTidCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(packageName)) {
                validationErrs.Add(GetMsgByKey("parameterPackageNameCannotBeEmpty"));
            }

            if (validationErrs.Count> 0){
                return new Result<string>(validationErrs);
            }
            ApkPushInfo apkPushInfo = new ApkPushInfo();
            apkPushInfo.TID = tid;
            apkPushInfo.PackageName = packageName;
            return SuspendApklPush(apkPushInfo);
        }

        public Result<string> DisableApkPushBySnAndPackageName(string serialNo, string packageName){
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(serialNo)){
                validationErrs.Add(GetMsgByKey("parameterSnCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(packageName)){
                validationErrs.Add(GetMsgByKey("parameterPackageNameCannotBeEmpty"));
            }

            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            ApkPushInfo apkPushInfo = new ApkPushInfo();
            apkPushInfo.SerialNo = serialNo;
            apkPushInfo.PackageName = packageName;
            return SuspendApklPush(apkPushInfo);
        }

        private Result<string> SuspendApklPush(ApkPushInfo suspendApkPushRequest){
            RestRequest request = new RestRequest(SUSPEND_TERMINAL_APK_URL, Method.POST);
            var suspendApkPushRequestJson = JsonConvert.SerializeObject(suspendApkPushRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, suspendApkPushRequestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> UninstallApkByTidAndPackageName(string tid, string packageName) {
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(tid)){
                validationErrs.Add(GetMsgByKey("parameterTidCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(packageName)){
                validationErrs.Add(GetMsgByKey("parameterPackageNameCannotBeEmpty"));
            }

            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            ApkPushInfo apkPushInfo = new ApkPushInfo();
            apkPushInfo.TID = tid;
            apkPushInfo.PackageName = packageName;
            return UninstallTerminalApk(apkPushInfo);
        }

        public Result<string> UninstallApkBySnAndPackageName(string serialNo, string packageName){
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(serialNo)){
                validationErrs.Add(GetMsgByKey("parameterSnCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(packageName)){
                validationErrs.Add(GetMsgByKey("parameterPackageNameCannotBeEmpty"));
            }

            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            ApkPushInfo apkPushInfo = new ApkPushInfo();
            apkPushInfo.SerialNo = serialNo;
            apkPushInfo.PackageName = packageName;
            return UninstallTerminalApk(apkPushInfo);
        }

        private Result<string> UninstallTerminalApk(ApkPushInfo uninstallTerminalApkRequest){
            RestRequest request = new RestRequest(UNINSTALL_TERMINAL_APK_URL, Method.POST);
            var uninstallApkRequestJson = JsonConvert.SerializeObject(uninstallTerminalApkRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, uninstallApkRequestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
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
                if (createTerminalApkRequest.Base64FileParameters != null) {
                    if (createTerminalApkRequest.Base64FileParameters.Count > 10)
                    {
                        validationErrs.Add("Exceed max counter (10) of file type parameters!");
                    }
                    for (int i = 0; i < createTerminalApkRequest.Base64FileParameters.Count; i++)
                    {
                        if (Base64FileUtil.GetBase64FileSizeKB(createTerminalApkRequest.Base64FileParameters[i].FileData) > 500)
                        {
                            validationErrs.Add("Exceed max size (500kb) per file type parameters!");
                            break;
                        }
                    }
                }
            
            }
            return validationErrs;
        }

        private List<String> ValidatePushFirmware2Terminal(PushFirmware2TerminalRequest pushFirmware2TerminalRequest)
        {
            List<string> validationErrs = new List<string>();
            if (pushFirmware2TerminalRequest == null)
            {
                validationErrs.Add(GetMsgByKey("parameterPushFirmware2TerminalRequestNull"));
            }
            else
            {
                if (string.IsNullOrEmpty(pushFirmware2TerminalRequest.SerialNo) && string.IsNullOrEmpty(pushFirmware2TerminalRequest.TID))
                {
                    validationErrs.Add(GetMsgByKey("parameterCreateTerminalApkRequestSnTidEmpty"));
                }
                if (string.IsNullOrEmpty(pushFirmware2TerminalRequest.FirmwareName))
                {
                    validationErrs.Add(GetMsgByKey("parameterpushFirmware2TerminalRequestSnTidEmpty"));
                }
            }
            return validationErrs;
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
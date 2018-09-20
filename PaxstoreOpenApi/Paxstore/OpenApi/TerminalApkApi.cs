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
    }
}
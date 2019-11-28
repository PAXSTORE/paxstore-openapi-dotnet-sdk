using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class TerminalApkParameterApi : BaseApi
    {

        private const string GET_TERMINAL_APK_PARAMETER_URL = "/v1/3rdsys/apkParameters";
        private const string CREATE_APK_PARAMETER_URL = "/v1/3rdsys/apkParameters";
        private const string UPDATE_APK_PARAMETER_URL = "/v1/3rdsys/apkParameters/{apkParameterId}";
        private const string DELETE_APK_PARAMETER_URL = "/v1/3rdsys/apkParameters/{apkParameterId}";

        public TerminalApkParameterApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<ApkParameter> GetTerminalApkParameter(String templateName, String packageName, String versionName)
        {
            RestRequest request = new RestRequest(GET_TERMINAL_APK_PARAMETER_URL, Method.GET);
            if (!string.IsNullOrEmpty(templateName)) {
                request.AddParameter("templateName", templateName);
            }
            if (!string.IsNullOrEmpty(packageName)) {
                request.AddParameter("packageName", packageName);
            }
            if (!string.IsNullOrEmpty(versionName)) {
                request.AddParameter("versionName", versionName);
            }
            
            var responseContent = Execute(request);
            ApkParameterPageResponse apkParameterPageResponse = JsonConvert.DeserializeObject<ApkParameterPageResponse>(responseContent);
            Result<ApkParameter> result = new Result<ApkParameter>(apkParameterPageResponse);
            return result;
        }

        public Result<ApkParameter> CreateApkParameter(CreateApkParameterRequest createApkParameterRequest)
        {
            RestRequest request = new RestRequest(CREATE_APK_PARAMETER_URL, Method.POST);
            var createRequestJson = JsonConvert.SerializeObject(createApkParameterRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, createRequestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            ApkParameterResponse apkParameterResponse = JsonConvert.DeserializeObject<ApkParameterResponse>(responseContent);
            Result<ApkParameter> result = new Result<ApkParameter>(apkParameterResponse);
            return result;
        }

        public Result<ApkParameter> UpdateApkParameter(long apkParameterId, UpdateApkParameterRequest updateApkParameterRequest)
        {
            RestRequest request = new RestRequest(UPDATE_APK_PARAMETER_URL, Method.PUT);
            var updateRequestJson = JsonConvert.SerializeObject(updateApkParameterRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, updateRequestJson, ParameterType.RequestBody);
            request.AddUrlSegment("apkParameterId", apkParameterId.ToString());
            var responseContent = Execute(request);
            ApkParameterResponse apkParameterResponse = JsonConvert.DeserializeObject<ApkParameterResponse>(responseContent);
            Result<ApkParameter> result = new Result<ApkParameter>(apkParameterResponse);
            return result;
        }

        public Result<string> DeleteApkParameter(long apkParameterId)
        {
            RestRequest request = new RestRequest(DELETE_APK_PARAMETER_URL, Method.DELETE);
            request.AddUrlSegment("apkParameterId",apkParameterId.ToString());
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
    }
}

using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.TerminalApkParameter;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public TerminalApkParameterApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public TerminalApkParameterApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public TerminalApkParameterApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public TerminalApkParameterApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }

        public Result<ApkParameter> GetTerminalApkParameter(int pageNo, int pageSize, Nullable<TerminalApkParamSearchOrderBy> orderBy, string templateName, string packageName, string versionName)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<ApkParameter>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_TERMINAL_APK_PARAMETER_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }
            if (!string.IsNullOrEmpty(templateName))
            {
                request.AddParameter("templateName", templateName);
            }
            if (!string.IsNullOrEmpty(packageName))
            {
                request.AddParameter("packageName", packageName);
            }
            if (!string.IsNullOrEmpty(versionName))
            {
                request.AddParameter("versionName", versionName);
            }

            var responseContent = Execute(request);
            ApkParameterPageResponse apkParameterPageResponse = JsonConvert.DeserializeObject<ApkParameterPageResponse>(responseContent);
            Result<ApkParameter> result = new Result<ApkParameter>(apkParameterPageResponse);
            return result;
        }

        public Result<string> CreateApkParameter(CreateApkParameterRequest createApkParameterRequest)
        {
            List<string> validationErrs = ValidateCreate(createApkParameterRequest, new TerminalApkParameterCreateValidator(), "paramCreateApkParameterRequestMandatory");
            if (createApkParameterRequest != null) {
                if (createApkParameterRequest.Parameters == null && createApkParameterRequest.Base64FileParameters == null) {
                    validationErrs.Add(GetMsgByKey("paramAndFileParamEmptySameTime"));
                }
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }

            RestRequest request = new RestRequest(CREATE_APK_PARAMETER_URL, Method.Post);
            var createRequestJson = JsonConvert.SerializeObject(createApkParameterRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, createRequestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> UpdateApkParameter(long apkParameterId, UpdateApkParameterRequest updateApkParameterRequest)
        {
            List<string> validationErrs = ValidateUpdate(apkParameterId, updateApkParameterRequest, new TerminalApkParameterUpdateValidator(), "paramterApkParameterIdIsNull", "parameterUpdateApkParameterRequestIsNull");

            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_APK_PARAMETER_URL, Method.Put);
            var updateRequestJson = JsonConvert.SerializeObject(updateApkParameterRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, updateRequestJson, ParameterType.RequestBody);
            request.AddUrlSegment("apkParameterId", apkParameterId.ToString());
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> DeleteApkParameter(long apkParameterId)
        {
            List<string> validationErrs = ValidateId(apkParameterId, "parameterApkParameterIdIsInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DELETE_APK_PARAMETER_URL, Method.Delete);
            request.AddUrlSegment("apkParameterId", apkParameterId.ToString());
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
    }

    public enum TerminalApkParamSearchOrderBy
    {
        [EnumValue("a.created_date DESC")]
        ApkParameter_desc,
        [EnumValue("a.created_date ASC")]
        ApkParameter_asc
    }
}

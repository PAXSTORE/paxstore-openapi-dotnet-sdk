using System;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Resources;
using System.Reflection;
using System.Threading;
using FluentValidation;
using FluentValidation.Results;
using Paxstore.OpenApi.Base.Dto;
using Paxstore.OpenApi.Validator;
using log4net;
using Newtonsoft.Json.Linq;
using Paxstore.OpenApi.Help;

namespace Paxstore.OpenApi.Base
{
    public class BaseApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(BaseApi));
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }


        private const string HEADER_X_RATE_LIMIT_LIMIT = "X-RateLimit-Limit";
        private const string HEADER_X_RATE_LIMIT_REMAINING = "X-RateLimit-Remaining";
        private const string HEADER_X_RATE_LIMIT_RESET = "X-RateLimit-Reset";
        


        protected RestClient Client;
        protected TimeZoneInfo defaultTimeZone = TimeZoneInfo.Local;
        public string StrClutrue { get; set; }

        public BaseApi(string baseUrl, string apiKey, string apiSecret)
        {
            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret)) {
                throw new ArgumentException("Construct parameter(s) is invalid");
            }
            if (baseUrl != null && baseUrl.EndsWith("/")) {
                baseUrl = baseUrl.Remove(baseUrl.Length - 1);
            }
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            Client = new RestClient(BaseUrl);
            Client.Timeout = 5000;
            Client.ReadWriteTimeout = 5000;
            //TimeZoneInfo.
        }

        public BaseApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) {
            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret) || timeZoneInfo == null)
            {
                throw new ArgumentException("Construct parameter(s) is invalid");
            }
            if (baseUrl != null && baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Remove(baseUrl.Length - 1);
            }
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            Client = new RestClient(BaseUrl);
            Client.Timeout = 5000;
            Client.ReadWriteTimeout = 5000;
            defaultTimeZone = timeZoneInfo;
        }

        public void SetConnectionTimeoutTime(int connectTimeoutTime) {
            if (connectTimeoutTime > 0) {
                Client.Timeout = connectTimeoutTime;
                _logger.DebugFormat("Set ConnectTimeout=" + connectTimeoutTime.ToString());
            }
        }

        public void SetReadWriteTimeoutTime(int readWriteTimeoutTime) {
            if (readWriteTimeoutTime > 0) {
                Client.ReadWriteTimeout = readWriteTimeoutTime;
                _logger.DebugFormat("Set read/write timeout=" + readWriteTimeoutTime.ToString());
            }
        }

        public void SetProxy(IWebProxy proxy) {
            Client.Proxy = proxy;
        }

        public BaseApi(string baseUrl, string apiKey, string apiSecret, int connectionTimeout, int readWriteTimeout)
        {
            if (baseUrl != null && baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Remove(baseUrl.Length - 1);
            }
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            Client = new RestClient(BaseUrl);
            
            if (connectionTimeout > 0)
            {
                Client.Timeout = connectionTimeout;
            }
            else {
                Client.Timeout = 5000;
            }
            if (readWriteTimeout > 0)
            {
                Client.ReadWriteTimeout = readWriteTimeout;
            }
            else {
                Client.ReadWriteTimeout = 5000;
            }
            
        }



        protected string Execute(RestRequest request)
        {
            _logger.DebugFormat("SDK Language is {0} and version is {1}", Constants.THIRD_PARTY_API_SDK_LANGUAGE, Constants.THIRD_PARTY_API_SDK_VERSION);
            _logger.DebugFormat("RestRequest={0}", request.ToString());
            _logger.DebugFormat("RestRequest URL\t\t={0}", request.Resource);
            _logger.DebugFormat("RestRequest Method\t\t={0}", request.Method);
            _logger.Debug("Request Parameters\t\t=");
            
            if (_logger.IsDebugEnabled) {
                for (int i = 0; i < request.Parameters.Count; i++) {
                    _logger.Debug(JsonConvert.SerializeObject(request.Parameters[i]));
                }
            }
            request.AddParameter(Constants.PARAM_NAME_SYSKEY, ApiKey, ParameterType.QueryString);
            string querystr = GetQueryString(Client, request);
            string signature = Utils.GenSignature(ApiSecret, querystr);
            request.AddHeader(Constants.HEADER_NAME_SIGNATURE, signature);
            request.AddHeader(Constants.REQ_HEADER_SDK_LANG, Constants.THIRD_PARTY_API_SDK_LANGUAGE);
            request.AddHeader(Constants.REQ_HEADER_SDK_VERSION, Constants.THIRD_PARTY_API_SDK_VERSION);
            request.AddHeader(Constants.TIME_ZONE, TimeZoneHelper.GetTimeZoneId(defaultTimeZone));
            IRestResponse response = Client.Execute(request);
            _logger.DebugFormat("Response StatusCode\t\t={0}", response.StatusCode);
            _logger.DebugFormat("Response Content=\n{0}", response.Content);
            IList<Parameter> headers = response.Headers;
            HttpStatusCode responseStatus = response.StatusCode;

            if (((HttpStatusCode.OK.Equals(responseStatus) || HttpStatusCode.Created.Equals(responseStatus) ||
                 HttpStatusCode.BadRequest.Equals(responseStatus) ||
                    HttpStatusCode.InternalServerError.Equals(responseStatus) || HttpStatusCode.Forbidden.Equals(responseStatus)) ) || HttpStatusCode.NoContent.Equals(responseStatus))
            {
                
                return HandleRateLimitHeader(headers, response.Content, response.ContentType);
            }
            else
            {
                return HandleRateLimitHeader(headers, HandleUnexceptedResponse(response), response.ContentType);
            }
        }

        private string HandleRateLimitHeader(IList<Parameter> headers, string responseBody, string contentType) {
            if (string.IsNullOrWhiteSpace(responseBody)) {
                responseBody = "{}";
            }else if (!string.IsNullOrWhiteSpace(contentType) && !contentType.ToUpper().Contains("JSON")) {
                responseBody = "{'BusinessCode':999,'Message':'" + responseBody + "'}";
            }
            if (headers != null && headers.Count > 0) {
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseBody);
                for (int i = 0; i < headers.Count; i++) {
                    if (HEADER_X_RATE_LIMIT_LIMIT.Equals(headers[i].Name))
                    {
                        jo.Add("rateLimit", headers[i].Value.ToString());
                    }
                    else if (HEADER_X_RATE_LIMIT_REMAINING.Equals(headers[i].Name))
                    {
                        jo.Add("rateLimitRemain", headers[i].Value.ToString());
                    }
                    else if (HEADER_X_RATE_LIMIT_RESET.Equals(headers[i].Name)) {
                        jo.Add("rateLimitReset", headers[i].Value.ToString());
                    }
                }
                return jo.ToString();
            }
            return responseBody;
        }

        private string HandleUnexceptedResponse(IRestResponse response)
        {
            HttpStatusCode responseStatus = response.StatusCode;
            if (HttpStatusCode.NotFound.Equals(responseStatus))
            {
                if (!string.IsNullOrEmpty(response.Content) && response.Content.Contains("businessCode")) {
                    return response.Content;
                }
                else {
                    return GenSdkRequestErrorJson(-2, GetMsgByKey("msg_16111"));
                }
                
            }
            else if (HttpStatusCode.RequestTimeout.Equals(responseStatus))
            {
                return GenSdkRequestErrorJson(-3, GetMsgByKey("msg_16104"));
            }
            else
            {
                return GenSdkRequestErrorJson(-4, string.IsNullOrWhiteSpace(response.ErrorMessage) ? GetMsgByKey("msg_16000") : response.ErrorMessage);
            }
        }

        //生成response json，直接并不是真正的response content，可能是由于网络等原因请求并未真正到达remote server，
        //这边根据responsestatus返回一个json给调用者，以便于让调用者知道请求失败的原因
        private string GenSdkRequestErrorJson(int businessCode, string message)
        {
            var result = new
            {
                businessCode,
                message
            };
            string resultJson = JsonConvert.SerializeObject(result);
            return resultJson;
        }

        protected string GetQueryString(RestClient client, RestRequest request)
        {
            return Client.BuildUri(request).Query.Replace("?", "");
        }

        protected List<string> ValidateId(long id, string errorMsgKey)
        {
            List<string> validationErrs = new List<string>();
            if (id <= 0L)
            {
                validationErrs.Add(GetMsgByKey(errorMsgKey));
            }
            return validationErrs;
        }

        protected List<string> ValidatePageSizeAndPageNo(int pageSize, int pageNo){
            PageMetadata pageMetadata = new PageMetadata(pageSize, pageNo);
            IValidator validator = new PageMetadataValidator();
            ValidationResult results = validator.Validate(pageMetadata);
            List<string> validationErrs = new List<string>();
            if (!results.IsValid){
                IList<ValidationFailure> failures = results.Errors;
                for (int i = 0; i < results.Errors.Count; i++)
                {
                    validationErrs.Add(results.Errors[i].ErrorMessage);
                }
            }
            return validationErrs;
        }

        protected List<string> ValidateCreate<T>(T createReq, IValidator validator, string objNullMsg){
            List<string> validationErrs = new List<string>();
            if(createReq== null) {
                validationErrs.Add(GetMsgByKey(objNullMsg));
                return validationErrs;
            }else {
               
                ValidationResult results = validator.Validate(createReq);
                if (!results.IsValid)
                {
                    IList<ValidationFailure> failures = results.Errors;
                    for (int i = 0; i < results.Errors.Count; i++)
                    {
                        validationErrs.Add(results.Errors[i].ErrorMessage);
                    }
                }
                return validationErrs;
            }
        }

        protected List<string> ValidateUpdate<T>(long id, T updateReq, IValidator validator, string idInvalidMsg, string objNullMsg){
            List<string> validationErrs = ValidateId(id, idInvalidMsg);
            if(updateReq== null) {
                validationErrs.Add(GetMsgByKey(objNullMsg));
                return validationErrs;
            }else if(validator != null){
                ValidationResult results = validator.Validate(updateReq);
                if (!results.IsValid)
                {
                    IList<ValidationFailure> failures = results.Errors;
                    for (int i = 0; i < results.Errors.Count; i++)
                    {
                        validationErrs.Add(results.Errors[i].ErrorMessage);
                    }
                }
                return validationErrs;
            }
            return validationErrs;
        }

        protected string GetMsgByKey(string key)
        {
            string strValue = string.Empty;
            try
            {
                ResourceManager resManager = new ResourceManager("Paxstore.OpenApi.Properties.ValidationMessages", Assembly.GetExecutingAssembly());
                strValue = resManager.GetString(key, Thread.CurrentThread.CurrentCulture);
                if (string.IsNullOrEmpty(strValue)) {
                    strValue = key;
                    _logger.WarnFormat("No value for key {0} is defined in resource file, please add.", key);
                }
            }
            catch
            {
                strValue = key;
                _logger.WarnFormat("No value for key {0} is defined in resource file, please add.", key);
            }
            return strValue;
        }

        private string GetMessage(string key)
        {
            return key;
        }

    }
}
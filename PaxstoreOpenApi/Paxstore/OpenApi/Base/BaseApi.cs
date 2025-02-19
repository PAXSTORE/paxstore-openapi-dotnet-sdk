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
using Newtonsoft.Json.Linq;
using Paxstore.OpenApi.Help;
using Serilog;
using Serilog.Events;
using System.Collections;


namespace Paxstore.OpenApi.Base
{
    public class BaseApi
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        protected const int DEFAULT_TIMEOUT = 5000;

        private const string HEADER_X_RATE_LIMIT_LIMIT = "X-RateLimit-Limit";
        private const string HEADER_X_RATE_LIMIT_REMAINING = "X-RateLimit-Remaining";
        private const string HEADER_X_RATE_LIMIT_RESET = "X-RateLimit-Reset";


        protected RestClient Client;
        protected TimeZoneInfo defaultTimeZone = TimeZoneInfo.Local;


        public BaseApi(string baseUrl, string apiKey, string apiSecret):this(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, null)
        {

        }

        public BaseApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : this(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public BaseApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : this(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public BaseApi(string baseUrl, string apiKey, string apiSecret, int timeout) : this(baseUrl, apiKey, apiSecret, null, timeout, null) {

        }

        public BaseApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = DEFAULT_TIMEOUT, IWebProxy proxy = null) {
            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentException("Construct parameter(s) is invalid");
            }
            if (timeout <= 0)
            {
                throw new ArgumentException("timeout must grate than 0");
            }
            if (timeZoneInfo != null) {
                this.defaultTimeZone = timeZoneInfo;
            }
            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Remove(baseUrl.Length - 1);
            }
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            RestClientOptions clientOptions = new RestClientOptions { BaseUrl = new Uri(BaseUrl) };
            clientOptions.MaxTimeout = timeout;
            if (proxy != null)
            {
                clientOptions.Proxy = proxy;
            }
            Client = new RestClient(clientOptions);
        }


        protected string Execute(RestRequest request)
        {
            Log.Debug("SDK Language is {0} and version is {1}", Constants.THIRD_PARTY_API_SDK_LANGUAGE, Constants.THIRD_PARTY_API_SDK_VERSION);
            Log.Debug("BaseUrl={0}", BaseUrl);
            Log.Debug("RestRequest={0}", request.ToString());
            Log.Debug("RestRequest URL\t\t={0}", request.Resource);
            Log.Debug("RestRequest Method\t\t={0}", request.Method);
            Log.Debug("Request Parameters\t\t=");
            if (Log.IsEnabled(LogEventLevel.Debug)) {
                IEnumerator ie = request.Parameters.GetEnumerator();
                while (ie.MoveNext())
                {
                    Log.Debug(JsonConvert.SerializeObject(ie.Current));
                }
            }
            
            request.AddParameter(Constants.PARAM_NAME_SYSKEY, ApiKey, ParameterType.QueryString);
            string querystr = GetQueryString(Client, request);
            string signature = Utils.GenSignature(ApiSecret, querystr);
            request.AddHeader(Constants.HEADER_NAME_SIGNATURE, signature);
            request.AddHeader(Constants.REQ_HEADER_SDK_LANG, Constants.THIRD_PARTY_API_SDK_LANGUAGE);
            request.AddHeader(Constants.REQ_HEADER_SDK_VERSION, Constants.THIRD_PARTY_API_SDK_VERSION);
            request.AddHeader(Constants.TIME_ZONE, TimeZoneHelper.GetTimeZoneId(defaultTimeZone));
            RestResponse response = Client.Execute(request);
            Log.Debug("Response StatusCode\t\t={0}", response.StatusCode);
            Log.Debug("Response Content=\n{0}", response.Content);
            IEnumerable<HeaderParameter> headers = response.Headers;
            HttpStatusCode responseStatus = response.StatusCode;
            if (HttpStatusCode.BadGateway.Equals(responseStatus)) {
                JObject result = new JObject();
                result.Add("BusinessCode", 502);
                result.Add("Message", "Bad gateway!");
                return result.ToString();
            } else if(HttpStatusCode.GatewayTimeout.Equals(responseStatus)) {
                JObject result = new JObject();
                result.Add("BusinessCode", 502);
                result.Add("Message", "Gateway timeout!");
                return result.ToString();
            } else if (HttpStatusCode.OK.Equals(responseStatus) 
                || HttpStatusCode.Created.Equals(responseStatus) 
                || HttpStatusCode.BadRequest.Equals(responseStatus)
                || HttpStatusCode.InternalServerError.Equals(responseStatus)
                || HttpStatusCode.Forbidden.Equals(responseStatus)
                || HttpStatusCode.NoContent.Equals(responseStatus)
                || "429".Equals(responseStatus.ToString()))
            {
                return HandleRateLimitHeader(headers, response.Content, response.ContentType);
            }
            else
            {
                return HandleRateLimitHeader(headers, HandleUnexceptedResponse(response), response.ContentType);
            }
        }

        private string HandleRateLimitHeader(IEnumerable<HeaderParameter> headers, string responseBody, string contentType) {
            if (string.IsNullOrWhiteSpace(responseBody)) {
                responseBody = "{}";
            }else if (!string.IsNullOrWhiteSpace(contentType) && !contentType.ToUpper().Contains("JSON")) {
                responseBody = "{'BusinessCode':999,'Message':'" + responseBody + "'}";
            }
            if (headers != null) {
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseBody);
                foreach (HeaderParameter h in headers)
                {
                    if (HEADER_X_RATE_LIMIT_LIMIT.Equals(h.Name))
                    {
                        jo.Add("rateLimit", h.Value.ToString());
                    }
                    else if (HEADER_X_RATE_LIMIT_REMAINING.Equals(h.Name))
                    {
                        jo.Add("rateLimitRemain", h.Value.ToString());
                    }
                    else if (HEADER_X_RATE_LIMIT_RESET.Equals(h.Name))
                    {
                        jo.Add("rateLimitReset", h.Value.ToString());
                    }
                }
                return jo.ToString();
            }
            return responseBody;
        }

        private string HandleUnexceptedResponse(RestResponse response)
        {
            HttpStatusCode responseStatus = response.StatusCode;
            if (HttpStatusCode.NotFound.Equals(responseStatus))
            {
                if (!string.IsNullOrEmpty(response.Content) && response.Content.Contains("businessCode"))
                {
                    return response.Content;
                }
                else
                {
                    return GenSdkRequestErrorJson(-2, GetMsgByKey("msg_16111"));
                }

            } else if (HttpStatusCode.RequestTimeout.Equals(responseStatus)) {
                return GenSdkRequestErrorJson(-3, GetMsgByKey("msg_16104"));
            } else
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
            IValidator<PageMetadata> validator = new PageMetadataValidator();
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

        protected List<string> ValidateCreate<T>(T createReq, IValidator<T> validator, string objNullMsg){
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

        protected List<string> ValidateUpdate<T>(long id, T updateReq, IValidator<T> validator, string idInvalidMsg, string objNullMsg){
            List<string> validationErrs = ValidateId(id, idInvalidMsg);
            if(updateReq== null) {
                validationErrs.Add(GetMsgByKey(objNullMsg));
                return validationErrs;
            }else {
               
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
                    Log.Warning("No value for key {0} is defined in resource file, please add.", key);
                }
            }
            catch
            {
                strValue = key;
                Log.Warning("No value for key {0} is defined in resource file, please add.", key);
            }
            return strValue;
        }

        private string GetMessage(string key)
        {
            return key;
        }

    }
}
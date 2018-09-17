using System;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using FluentValidation;
using FluentValidation.Results;
using Paxstore.OpenApi.Base.Dto;
using Paxstore.OpenApi.Validator;

namespace Paxstore.OpenApi.Base
{
    public class BaseApi
    {
        // private static ILog log = LogManager.GetLogger(typeof(BaseApi));
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        protected RestClient Client;

        public string strClutrue { get; set; }

        public BaseApi(string baseUrl, string apiKey, string apiSecret)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            Client = new RestClient(BaseUrl);
        }

        protected string Execute(RestRequest request)
        {
            if (string.IsNullOrEmpty(strClutrue))
            {
                strClutrue = "en";
            }
            CultureInfo currentClutrue = new CultureInfo(strClutrue);
            Thread.CurrentThread.CurrentCulture = currentClutrue;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(strClutrue);

            request.AddParameter(Constants.PARAM_NAME_SYSKEY, ApiKey, ParameterType.QueryString);
            string querystr = GetQueryString(Client, request);
            string signature = Utils.GenSignature(ApiSecret, querystr);
            request.AddHeader(Constants.HEADER_NAME_SIGNATURE, signature);
            IRestResponse response = Client.Execute(request);
            HttpStatusCode responseStatus = response.StatusCode;
            if ((HttpStatusCode.OK.Equals(responseStatus) || HttpStatusCode.Created.Equals(responseStatus) ||
                HttpStatusCode.NoContent.Equals(responseStatus) || HttpStatusCode.BadRequest.Equals(responseStatus) ||
                    HttpStatusCode.InternalServerError.Equals(responseStatus) || HttpStatusCode.Forbidden.Equals(responseStatus)) && !string.IsNullOrWhiteSpace(response.Content))
            {
                Console.WriteLine(response.Content);
                return response.Content;
            }
            else
            {
                return HandleUnexceptedResponse(response);
            }
        }

        private string HandleUnexceptedResponse(IRestResponse response)
        {
            HttpStatusCode responseStatus = response.StatusCode;
            if (HttpStatusCode.NotFound.Equals(responseStatus))
            {
                return GenSdkRequestErrorJson(16111, "File Not Found");
            }
            else if (HttpStatusCode.RequestTimeout.Equals(responseStatus))
            {
                return GenSdkRequestErrorJson(16104, "Connection timeout");
            }
            else
            {
                Console.WriteLine(responseStatus);
                return GenSdkRequestErrorJson(16000, string.IsNullOrWhiteSpace(response.ErrorMessage) ? "SDK request error" : response.ErrorMessage);
            }
        }

        //生成response json，直接并不是真正的response content，可能是由于网络等原因请求并未真正到达remote server，
        //这边根据responsestatus返回一个json给调用者，以便于让调用者知道请求失败的原因
        private string GenSdkRequestErrorJson(int businessCode, string message)
        {
            var result = new
            {
                businessCode = businessCode,
                message = message
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
            if (id < 0L)
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
                ResourceManager resManager = new ResourceManager("paxstore-openapi-sdk.Resources.ValidationMessages", Assembly.GetExecutingAssembly());
                strValue = resManager.GetString(key, Thread.CurrentThread.CurrentCulture);
            }
            catch
            {
                strValue = "No id:" + key + "please add";
            }
            return strValue;
        }

        private string GetMessage(string key)
        {
            return key;
        }

    }
}
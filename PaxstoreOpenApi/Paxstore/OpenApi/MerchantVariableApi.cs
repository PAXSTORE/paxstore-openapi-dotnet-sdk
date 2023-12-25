/**
 ***************************************************************************************** 
 * Copyright 2018 PAX Computer Technology(Shenzhen) CO., LTD("PAX")
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at following link.
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 ****************************************************************************************
*/


using System;

using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using System.Net;
using Paxstore.OpenApi.Help;

namespace Paxstore.OpenApi


{
    public class MerchantVariableApi : BaseApi
    {
        private const string SEARCH_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables";
        private const string CREATE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables";
        private const string UPDATE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables/{merchantVariableId}";
        private const string DELETE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables/{merchantVariableId}";
        private const string BATCH_DELETE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables/batch/deletion";
        public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }

        public Result<MerchantVariable> SearchMerchantVariable(int pageNo, int pageSize, Nullable<MerchantVariableSearchOrderBy> orderBy, long merchantId, string packageName, string key, Nullable<MerchantVariableSource> source)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<MerchantVariable>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_MERCHANT_VARIABLE_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }
            request.AddParameter("merchantId", merchantId+"");
            if (!string.IsNullOrWhiteSpace(key))
            {
                request.AddParameter("key", key);
            }
            if (!string.IsNullOrWhiteSpace(packageName))
            {
                request.AddParameter("packageName", packageName);
            }
            if (source != null)
            {
                request.AddParameter("source", ExtEnumHelper.GetEnumValue(source));
            }
            var responseContent = Execute(request);
            MerchantVariablePageResponse merchantVariablePage = JsonConvert.DeserializeObject<MerchantVariablePageResponse>(responseContent);
            Result<MerchantVariable> result = new Result<MerchantVariable>(merchantVariablePage);
            return result;
        }

        public Result<string> CreateMerchantVariable(MerchantVariableCreateRequest merchantVariableCreateRequest)
        {
            List<String> validationErrs = new List<string>();
            if (merchantVariableCreateRequest == null)
            {
                validationErrs.Add(GetMsgByKey("merchantVariableCreateRequestNull"));
            }
            else if (merchantVariableCreateRequest.VariableList == null || merchantVariableCreateRequest.VariableList.Count == 0)
            {
                validationErrs.Add(GetMsgByKey("variableListMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            for (var i = 0; i < merchantVariableCreateRequest.VariableList.Count; i++) {
                encryptPasswordVariable(merchantVariableCreateRequest.VariableList[i]);
            }
            RestRequest request = new RestRequest(CREATE_MERCHANT_VARIABLE_URL, Method.Post);
            var requestJson = JsonConvert.SerializeObject(merchantVariableCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> UpdateMerchantVariable(long merchantVariableId, MerchantVariableUpdateRequest merchantVariableUpdateRequest)
        {
            List<String> validationErrs = new List<string>();
            if (merchantVariableUpdateRequest == null)
            {
                validationErrs.Add(GetMsgByKey("merchantVariableUpdateRequestNull"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            encryptPasswordVariable(merchantVariableUpdateRequest);
            RestRequest request = new RestRequest(UPDATE_MERCHANT_VARIABLE_URL, Method.Put);
            var requestJson = JsonConvert.SerializeObject(merchantVariableUpdateRequest);
            request.AddUrlSegment("merchantVariableId", merchantVariableId);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> DeleteMerchantVariable(long merchantVariableId)
        {
            RestRequest request = new RestRequest(DELETE_MERCHANT_VARIABLE_URL, Method.Delete);
            request.AddUrlSegment("merchantVariableId", merchantVariableId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> BatchDeletionMerchantVariable(MerchantVariableDeleteRequest batchDeleteRequest)
        {
            List<String> validationErrs = new List<string>();
            if (batchDeleteRequest == null) {
                validationErrs.Add(GetMsgByKey("batchDeleteRequestNull"));
            }
            else if (batchDeleteRequest.VariableIds == null || batchDeleteRequest.VariableIds.Count == 0) {
                validationErrs.Add(GetMsgByKey("variableIdsMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(BATCH_DELETE_MERCHANT_VARIABLE_URL, Method.Delete);
            var requestJson = JsonConvert.SerializeObject(batchDeleteRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        private void encryptPasswordVariable(ParameterVariable parameterVariable)
        {
            if (string.Equals("P", parameterVariable.Type) && !string.IsNullOrWhiteSpace(parameterVariable.Value)) { 
                parameterVariable.Value = SecurityHelper.EncryptPasswordParameter(parameterVariable.Value, ApiSecret);
            }
        }
    }



    public enum MerchantVariableSearchOrderBy
    {
        [EnumValue("createdDate DESC")]
        CREATED_DESC,

        [EnumValue("createdDate ASC")]
        CREATED_ASC
    }

    public enum MerchantVariableSource
    {

        [EnumValue("M")]
        MARKET,

        [EnumValue("C")]
        MERCHANT
    }
}

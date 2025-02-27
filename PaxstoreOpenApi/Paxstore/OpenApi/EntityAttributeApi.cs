﻿using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class EntityAttributeApi: BaseApi
    {
        private const string GET_ENTITY_ATTRIBUTES_URL = "/v1/3rdsys/attributes/{attributeId}";
        private const string SEARCH_ENTITY_ATTRIBUTES_URL = "/v1/3rdsys/attributes";
        private const string CREATE_ENTITY_ATTRIBUTES_URL = "/v1/3rdsys/attributes";
        private const string UPDATE_ENTITY_ATTRIBUTES_URL = "/v1/3rdsys/attributes/{attributeId}";
        private const string UPDATE_ENTITY_ATTRIBUTES_LABEL_URL = "/v1/3rdsys/attributes/{attributeId}/label";
        private const string DELETE_ENTITY_ATTRIBUTES_URL = "/v1/3rdsys/attributes/{attributeId}";

        public EntityAttributeApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null) 
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public EntityAttributeApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public EntityAttributeApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public EntityAttributeApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }

        public Result<EntityAttribute> GetEntityAttribute(long attributeId)
        {
            IList<string> validationErrs = ValidateId(attributeId, "parameterAttributeIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<EntityAttribute>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_ENTITY_ATTRIBUTES_URL, Method.Get);
            request.AddUrlSegment("attributeId", attributeId);
            var responseContent = Execute(request);
             EntityAttributeResponse entityAttributeResponse = JsonConvert.DeserializeObject<EntityAttributeResponse>(responseContent);
            Result<EntityAttribute> result = new Result<EntityAttribute>(entityAttributeResponse);
                return result;
        }

        public Result<EntityAttribute> SearchEntityAttributes(int pageNo, int pageSize, Nullable<EntityAttributeSearchOrderBy> orderBy, string key, Nullable<EntityAttributeType> entityType)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<EntityAttribute>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_ENTITY_ATTRIBUTES_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }
            if (!string.IsNullOrEmpty(key)) {
                request.AddParameter("key", key);
            }
            if (entityType != null) {
                request.AddParameter("entityType", ExtEnumHelper.GetEnumValue(entityType));
            }
            
            var responseContent = Execute(request);
            EntityAttributePageResponse entityAttributePageResponse = JsonConvert.DeserializeObject<EntityAttributePageResponse>(responseContent);
            Result<EntityAttribute> result = new Result<EntityAttribute>(entityAttributePageResponse);
            return result;
        }

        public Result<EntityAttribute> CreateEntityAttribute(EntityAttributeCreateRequest entityAttributeCreateRequest)
        {
            List<string> validationErrs = new List<string>();
            if (entityAttributeCreateRequest == null) {
                validationErrs.Add(GetMsgByKey("parameterEntityAttributeCreateRequestNull"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<EntityAttribute>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_ENTITY_ATTRIBUTES_URL, Method.Post);
            var requestJson = JsonConvert.SerializeObject(entityAttributeCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContent = Execute(request);
            EntityAttributeResponse entityAttributeResponse = JsonConvert.DeserializeObject<EntityAttributeResponse>(responseContent);
            Result<EntityAttribute> result = new Result<EntityAttribute>(entityAttributeResponse);
            return result;
        }

        public Result<EntityAttribute> UpdateEntityAttribute(long attributeId, EntityAttributeUpdateRequest entityAttributeUpdateRequest)
        {
            List<string> validationErrs = new List<string>();
            if (entityAttributeUpdateRequest == null) {
                validationErrs.Add(GetMsgByKey("parameterEntityAttributeUpdateRequestNull"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<EntityAttribute>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_ENTITY_ATTRIBUTES_URL, Method.Put);
            var requestJson = JsonConvert.SerializeObject(entityAttributeUpdateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            request.AddUrlSegment("attributeId", attributeId.ToString());
            string responseContent = Execute(request);
            EntityAttributeResponse entityAttributeResponse = JsonConvert.DeserializeObject<EntityAttributeResponse>(responseContent);
            Result<EntityAttribute> result = new Result<EntityAttribute>(entityAttributeResponse);
            return result;
        }

        public Result<string> UpdateEntityAttributeLabel(long attributeId, EntityAttributeLabelUpdateRequest updateLabelRequest)
        {
            if (updateLabelRequest == null) {
                List<string> validationErrs = new List<string>();
                validationErrs.Add(GetMsgByKey("parameterUpdateLabelRequestNull"));
            }
            RestRequest request = new RestRequest(UPDATE_ENTITY_ATTRIBUTES_LABEL_URL, Method.Put);
            request.AddUrlSegment("attributeId", attributeId.ToString());
            var requestJson = JsonConvert.SerializeObject(updateLabelRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> DeleteEntityAttribute(long attributeId){
            RestRequest request = new RestRequest(DELETE_ENTITY_ATTRIBUTES_URL, Method.Delete);
            request.AddUrlSegment("attributeId", attributeId.ToString());
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        
    }

    public enum EntityAttributeSearchOrderBy
    {
        [EnumValue("a.entity_type DESC")]
        EntityType_desc,
        [EnumValue("a.entity_type ASC")]
        EntityType_asc
    }

    public enum EntityAttributeType {

        [EnumValue("Merchant")]
        Merchant,
        [EnumValue("Reseller")]
        Reseller
    }

    public enum EntityAttributeInputType
    {

        [EnumValue("SELECTOR")]
        Selector,
        [EnumValue("TEXT")]
        Text
    }
}

using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.MerchantVariable;
using Paxstore.OpenApi.Validator.MerchantVariable;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class MerchantVariableApi : BaseApi
    {
        public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        private const string SEARCH_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables";
        private const string CREATE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables";
        private const string UPDATE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables/{merchantVariableId}";
        private const string DELETE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables/{merchantVariableId}";
        private const string BATCH_DELETE_MERCHANT_VARIABLE_URL = "/v1/3rdsys/merchant/variables/batch/deletion";

        public Result<MerchantVariable> SearchMerchantVariable(int pageNo, int pageSize, Nullable<MerchantVariableSearchOrderBy> orderBy,
            long merchantId, string packageName, string key, Nullable<MerchantVariableSource> source)
        {

            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<MerchantVariable>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_MERCHANT_VARIABLE_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null)
            {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }
            if (source != null)
            {
                request.AddParameter("source", ExtEnumHelper.GetEnumValue(source));
            }
            request.AddParameter("merchantId", merchantId);
            if (!string.IsNullOrEmpty(packageName))
            {
                request.AddParameter("packageName", packageName);
            }
            if (!string.IsNullOrEmpty(key))
            {
                request.AddParameter("key", key);
            }
            
            var responseContent = Execute(request);
            MerchantVariablePageResponse resellerPage = JsonConvert.DeserializeObject<MerchantVariablePageResponse>(responseContent);
            Result<MerchantVariable> result = new Result<MerchantVariable>(resellerPage);//todo
            return result;
        }

        public Result<string> CreateMerchantVariable(MerchantVariableCreateRequest merchantVariableCreateRequest)
        {

            List<string> validationErrs = ValidateCreate(merchantVariableCreateRequest, new MerchantVariableCreateValidator(), "merchantVariableCreateRequestIsNull");
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            for (int i = 0; i < merchantVariableCreateRequest.VariableList.Count(); i++)
            {
                encryptPasswordVariable(merchantVariableCreateRequest.VariableList[i]);
            }
            RestRequest request = new RestRequest(CREATE_MERCHANT_VARIABLE_URL, Method.POST);
            var requestJson = JsonConvert.SerializeObject(merchantVariableCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> UpdateMerchantVariable(long merchantVariableId, MerchantVariableUpdateRequest merchantVariableUpdateRequest)
        {
            List<string> validationErrs = ValidateUpdate(merchantVariableId, merchantVariableUpdateRequest, null, "merchantVariableIdInvalid", "merchantVariableUpdateRequestNull");
            if (validationErrs.Count() > 0)
            {
                return new Result<string>(validationErrs);
            }
            encryptPasswordVariable(merchantVariableUpdateRequest);
            RestRequest request = new RestRequest(UPDATE_MERCHANT_VARIABLE_URL, Method.PUT);
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
            RestRequest request = new RestRequest(DELETE_MERCHANT_VARIABLE_URL, Method.DELETE);
            request.AddUrlSegment("merchantVariableId", merchantVariableId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            return new Result<string>(emptyResponse);
        }

        public Result<string> BatchDeleteMerchantVariable(MerchantVariableDeleteRequest batchDeleteMerchantVariableRequest)
        {
            List<string> validationErrs = new List<string>();
            if (batchDeleteMerchantVariableRequest == null)
            {
                validationErrs.Add(GetMsgByKey("batchDeleteMerchantVariableRequestNull"));
                return new Result<string>(validationErrs);
            }
            else if (batchDeleteMerchantVariableRequest.VariableIds == null || batchDeleteMerchantVariableRequest.VariableIds.Count() == 0)
            {
                validationErrs.Add(GetMsgByKey("variableIdsIsMandatory"));
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(BATCH_DELETE_MERCHANT_VARIABLE_URL, Method.DELETE);
            var requestJson = JsonConvert.SerializeObject(batchDeleteMerchantVariableRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        private void encryptPasswordVariable(ParameterVariable parameterVariable)
        {
            if ("P".Equals(parameterVariable.Type) && !string.IsNullOrEmpty(parameterVariable.Value))
            {
                parameterVariable.Value = SecurityHelper.EncryptPasswordParameter(parameterVariable.Value, ApiSecret);
            }
        }
    }

    public enum MerchantVariableSearchOrderBy
    {
        [EnumValue("createdDate DESC")]
        CREATE_DATE_DESC,
        [EnumValue("createdDate ASC")]
        CREATE_DATE_ASC
    }

    public enum MerchantVariableSource
    {
        [EnumValue("M")]
        MARKET,
        [EnumValue("C")]
        MERCHANT
    }
}

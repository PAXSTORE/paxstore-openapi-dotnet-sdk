using System.Collections.Generic;
using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.MerchantCategory;
using RestSharp;

namespace Paxstore.OpenApi
{
    public class MerchantCategoryApi: BaseApi
    {
        private const string GET_CATEGORIES_URL = "/v1/3rdsys/merchantCategories";
	    private const string CREATE_CATEGORY_URL = "/v1/3rdsys/merchantCategories";
	    private const string UPDATE_CATEGORY_URL = "/v1/3rdsys/merchantCategories/{merchantCategoryId}";
	    private const string DELETE_CATEGORY_URL = "/v1/3rdsys/merchantCategories/{merchantCategoryId}";
	    private const string BATCH_CREATE_CATEGORY_URL = "/v1/3rdsys/merchantCategories/batch";
	    private const int MAX_LENGTH_CATEGORY_NAME = 128;
	    private const int MAX_LENGTH_CATEGORY_REMARKS = 255;
        public MerchantCategoryApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<List<MerchantCategory>> GetMerchantCategories(string name) {
            RestRequest request = new RestRequest(GET_CATEGORIES_URL, Method.GET);
            request.AddParameter("name", name);
            var responseContent = Execute(request);
            MerchantCategoryListResponse categoryList = JsonConvert.DeserializeObject<MerchantCategoryListResponse>(responseContent);
            Result<List<MerchantCategory>> result = new Result<List<MerchantCategory>>(categoryList);
            return result;
        }

        public Result<MerchantCategory> CreateMerchantCategory(MerchantCategoryCreateRequest merchantCategoryCreateRequest){
            List<string> validationErrs = ValidateCreate(merchantCategoryCreateRequest, new MerchantCategoryCreateValidator(),"merchantCategoryCreateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<MerchantCategory>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_CATEGORY_URL, Method.POST);
            var merchantCategoryJson = JsonConvert.SerializeObject(merchantCategoryCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, merchantCategoryJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            MerchantCategoryResponse merchantCategoryResponse = JsonConvert.DeserializeObject<MerchantCategoryResponse>(responseContent);
            Result<MerchantCategory> result = new Result<MerchantCategory>(merchantCategoryResponse);
            return result;
        }
        
        public Result<MerchantCategory> UpdateMerchantCategory(long merchantCategoryId, MerchantCategoryUpdateRequest merchantCategoryUpdateRequest){
            IList<string> validationErrs = ValidateUpdate(merchantCategoryId, merchantCategoryUpdateRequest, new MerchantCategoryUpdateValidator(), "merchantCategoryIdInvalid", "merchantCategoryUpdateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<MerchantCategory>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_CATEGORY_URL, Method.PUT);
            var merchantCategoryJson = JsonConvert.SerializeObject(merchantCategoryUpdateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, merchantCategoryJson, ParameterType.RequestBody);
            request.AddUrlSegment("merchantCategoryId",merchantCategoryId);
            var responseContent = Execute(request);
            MerchantCategoryResponse merchantCategoryResponse = JsonConvert.DeserializeObject<MerchantCategoryResponse>(responseContent);
            Result<MerchantCategory> result = new Result<MerchantCategory>(merchantCategoryResponse);
            return result;
        }
        
        public Result<string> DeleteMerchantCategory(long merchantCategoryId) {
            IList<string> validationErrs = ValidateId(merchantCategoryId, "parameterMerchantCategoryIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DELETE_CATEGORY_URL, Method.DELETE);
            request.AddUrlSegment("merchantCategoryId",merchantCategoryId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
        
        public Result<List<MerchantCategory>> BatchCreateMerchantCategory(List<MerchantCategoryCreateRequest> merchantCategoryBatchCreateRequest, bool skipExist){
            List<string> validationErrs = ValidateBatchCreate(merchantCategoryBatchCreateRequest);
            if(validationErrs.Count>0) {
                return new Result<List<MerchantCategory>>(validationErrs);
            }
            RestRequest request = new RestRequest(BATCH_CREATE_CATEGORY_URL, Method.POST);
            request.AddParameter("skipExist",skipExist, ParameterType.QueryString);

            var merchantCategoryJson = JsonConvert.SerializeObject(merchantCategoryBatchCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, merchantCategoryJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            MerchantCategoryListResponse categoryList = JsonConvert.DeserializeObject<MerchantCategoryListResponse>(responseContent);
            Result<List<MerchantCategory>> result = new Result<List<MerchantCategory>>(categoryList);
            return result;
        }
        
        private List<string> ValidateBatchCreate(List<MerchantCategoryCreateRequest> merchantCategoryBatchCreateRequest){
            List<string> validationErrs = new List<string>();
            if(merchantCategoryBatchCreateRequest == null || merchantCategoryBatchCreateRequest.Count == 0) {
                validationErrs.Add(GetMsgByKey("merchantCategoryBatchCreateRequestInvalid"));
            }else {
                for(int i=0;i<merchantCategoryBatchCreateRequest.Count;i++) {
                    MerchantCategoryCreateRequest category = merchantCategoryBatchCreateRequest[i];
                    if(string.IsNullOrEmpty(category.Name)){
                        validationErrs.Add(GetMsgByKey("merchantCategoryNameEmpty"));
                        break;
                    }
                }
                
                for(int i=0;i<merchantCategoryBatchCreateRequest.Count;i++) {
                    MerchantCategoryCreateRequest category = merchantCategoryBatchCreateRequest[i];
                    if(category.Name!=null && category.Name.Length>MAX_LENGTH_CATEGORY_NAME) {
                        validationErrs.Add(GetMsgByKey("merchanteCategoryNameTooLong").Replace("\\[NAME\\]", category.Name));
                    }
                }
                
                for(int i=0;i<merchantCategoryBatchCreateRequest.Count;i++) {
                    MerchantCategoryCreateRequest category = merchantCategoryBatchCreateRequest[i];
                    if(category.Remarks!=null && category.Remarks.Length>MAX_LENGTH_CATEGORY_REMARKS) {
                        validationErrs.Add(GetMsgByKey("merchanteCategoryRemarksTooLong").Replace("\\[REMARKS\\]", category.Remarks));
                    }
                }
            }
            return validationErrs;
        }
    }
}
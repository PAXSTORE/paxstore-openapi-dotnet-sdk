using System.Collections.Generic;
using Com.Pax.OpenApi.Sdk.Base;
using Com.Pax.OpenApi.Sdk.Base.Dto;
using Com.Pax.OpenApi.Sdk.Dto.MerchantCategory;
using Com.Pax.OpenApi.Sdk.Validator.MerchantCategory;
using Newtonsoft.Json;
using RestSharp;

namespace Com.Pax.OpenApi.Sdk.Api
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

        public Result<List<MerchantCategoryDTO>> GetMerchantCategories(string name) {
            RestRequest request = new RestRequest(GET_CATEGORIES_URL, Method.GET);
            request.AddParameter("name", name);
             var responseContent = Execute(request);
            MerchantCategoryListResponseDTO categoryList = JsonConvert.DeserializeObject<MerchantCategoryListResponseDTO>(responseContent);
            Result<List<MerchantCategoryDTO>> result = new Result<List<MerchantCategoryDTO>>(categoryList);
            return result;
        }

        public Result<MerchantCategoryDTO> CreateMerchantCategory(MerchantCategoryCreateRequest merchantCategoryCreateRequest){
            
            List<string> validationErrs = ValidateCreate(merchantCategoryCreateRequest, new MerchantCategoryCreateValidator(),"merchantCategoryCreateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<MerchantCategoryDTO>(validationErrs);
            }

           
            RestRequest request = new RestRequest(CREATE_CATEGORY_URL, Method.POST);

            var merchantCategoryJson = request.JsonSerializer.Serialize(merchantCategoryCreateRequest);
            request.AddParameter("application/json; charset=utf-8", merchantCategoryJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            MerchantCategoryResponseDTO merchantCategoryResponseDTO = JsonConvert.DeserializeObject<MerchantCategoryResponseDTO>(responseContent);
            Result<MerchantCategoryDTO> result = new Result<MerchantCategoryDTO>(merchantCategoryResponseDTO);
            return result;
        }
        
        public Result<MerchantCategoryDTO> UpdateMerchantCategory(long merchantCategoryId, MerchantCategoryUpdateRequest merchantCategoryUpdateRequest){
            IList<string> validationErrs = ValidateUpdate(merchantCategoryId, merchantCategoryUpdateRequest, new MerchantCategoryUpdateValidator(), "merchantCategoryIdInvalid", "merchantCategoryUpdateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<MerchantCategoryDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_CATEGORY_URL, Method.PUT);
            var merchantCategoryJson = request.JsonSerializer.Serialize(merchantCategoryUpdateRequest);
            request.AddParameter("application/json; charset=utf-8", merchantCategoryJson, ParameterType.RequestBody);
            request.AddUrlSegment("merchantCategoryId",merchantCategoryId);
            var responseContent = Execute(request);
            MerchantCategoryResponseDTO merchantCategoryResponseDTO = JsonConvert.DeserializeObject<MerchantCategoryResponseDTO>(responseContent);
            Result<MerchantCategoryDTO> result = new Result<MerchantCategoryDTO>(merchantCategoryResponseDTO);
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
        
        public Result<List<MerchantCategoryDTO>> BatchCreateMerchantCategory(List<MerchantCategoryCreateRequest> merchantCategoryBatchCreateRequest, bool skipExist){
            List<string> validationErrs = ValidateBatchCreate(merchantCategoryBatchCreateRequest);
            if(validationErrs.Count>0) {
                return new Result<List<MerchantCategoryDTO>>(validationErrs);
            }
            RestRequest request = new RestRequest(BATCH_CREATE_CATEGORY_URL, Method.POST);
            request.AddParameter("skipExist",skipExist, ParameterType.QueryString);
            var responseContent = Execute(request);
            MerchantCategoryListResponseDTO categoryList = JsonConvert.DeserializeObject<MerchantCategoryListResponseDTO>(responseContent);
            Result<List<MerchantCategoryDTO>> result = new Result<List<MerchantCategoryDTO>>(categoryList);
            return result;
        }
        
        private List<string> ValidateBatchCreate(List<MerchantCategoryCreateRequest> merchantCategoryBatchCreateRequest){
            List<string> validationErrs = new List<string>();
            if(merchantCategoryBatchCreateRequest == null || merchantCategoryBatchCreateRequest.Count == 0) {
                validationErrs.Add(GetMsgByKey("parameter.merchantCategoryBatchCreateRequest.invalid"));
            }else {
                for(int i=0;i<merchantCategoryBatchCreateRequest.Count;i++) {
                    MerchantCategoryCreateRequest category = merchantCategoryBatchCreateRequest[i];
                    if(category.Name == null || "".Equals(category.Name.Trim())){
                        validationErrs.Add(GetMsgByKey("merchantCategory.name.null"));
                        break;
                    }
                }
                
                for(int i=0;i<merchantCategoryBatchCreateRequest.Count;i++) {
                    MerchantCategoryCreateRequest category = merchantCategoryBatchCreateRequest[i];
                    if(category.Name!=null && category.Name.Length>MAX_LENGTH_CATEGORY_NAME) {
                        validationErrs.Add(GetMsgByKey("merchanteCategory.name.too.long").Replace("\\[NAME\\]", category.Name));
                    }
                }
                
                for(int i=0;i<merchantCategoryBatchCreateRequest.Count;i++) {
                    MerchantCategoryCreateRequest category = merchantCategoryBatchCreateRequest[i];
                    if(category.Remarks!=null && category.Remarks.Length>MAX_LENGTH_CATEGORY_REMARKS) {
                        validationErrs.Add(GetMsgByKey("merchanteCategory.remarks.too.long").Replace("\\[REMARKS\\]", category.Remarks));
                    }
                }
            }
            return validationErrs;
        }
    }
}
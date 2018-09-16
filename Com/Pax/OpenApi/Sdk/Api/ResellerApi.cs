using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using Com.Pax.OpenApi.Sdk.Base;
using Com.Pax.OpenApi.Sdk.Base.Dto;
using Com.Pax.OpenApi.Sdk.Dto.Reseller;
using Com.Pax.OpenApi.Sdk.Validator.Reseller;
using FluentValidation;
using FluentValidation.Results;

namespace Com.Pax.OpenApi.Sdk.Api
{
    public class ResellerApi : BaseApi
    {

        private const string SEARCH_RESELLER_URL = "/v1/3rdsys/resellers";
        private const string GET_RESELLER_URL = "/v1/3rdsys/resellers/{resellerId}";
        private const string CREATE_RESELLER_URL = "/v1/3rdsys/resellers";
        private const string UPDATE_RESELLER_URL = "/v1/3rdsys/resellers/{resellerId}";
        private const string ACTIVATE_RESELLER_URL = "/v1/3rdsys/resellers/{resellerId}/active";
        private const string DISABLE_RESELLER_URL = "/v1/3rdsys/resellers/{resellerId}/disable";
        private const string DELETE_RESELLER_URL = "/v1/3rdsys/resellers/{resellerId}";

        public ResellerApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<ResellerPageDTO> SearchReseller(int pageNo, int pageSize, ResellerSearchOrderBy orderBy, string name, ResellerStatus status)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<ResellerPageDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_RESELLER_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            request.AddParameter("name", name);
            request.AddParameter("status", GetStatusValue(status));
            var responseContent = Execute(request);
            ResellerPageResponse resellerPageDTO = JsonConvert.DeserializeObject<ResellerPageResponse>(responseContent);
            Result<ResellerPageDTO> result = new Result<ResellerPageDTO>(resellerPageDTO);
            return result;
        }

        public Result<ResellerDTO> GetReseller(long resellerId)
        {
            IList<string> validationErrs = ValidateId(resellerId, "resellerIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<ResellerDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_RESELLER_URL, Method.GET);
            request.AddUrlSegment("resellerId", resellerId.ToString());
            var responseContent = Execute(request);
            ResellerResponse resellerResponseDTO = JsonConvert.DeserializeObject<ResellerResponse>(responseContent);
            Result<ResellerDTO> result = new Result<ResellerDTO>(resellerResponseDTO);
            return result;
        }

        public Result<ResellerDTO> CreateReseller(ResellerCreateRequest resellerCreateRequest)
        {
            List<string> validationErrs = ValidateCreate(resellerCreateRequest, new ResellerCreateValidator(),"resellerCreateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<ResellerDTO>(validationErrs);
            }

           
            RestRequest request = new RestRequest(CREATE_RESELLER_URL, Method.POST);

            var resellerJson = request.JsonSerializer.Serialize(resellerCreateRequest);
            request.AddParameter("application/json; charset=utf-8", resellerJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            ResellerResponse resellerResponseDTO = JsonConvert.DeserializeObject<ResellerResponse>(responseContent);
            Result<ResellerDTO> result = new Result<ResellerDTO>(resellerResponseDTO);
            return result;
        }

        

        public Result<ResellerDTO> UpdateReseller(long resellerId, ResellerUpdateRequest resellerUpdateRequest){
            IList<string> validationErrs = ValidateUpdate(resellerId, resellerUpdateRequest, new ResellerUpdateValidator(), "resellerIdInvalid", "resellerUpdateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<ResellerDTO>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_RESELLER_URL, Method.PUT);
            var resellerJson = request.JsonSerializer.Serialize(resellerUpdateRequest);
            request.AddParameter("application/json; charset=utf-8", resellerJson, ParameterType.RequestBody);
            request.AddUrlSegment("resellerId",resellerId);
            var responseContent = Execute(request);
            ResellerResponse resellerResponseDTO = JsonConvert.DeserializeObject<ResellerResponse>(responseContent);
            Result<ResellerDTO> result = new Result<ResellerDTO>(resellerResponseDTO);
            return result;
	    }

        public Result<string> ActivateReseller(long resellerId){
            IList<string> validationErrs = ValidateId(resellerId, "resellerIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(ACTIVATE_RESELLER_URL, Method.PUT);
            request.AddUrlSegment("resellerId",resellerId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<String> result = new Result<String>(emptyResponse);
            return result;
        }

        public Result<string> DisableReseller(long resellerId){
            IList<string> validationErrs = ValidateId(resellerId, "resellerIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DISABLE_RESELLER_URL, Method.PUT);
            request.AddUrlSegment("resellerId",resellerId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<String> result = new Result<String>(emptyResponse);
            return result;
        }

        public Result<string> DeleteReseller(long resellerId){
            IList<string> validationErrs = ValidateId(resellerId, "resellerIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DISABLE_RESELLER_URL, Method.DELETE);
            request.AddUrlSegment("resellerId",resellerId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<String> result = new Result<String>(emptyResponse);
            return result;
        }



        string GetStatusValue(ResellerStatus status)
        {
            switch (status)
            {
                case ResellerStatus.Active:
                    return "A";
                case ResellerStatus.Inactive:
                    return "P";
                case ResellerStatus.Suspend:
                    return "S";
                case ResellerStatus.All:
                    return null;
            }
            return null;
        }

        string GetOrderValue(ResellerSearchOrderBy order)
        {
            switch (order)
            {
                case ResellerSearchOrderBy.Name:
                    return "name";
                case ResellerSearchOrderBy.Contact:
                    return "contact";
                case ResellerSearchOrderBy.Phone:
                    return "phone";
            }
            return "name";
        }

    }



public enum ResellerSearchOrderBy
{
    Name,
    Contact,
    Phone
}

public enum ResellerStatus
{
    All,
    Active,
    Inactive,
    Suspend
}
    
}
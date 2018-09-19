using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Validator.Reseller;
using log4net;

namespace Paxstore.OpenApi
{
    public class ResellerApi : BaseApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(ResellerApi));

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

        public Result<PagedReseller> SearchReseller(int pageNo, int pageSize, ResellerSearchOrderBy orderBy, string name, ResellerStatus status){
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<PagedReseller>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_RESELLER_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            request.AddParameter("name", name);
            request.AddParameter("status", GetStatusValue(status));
            var responseContent = Execute(request);
            ResellerPageResponse resellerPage = JsonConvert.DeserializeObject<ResellerPageResponse>(responseContent);
            Result<PagedReseller> result = new Result<PagedReseller>(resellerPage);
            return result;
        }

        public Result<Reseller> GetReseller(long resellerId)
        {
            IList<string> validationErrs = ValidateId(resellerId, "resellerIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<Reseller>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_RESELLER_URL, Method.GET);
            request.AddUrlSegment("resellerId", resellerId.ToString());
            var responseContent = Execute(request);
            ResellerResponse resellerResponse = JsonConvert.DeserializeObject<ResellerResponse>(responseContent);
            Result<Reseller> result = new Result<Reseller>(resellerResponse);
            return result;
        }

        public Result<Reseller> CreateReseller(ResellerCreateRequest resellerCreateRequest)
        {
            List<string> validationErrs = ValidateCreate(resellerCreateRequest, new ResellerCreateValidator(),"resellerCreateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<Reseller>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_RESELLER_URL, Method.POST);
            var resellerJson = JsonConvert.SerializeObject(resellerCreateRequest);

            request.AddParameter(Constants.CONTENT_TYPE_JSON, resellerJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            ResellerResponse resellerResponse = JsonConvert.DeserializeObject<ResellerResponse>(responseContent);
            Result<Reseller> result = new Result<Reseller>(resellerResponse);
            return result;
        }

        

        public Result<Reseller> UpdateReseller(long resellerId, ResellerUpdateRequest resellerUpdateRequest){
            IList<string> validationErrs = ValidateUpdate(resellerId, resellerUpdateRequest, new ResellerUpdateValidator(), "resellerIdInvalid", "resellerUpdateRequestIsNull");
            if(validationErrs.Count>0){
                return new Result<Reseller>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_RESELLER_URL, Method.PUT);
            var resellerJson = JsonConvert.SerializeObject(resellerUpdateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, resellerJson, ParameterType.RequestBody);
            request.AddUrlSegment("resellerId",resellerId);
            var responseContent = Execute(request);
            ResellerResponse resellerResponse = JsonConvert.DeserializeObject<ResellerResponse>(responseContent);
            Result<Reseller> result = new Result<Reseller>(resellerResponse);
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
            Result<string> result = new Result<string>(emptyResponse);
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
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> DeleteReseller(long resellerId){
            IList<string> validationErrs = ValidateId(resellerId, "resellerIdInvalid");
            if (validationErrs.Count > 0){
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DELETE_RESELLER_URL, Method.DELETE);
            request.AddUrlSegment("resellerId",resellerId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
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
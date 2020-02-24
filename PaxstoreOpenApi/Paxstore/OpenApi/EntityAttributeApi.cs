using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public EntityAttributeApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<EntityAttribute> GetEntityAttribute(long attributeId)
        {
            IList<string> validationErrs = ValidateId(attributeId, "parameterAttributeIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<EntityAttribute>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_ENTITY_ATTRIBUTES_URL, Method.GET);
            request.AddUrlSegment("attributeId", attributeId);
            var responseContent = Execute(request);
             EntityAttributeResponse entityAttributeResponse = JsonConvert.DeserializeObject<EntityAttributeResponse>(responseContent);
            Result<EntityAttribute> result = new Result<EntityAttribute>(entityAttributeResponse);
                return result;
        }

        public Result<EntityAttribute> SearchEntityAttributes(int pageNo, int pageSize, SearchOrderBy orderBy, string key, string entityType)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<EntityAttribute>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_ENTITY_ATTRIBUTES_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            request.AddParameter("key", key);
            request.AddParameter("entityType",entityType);
            var responseContent = Execute(request);
            EntityAttributePageResponse entityAttributePageResponse = JsonConvert.DeserializeObject<EntityAttributePageResponse>(responseContent);
            Result<EntityAttribute> result = new Result<EntityAttribute>(entityAttributePageResponse);
            return result;
        }



    public enum SearchOrderBy
        {
            [EnumValue("a.entity_type DESC")]
            EntityType_desc,
            [EnumValue("a.entity_type ASC")]
            EntityType_asc
        }
    }
}

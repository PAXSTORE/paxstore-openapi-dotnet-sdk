using Paxstore.OpenApi.Base;
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
    }
}

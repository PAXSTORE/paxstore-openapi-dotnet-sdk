using Newtonsoft.Json;
using Paxstore.OpenAp.Model;
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
    public class TerminalVariableApi:BaseApi
    {
        private const string GET_TERMINAL_VARIABLE_URL = "/v1/3rdsys/terminalVariables";
        private const string CREATE_TERMINAL_VARIABLE_URL = "/v1/3rdsys/terminalVariables";
        private const string UPDATE_TERMINAL_VARIABLE_URL = "/v1/3rdsys/terminalVariables/{terminalVariableId}";
        private const string DELETE_TERMINAL_VARIABLE_URL = "/v1/3rdsys/terminalVariables/{terminalVariableId}";
        private const string BATCH_DELETION_TERMINAL_VARIABLE_URL = "/v1/3rdsys/terminalVariables/batch/deletion";

        public TerminalVariableApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public TerminalVariableApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public TerminalVariableApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public TerminalVariableApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }

        public Result<TerminalParameterVariable> GetTerminalVariable(int pageNo, int pageSize, Nullable<VariableSearchOrderBy> orderBy, string tid, string serialNo, string packageName, string key, Nullable<VariableSource> source)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrEmpty(tid) && string.IsNullOrEmpty(serialNo)) {
                validationErrs.Add(GetMsgByKey("tidAndSnIsMandatory"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<TerminalParameterVariable>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_TERMINAL_VARIABLE_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            if (orderBy != null) {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }
            if (!string.IsNullOrEmpty(tid)) {
                request.AddParameter("tid", tid);
            }
            if (!string.IsNullOrEmpty(serialNo)) {
                request.AddParameter("serialNo", serialNo);
            }
            if (!string.IsNullOrEmpty(packageName)) {
                request.AddParameter("packageName", packageName);
            }
            if (!string.IsNullOrEmpty(key)) {
                request.AddParameter("key", key);
            }
            if (source != null) {
                request.AddParameter("source", ExtEnumHelper.GetEnumValue(source));
            }
            
            
            var responseContent = Execute(request);
            ParameterVariablePageResponse parameterVariablePageResponse = JsonConvert.DeserializeObject<ParameterVariablePageResponse>(responseContent);
            Result<TerminalParameterVariable> result = new Result<TerminalParameterVariable>(parameterVariablePageResponse);
            return result;
        }

        public Result<string> CreateTerminalVariable(TerminalParameterVariableCreateRequest terminalParameterVariableCreateRequest)
        {
            IList<string> validationErrs = new List<string>();
            if (terminalParameterVariableCreateRequest == null) {
                
                validationErrs.Add(GetMsgByKey("parameterTerminalParameterVariableCreateRequestMandatory"));
                return new Result<string>(validationErrs);
            }
            if (string.IsNullOrEmpty(terminalParameterVariableCreateRequest.TID) && string.IsNullOrEmpty(terminalParameterVariableCreateRequest.SerialNo)) {
                validationErrs.Add(GetMsgByKey("tidAndSnIsMandatory"));
            }
            if (terminalParameterVariableCreateRequest.VariableList == null || terminalParameterVariableCreateRequest.VariableList.Count == 0) {
                validationErrs.Add(GetMsgByKey("variableListMandatory"));
            }
            if (validationErrs.Count > 0) {
                return new Result<string>(validationErrs);
            }
            for (int i = 0; i < terminalParameterVariableCreateRequest.VariableList.Count; i++) {
                ParameterVariable temp = terminalParameterVariableCreateRequest.VariableList[i];
                if ("P".Equals(temp.Type) && !string.IsNullOrEmpty(temp.Value)) {
                    temp.Value = SecurityHelper.EncryptPasswordParameter(temp.Value, ApiSecret);
                }
            }
            RestRequest request = new RestRequest(CREATE_TERMINAL_VARIABLE_URL, Method.Post);
            var requestJson = JsonConvert.SerializeObject(terminalParameterVariableCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse temptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<String> result = new Result<String>(temptyResponse);
            return result;
        }

        public Result<string> UpdateTerminalVariable(long terminalVariableId, TerminalVariableUpdateRequest terminalVariableUpdateRequest)
        {
            IList<string> validationErrs = ValidateId(terminalVariableId, "parameterTerminalVariableIdInvalid");
            if (terminalVariableUpdateRequest == null)
            {
                validationErrs.Add(GetMsgByKey("parameterTerminalVariableUpdateRequestMandatory"));
                return new Result<string>(validationErrs);
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            if ("P".Equals(terminalVariableUpdateRequest.Type) && !string.IsNullOrEmpty(terminalVariableUpdateRequest.Value)) {
                terminalVariableUpdateRequest.Value = SecurityHelper.EncryptPasswordParameter(terminalVariableUpdateRequest.Value, ApiSecret);
            }
            RestRequest request = new RestRequest(UPDATE_TERMINAL_VARIABLE_URL, Method.Put);
            var requestJson = JsonConvert.SerializeObject(terminalVariableUpdateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            request.AddUrlSegment("terminalVariableId",terminalVariableId.ToString());
            var responseContent = Execute(request);
            EmptyResponse temptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<String> result = new Result<String>(temptyResponse);
            return result;
        }

        public Result<string> DeleteTerminalVariable(long terminalVariableId)
        {
            IList<string> validationErrs = ValidateId(terminalVariableId, "terminalVariableIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DELETE_TERMINAL_VARIABLE_URL, Method.Delete);
            request.AddUrlSegment("terminalVariableId", terminalVariableId.ToString());
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> BatchDeletionTerminalVariable(TerminalParameterVariableDeleteRequest batchDeletionRequest)
        {
            IList<string> validationErrs = new List<string>();
            if (batchDeletionRequest == null)
            {
                validationErrs.Add(GetMsgByKey("parameterBatchDeletionRequestMandatory"));
            }else {
                if (batchDeletionRequest.VariableIds == null || batchDeletionRequest.VariableIds.Count == 0) {
                    validationErrs.Add(GetMsgByKey("variableIdsMandatory"));
                }
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(BATCH_DELETION_TERMINAL_VARIABLE_URL, Method.Post);
            var requestJson = JsonConvert.SerializeObject(batchDeletionRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

    }

    public enum VariableSearchOrderBy
    {
        [EnumValue("createdDate DESC")]
        Variable_desc,

        [EnumValue("createdDate ASC")]
        Variable_asc

    }

    public enum VariableSource
    {
        [EnumValue("T")]
        TERMINAL,

        [EnumValue("M")]
        MARKET,

        [EnumValue("C")]
        MERCHANT
    }
}

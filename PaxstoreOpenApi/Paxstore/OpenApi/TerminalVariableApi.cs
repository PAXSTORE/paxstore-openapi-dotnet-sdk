using Newtonsoft.Json;
using Paxstore.OpenAp.Model;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TerminalVariableApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<TerminalParameterVariable> GetTerminalVariable(string tid, string serialNo, string packageName, string key, string source)
        {
            RestRequest request = new RestRequest(GET_TERMINAL_VARIABLE_URL, Method.GET);
            request.AddParameter("tid", tid);
            request.AddParameter("serialNo", serialNo);
            request.AddParameter("packageName", packageName);
            request.AddParameter("key", key);
            request.AddParameter("source", source);
            var responseContent = Execute(request);
            ParameterVariablePageResponse parameterVariablePageResponse = JsonConvert.DeserializeObject<ParameterVariablePageResponse>(responseContent);
            Result<TerminalParameterVariable> result = new Result<TerminalParameterVariable>(parameterVariablePageResponse);
            return result;
        }

       public Result<String> CreateTerminalVariable(TerminalParameterVariableCreateRequest createRequest)
        {
            RestRequest request = new RestRequest(CREATE_TERMINAL_VARIABLE_URL, Method.POST);
            var requestJson = JsonConvert.SerializeObject(createRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse temptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<String> result = new Result<String>(temptyResponse);
            return result;
        }

        public Result<string> UpdateTerminalVariable(long terminalVariableId, TerminalVariableUpdateRequest updateRequest)
        {
            RestRequest request = new RestRequest(UPDATE_TERMINAL_VARIABLE_URL, Method.PUT);
            var requestJson = JsonConvert.SerializeObject(updateRequest);
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

            RestRequest request = new RestRequest(DELETE_TERMINAL_VARIABLE_URL, Method.DELETE);
            request.AddUrlSegment("terminalVariableId", terminalVariableId.ToString());
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> BatchDeletionTerminalVariable(TerminalParameterVariableDeleteRequest batchDeletionRequest)
        {
            RestRequest request = new RestRequest(BATCH_DELETION_TERMINAL_VARIABLE_URL, Method.POST);
            var requestJson = JsonConvert.SerializeObject(batchDeletionRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }
    }
}

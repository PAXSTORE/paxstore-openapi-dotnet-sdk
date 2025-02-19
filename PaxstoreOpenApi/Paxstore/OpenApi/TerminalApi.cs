using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Validator.Terminal;
using Paxstore.OpenApi.Help;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Paxstore.OpenApi
{
    public class TerminalApi : BaseApi
    {
        private const string SEARCH_TERMINAL_URL = "/v1/3rdsys/terminals";
        private const string GET_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}";
        private const string ACTIVE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}/active";
        private const string DISABLE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}/disable";
        private const string DELETE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}";
        private const string CREATE_TERMINAL_URL = "/v1/3rdsys/terminals";
        private const string UPDATE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}";
        private const string ADD_TERMINAL_TO_GROUP_URL = "/v1/3rdsys/terminals/groups";
        private const string MOVE_TERMINAL_URL = "/v1/3rdsys/terminals/{terminalId}/move";
        private const string GET_TERMINAL_PED_STATUS_URL = "/v1/3rdsys/terminals/{terminalId}/ped";
        private const string UPDATE_TERMINAL_REMOTE_CONFIG_URL = "/v1/3rdsys/terminals/{terminalId}/config";
        private const string GET_TERMINAL_REMOTE_CONFIG_URL = "/v1/3rdsys/terminals/{terminalId}/config";
        private const string PUSH_TERMINAL_ACTION_URL = "/v1/3rdsys/terminals/{terminalId}/operation";
        private const string COPY_TERMINAL_URL = "/v1/3rdsys/terminals/copy";
        private const string GET_TERMINAL_NETWORK_URL = "/v1/3rdsys/terminals/network";

        private const string URL_SEGMENT_TERMINAL_ID = "terminalId";

        public TerminalApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
            : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public TerminalApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public TerminalApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public TerminalApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }

        public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, string snNameTID)
        {
            return this.SearchTerminal(pageNo, pageSize, orderBy, status, snNameTID, false, false, false);
        }

        public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, string resellerName, string merchantName, TerminalStatus status, String snNameTID)
        {
            return SearchTerminal(pageNo, pageSize, orderBy, resellerName, merchantName, status, snNameTID, false, false, false);
        }

        public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, String snNameTID, bool includeGeoLocation, bool includeInstalledApks, bool includeInstalledFirmware)
        {
            return SearchTerminal(pageNo, pageSize, orderBy, null, null, status, snNameTID, includeGeoLocation, includeInstalledApks, includeInstalledFirmware);
        }

        public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, string resellerName, string merchantName, TerminalStatus status, string snNameTID, bool includeGeoLocation, bool includeInstalledApks, bool includeInstalledFirmware)
        {
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_URL, Method.Get);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            if (!string.IsNullOrEmpty(snNameTID))
            {
                request.AddParameter("snNameTID", snNameTID);
            }
            if (!string.IsNullOrEmpty(resellerName))
            {
                request.AddParameter("resellerName", resellerName);
            }
            if (!string.IsNullOrEmpty(merchantName))
            {
                request.AddParameter("merchantName", merchantName);
            }
            request.AddParameter("status", GetStatusValue(status));

            request.AddParameter("includeGeoLocation", includeGeoLocation.ToString());
            request.AddParameter("includeInstalledFirmware", includeInstalledFirmware.ToString());
            request.AddParameter("includeInstalledApks", includeInstalledApks.ToString());

            var responseContent = Execute(request);
            TerminalPageResponse resellerPage = JsonConvert.DeserializeObject<TerminalPageResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(resellerPage);
            return result;
        }

        public Result<Terminal> GetTerminal(long terminalId)
        {
            return GetTerminal(terminalId, false);
        }

        public Result<Terminal> GetTerminal(long terminalId, bool includeDetailInfoList)
        {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(GET_TERMINAL_URL, Method.Get);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            request.AddParameter("includeDetailInfoList", includeDetailInfoList);
            var responseContent = Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalResponse);
            return result;
        }

        public Result<Terminal> CreateTerminal(TerminalCreateRequest terminalCreateRequest)
        {
            List<string> validationErrs = ValidateCreate(terminalCreateRequest, new TerminalCreateValidator(), "terminalCreateRequestIsNull");
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(CREATE_TERMINAL_URL, Method.Post);
            var terminalJson = JsonConvert.SerializeObject(terminalCreateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, terminalJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalResponse);
            return result;
        }

        public Result<Terminal> UpdateTerminal(long terminalId, TerminalUpdateRequest terminalUpdateRequest)
        {
            IList<string> validationErrs = ValidateUpdate(terminalId, terminalUpdateRequest, new TerminalUpdateValidator(), "terminalIdInvalid", "terminalUpdateRequestIsNull");
            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_TERMINAL_URL, Method.Put);
            var terminalJson = JsonConvert.SerializeObject(terminalUpdateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, terminalJson, ParameterType.RequestBody);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            var responseContent = Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalResponse);
            return result;
        }

        public Result<string> ActivateTerminal(long terminalId)
        {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(ACTIVE_TERMINAL_URL, Method.Put);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> DisableTerminal(long terminalId)
        {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DISABLE_TERMINAL_URL, Method.Put);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<String> DeleteTerminal(long terminalId)
        {
            IList<string> validationErrs = ValidateId(terminalId, "terminalIdInvalid");
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(DELETE_TERMINAL_URL, Method.Delete);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> BatchAddTerminalToGroup(TerminalGroupRequest batchAddTerminalToGroupRequest)
        {
            List<string> validationErrs = new List<string>();
            if (batchAddTerminalToGroupRequest == null)
            {
                validationErrs.Add(GetMsgByKey("parameterBatchAddTerminalToGroupRequestNull"));
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(ADD_TERMINAL_TO_GROUP_URL, Method.Post);
            var requestJson = JsonConvert.SerializeObject(batchAddTerminalToGroupRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<string> MoveTerminal(long terminalId, string resellerName, string merchantName)
        {
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(resellerName))
            {
                validationErrs.Add(GetMsgByKey("parameterResellerNameIsNull"));
            }
            if (string.IsNullOrEmpty(merchantName))
            {
                validationErrs.Add(GetMsgByKey("parameterMerchantNameIsNull"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(MOVE_TERMINAL_URL, Method.Put);
            TerminalMoveRequest terminalMoveRequest = new TerminalMoveRequest();
            terminalMoveRequest.ResellerName = resellerName;
            terminalMoveRequest.MerchantName = merchantName;

            var requestJson = JsonConvert.SerializeObject(terminalMoveRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<TerminalPED> GetTerminalPED(long terminalId)
        {
            RestRequest request = new RestRequest(GET_TERMINAL_PED_STATUS_URL, Method.Get);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            var responseContent = Execute(request);
            TerminalPEDResponse terminalPEDResponse = JsonConvert.DeserializeObject<TerminalPEDResponse>(responseContent);
            Result<TerminalPED> result = new Result<TerminalPED>(terminalPEDResponse);
            return result;
        }

        public Result<string> UpdateTerminalConfig(long terminalId, TerminalConfigUpdateRequest terminalConfigUpdateRequest)
        {
            if (terminalConfigUpdateRequest == null)
            {
                List<string> validationErrs = new List<string>();
                validationErrs.Add(GetMsgByKey("parameterTerminalConfigUpdateRequestIsNull"));
                return new Result<string>(validationErrs);
            }
            RestRequest request = new RestRequest(UPDATE_TERMINAL_REMOTE_CONFIG_URL, Method.Put);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            var requestJson = JsonConvert.SerializeObject(terminalConfigUpdateRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, requestJson, ParameterType.RequestBody);
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<TerminalConfig> GetTerminalConfig(long terminalId)
        {
            RestRequest request = new RestRequest(GET_TERMINAL_REMOTE_CONFIG_URL, Method.Get);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            var responseContent = Execute(request);
            TerminalConfigResponse terminalConfigResponse = JsonConvert.DeserializeObject<TerminalConfigResponse>(responseContent);
            Result<TerminalConfig> result = new Result<TerminalConfig>(terminalConfigResponse);
            return result;
        }

        public Result<string> PushCmdToTerminal(long terminalId, TerminalPushCmd command)
        {
            RestRequest request = new RestRequest(PUSH_TERMINAL_ACTION_URL, Method.Post);
            request.AddUrlSegment(URL_SEGMENT_TERMINAL_ID, terminalId);
            request.AddParameter("command", ExtEnumHelper.GetEnumValue(command));
            string responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        public Result<Terminal> CopyTerminal(TerminalCopyRequest terminalCopyRequest)
        {
            List<String> validationErrs = new List<string>();
            if (terminalCopyRequest == null)
            {
                validationErrs.Add(GetMsgByKey("terminalCopyRequestNull"));
            }

            if (validationErrs.Count > 0)
            {
                return new Result<Terminal>(validationErrs);
            }
            RestRequest request = new RestRequest(COPY_TERMINAL_URL, Method.Post);
            var merchantJson = JsonConvert.SerializeObject(terminalCopyRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, merchantJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(responseContent);
            Result<Terminal> result = new Result<Terminal>(terminalResponse);
            return result;
        }

        public Result<TerminalNetworkInfo> GetTerminalNetowrkInfoBySN(string serialNo)
        {
            return getTerminalNetwork(serialNo, null);
        }

        public Result<TerminalNetworkInfo> GetTerminalNetworkInfoByTid(string tid)
        {
            return getTerminalNetwork(null, tid);
        }

        private Result<TerminalNetworkInfo> getTerminalNetwork(string serialNo, string tid)
        {
            RestRequest request = new RestRequest(GET_TERMINAL_NETWORK_URL, Method.Get);
            if (!string.IsNullOrWhiteSpace(serialNo))
            {
                request.AddParameter("serialNo", serialNo.Trim());
            }
            if (!string.IsNullOrWhiteSpace(tid))
            {
                request.AddParameter("tid", tid.Trim());
            }
            var responseContent = Execute(request);
            TerminalNetworkInfoResponse temp = JsonConvert.DeserializeObject<TerminalNetworkInfoResponse>(responseContent);
            Result<TerminalNetworkInfo> result = new Result<TerminalNetworkInfo>(temp);
            return result;
        }



        string GetStatusValue(TerminalStatus status)
        {
            switch (status)
            {
                case TerminalStatus.Active:
                    return "A";
                case TerminalStatus.Inactive:
                    return "P";
                case TerminalStatus.Suspend:
                    return "S";
                case TerminalStatus.All:
                    return null;
            }
            return null;
        }

        public static string GetOrderValue(TerminalSearchOrderBy order)
        {
            switch (order)
            {
                case TerminalSearchOrderBy.Name:
                    return "name";
                case TerminalSearchOrderBy.TID:
                    return "tid";
                case TerminalSearchOrderBy.SerialNo:
                    return "serialNo";
            }
            return "name";
        }
    }

    public enum TerminalStatus {
		Active,
		Inactive,
		Suspend,
        All
	}
	
	public enum TerminalSearchOrderBy {
		Name,
		TID,
		SerialNo
	}

    public enum TerminalPushCmd {
        [EnumValue("Restart")]
        Restart,
        [EnumValue("Lock")]
        Lock,
        [EnumValue("Unlock")]
        Unlock
    }
}
using Newtonsoft.Json;
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
    public class TerminalFirmwareApi : BaseApi
    {
        private const string CREATE_TERMINAL_FIRMWARE_URL = "/v1/3rdsys/terminalFirmwares";
        private const string SEARCH_TERMINAL_FIRMWARE_LIST_URL = "/v1/3rdsys/terminalFirmwares";
        private const string GET_TERMINAL_FIRMWARE_URL = "/v1/3rdsys/terminalFirmwares/{terminalFirmwareId}";
        private const string SUSPEND_TERMINAL_FIRMWARE_URL = "/v1/3rdsys/terminalFirmwares/suspend";

        public TerminalFirmwareApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret) {

        }

        public Result<string> PushFirmware2TerminalByTidAndFirmwareName(string tid, string firmwareName) {
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(tid))
            {
                validationErrs.Add(GetMsgByKey("parameterTidCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(firmwareName))
            {
                validationErrs.Add(GetMsgByKey("parameterFirmwareNameCannotBeEmpty"));
            }
            PushFirmware2TerminalRequest request = new PushFirmware2TerminalRequest();
            request.TID = tid;
            request.FirmwareName = firmwareName;
            return PushFirmware2Terminal(request);
        }

        public Result<string> PushFirmware2TerminalBySnAndFirmwareName(string serialNo, string firmwareName)
        {
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(serialNo))
            {
                validationErrs.Add(GetMsgByKey("parameterSnCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(firmwareName))
            {
                validationErrs.Add(GetMsgByKey("parameterFirmwareNameCannotBeEmpty"));
            }
            PushFirmware2TerminalRequest request = new PushFirmware2TerminalRequest();
            request.SerialNo = serialNo;
            request.FirmwareName = firmwareName;
            return PushFirmware2Terminal(request);
        }

        private Result<string> PushFirmware2Terminal(PushFirmware2TerminalRequest pushFirmware2TerminalRequest) {
            List<String> validationErrs = validatePushFirmware2Terminal(pushFirmware2TerminalRequest);

            if (validationErrs.Count > 0) {
                return new Result<String>(validationErrs);
            }

            RestRequest request = new RestRequest(CREATE_TERMINAL_FIRMWARE_URL, Method.POST);

            var terminalFirmwareJson = JsonConvert.SerializeObject(pushFirmware2TerminalRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, terminalFirmwareJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }


        public Result<PushFirmwareTaskInfo> SearchPushFirmwareTasks(int pageNo, int pageSize, SearchOrderBy orderBy,
                                                               string terminalTid, string fmName, PushStatus status){
            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (string.IsNullOrWhiteSpace(terminalTid))
            {
                validationErrs.Add(GetMsgByKey("parameterTerminalTidEmpty"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<PushFirmwareTaskInfo>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_TERMINAL_FIRMWARE_LIST_URL, Method.GET);
            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());
            request.AddParameter("terminalTid", terminalTid);
            request.AddParameter("fmName", fmName);
            request.AddParameter("orderBy", GetOrderValue(orderBy));
            request.AddParameter("status", GetPushStatusValue(status));
            var responseContent = Execute(request);
            PushFirmwareTaskPageResponse response = JsonConvert.DeserializeObject<PushFirmwareTaskPageResponse>(responseContent);
            Result<PushFirmwareTaskInfo> result = new Result<PushFirmwareTaskInfo>(response);
            return result;
        }


        public Result<PushFirmwareTaskInfo> GetPushFirmwareTask(long pushFirmwareTaskId){
            RestRequest request = new RestRequest(GET_TERMINAL_FIRMWARE_URL, Method.GET);
            request.AddUrlSegment("terminalFirmwareId", pushFirmwareTaskId.ToString());
            var responseContent = Execute(request);
            PushFirmwareTaskResponse response = JsonConvert.DeserializeObject<PushFirmwareTaskResponse>(responseContent);
            Result<PushFirmwareTaskInfo> result = new Result<PushFirmwareTaskInfo>(response);
            return result;
        }

        public Result<string> DisablePushFirmwareTaskBySnAndFirmwareName(string serialNo, string firmwareName){
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(serialNo))
            {
                validationErrs.Add(GetMsgByKey("parameterSnCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(firmwareName))
            {
                validationErrs.Add(GetMsgByKey("parameterFirmwareNameCannotBeEmpty"));
            }
            PushFirmware2TerminalRequest request = new PushFirmware2TerminalRequest();
            request.SerialNo = serialNo;
            request.FirmwareName = firmwareName;
            return DisablePushFirmwareTask(request);
        }

        public Result<string> DisablePushFirmwareTaskByTidAndFirmwareName(string tid, string firmwareName){
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrEmpty(tid)){
                validationErrs.Add(GetMsgByKey("parameterTidCannotBeEmpty"));
            }
            if (string.IsNullOrEmpty(firmwareName)){
                validationErrs.Add(GetMsgByKey("parameterFirmwareNameCannotBeEmpty"));
            }
            PushFirmware2TerminalRequest request = new PushFirmware2TerminalRequest();
            request.TID = tid;
            request.FirmwareName = firmwareName;
            return DisablePushFirmwareTask(request);
}

        private Result<string> DisablePushFirmwareTask(PushFirmware2TerminalRequest disablePushFirmwareTaskRequest){
            RestRequest request = new RestRequest(SUSPEND_TERMINAL_FIRMWARE_URL, Method.POST);
            var disablePushFirmwareTaskRequestJson = JsonConvert.SerializeObject(disablePushFirmwareTaskRequest);
            request.AddParameter(Constants.CONTENT_TYPE_JSON, disablePushFirmwareTaskRequestJson, ParameterType.RequestBody);
            var responseContent = Execute(request);
            EmptyResponse emptyResponse = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);
            Result<string> result = new Result<string>(emptyResponse);
            return result;
        }

        private List<String> validatePushFirmware2Terminal(PushFirmware2TerminalRequest pushFirmware2TerminalRequest){
            List<string> validationErrs = new List<string>();
            if (pushFirmware2TerminalRequest == null)
            {
                validationErrs.Add(GetMsgByKey("parameterPushFirmware2TerminalRequestNull"));
            }
            else
            {
                if (string.IsNullOrEmpty(pushFirmware2TerminalRequest.SerialNo) && string.IsNullOrEmpty(pushFirmware2TerminalRequest.TID))
                {
                    validationErrs.Add(GetMsgByKey("parameterCreateTerminalApkRequestSnTidEmpty"));
                }
                if (string.IsNullOrEmpty(pushFirmware2TerminalRequest.FirmwareName))
                {
                    validationErrs.Add(GetMsgByKey("parameterpushFirmware2TerminalRequestSnTidEmpty"));
                }
            }
            return validationErrs;
        }

        string GetPushStatusValue(PushStatus status)
        {
            switch (status)
            {
                case PushStatus.Active:
                    return "A";
                case PushStatus.Suspend:
                    return "S";
                case PushStatus.All:
                    return null;
            }
            return null;
        }

        string GetOrderValue(SearchOrderBy order)
        {
            switch (order)
            {
                case SearchOrderBy.CreatedDate_asc:
                    return "a.created_date ASC";
                case SearchOrderBy.CreatedDate_desc:
                    return "a.created_date DESC";
            }
            return "a.created_date DESC";
        }
    }
}

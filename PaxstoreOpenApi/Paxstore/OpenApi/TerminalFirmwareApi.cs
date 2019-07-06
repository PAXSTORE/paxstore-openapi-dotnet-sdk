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
    }
}

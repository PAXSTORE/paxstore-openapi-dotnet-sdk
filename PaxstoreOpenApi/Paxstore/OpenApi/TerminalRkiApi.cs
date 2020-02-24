using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class TerminalRkiApi : BaseApi
    {
        private const string CREATE_TERMINAL_RKI_KEY_URL = "/v1/3rdsys/terminalRkis";
        private const string SEARCH_TERMINAL_RKI_KEY_LIST_URL = "/v1/3rdsys/terminalRkis";
        private const string GET_TERMINAL_RKI_KEY_URL = "/v1/3rdsys/terminalRkis/{terminalRkiId}";
        private const string SUSPEND_TERMINAL_RKI_KEY_URL = "/v1/3rdsys/terminalRkis/suspend";

        public TerminalRkiApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<TerminalRkiTaskInfo> PushRkiKey2Terminal(PushRki2TerminalRequest pushRki2TerminalRequest)
        {
            List<string> validationErrs = validateCreateTerminalRki(pushRki2TerminalRequest);

            if (validationErrs.Count > 0)
            {
                return new Result<TerminalRkiTaskInfo>(validationErrs);
            }
            ThirdPartySysApiClient client = new ThirdPartySysApiClient(getBaseUrl(), getApiKey(), getApiSecret());
            SdkRequest request = createSdkRequest(CREATE_TERMINAL_RKI_KEY_URL);
            request.setRequestMethod(SdkRequest.RequestMethod.POST);
            request.addHeader(Constants.CONTENT_TYPE, Constants.CONTENT_TYPE_JSON);
            request.setRequestBody(new Gson().toJson(pushRki2TerminalRequest, PushRki2TerminalRequest.class));
        Response response = EnhancedJsonUtils.fromJson(client.execute(request), Response.class);
        Result<PushRkiTaskDTO> result = new Result<PushRkiTaskDTO>(response);
        return result;
    }

    //    public Result<PushRkiTaskDTO> searchPushRkiTasks(int pageNo, int pageSize, SearchOrderBy orderBy,
    //                                                               String terminalTid, String rkiKey, PushStatus status)
    //    {
    //        ThirdPartySysApiClient client = new ThirdPartySysApiClient(getBaseUrl(), getApiKey(), getApiSecret());
    //        PageRequestDTO page = new PageRequestDTO();
    //        page.setPageNo(pageNo);
    //        page.setPageSize(pageSize);
    //        if (orderBy != null)
    //        {
    //            page.setOrderBy(orderBy.val());
    //        }

    //        List<String> validationErrs = validate(page);
    //        if (validationErrs.size() > 0)
    //        {
    //            return new Result<PushRkiTaskDTO>(validationErrs);
    //        }
    //        if (StringUtils.isEmpty(terminalTid))
    //        {
    //            validationErrs.add(super.getMessage("parameter.searchPushRkiTasks.terminalTid.empty"));
    //            return new Result<PushRkiTaskDTO>(validationErrs);
    //        }

    //        SdkRequest request = getPageRequest(SEARCH_TERMINAL_RKI_KEY_LIST_URL, page);
    //        request.addRequestParam("terminalTid", terminalTid);
    //        request.addRequestParam("rkiKey", rkiKey);
    //        if (status != null)
    //        {
    //            request.addRequestParam("status", status.val());
    //        }

    //        PushRkiTaskPageResponse pageResponse = EnhancedJsonUtils.fromJson(client.execute(request), PushRkiTaskPageResponse.class);
    //        Result<PushRkiTaskDTO> result = new Result<PushRkiTaskDTO>(pageResponse);

    //        return result;
    //    }

    //public Result<PushRkiTaskDTO> getPushRkiTask(Long pushRkiTaskId)
    //{
    //    logger.debug("pushRkiTaskId=" + pushRkiTaskId);
    //    List<String> validationErrs = validateId(pushRkiTaskId, "parameter.pushRkiTaskId.invalid");
    //    if (validationErrs.size() > 0)
    //    {
    //        return new Result<PushRkiTaskDTO>(validationErrs);
    //    }
    //    ThirdPartySysApiClient client = new ThirdPartySysApiClient(getBaseUrl(), getApiKey(), getApiSecret());
    //    SdkRequest request = createSdkRequest(GET_TERMINAL_RKI_KEY_URL.replace("{terminalRkiId}", pushRkiTaskId + ""));
    //    request.setRequestMethod(SdkRequest.RequestMethod.GET);
    //    PushRkiTaskResponse resp = EnhancedJsonUtils.fromJson(client.execute(request), PushRkiTaskResponse.class);
    //        Result<PushRkiTaskDTO> result = new Result<PushRkiTaskDTO>(resp);
    //        return result;
    //    }

    //    public Result<String> disablePushRkiTask(DisablePushRkiTask disablePushRkiTask)
    //{
    //    List<String> validationErrs = validateDisablePushRki(disablePushRkiTask);

    //    if (validationErrs.size() > 0)
    //    {
    //        return new Result<String>(validationErrs);
    //    }
    //    ThirdPartySysApiClient client = new ThirdPartySysApiClient(getBaseUrl(), getApiKey(), getApiSecret());
    //    SdkRequest request = createSdkRequest(SUSPEND_TERMINAL_RKI_KEY_URL);
    //    request.setRequestMethod(SdkRequest.RequestMethod.POST);
    //    request.addHeader(Constants.CONTENT_TYPE, Constants.CONTENT_TYPE_JSON);
    //    request.setRequestBody(new Gson().toJson(disablePushRkiTask, DisablePushRkiTask.class));
    //        Response resp = EnhancedJsonUtils.fromJson(client.execute(request), Response.class);
    //        Result<String> result = new Result<String>(resp);
    //        return result;
    //    }

    private List<string> validateCreateTerminalRkiRequest(PushRki2TerminalRequest request)
    {
        List<string> validationErrs = new List<string>();
        if (request == null)
        {
            validationErrs.Add(GetMsgByKey("parameterPushRki2TerminalRequestNull"));
        }else{
            if (string.IsNullOrEmpty(request.RkiKey)) {
                validationErrs.Add(GetMsgByKey("rkiKeyMandatory"));
            }
            
            if (string.IsNullOrEmpty(request.SerialNo) && string.IsNullOrEmpty(request.Tid))
            {
                validationErrs.Add(GetMsgByKey("snTidMandatory"));
            }
        }
        return validationErrs;
    }

    //private List<String> validateDisablePushRki(DisablePushRkiTask disablePushRkiTask)
    //{
    //    List<String> validationErrs = new ArrayList<String>();
    //    if (disablePushRkiTask == null)
    //    {
    //        validationErrs.add(super.getMessage("parameter.disablePushRkiTask.null"));
    //    }
    //    else
    //    {
    //        validationErrs.addAll(validate(disablePushRkiTask));
    //        if (StringUtils.isEmpty(disablePushRkiTask.getSerialNo()) && StringUtils.isEmpty(disablePushRkiTask.getTid()))
    //        {
    //            validationErrs.add(super.getMessage("parameter.disablePushRkiTask.sn.tid.empty"));
    //        }
    //    }
    //    return validationErrs;
    //}
}
}

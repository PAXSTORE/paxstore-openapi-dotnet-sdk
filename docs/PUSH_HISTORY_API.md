## PushHistory API

Push history API is used to search the push result. 
The related APIs are encapsulated in the class *Paxstore.OpenApi.PushHistoryApi*.

**Constructors of PushHistoryApi**

```
public PushHistoryApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### **SearchAppPushHistory**

SearchAppPushHistory API allow the third party system  to find the push history of app

**API**

```
public Result<AppPushHistoryInfo> SearchAppPushStatus(int pageNo, int pageSize, Nullable<PushHistorySearchOrderBy> orderBy, string packageName, string snNameTID, Nullable<PushHistoryStatus> appPushStatus, Nullable<PushHistoryStatus> parameterPushStatus)
```

**Input parameter(s) description**  

| Name                | Type                     | Nullable | Description                                                  |
| :------------------ | :----------------------- | :------- | :----------------------------------------------------------- |
| pageNo              | int                      | false    | page number, value must >=1                                  |
| pageSize            | int                      | false    | the record number per page, range is 1 to 1000               |
| packageName         | string                   | false    | search filter by app packageName                             |
| serialNo            | string                   | true     | only terminal with specified serialNo will search out        |
| pushStatus       	  | Nullable<PushHistoryStatus>        | true     | the push status  the value can be PushHistoryStatus.Success, PushHistoryStatus.Failed |
| pushTime            | DateTime                 | true     | search the push history after the push time                  |




**Sample codes**

```
PushHistoryApi API = new PushHistoryApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<ParameterPushHistoryInfo> result = API.SearchParameterPushHistory(1, 10, "com.pax.posviewer",null, PushHistoryStatus.Failed, nul);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter packageName is mandatory!"]
}
```

**Successful sample result(JSON formatted)**

```
{
  "BusinessCode": 0,
  "RateLimit": "",
  "PageNo": 1,
  "Limit": 2,
  "HasNext": true,
  "TotalCount": 17,
  "Dataset": [
    {
      "ParameterPushStatus": "Success",
      "AppPushStatus": "Success",
      "AppName": "PAXSTORE SDK Demo",
      "AppPushTime": 1575274370000,
      "PushStartTime": 1575274320000,
      "TerminalId": 1013403755,
      "VersionName": "7.0.0-inner",
      "ParameterPushError": null,
      "ParameterPushTime": 1575274373000,
      "SerialNo": "HMP4C15A12000186",
      "PushType": "Terminal",
      "ParameterVariables": "{\"#{test}\": \"44\"}",
      "ParameterValues": "{\"sys_F2_sys_param_termId\": \"#{test}\", \"sys_F2_sys_param_merCode\": \"000000000000001\", \"sys_F2_sys_param_merName\": \"Union Pay\", \"sys_F2_sys_param_acqInsCode\": \"00000000000\"}",
      "AppPushError": null
    },
    {
      "ParameterPushStatus": "Success",
      "AppPushStatus": "Success",
      "AppName": "PAXSTORE SDK Demo",
      "AppPushTime": 1575102052000,
      "PushStartTime": 1575102000000,
      "TerminalId": 1013403370,
      "VersionName": "5.02.02",
      "ParameterPushError": null,
      "ParameterPushTime": 1575102054000,
      "SerialNo": "1170000652",
      "PushType": "Terminal",
      "ParameterVariables": "{}",
      "ParameterValues": "{\"sys_F1_sys_cap_test01\": \"333\", \"sys_F1_sys_cap_test02\": \"111\"}",
      "AppPushError": null
    }
  ]
}
```

The type in dataSet of is ParameterPushHistoryInfo. And the structure shows like below.

|Property Name|Type|Description|
|:--|:--|:--|
|TerminalId		|long	|the id of terminal|
|SerialNo		|string	|the serial number of terminal|
|AppName		|string	|the name of the app pushed|
|VersionName	|string	|the version name of app|
|PushStartTime	|long	|the start time of the push, it is millisecond|
|AppPushTime	|long	|app push time|
|AppPushStatus	|string	|the push result status, value can be Success and Fail|
|AppPushError	|string	|the reason of app push fail|
|ParameterPushTime|long	|parameter push time|
|ParameterPushStatus|string|the parameter push result status, value can be Success and Fail|
|ParameterPushError|string|the reason of parameter push failed|
|ParameterValues|string	|parameter values|
|ParameterVariables|string|parameter variables|
|PushType		|string	|push type, value can be Terminal or Group|	



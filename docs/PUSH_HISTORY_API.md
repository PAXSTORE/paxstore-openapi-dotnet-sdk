## PushHistory API

PushHistoryApi is used to search the push result. All the terminal APIs are in the class *Paxstore.OpenApi.TerminalApi*. 


**Constructors of TerminalApkApi**

```
public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo)
public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy)
public PushHistoryApi(string baseUrl, string apiKey, string apiSecret, int timeout)
```

**Constructor parameters description**

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### **SearchParameterPushHistory**

SearchParameterPushHistory API allow the third-party system  to find all the terminal push history of parameter application

**API**

```
public Result<ParameterPushHistoryInfo> SearchParameterPushHistory(int pageNo, int pageSize, string packageName, string serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime)
```

**Input parameter(s) description**  

| Name        | Type       | Nullable | Description                                                  |
| :---------- | :--------- | :------- | :----------------------------------------------------------- |
| pageNo      | int        | false    | page number, value must >=1                                  |
| pageSize    | int        | false    | the record number per page, range is 1 to 100                |
| packageName | string     | false    | search filter by app packageName                             |
| serialNo    | string     | true     | only terminal with specified serialNo will search out        |
| pushStatus  | PushHistoryStatus | true     | the push status  the value can be PushHistoryStatus.Success, PushHistoryStatus.Failed |
| pushTime    | DateTime       | true     | search the push history after the push time                  |



**Sample codes**

```
PushHistoryApi pushHistoryApi = new PushHistoryApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<ParameterPushHistoryInfo> result = pushHistoryApi.SearchParameterPushHistory(1, 2,  "com.pax.android.demoapp", null, PushHistoryStatus.Success, null);
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
    "Message": null, 
    "ValidationErrors": null, 
    "Data": null, 
    "PageInfo": {
        "PageNo": 1, 
        "Limit": 10, 
        "TotalCount": 2, 
        "HasNext": false, 
        "DataSet": [
            {
                "TerminalId": 1417529218039870, 
                "SerialNo": "0820534734", 
                "AppName": "PAXSTORE SDK Demo", 
                "VersionName": "8.1.3-SNAPSHOT", 
                "ParameterTemplateName": "parameter.xml",
                "PushStartTime": 1624850040000, 
                "AppPushTime": 1624850127000, 
                "AppPushStatus": "Success", 
                "AppPushError": null, 
                "ParameterPushTime": 1624850137000, 
                "ParameterPushStatus": "Success", 
                "ParameterPushError": null, 
                "ParameterValues": "{\"sys_F2_password1\": \"1\", \"sys_F2_password2\": \"abc\"}", 
                "ParameterVariables": "{}", 
                "PushType": "Terminal"
            }, 
            {
                "TerminalId": 1422188150259774, 
                "SerialNo": "1000000423", 
                "AppName": "PAXSTORE SDK Demo", 
                "VersionName": "8.1.3-SNAPSHOT", 
                "ParameterTemplateName": "parameter.xml",
                "PushStartTime": 1624838340000, 
                "AppPushTime": 1624838403000, 
                "AppPushStatus": "Success", 
                "AppPushError": "App Exist", 
                "ParameterPushTime": 1624838420000, 
                "ParameterPushStatus": "Success", 
                "ParameterPushError": null, 
                "ParameterValues": "{\"sys_F2_pid1\": \"v1\", \"sys_F2_pid2\": \"v2\", \"sys_F2_pid3\": \"v3\", \"sys_F2_pid4\": \"v4\", \"sys_F2_pid5\": \"v5\", \"sys_F2_pid6\": \"v6\", \"sys_F2_pid7\": \"v7\", \"sys_F2_pid8\": \"v8\", \"sys_F2_pid9\": \"v9\", \"sys_F2_pid10\": \"v10\", \"sys_F2_pid11\": \"0.03\", \"sys_F2_pid11_1\": \"0.02\"}", 
                "ParameterVariables": "{}", 
                "PushType": "Terminal"
            }, 
        ]
    }, 
    "RateLimit": "3000", 
    "RateLimitRemain": "2900", 
    "RateLimitReset": "1624858730000"
}

```

The type in dataSet of is ParameterPushHistoryDTO. And the structure shows like below.

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
|ParameterTemplateName	|string	|the pushed parameter template name|
|ParameterPushTime|long	|parameter push time|
|ParameterPushStatus|string|the parameter push result status, value can be Success and Fail|
|ParameterPushError|string|the reason of parameter push failed|
|ParameterValues|string	|raw parameter values, parameter key is the parameter file id combined parameter pid, parameter variables are not replaced|
|ParameterVariables|string|parameter variables|
|PushType		|string	|push type, value can be Terminal or Group|


### **SearchLatestParameterPushHistory**

SearchLatestParameterPushHistory API allow the third-party system  to find all the terminal latest push history of parameter application

**API**

```
public Result<ParameterPushHistoryInfo> SearchLatestParameterPushHistory(int pageNo, int pageSize, string packageName, string serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime)
```

**Input parameter(s) description**  

| Name        | Type       | Nullable | Description                                                  |
| :---------- | :--------- | :------- | :----------------------------------------------------------- |
| pageNo      | int        | false    | page number, value must >=1                                  |
| pageSize    | int        | false    | the record number per page, range is 1 to 100                |
| packageName | string     | false    | search filter by app packageName                             |
| serialNo    | string     | true     | only terminal with specified serialNo will search out        |
| pushStatus  | PushHistoryStatus | true     | the push status  the value can be PushHistoryStatus.Success, PushHistoryStatus.Failed |
| pushTime    | DateTime       | true     | search the push history after the push time                  |



**Sample codes**

```
PushHistoryApi pushHistoryApi = new PushHistoryApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<ParameterPushHistoryInfo> result = pushHistoryApi.SearchLatestParameterPushHistory(1, 2,  "com.pax.android.demoapp", "0820534734", PushHistoryStatus.Success, null);
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
    "Message": null, 
    "ValidationErrors": null, 
    "Data": null, 
    "PageInfo": {
        "PageNo": 1, 
        "Limit": 10, 
        "TotalCount": 1, 
        "HasNext": false, 
        "DataSet": [
            {
                "TerminalId": 1417529218039870, 
                "SerialNo": "0820534734", 
                "AppName": "PAXSTORE SDK Demo", 
                "VersionName": "8.1.3-SNAPSHOT", 
                "ParameterTemplateName": "parameter.xml",
                "PushStartTime": 1624850040000, 
                "AppPushTime": 1624850127000, 
                "AppPushStatus": "Success", 
                "AppPushError": null, 
                "ParameterPushTime": 1624850137000, 
                "ParameterPushStatus": "Success", 
                "ParameterPushError": null, 
                "ParameterValues": "{\"sys_F2_password1\": \"1\", \"sys_F2_password2\": \"abc\"}", 
                "ParameterVariables": "{}", 
                "PushType": "Terminal"
            }
        ]
    }, 
    "RateLimit": "3000", 
    "RateLimitRemain": "2900", 
    "RateLimitReset": "1624858730000"
}
```

The type in dataSet of is ParameterPushHistoryDTO. And the structure shows like below.

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
|ParameterTemplateName	|string	|the pushed parameter template name|
|ParameterPushTime|long	|parameter push time|
|ParameterPushStatus|string|the parameter push result status, value can be Success and Fail|
|ParameterPushError|string|the reason of parameter push failed|
|ParameterValues|string	|raw parameter values, parameter key is the parameter file id combined parameter pid, parameter variables are not replaced|
|ParameterVariables|string|parameter variables|
|PushType		|string	|push type, value can be Terminal or Group|


### **SearchOptimizedParameterPushHistory**

SearchOptimizedParameterPushHistory API allow the third-party system  to find all the optimized terminal push history of parameter application

**API**

```
public Result<OptimizedParamPushHistory> SearchOptimizedParameterPushHistory(int pageNo, int pageSize, string packageName, string serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime)
```

**Input parameter(s) description**  

| Name        | Type       | Nullable | Description                                                  |
| :---------- | :--------- | :------- | :----------------------------------------------------------- |
| pageNo      | int        | false    | page number, value must >=1                                  |
| pageSize    | int        | false    | the record number per page, range is 1 to 100                |
| packageName | string     | false    | search filter by app packageName                             |
| serialNo    | string     | true     | only terminal with specified serialNo will search out        |
| pushStatus  | PushHistoryStatus | true     | the push status  the value can be PushHistoryStatus.Success, PushHistoryStatus.Failed |
| pushTime    | DateTime       | true     | search the push history after the push time                  |



**Sample codes**

```
PushHistoryApi pushHistoryApi = new PushHistoryApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<OptimizedParamPushHistory> result = pushHistoryApi.SearchOptimizedParameterPushHistory(1, 2,  "com.pax.android.demoapp", "0820534734", PushHistoryApi.Success, null);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["packageName cannot be null!"]
}
```

**Successful sample result(JSON formatted)**

```
{
    "BusinessCode": 0, 
    "Message": null, 
    "ValidationErrors": null, 
    "Data": null, 
    "PageInfo": {
        "PageNo": 1, 
        "Limit": 10, 
        "TotalCount": 1, 
        "HasNext": false, 
        "DataSet": [
            {
                "TerminalId": 1417529218039870, 
                "SerialNo": "0820534734", 
                "AppName": "PAXSTORE SDK Demo", 
                "VersionName": "8.1.3-SNAPSHOT", 
                "ParameterTemplateName": "parameter.xml",
                "PushStartTime": 1624850040000, 
                "AppPushTime": 1624850127000, 
                "AppPushStatus": "Success", 
                "AppPushError": null, 
                "ParameterPushTime": 1624850137000, 
                "ParameterPushStatus": "Success", 
                "ParameterPushError": null, 
                "Parameters": {
                    "password2": "abc", 
                    "password1": "1"
                }, 
                "PushType": "Terminal"
            }
        ]
    }, 
    "RateLimit": "3000", 
    "RateLimitRemain": "2900", 
    "RateLimitReset": "1624858730000"
}

```

The type in dataSet of is OptimizedParameterPushHistoryDTO. And the structure shows like below.

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
|ParameterTemplateName	|string	|the pushed parameter template name|
|ParameterPushTime|long	|parameter push time|
|ParameterPushStatus|string|the parameter push result status, value can be Success and Fail|
|ParameterPushError|string|the reason of parameter push failed|
|Parameters|map	|optimized parameter values, parameter variables are replaced, parameter key is the parameter pid|
|PushType		|string	|push type, value can be Terminal or Group|

### **SearchLatestOptimizedParameterPushHistory**

SearchLatestOptimizedParameterPushHistory API allow the third party system  to find all the optimized terminal push history of parameter application

**API**

```
public Result<OptimizedParamPushHistory> SearchLatestOptimizedParameterPushHistory(int pageNo, int pageSize, string packageName, string serialNo, Nullable<PushHistoryStatus> pushStatus, Nullable<DateTime> pushTime)
```

**Input parameter(s) description**  

| Name        | Type       | Nullable | Description                                                  |
| :---------- | :--------- | :------- | :----------------------------------------------------------- |
| pageNo      | int        | false    | page number, value must >=1                                  |
| pageSize    | int        | false    | the record number per page, range is 1 to 100                |
| packageName | string     | false    | search filter by app packageName                             |
| serialNo    | string     | true     | only terminal with specified serialNo will search out        |
| pushStatus  | PushHistoryStatus | true     | the push status  the value can be PushHistoryStatus.Success, PushHistoryStatus.Failed |
| pushTime    | DateTime       | true     | search the push history after the push time                  |



**Sample codes**

```
PushHistoryApi pushHistoryApi = new PushHistoryApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<OptimizedParamPushHistory> result = pushHistoryApi.SearchLatestOptimizedParameterPushHistory(1, 2,  "com.pax.android.demoapp", "0820534734", PushHistoryStatus.Success, null);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["packageName cannot be null!"]
}
```

**Successful sample result(JSON formatted)**

```
{
    "BusinessCode": 0, 
    "Message": null, 
    "ValidationErrors": null, 
    "Data": null, 
    "PageInfo": {
        "PageNo": 1, 
        "Limit": 10, 
        "TotalCount": 1, 
        "HasNext": false, 
        "DataSet": [
            {
                "TerminalId": 1417529218039870, 
                "SerialNo": "0820534734", 
                "AppName": "PAXSTORE SDK Demo", 
                "VersionName": "8.1.3-SNAPSHOT", 
                "ParameterTemplateName": "parameter.xml",
                "PushStartTime": 1624850040000, 
                "AppPushTime": 1624850127000, 
                "AppPushStatus": "Success", 
                "AppPushError": null, 
                "ParameterPushTime": 1624850137000, 
                "ParameterPushStatus": "Success", 
                "ParameterPushError": null, 
                "Parameters": {
                    "password2": "abc", 
                    "password1": "1"
                }, 
                "PushType": "Terminal"
            }
        ]
    }, 
    "RateLimit": "3000", 
    "RateLimitRemain": "2900", 
    "RateLimitReset": "1624858730000"
}

```

The type in dataSet of is OptimizedParameterPushHistoryDTO. And the structure shows like below.

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
|ParameterTemplateName	|string	|the pushed parameter template name|
|ParameterPushTime|long	|parameter push time|
|ParameterPushStatus|string|the parameter push result status, value can be Success and Fail|
|ParameterPushError|string|the reason of parameter push failed|
|Parameters|map	|optimized parameter values, parameter variables are replaced, parameter key is the parameter pid|
|PushType		|string	|push type, value can be Terminal or Group|

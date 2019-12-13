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
| orderBy             | PushHistorySearchOrderBy | true     | the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of PushHistorySearchOrderBy.AppPushTime and PushHistorySearchOrderBy.SerialNo. |
| packageName         | string                   | false    | search filter by app packageName                                |
| snNameTID           | string                   | true     | search filter by terminal tid                             |
| appPushStatus       | PushHistoryStatus        | true     | the push status  the value can be PushHistoryStatus.Success, PushHistoryStatus.Failed |
| parameterPushStatus | PushHistoryStatus        | true     |                                                              |




**Sample codes**

```
PushHistoryApi API = new PushHistoryApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<AppPushHistoryInfo> result = API.SearchAppPushStatus(1, 10, PushHistorySearchOrderBy.AppPushTime, "com.pax.posviewer", null, PushHistoryStatus.Failed, null);
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
	"PageInfo": {
		"PageNo": 1,
		"Limit": 2,
		"TotalCount": 586,
		"HasNext": true,
		"DataSet": [{
		"ParameterPushStatus": "None",
		"AppPushStatus": "Failed",
		"AppName": "百度贴吧",
		"AppPushTime": 1575855301000,
		"PushStartTime": 1575627420000,
		"TerminalId": 461706,
		"VersionName": "10.3.8.30",
		"SerialNo": "1150000070",
		"PushType": "Group",
		"AppPushError": "任务已删除"
	}, {
		"ParameterPushStatus": "None",
		"AppPushStatus": "Failed",
		"AppName": "百度贴吧",
		"AppPushTime": 1575855301000,
		"PushStartTime": 1575627420000,
		"TerminalId": 461720,
		"VersionName": "10.3.8.30",
		"SerialNo": "1140000570",
		"PushType": "Group",
		"AppPushError": "任务已删除"
	}]
	}
}
```

The type in dataSet of is AppPushHistoryInfo. And the structure shows like below.

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



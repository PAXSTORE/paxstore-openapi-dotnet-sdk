## TerminalApk API

All the terminalApk related APIs are encapsulated in the class *Paxstore.OpenApi.TerminalApkApi*.

**Constructors of TerminalApkApi**

```
public TerminalApkApi(string baseUrl, string apiKey, string apiSecret)
```

**Constructor parameters description**

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Create terminalApk

Create terminalApk API allow the thirdparty system create a terminalApk (create a push apk task for the specified terminal).


**API**

```
public Result<string> CreateTerminalApk(CreateTerminalApkRequest createTerminalApkRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|createTerminalApkRequest|CreateTerminalApkRequest|false|The create request object. The structure shows below.|


Structure of class TerminalCreateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|TID|string|true|The TID of terminal|
|SerialNo|string|true|The serial number of terminal|
|PackageName|string|false|The package name which indicate the application you want to push to the terminal|
|Version|string|true|The version name of application which you want to push, if it is blank API will use the latest version|
|TemplateName|string|true|The template file name of paramter application. The template file name can be found in the detail of the parameter application. If user want to push more than one template the please use &#124; to concact the different template file names like tempate1.xml&#124;template2.xml&#124;template3.xml, the max size of template file names is 10.|
|Parameters|Dictionary&lt;string, string&gt;|false|The parameter key and value, the key the the PID in template|

Note: TID and serialNo cannot be empty at same time.


**Sample codes**

```
TerminalApkApi api = new TerminalApkApi(API_BASE_URL, API_KEY, API_SECRET);
CreateTerminalApkRequest createTerminalApkRequest = new CreateTerminalApkRequest();
createTerminalApkRequest.TID = "ABC09098989";
createTerminalApkRequest.PackageName = "com.baidu.map";
createTerminalApkRequest.TemplateName = "template_map";
Dictionary<string, string> parameters = new Dictionary<string, string>();
parameters.Add("PID.locationCode", "cn_js_sz");
parameters.Add("PID.showtraffic", "true");
createTerminalApkRequest.Parameters = parameters;
Result<string> result = api.CreateTerminalApk(createTerminalApkRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["The property SerialNo and TID in createTerminalApkRequest cannot be blank at same time!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2028,
	"Message": "TerminalApk not found",
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```



**Possible validation errors**

> <font color=red>Parameter createTerminalApkRequest cannot be null!</font>  
> <font color=red>The property parameters of createTerminalApkRequest cannot be empty!</font>  
> <font color=red>The property serialNo and tid in createTerminalApkRequest cannot be blank at same time!</font> 
> <font color=red>'Package Name' should not be empty.</font> 
> <font color="red">The max size of template names is 10!</font>


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|2028|Terminal not found|Please check the value of tid or serialNo|
|2029|Apk not found|Cannot find apk by packagename and version|
|2030|Parameter template not found|The given template name(s) not exist in system|
|13100|Invalid application parameter variables||
|2026|Tid and serialNo cannot empty at same time||
|2027|Package name cannot be empty||
|2001|Terminal app not found||
|2000|Terminal app status is invalid||
|9306|App is not available||
|2022|Same version of pending terminal app already exists||
|2023|Same version of active terminal app already exists||
|1905|Terminal task app parameter is invalid||
|13100|Invalid application parameter variables||
|1111|Selected parameter templates exceeded the max limit||
|2031|Template name cannot be empty|&nbsp;|




### Search apk push history

The search apk push history API allows thirdparty system to search pushed apks to the specified terminal by page.   

**API**

```
public Result<PushApkHistory> SearchPushApkHistory(int pageNo, int pageSize, SearchOrderBy orderBy,
                                                    string terminalTid, string appPackageName, PushStatus status)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|SearchOrderBy|true|the sort order by field name. The value of this parameter can be one of SearchOrderBy.CreatedDate_desc and SearchOrderBy.CreatedDate_asc.|
|terminalTid|String|false|search filter by terminal tid|
|appPackageName|String|true|search filter by app packageName|
|status|PushStatus|true|the push status<br/> the value can be PushStatus.Active, PushStatus.Suspend, PushStatus.All|

**Sample codes**

```
TerminalApkApi api = new TerminalApkApi(API_BASE_URL, API_KEY, API_SECRET);
Result<PushApkHistory> result = api.SearchPushApkHistory(1, 10, SearchOrderBy.CreatedDate_desc, "7L03HWP9", "com.pax.android.demoapp", PushStatus.All);

```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["pageNo:must be greater than or equal to 1"]
}
```

**Successful sample result**

```
{
	"businessCode": 0,
	"pageInfo": {
		"pageNo": 1,
		"limit": 12,
		"totalCount": 1,
		"hasNext": false,
		"dataSet": [{
			"id": 17850,
            "apkPackageName": "com.pax.demo",
            "apkVersionName": "7.5.0",
            "apkVersionCode": "75",
            "terminalSN": "87879696",
            "status": "A",
            "actionStatus": 2
		}]
	}
}
```

The type in dataSet is PushApkHistory. And the structure like below.

|Name|Type|Description|
|:---|:---|:---|
|id|long|the id of terminal apk|
|apkPackageName|string|the packageName of apk|
|apkVersionName|string|the version name of apk|
|apkVersionCode|long|the version code of apk|
|terminalSN|string|the serialNo of terminal|
|status|string|the status of terminal apk|
|actionStatus|string|the action status|

**Possible client validation errors**  

> <font color=red>pageNo:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be less than or equal to 1000</font>  
> <font color=red>Parameter terminalTid cannot be blank!</font> 

### Get push apk history by id

Get terminal push apk history by id.


**API**

```
public Result<PushApkHistory> GetPushApkHistory(long pushApkId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pushApkId|long|false|the id of terminalApk, it can be get by API SearchPushApkHistory|

**Sample codes**

```
TerminalApkApi api = new TerminalApkApi(API_BASE_URL, API_KEY, API_SECRET);
Result<PushApkHistory> result = api.GetPushApkHistory(1000062203);
```



**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 2001,
	"message": "Terminal app not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0,
	"data": {
		"id": 17850,
		"apkPackageName": "com.pax.demo",
		"apkVersionName": "7.5.0",
		"apkVersionCode": "75",
		"terminalSN": "87879696",
		"status": "A",
		"actionStatus": 2,
		"errorCode": ""
	}
}
```

<br>
The type of data is TerminalApkDTO, and the structure shows below.

|Name|Type|Description|
|:---|:---|:---|
|id|long|the id of terminal apk|
|apkPackageName|string|the packageName of apk|
|apkVersionName|string|the version name of apk|
|apkVersionCode|long|the version code of apk|
|terminalSN|string|the serialNo of terminal|
|status|string|the status of terminal apk, value can be one of A(Active) and S(Suspend)|
|actionStatus|string|the action status|
|errorCode|string|the error code|


**Possible client validation errors**


> <font color="red">Parameter terminalApkId cannot be null and cannot be less than 1!</font>


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2001|Terminal app not found|&nbsp;|

**Possible action status**

|action status|status|Description|
|:---|:---|:---|
|0|None|The push task no start|
|1|Pending|The push task staring|
|2|Succeed|The push task is succeed|
|3|Failed|The push task is failed|
|4|Watting|The push task is watting, no need push|

**Possible error codes**

|Error Code|Description|
|:---|:---|
|1|Download error|
|2|Install error|
|3|App exist|
|4|App version too low|
|5|App param duplicate|
|6|Apk not exist|
|7|Apk version mismatch|
|12|The push is disabled|

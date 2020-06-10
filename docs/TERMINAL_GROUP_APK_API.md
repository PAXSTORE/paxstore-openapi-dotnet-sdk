## TerminalGroupApk API

All the push terminal group apk to terminal related APIs are encapsulated in the class *Paxstore.OpenApi.TerminalGroupApk*.

**Constructors of TerminalGroupApkApi**

```
public TerminalGroupApkApi(string baseUrl, string apiKey, string apiSecret)
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|



### Get terminal group apk

Get terminal group apk by groupApkId and pidList


**API**

```
public Result<TerminalGroupApkInfo> GetTerminalGroupApk(long groupApkId, List<string> pidList)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|groupApkId|long|false|the id of terminalGroupApk|
|pidList|List\<string\>|true|the pid of the configured parameters to return|

**Sample codes**

```
TerminalGroupApkApi api = new TerminalGroupApkApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
List<string> pidList = new List<string>();
pidList.Add("sys.cap.test01");
pidList.Add("sys.cap.test02");
pidList.Add("sys.cap.test03");
Result<TerminalGroupApkInfo> result = api.GetTerminalGroupApk(17850, pidList);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2101,
	"Message": "Group app not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
    	"GroupApkParam": {
			"FailedCount": 0,
			"PendingCount": 8,
			"ParamTemplateName": "PassWord_Param02.xml",
			"SuccessCount": 0
		},
		"ApkVersionName": "10.3.8.30",
		"FailedCount": 0,
		"ActionStatus": 1,
		"ApkPackageName": "com.baidu.tieba",
		"PendingCount": 8,
		"EffectiveTime": 1576118640000,
		"ApkVersionCode": 167968776,
		"SuccessCount": 0,
		"Id": 1728,
		"UpdatedDate": 1576118649000,
		"Status": "A"
    }
}
```

The type of data is TerminalGroupApkInfo, the structure is like below.  

|Name|Type|Description|
|:---|:---|:---|
|Id|long|the id of terminal group apk|
|ApkPackageName|string|the packageName of terminal group apk|
|ApkVersionName|string|the version name of terminal group apk|
|ApkVersionCode|long|the version code of terminal group apk|
|EffectiveTime|Nullable\<long\>|the effective time|
|ExpiredTime|Nullable\<long\>|the expire time|
|UpdatedDate|Nullable\<long\>||
|ActionStatus|int|action status, value can be 0 and 1, please refer to [Action Status](APPENDIX.md#user-content-action-status)|
|Status|string|the push status|
|PendingCount|Nullable\<int\>||
|SuccessCount|Nullable\<int\>||
|FailedCount|Nullable\<int\>||
|GroupApkParam|TerminalGroupApkParamInfo|the structure like below|


The structure of TerminalGroupApkParamInfo.

| Name                 | Type               | Description                       |
| :------------------- | :----------------- | :-------------------------------- |
| ParamTemplateName    | string             |                                   |
| ConfiguredParameters | Dictionary<string, string> | Configuration parameters in param |
| PendingCount         | Nullable\<int\>                |                                   |
| SuccessCount         | Nullable\<int\>                |                                   |
| FailedCount          | Nullable\<int\>                |                                   |




**Possible business codes**

| Business Code | Message             | Description |
| :------------ | :------------------ | :---------- |
| 2101          | Group app not found |             |


### Search terminal group apk

The search terminal group apk API allows third party system to search group apks to the specified group by page.
**API**

```
public Result<TerminalGroupApkInfo> SearchTerminalGroupApk(int pageNo, int pageSize, Nullable<TerminalGroupApkSearchOrderBy> orderBy, long groupId, Nullable<bool> pendingOnly, Nullable<bool> historyOnly, string keyWords)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|Nullable\<TerminalGroupApkSearchOrderBy\>|true|the sort order of search result, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of TerminalGroupApkSearchOrderBy.CreatedDate_desc and TerminalGroupApkSearchOrderBy.CreatedDate_asc.|
|groupId|long|false|the id of the group|
|pendingOnly|Nullable\<bool\>|true|Indicate whether to search the pending push task only|
|historyOnly|Nullable\<bool\>|true|Indicate whether to search history push task only                                                              |
|keyWords|string|true|Key words, it will match APP's package name, APK's name, APK's short description and APK's description|

**Sample codes**

```
TerminalGroupApkApi api = new TerminalGroupApkApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalGroupApkInfo> result = api.SearchTerminalGroupApk(1,1, TerminalGroupApkSearchOrderBy.CreatedDate_asc,16526,true,null,null);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["'Page No' must be greater than '0'."]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2150,
	"Message": "Terminal group not found"
}
```

**Successful sample result**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 1,
		"TotalCount": 3,
		"HasNext": true,
		"DataSet": [{
		"GroupApkParam": {
			"FailedCount": 0,
			"PendingCount": 0,
			"ParamTemplateName": "paramTemplate.xml",
			"SuccessCount": 0
		},
		"ApkVersionName": "5.01.07",
		"FailedCount": 0,
		"ActionStatus": 0,
		"ApkPackageName": "com.pax.android.demoapp",
		"PendingCount": 0,
		"ApkVersionCode": 57,
		"SuccessCount": 0,
		"Id": 1644,
		"UpdatedDate": 1562219872000,
		"Status": "P"
	}]
	}
}
```

The type in dataSet is TerminalGroupApkInfo. 

**Possible client validation errors**  

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font> 

**Possible business codes**

| Business Code | Message                  | Description |
| :------------ | :----------------------- | :---------- |
| 2150          | Terminal group not found |             |



### Create and active group apk

Create and active  a group push apk task by CreateTerminalGroupApkRequest.

**API**

```
public Result<TerminalGroupApkInfo> CreateAndActiveGroupApk(CreateTerminalGroupApkRequest createTerminalGroupApkRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|createTerminalGroupApkRequest|CreateTerminalGroupApkRequest|false||

Structure of class CreateTerminalGroupApkRequest.

| Property Name        | Type                | Nullable | Description                                                  |
| :------------------- | :------------------ | :------- | :----------------------------------------------------------- |
| GroupId              | long                | false    | The id of the terminal group                                 |
| PushTemplateName     | string              | true     | the name of the push Template                                |
| PackageName          | string              | false    | the package name of push apk                                 |
| Version              | string              | true     | The package name which indicate the application you want to push |
| TemplateName         | string              | true     | The template file name of paramter application. The template file name can be found in the detail of the parameter application. If user want to push more than one template the please use &#124; to concact the different template file names like tempate1.xml&#124;template2.xml&#124;template3.xml, the max size of template file names is 10. |
| Parameters           | Dictionary\<string, string\> | true     | The parameter key and value, the key the PID in template     |
| Base64FileParameters | List\<FileParameter\> | true     | The parameter of file type, the max counter of file type parameter is 10, and the max size of each parameter file is 500kb |

Structure of class FileParameter

| Property Name | Type   | Nullable | Description                                             |
| :------------ | :----- | :------- | :------------------------------------------------------ |
| PID           | string | true     | The PID in template                                     |
| FileName      | string | true     | The parameter of file type, file name containing suffix |
| FileData      | string | true     | The parameter of file type, file base64 data            |

**Sample codes**

```
TerminalGroupApkApi api = new TerminalGroupApkApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
CreateTerminalGroupApkRequest createRequest = new CreateTerminalGroupApkRequest();
createRequest.GroupId = 16543; 
createRequest.PushTemplateName = "testCreate3RD-result-api-test-CREATEbY-newest-12334111";
createRequest.PackageName = "com.baidu.tieba";
createRequest.TemplateName = "123 (3).xml";
Result<TerminalGroupApkInfo> result = api.CreateAndActiveGroupApk(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter createTerminalGroupApkRequest is mandatory!"]
}
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2150,
	"Message": "Terminal group not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"GroupApkParam": {
			"FailedCount": 0,
			"PendingCount": 0,
			"ParamTemplateName": "123 (3).xml",
			"SuccessCount": 0
		},
		"ApkVersionName": "10.3.8.30",
		"FailedCount": 0,
		"ActionStatus": 0,
		"ApkPackageName": "com.baidu.tieba",
		"PendingCount": 0,
		"EffectiveTime": 1583396340000,
		"ApkVersionCode": 167968776,
		"SuccessCount": 0,
		"Id": 1743,
		"UpdatedDate": 1583396371789,
		"Status": "A"
	}
}
```

<br>
The type of data is TerminalGroupApkInfo.

**Possible client validation errors**


> <font color="red">Parameter createTerminalGroupApkRequest is mandatory!</font>

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
| 135           |Request parameter is missing or invalid|&nbsp;|
| 1111          |Selected parameter templates exceeded the max limit||
| 2027          |Package name is mandatory||
| 2029          |Apk not found||
| 2030          |Parameter template not found||
| 2031          |Template Name cannot be empty||
| 2032          |API does not support push non free applicationAPI does not support push non free application||
| 2150          |Terminal group not found||
| 13100         |Invalid application parameter variables||




### Suspend terminal group apk

This api allows the third party system suspend apk of group push task.

**API**

```
public Result<TerminalGroupApkInfo> SuspendTerminalGroupApk(long groupApkId)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|groupApkId|long|false|the id of the group push apk task|


**Sample codes**

```
TerminalGroupApkApi api = new TerminalGroupApkApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalGroupApkInfo> result = api.SuspendTerminalGroupApk(1743);
```



**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2101,
	"Message": "Group app not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
	"Data": {
		"GroupApkParam": {
			"FailedCount": 0,
			"PendingCount": 0,
			"ParamTemplateName": "123 (3).xml",
			"SuccessCount": 0
		},
		"ApkVersionName": "10.3.8.30",
		"FailedCount": 0,
		"ActionStatus": 0,
		"ApkPackageName": "com.baidu.tieba",
		"PendingCount": 0,
		"EffectiveTime": 1583396760000,
		"ApkVersionCode": 167968776,
		"SuccessCount": 0,
		"Id": 1743,
		"UpdatedDate": 1583398464000,
		"Status": "S"
	}
}
```

The type of data is TerminalGroupApkInfo.


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2101|Group app not found||
|2110|Group app is not active||



### Delete terminal group apk

This api allows the third party system  delete apk of group push task.


**API**

```
public Result<string> DeleteTerminalGroupApk(long groupApkId)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|groupApkId|long|false|the id of the group push apk task|

**Sample codes**

```
TerminalGroupApkApi api = new TerminalGroupApkApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = api.DeleteTerminalGroupApk(1743);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2101,
	"Message": "Group app not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0
}
```

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2101|Group app not found||
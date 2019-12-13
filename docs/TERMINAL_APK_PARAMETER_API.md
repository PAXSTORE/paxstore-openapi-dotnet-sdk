## TerminalApkParameter API

All terminal related APK parameter APIs are encapsulated in classes *Paxstore.OpenApi.TerminalApkParameterApi*.

**Constructors of TerminalApkParameter **

```
public TerminalApkParameterApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|



### Get terminal apk parameter

Get terminal apk parameter(s) by templateName, packageName and versionName.

**API**

```
public Result<TerminalParameterVariable> GetTerminalVariable(int pageNo, int pageSize, Nullable<VariableSearchOrderBy> orderBy, string tid, string serialNo, string packageName, string key, Nullable<VariableSource> source)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|PageNo|int|false|page number, value must >=1|
|PageSize|int|false|the record number per page, range is 1 to 1000|
|OrderBy|VariableSearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of VariableSearchOrderBy.ApkParameter_asc and VariableSearchOrderBy.ApkParameter_desc.|
|TemplateName|String|false|Apk parameter template name|
|PackageName|string|true|get by app packageName|
|VersionName|string|true|The version name of application|

**Sample codes**

```
TerminalApkParameterApi API = new TerminalApkParameterApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<ApkParameter> result = API.GetTerminalApkParameter(1, 10, TerminalApkParamSearchOrderBy.ApkParameter_asc, null, "zhiyoucanshu", "1.2");
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["terminal apk parameter packageName and versionName cannot be null and cannot be less than 1!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2028,
	"Message": "TerminalApk not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 1,
		"TotalCount": 4,
		"HasNext": true,
		"DataSet": [{
			"CreatedDate": 1574402799000,
			"Name": "testCreate3RD-result-api-test",
			"ParamTemplateName": "1000084085_(3).xml|schema1.xml",
			"Id": 1148,
			"UpdatedDate": 1574402799000,
			"Apk": {
				"ApkType": "P",
				"ApkFileType": "A",
				"ApkFile": {
					"Permissions": "USE_CREDENTIALS,READ_SYNC_SETTINGS,BROADCAST_BADGE,RECEIVE,WAKE_LOCK,SENSOR_ENABLE,CHANGE_BADGE,WRITE_EXTERNAL_STORAGE,CAMERA,MOUNT_UNMOUNT_FILESYSTEMS,UPDATE_BADGE,READ,SENSOR_INFO,READ_PHONE_STATE,GET_TASKS,RESTART_PACKAGES,MANAGE_ACCOUNTS,WRITE_SETTINGS,READ_LOGS,MIPUSH_RECEIVE,INSTALL_SHORTCUT,ACCESS_FINE_LOCATION,AUTHENTICATE_ACCOUNTS,WRITE,MESSAGE,ACCESS_NETWORK_STATE,CHANGE_WIFI_STATE,WRITE_SYNC_SETTINGS,READ_SETTINGS,READ_APP_BADGE,UNINSTALL_SHORTCUT,C2D_MESSAGE,PROVIDER_INSERT_BADGE,INTERNET,GET_ACCOUNTS,READ_EXTERNAL_STORAGE,SYSTEM_ALERT_WINDOW,RECEIVE_BOOT_COMPLETED,DISABLE_KEYGUARD,ACCESS_LOCATION_EXTRA_COMMANDS,RECIEVE_MCS_MESSAGE,CHANGE_CONFIGURATION,ACCESS_COARSE_LOCATION,UPDATE_SHORTCUT,READ_CONTACTS,ACCESS_MOCK_LOCATION,BLUETOOTH,CHANGE_NETWORK_STATE,VIBRATE,ACCESS_WIFI_STATE",
					"PaxPermission": ""
				},
				"OsType": "A",
				"VersionName": "6.6.4",
				"VersionCode": 664,
				"Status": "O"
			}
		}]
	}
}
```

The type in dataSet is ApkParameter. And the structure like below.

| Property Name     | Type   | Description                 |
| ----------------- | ------ | --------------------------- |
| Id                | long   | The id of Apk parameter     |
| Name              | string | The name of Apk Parameter   |
| ParamTemplateName | string | The Apk param template name |
| CreatedDate       | long   | Create record time          |
| UpdatedDate       | long   | Update record time          |
| Apk               | Apk    | App Info                    |

The structure of class Apk

| Property Name | Type       | Description                                                  |
| :------------ | :--------- | :----------------------------------------------------------- |
| Status        | string     | Status of apk. Value can be one of P(Pending), O(Online), R(Rejected) and U(Offline) |
| VersionCode   | long       | version code of apk.                                         |
| VersionName   | string     | version name of apk.                                         |
| ApkType       | string     | base type of apk. Value can be one of N(Normal) and P(Parameter) |
| ApkFileType   | string     | file type of apk. Value can be one of A(Android), P(Prolin) and B(Broadpos) |
| ApkFile       | ApkFile | install package file of apk.                                 |
| OSType       | string | OS type, A is for android, T is for traditional                |

The structure of class ApkFile

| Property Name | Type   | Description               |
| :------------ | :----- | :------------------------ |
| Permissions   | string | Android OS Authorization. |
| PaxPermission | string | Paydroid Authorizationr.  |

**Possible client validation errors**  

> <font color=red>pageNo:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be less than or equal to 1000</font>  



### Create a Apk Parameter


**API**

```
public Result<ApkParameter> CreateApkParameter(CreateApkParameterRequest createApkParameterRequest)
```

**Input parameter(s) description**

| Parameter Name            | Type                      | Nullable | Description                                         |
| :------------------------ | :------------------------ | :------- | :-------------------------------------------------- |
| createApkParameterRequest | CreateApkParameterRequest | true     | the create request object, the structure like below |

Structure of class CreateApkParameterRequest

| Property Name | Type | Nullable|Description |
|:--- | :---|:---|:---|
|PackageName  		|string						|true	|The package name which indicate the application you want to push to the terminal|
|Version			|string						|true	|The version name of application which you want to push|
|Name				|string						|true	|The name of Apk Parameter|
|ParamTemplateName	|string						|false	|The name of  Apk param template name|
|Parameters			|Dictionary<string, string>	|false	|The parameter key and value, the key the PID in template|
|Base64FileParameters|List<FileParameter>		|true	|The file type parameters, the max number of file type parameters is 10, and the max size of each parameter file is 500kb|

Structure of class FileParameter


| Property Name | Type | Nullable|Description |
|:--- | :---|:---|:---|
|PID  			|string						|false	|The PID in template|
|FileName		|string						|false	|The parameter of file type, filename containing suffix|
|FileData		|string						|false	|The parameter of file type, it is the base64 string of the file, max size is 500kb|


**Sample codes**

```
TerminalApkParameterApi API = new TerminalApkParameterApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
CreateApkParameterRequest createApkParameterRequest = new CreateApkParameterRequest();
createApkParameterRequest.ParamTemplateName= "paxstore_app_param.xml";
createApkParameterRequest.Name= "test114";
createApkParameterRequest.PackageName= "zhiyoucanshu";
createApkParameterRequest.Version= "1.2";
Dictionary<string, string> parameters = new Dictionary<string, string>();
parameters.Add("sys_F2_sys_param_acqInsCode", "00000000022");
createApkParameterRequest.Parameters = parameters;
Result<ApkParameter> result = API.CreateApkParameter(createApkParameterRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter createApkParameterRequest is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1002,
	"Message": "Apk not found"
}
```

**Successful sample result**

```
{
	"BusinessCode": 0
}
```

**Possible validation errors**

> <font color=red>Parameter createApkParameterRequest is mandatory!</font>  
> <font color=red>'Package Name' should not be empty.</font> 
> <font color=red>'Version' should not be empty.</font> 
> <font color=red>'Name' should not be empty.</font>
> <font color=red>'ParamTemplateName' should not be empty.</font>

### Update terminal apk parameter by id

Update terminal apk parameter by id.


**API**

```
public Result<ApkParameter> UpdateApkParameter(long apkParameterId, UpdateApkParameterRequest updateApkParameterRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|apkParameterId|long|true|the id of apk parameter|
|updateApkParameterRequest|UpdateApkParameterRequest|true|The update request object. The structure shows below.|

Structure of class UpdateApkParameterRequest

| Property Name     | Type                			| Nullable 	| Description                                              |
| :---------------- | :------------------ 			| :------- 	| :------------------------------------------------------- |
| ParamTemplateName | String              			| false    	| The template file name of paramter application. The template file name can be found in the detail of the parameter application. If user want to push more than one template the please use \| to concact the different template file names like tempate1.xml|template2.xml|template3.xml, the max size of template file names is 10.                               |
| Parameters        | Dictionary<string, string> 	| true    	| The parameter key and value, the key the PID in template |
|Base64FileParameters|List<FileParameter>			| true		| The file type parameters, the max number of file type parameters is 10, and the max size of each parameter file is 500kb|

Note:UpdateApkParameterRequest cannot be empty or  paramTemplateName and  parameters cannot be empty at same time.

**Sample codes**

```
TerminalApkParameterApi API = new TerminalApkParameterApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
UpdateApkParameterRequest updateApkParameterRequest = new UpdateApkParameterRequest();
updateApkParameterRequest.ParamTemplateName = "paxstore_app_param.xml";
Dictionary<string, string> parameters = new Dictionary<string, string>();
parameters.Add("sys_F2_sys_param_acqInsCode", "00000000033");
updateApkParameterRequest.Parameters = parameters;
Result<ApkParameter> result = API.UpdateApkParameter(1000101970, updateApkParameterRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["terminal apk parameter Id cannot be null and cannot be less than 1!,Parameter apkParameterUpdateRequest cannot be null!"]
}
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 113,
	"message": "Your request is invalid, please try again or contact marketplace administrator"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0
s}
```

> <font color="red">Parameter apkParameterId cannot be null and cannot be less than 1!</font>


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|1272|Parameter template {0} not found||
|9001|Push template not found||



### Delete apk parameter by apk parameter id

The delete apk parameter API allows third party system to delete apk parameter

**API**

```
public Result<string> DeleteApkParameter(long apkParameterId)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|apkParameterId|long|true|the apk parameter's id|


**Sample codes**

```
TerminalApkParameterApi API = new TerminalApkParameterApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
API.DeleteApkParameter(apkParameterId);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["terminal apk parameter Id cannot be null and cannot be less than 1!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 9001,
	"message": Push template not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```

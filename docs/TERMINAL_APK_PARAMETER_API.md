## TerminalApkParameter API

All terminal related APK parameter APIs are encapsulated in classes *com.pax.market.api.sdk.java.api.terminalApkParameter.TerminalApkParameterApi*.

**Constructors of TerminalApkParameter **

```
public TerminalApkParameterApi(String baseUrl, String apiKey, String apiSecret);
public TerminalApkParameterApi(String baseUrl, String apiKey, String apiSecret, Locale locale);
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|String|the base url of REST API|
|apiKey|String|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|String|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|
|locale|Locale|the locale, the default locale is Locale.ENGLISH, the language of message and errors in return object depend on locale|


### get terminal apk parameter by templateName ,packageName,versionName

Get terminal  apk parameter history by templateName ,packageName  ,versionName.

**API**

```
 public Result<ApkParameterDTO> getTerminalApkParameter(String templateName ,String packageName, String versionName)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|SearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of SearchOrderBy.ApkParameter_asc and SearchOrderBy.ApkParameter_desc.|
|templateName|String|false|Apk parameter template name|
|packageName|String|true|get by app packageName|
|versionName|String|true|The version name of application|

**Sample codes**

```
TerminalApkParameterApi terminalApkParameterApi = new TerminalApkParameterApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<ApkParameterDTO> result = terminalApkParameterApi.getTerminalApkParameter(1,2, TerminalApkParameterApi.SearchOrderBy.ApkParameter_asc,"12312","com.ss.android.article.lite","6.6.4");
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["terminal apk parameter packageName and versionName cannot be null and cannot be less than 1!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 2028,
	"message": "TerminalApk not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0,
	"pageInfo": {
		"pageNo": 1,
		"limit": 1,
		"totalCount": 4,
		"hasNext": true,
		"dataSet": [{
			"createdDate": 1574402799000,
			"name": "testCreate3RD-result-api-test",
			"paramTemplateName": "1000084085_(3).xml|schema1.xml",
			"id": 1148,
			"updatedDate": 1574402799000,
			"apk": {
				"apkType": "P",
				"apkFileType": "A",
				"apkFile": {
					"permissions": "USE_CREDENTIALS,READ_SYNC_SETTINGS,BROADCAST_BADGE,RECEIVE,WAKE_LOCK,SENSOR_ENABLE,CHANGE_BADGE,WRITE_EXTERNAL_STORAGE,CAMERA,MOUNT_UNMOUNT_FILESYSTEMS,UPDATE_BADGE,READ,SENSOR_INFO,READ_PHONE_STATE,GET_TASKS,RESTART_PACKAGES,MANAGE_ACCOUNTS,WRITE_SETTINGS,READ_LOGS,MIPUSH_RECEIVE,INSTALL_SHORTCUT,ACCESS_FINE_LOCATION,AUTHENTICATE_ACCOUNTS,WRITE,MESSAGE,ACCESS_NETWORK_STATE,CHANGE_WIFI_STATE,WRITE_SYNC_SETTINGS,READ_SETTINGS,READ_APP_BADGE,UNINSTALL_SHORTCUT,C2D_MESSAGE,PROVIDER_INSERT_BADGE,INTERNET,GET_ACCOUNTS,READ_EXTERNAL_STORAGE,SYSTEM_ALERT_WINDOW,RECEIVE_BOOT_COMPLETED,DISABLE_KEYGUARD,ACCESS_LOCATION_EXTRA_COMMANDS,RECIEVE_MCS_MESSAGE,CHANGE_CONFIGURATION,ACCESS_COARSE_LOCATION,UPDATE_SHORTCUT,READ_CONTACTS,ACCESS_MOCK_LOCATION,BLUETOOTH,CHANGE_NETWORK_STATE,VIBRATE,ACCESS_WIFI_STATE",
					"paxPermission": ""
				},
				"osType": "A",
				"versionName": "6.6.4",
				"versionCode": 664,
				"status": "O"
			}
		}]
	}
}
```

The type in dataSet is ApkParameterDTO. And the structure like below.

| Property Name     | Type   | Description                 |
| ----------------- | ------ | --------------------------- |
| id                | Long   | The id of Apk parameter     |
| name              | String | The name of Apk Parameter   |
| paramTemplateName | String | The Apk param template name |
| createdDate       | Long   | Create record time          |
| updatedDate       | Long   | Update record time          |
| apk               | ApkDTO | App Info                    |

The structure of class ApkDTO

| Property Name | Type       | Description                                                  |
| :------------ | :--------- | :----------------------------------------------------------- |
| status        | String     | Status of apk. Value can be one of P(Pending), O(Online), R(Rejected) and U(Offline) |
| versionCode   | Long       | version code of apk.                                         |
| versionName   | String     | version name of apk.                                         |
| apkType       | String     | base type of apk. Value can be one of N(Normal) and P(Parameter) |
| apkFileType   | String     | file type of apk. Value can be one of A(Android), P(Prolin) and B(Broadpos) |
| apkFile       | ApkFileDTO | install package file of apk.                                 |

The structure of class ApkFileDTO

| Property Name | Type   | Description               |
| :------------ | :----- | :------------------------ |
| permissions   | String | Android OS Authorization. |
| paxPermission | String | Paydroid Authorizationr.  |

**Possible client validation errors**  

> <font color=red>pageNo:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be less than or equal to 1000</font>  



### Create a Apk Parameter


**API**

```
public Result<String> createApkParameter(CreateApkParameterRequest createApkParameterRequest)
```

**Input parameter(s) description**

| Parameter Name            | Type                      | Nullable | Description                                         |
| :------------------------ | :------------------------ | :------- | :-------------------------------------------------- |
| CreateApkParameterRequest | createApkParameterRequest | true     | the create request object, the structure like below |

Structure of class CreateApkParameterRequest

| Property Name | Type | Nullable|Description |
|:--- | :---|:---|:---|
|packageName|String|true|The package name which indicate the application you want to push to the terminal|
|version|String|true|The version name of application which you want to push|
|name|String|true|The name of Apk Parameter|
|paramTemplateName|String|false|The name of  Apk param template name|
|parameters|Map<String, String>|false|The parameter key and value, the key the PID in template|

**Sample codes**

```
TerminalApkParameterApi terminalApkParameterApi = new TerminalApkParameterApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
CreateApkParameterRequest createApkParameterRequest = new CreateApkParameterRequest();
createApkParameterRequest.setParamTemplateName("1000084085_(3).xml|schema1.xml");
createApkParameterRequest.setName("testCreate3RD-result-api-test-CREATEbY-newest");
createApkParameterRequest.setPackageName("com.ss.android.article.lite");
createApkParameterRequest.setVersion("6.6.4");
Map<String, String> parameters = new HashMap<String, String>();
parameters.put("sys.cap.emvParamCheckType", "abc");
createApkParameterRequest.setParameters(parameters);
Result<String> result = terminalApkParameterApi.createApkParameter(createApkParameterRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["Parameter apkParameterCreateRequest cannot be null!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 1002,
	"message": "Apk not found"
}
```

**Successful sample result**

```
{
	"businessCode": 0
}
```

**Possible validation errors**

> <font color=red>Parameter createApkParameterRequest cannot be null!</font>  
> <font color=red>packageName:cannot be empty</font> 
>
> <font color=red>version:cannot be empty</font> 
>
> <font color=red>name : cannot be empty</font>
>
> <font color=red>paramTemplateName and parameters :may not be empty</font>

### Update terminal apk parameter by id

Update terminal apk parameter by id.


**API**

```
public Result<String> updateApkParameter(Long apkParameterId,UpdateApkParameterRequest updateApkParameterRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|apkParameterId|Long|true|the id of apk parameter|
|updateApkParameterRequest|UpdateApkParameterRequest|true|The update request object. The structure shows below.|

Structure of class UpdateApkParameterRequest

| Property Name     | Type                | Nullable | Description                                              |
| :---------------- | :------------------ | :------- | :------------------------------------------------------- |
| paramTemplateName | String              | false    | The name of param template                               |
| parameters        | Map<String, String> | false    | The parameter key and value, the key the PID in template |

Note:UpdateApkParameterRequest cannot be empty or  paramTemplateName and  parameters cannot be empty at same time.

**Sample codes**

```
TerminalApkParameterApi terminalApkParameterApi = new TerminalApkParameterApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Long apkParameterId = 1149L;
UpdateApkParameterRequest updateApkParameterRequest = new UpdateApkParameterRequest();
updateApkParameterRequest.setParamTemplateName(null);
Map<String, String> parameters = new HashMap<String, String>();
parameters.put("sys.param.acqInsCode", "abc");
parameters.put("wsplink_F1_wsplink_param_password", "123");
parameters.put("wsplink_F1_wsplink_param_TID", "abc");
parameters.put("wsplink_F1_wsplink_param_posID", "abc");
parameters.put("wsplink_F1_wsplink_param_Token", "abc");
parameters.put("wsplink_F1_wsplink_param_authURL0", "abc");
parameters.put("wsplink_F1_wsplink_param_settingsPassword", "123");
updateApkParameterRequest.setParameters(parameters);
Result<String> result = terminalApkParameterApi.updateApkParameter(apkParameterId,updateApkParameterRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["terminal apk parameter Id cannot be null and cannot be less than 1!,Parameter apkParameterUpdateRequest cannot be null!"]
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
|113|Malformed or illegal request|&nbsp;|
|1272|Parameter template {0} not found||
|9001|Push template not found||



### Delete apk parameter by apk parameter id

The delete apk parameter API allows third party system to delete apk parameter

**API**

```
public Result<String> deleteApkParameter(Long apkParameterId)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|apkParameterId|Long|true|the  apk parameter's id|


**Sample codes**

```
TerminalApkParameterApi terminalApkParameterApi = new TerminalApkParameterApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
terminalApkParameterApi.deleteApkParameter(apkParameterId);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["terminal apk parameter Id cannot be null and cannot be less than 1!"]
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
}
```

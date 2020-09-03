## TerminalVariable API

This API allow third party system get, create, update and  delete terminal variable(s).

All the related Variable APIs are encapsulated in the class *Paxstore.OpenApi.TerminalVariableApi*.

**Constructors of TerminalApkParameter**

```
public TerminalVariableApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|

### Get terminal variable by tid , serialNo , packageName, key, source

This API allows third party system to get terminal variable by tid , serialNo , packageName, key and source

**API**

```
public Result<TerminalParameterVariable> GetTerminalVariable(int pageNo, int pageSize, VariableSearchOrderBy orderBy, string tid, string serialNo, string packageName, string key, Nullable<VariableSource> source)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 100|
|orderBy|VariableSearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of VariableSearchOrderBy.Variable_asc and VariableSearchOrderBy.Variable_desc.|
|tid|string|true|The tid of terminal|
|serialNo|string|true|The serial number of terminal. Note: tid and serialNo cannot be empty at same time|
|packageName|string|true|The package name required to get the terminal variable|
|key|string|true|The terminal variable key|
|source|VariableSource|true|The  source of terminal variable, the value can be VariableSource.TERMINAL, VariableSource.GROUP, VariableSource.MARKET, VariableSource.MERCHANT|

**Sample codes**

```
TerminalVariableApi api = new TerminalVariableApi(API_BASE_URL, API_KEY, API_SECRET);
Result<TerminalParameterVariable> result = api.GetTerminalVariable(1,2,VariableSearchOrderBy.Variable_asc,"124465D345",null,null,null,null);
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1800,
	"Message": "Terminal not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 2,
		"TotalCount": 10,
		"HasNext": true,
		"Dataset": [{
			"CreatedDate": 1519609650000,
			"AppName": null,
			"AppPackageName": null,
			"ID": 1000000169,
			"Source": "M",
			"UpdatedDate": 1519609650000,
			"Value": "www",
			"Key": "MARKET_DOMAIN",
			"Remarks": "This variable is used in all apps of the market terminals"
		}, {
			"CreatedDate": 1519609650000,
			"AppName": "一键清理大师",
			"AppPackageName": "cn.com.opda.android.clearmaster",
			"ID": 1000000162,
			"Source": "M",
			"UpdatedDate": 1519803201000,
			"Value": "Global",
			"Key": "MARKET_NAME",
			"Remarks": "This variable is only used in the specified app of the market terminals"
		}]
	}
}
```

The type in DataSet is TerminalParameterVariable. And the structure like below.

| Property Name  | Type   | Description                                                  |
| -------------- | ------ | ------------------------------------------------------------ |
| ID             | long   | The id of terminal variable                                  |
| AppPackageName | string | The app package name                                         |
| AppName        | string | The app name                                                 |
| Key            | string | Terminal variable key                                        |
| Value          | string | Terminal variable value                                      |
| Remarks        | string | Comment                                                      |
| Source         | string | Source type, value can be T (Terminal), G (Group), M (Marketplace) and C (Merchant) |
| CreatedDate    | long   |                                                              |
| UpdatedDate    | long   |                                                              |

**Possible client validation errors**  

> <font color="red">'Page Size' must be less than or equal to '100'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>
> <font color="red">The parameter serialNo and tid  cannot be blank at same time!</font>



### Create a terminal variable


**API**

```
public Result<string> CreateTerminalVariable(TerminalParameterVariableCreateRequest createRequest)
```

**Input parameter(s) description**

| Parameter Name | Type                                   | Nullable | Description                                         |
| :------------- | :------------------------------------- | :------- | :-------------------------------------------------- |
| createRequest  | TerminalParameterVariableCreateRequest | false    | the create request object, the structure like below |

Structure of class TerminalParameterVariableCreateRequest

| Property Name | Type | Nullable|Description |
|:--- | :---|:---|:---|
|TID|string|true|the tid of terminal, tid and serialNo cannot be empty at same time|
|SerialNo|string|true|the serial number of terminal, tid and serialNo cannot be empty at same time|
|VariableList|List\<ParameterVariable\>|true|List of parametervariables,the structure like below|

Structure of class ParameterVariable

| Property Name | Type   | Nullable | Description             |
| :------------ | :----- | :------- | :---------------------- |
| PackageName   | string | false    | The app package name    |
| Version       | string | true    | The app  version        |
| Key           | string | false     | Terminal variable key   |
| Value         | string | true     | Terminal variable value |
| Remarks       | string | false    | Comment                 |

**Sample codes**

```
TerminalVariableApi api = new TerminalVariableApi(API_BASE_URL, API_KEY, API_SECRET);
TerminalParameterVariableCreateRequest createRequest = new TerminalParameterVariableCreateRequest();
ParameterVariable parameterVariable1 = new ParameterVariable();
parameterVariable1.Key="testCreateVariable1Api3";
parameterVariable1.Value="testApiCreate3";
parameterVariable1.PackageName= "com.pax.android.demoapp";
parameterVariable1.Remarks="今日头条app testCreateApi3";

ParameterVariable parameterVariable2 = new ParameterVariable();
parameterVariable2.Key="testCreateVariable1Api4";
parameterVariable2.Value="testApiCreate4";
parameterVariable2.PackageName= "com.pax.android.demoapp";
parameterVariable2.Remarks="今日头条app testCreateApi4";

List<ParameterVariable> variableList = new List<ParameterVariable>();
variableList.Add(parameterVariable1);
variableList.Add(parameterVariable2);
createRequest.TID= "JDEW5LCP";
createRequest.VariableList = variableList;
Result<String> createResult = API.CreateTerminalVariable(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter terminalParameterVariableCreateReques is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2026,
	"Message": "Tid and serialNo cannot be empty at same time"
}
```

**Successful sample result**

```
{
	"BusinessCode": 0
}
```

**Possible validation errors**

> <font color=red>Parameter terminalParameterVariableCreateReques is mandatory!y</font>  
> <font color=red>variableList can not be empty</font>  
> <font color=red>The parameter serialNo and tid  cannot be blank at same time!</font> 

**Possible business codes**

| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 113           | Your request is invalid, please try again or contact marketplace administrator |             |
| 1000          | App not found                                                |             |
| 1800          | Terminal not found                                           |             |
| 2026          | Tid and serialNo cannot be empty at same time                |             |
| 13101         | Invalid terminal parameter variables                         |             |



### Update terminal variable by id

Update terminal variable  by id.


**API**

```
public Result<string> UpdateTerminalVariable(long terminalVariableId, TerminalVariableUpdateRequest terminalVariableUpdateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|terminalVariableId|long|true|the id of terminal variable|
|updateRequest|TerminalVariableUpdateRequest|true|The request object. The structure shows below.|

Structure of class ParameterVariable

| Property Name | Type                | Nullable | Description                                              |
| :------------ | :------------------ | :------- | :------------------------------------------------------- |
| PackageName   | string              | false    | The name of param template                               |
| Version       | string              | true     |  |
| Key           | string              | false    | Terminal variable key                                    |
| Value         | string              | true     | Terminal variable value                                  |
| Remarks       | string              | true     | Comment                                                  |

Note: parameterVariable cannot be empty

**Sample codes**

```
TerminalVariableApi api = new TerminalVariableApi(API_BASE_URL, API_KEY, API_SECRET);
TerminalVariableUpdateRequest updateRequest = new TerminalVariableUpdateRequest();
updateRequest.Key = "testCreateVariable1Api4";
updateRequest.Value= "testApiCreate4_updated";
updateRequest.Remarks="updateRemarks1";
updateRequest.PackageName= "com.pax.android.demoapp";

Result<string> updateResult = API.UpdateTerminalVariable(terminalVariableId, updateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter terminalVariableId cannot be null and cannot be less than 1!"]
}
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 13000
	"Message"::"Variable not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```

**Possible validation errors**

> <font color="red">Parameter terminalVariableId cannot be null and cannot be less than 1!</font>
> <font color="red">Parameter terminalVariableUpdateRequest is mandatory!</font>



**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|113|Your request is invalid, please try again or contact marketplace administrator|&nbsp;|
|13100|Variable not found||
|13101|Invalid terminal parameter variables||



### Delete terminal variable by terminal variable id

The delete terminal variable API allows third party system to delete terminal variable.

**API**

```
public Result<string> DeleteTerminalVariable(long terminalVariableId)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|terminalVariableId|long|false|The  terminal variable id|

**Sample codes**

```
TerminalVariableApi api = new TerminalVariableApi(API_BASE_URL, API_KEY, API_SECRET);
long terminalVariableId = 1000001156;
Result<string> deleteResult = API.DeleteTerminalVariable(terminalVariableId);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter terminalVariableId invalid!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 13000,
	"Message": "Variable not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0
}
```

| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 113           | Your request is invalid, please try again or contact marketplace administrator |             |
| 13100         | Variable not found                                           |             |



### Batch  delete terminalvariable 

**API**

```
public Result<string> BatchDeletionTerminalVariable(TerminalParameterVariableDeleteRequest batchDeletionRequest)
```

**Input parameter(s) description** 

| Parameter Name       | Type                                   | Nullable | Description                                                 |
| :------------------- | :------------------------------------- | :------- | :---------------------------------------------------------- |
| batchDeletionRequest | TerminalParameterVariableDeleteRequest | false     | The batchDeletionRequest object. The structure shows below. |

Structure of class ParameterVariable

| Property Name | Type       | Nullable | Description                     |
| :------------ | :--------- | :------- | :------------------------------ |
| VariableIds   | List\<long\> | false    | The ids of terminal variable id |

**Sample codes**

```
TerminalVariableApi api = new TerminalVariableApi(API_BASE_URL, API_KEY, API_SECRET);
TerminalParameterVariableDeleteRequest batchDeletionRequest = new TerminalParameterVariableDeleteRequest();
List<long> variableIds = new List<long>();
variableIds.Add(1001464163);
variableIds.Add(1001464142);
batchDeletionRequest.VariableIds = variableIds;
Result<string> batchDeletionResult = API.BatchDeletionTerminalVariable(batchDeletionRequest);
```



**Possible validation errors**

> <font color="red">Parameter batchDeletionRequest is mandatory!</font>  
> <font color="red">VariableIds cannot be empty!</font>

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0
}
```

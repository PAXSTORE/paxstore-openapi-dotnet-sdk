## MerchantVariable API

Merchant variable related APIs. All the APIs are in the class *Paxstore.OpenApi.MerchantVariableApi*


**Constructors of MerchantVariableApi**

```
public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo)
public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy)
public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret, int timeout)
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|String|the base url of REST API|
|apiKey|String|the apiKey of marketplace, get this key from PAXSTORE admin console, refer to chapter Apply access rights|
|apiSecret|String|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|

### Search merchant variable by merchantId, packageName, key, source

The search merchant variable API allows third party system to search merchant variable by merchantId, packageName, key, source

**API**

```
public Result<MerchantVariable> SearchMerchantVariable(int pageNo, int pageSize, Nullable<MerchantVariableSearchOrderBy> orderBy, long merchantId, string packageName, string key, Nullable<MerchantVariableSource> source)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 100|
|orderBy|MerchantVariableSearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. |
|merchantId|long|false|The id of merchant|
|packageName|string|true|The package name required to get the merchant variable|
|key|string|true|The variable key|
|source|MerchantVariableSource|true|The source of variable|

**Sample codes**

```
MerchantVariableApi merchantVariableApi = new MerchantVariableApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<MerchantVariable> result = merchantVariableApi.SearchMerchantVariable(1, 2, MerchantVariableSearchOrderBy.CREATED_DESC, 1018624270, null,null , null);
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1720,
	"Message": "Merchant doesn't exist"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"Limit": 2,
    "PageNo": 1,
    "TotalCount": 2,
    "HasNext": false,
		"Dataset": [{
      "ID": 1000000009,
      "AppPackageName": "",
      "Type": "T",
      "Key": "key1",
      "Value": "value1",
      "Remarks": "R1",
      "Source": "C",
      "CreatedDate": 1666160856000,
      "UpdatedDate": 1666160856000
    },
    {
      "ID": 1000000010,
      "AppPackageName": "",
      "Type": "T",
      "Key": "key2",
      "Value": "value2",
      "Remarks": "R2",
      "Source": "C",
      "CreatedDate": 1666161025000,
      "UpdatedDate": 1666161025000
    }]
	}
}
```

The type in dataSet is MerchantVariableDTO. And the structure like below.

| Property Name  | Type   | Description                                    |
| -------------- | ------ | ---------------------------------------------- |
| ID             | long   | The id of merchant variable                    |
| AppPackageName | string | The app package name                           |
| AppName        | string | The app name                                   |
| Type           | string | Merchant variable type, T(text) or P(password) |
| Key            | string | Merchant variable key                          |
| Value          | string | Merchant variable value                        |
| Remarks        | string | Comment                                        |
| Source         | string | Source type,M(Market) or C(Merchant)           |
| CreatedDate    | long   |                                                |
| UpdatedDate    | long   |                                                |

**Possible client validation errors**

> <font color="red">'Page Size' must be less than or equal to '100'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>  


### Create a merchant variable

**API**

```
public Result<string> CreateMerchantVariable(MerchantVariableCreateRequest merchantVariableCreateRequest)
```

**Input parameter(s) description**

| Parameter Name | Type                          | Nullable | Description                                         |
| :------------- | :---------------------------- | :------- | :-------------------------------------------------- |
| createRequest  | MerchantVariableCreateRequest | false    | the create request object, the structure like below |

Structure of class MerchantVariableCreateRequest

| Property Name | Type | Nullable|Description |
|:--- | :---|:---|:---|
| MerchantId    |long|false|the id of merchant|
|VariableList|IList\<ParameterVariable\>|false|List of parametervariables,the structure like below|

Structure of class ParameterVariable

| Property Name | Type   | Nullable | Description                                    |
| :------------ | :----- | :------- | :--------------------------------------------- |
| PackageName   | string | true     | The app package name                           |
| Type          | string | true     | Merchant variable type, T(text) or P(password), When it is empty, the default value is "T"

|
| Key           | string | false    | Merchant variable key                          |
| Value         | string | true     | Merchant variable value                        |
| Remarks       | string | true    | Remark                                        |

**Sample codes**
n
```
MerchantVariableApi merchantVariableApi = new MerchantVariableApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
MerchantVariableCreateRequest request = new MerchantVariableCreateRequest();
request.MerchantId = 1018624270;
IList<ParameterVariable> merchantVariable = new List<ParameterVariable>();
ParameterVariable parameter = new ParameterVariable();
parameter.Key = "testkey1";
parameter.PackageName = "com.pax.pdm";
merchantVariable.Add(parameter);
ParameterVariable parameter2 = new ParameterVariable();
parameter2.Key = "testkey2";
parameter2.PackageName = "com.pax.pdm";
parameter2.Value = "test key2 value";
merchantVariable.Add(parameter2);
ParameterVariable parameter3 = new ParameterVariable();
parameter3.Key = "testkey3";
parameter3.PackageName = "com.pax.pdm";
parameter3.Type = "P";
parameter3.Value = "pwdtest";
merchantVariable.Add(parameter3);
request.VariableList = merchantVariable;
Result<string> result = merchantVariableApi.CreateMerchantVariable(request);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter merchantVariableCreateRequest cannot be null!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1720,
	"Message": "Merchant doesn't exist"
}
```

**Successful sample result**

```
{
	"BusinessCode": 0
}
```

**Possible validation errors**

> <font color=red>VariableList can not be empty</font>
> <font color=red>Parameter merchantVariableCreateRequest cannot be null!</font>



**Possible business codes**

| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 135           | Request parameter is missing or invalid                      |             |
| 1000          | App not found                                                |             |
| 1010          | App is invalid                                               |             |
| 1720          | Merchant doesn't exist                                       |             |
| 13001         | Variable key is mandatory                                    |             |
| 13002         | Variable key is invalid, only letters, numbers, dash, underline and dot is allowed |             |
| 13003         | Invalid variable type, only text(T) and password(P) is allowed |             |
| 13004         | Variable value is too long                                   |             |
| 13011         | Variable remarks is too long                                 |             |
| 13015         | Variable key is too long                                     |             |
| 13016         | Variable with same key and application already exist         |             |
| 13200         | Merchant parameter variable invalid                          |             |

### Update merchant variable by id

Update merchant variable by id.

**API**

```
public Result<string> UpdateMerchantVariable(long merchantVariableId, MerchantVariableUpdateRequest merchantVariableUpdateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|merchantVariableId|long|false|the id of merchant variable|
|merchantVariableUpdateRequest|MerchantVariableUpdateRequest|false|The parameterVariable request object. The structure shows below.|

Structure of class MerchantVariableUpdateRequest

| Property Name | Type   | Nullable | Description                                    |
| :------------ | :----- | :------- | :--------------------------------------------- |
| PackageName   | string | true     | The name of param template                     |
| Type          | string | true     | Merchant variable type, T(text) or P(password), When it is empty, the default value is "T"

|
| Key           | string | false    | Merchant variable key                          |
| Value         | string | true     | Merchant variable value                        |
| Remarks       | string | true     | Comment                                        |

Note: updateRequest cannot be empty

**Sample codes**

```
MerchantVariableApi merchantVariableApi = new MerchantVariableApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
MerchantVariableUpdateRequest request = new MerchantVariableUpdateRequest();
request.Key = "key001";
request.Remarks = "new remark";
Result<string> result = API.UpdateMerchantVariable(1000534439, request);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter merchantVariableUpdateRequest cannot be null!"]
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

> <font color="red">Parameter merchantVariableUpdateRequest cannot be null!</font>

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|113|Your request is invalid, please try again or contact marketplace administrator|&nbsp;|
| 13001         | Variable key is mandatory                                    |             |
| 13002         | Variable key is invalid, only letters, numbers, dash, underline and dot is allowed |             |
| 13003         | Invalid variable type, only text(T) and password(P) is allowed |             |
| 13004         | Variable value is too long                                   |             |
| 13011         | Variable remarks is too long                                 |             |
| 13015         | Variable key is too long                                     ||
|13016|Variable with same key and application already exist||
|13100|Variable not found||
|13200|Merchant parameter variable invalid||

### Delete merchant variable by merchant variable id

The delete merchant variable API allows third party system to delete merchant variable.

**API**

```
public Result<string> DeleteMerchantVariable(long merchantVariableId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|merchantVariableId|long|false|The merchant variable id|

**Sample codes**

```
MerchantVariableApi merchantVariableApi = new MerchantVariableApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = API.DeleteMerchantVariable(1000534439);
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
	"BusinessCode": 0
}
```

| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 113           | Your request is invalid, please try again or contact marketplace administrator |             |
| 13100         | Variable not found                                           |             |



### Batch delete merchant variable

**API**

```
public Result<string> BatchDeletionMerchantVariable(MerchantVariableDeleteRequest batchDeleteRequest)
```

**Input parameter(s) description**

| Parameter Name       | Type                                   | Nullable | Description                                                 |
| :------------------- | :------------------------------------- | :------- | :---------------------------------------------------------- |
| BatchDeleteRequest | MerchantVariableDeleteRequest | false     | The batchDeletionRequest object. The structure shows below. |

Structure of class MerchantVariableDeleteRequest

| Property Name | Type       | Nullable | Description                     |
| :------------ | :--------- | :------- | :------------------------------ |
| VariableIds   | IList\<long\> | false    | The ids of merchant variable id |

**Sample codes**

```
MerchantVariableApi merchantVariableApi = new MerchantVariableApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
MerchantVariableDeleteRequest request = new MerchantVariableDeleteRequest();
IList<long> deleteIds = new List<long>();
deleteIds.Add(1000534440);
deleteIds.Add(1000534441);
request.VariableIds = deleteIds;
Result<string> result = merchantVariableApi.BatchDeletionMerchantVariable(request);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["Parameter merchantVariableDeleteRequest cannot be null!"]
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0
}
```

**Possible validation errors**

> <font color=red>Parameter merchantVariableDeleteRequest cannot be null!</font>   
> <font color=red>variableIds cannot be empty!</font>



| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 113           | Your request is invalid, please try again or contact marketplace administrator |             |
| 13100         | Variable not found                                           |             |

## MerchantVariable API

All merchant variable related APIs are the in classes *Paxstore.OpenApi.MerchantVariableApi*.

**Constructors of MerchantVariableApi**

```
public MerchantVariableApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|

### Search merchant variable

The search merchant variable API allows third party system to search merchant variable by merchantId, packageName, key, source

**API**

```
public Result<MerchantVariable> SearchMerchantVariable(int pageNo, int pageSize, Nullable<MerchantVariableSearchOrderBy> orderBy,long merchantId, string packageName, string key, Nullable<MerchantVariableSource> source)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 100|
|orderBy|MerchantVariableSearchOrderBy|true|the search result order, if this parameter is null the search result will order by created date descend.|
|merchantId|long|false|The id of merchant|
|packageName|string|true|The package name required to get the merchant variable|
|key|string|true|The variable key|
|source|MerchantVariableSource|true|The source of variable, the value can be one of MARKET, MERCHANT|

**Sample codes**

```
MerchantVariableApi api = new MerchantVariableApi(API_BASE_URL, API_KEY, API_SECRET);
Result<MerchantVariable> result = api.SearchMerchantVariable(1, 2,MerchantVariableSearchOrderBy.CREATE_DATE_DESC, 113781, null, key, null);
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
      "AppPackageName": "com.pax.demo",
      "Type": "T",
      "AppName": "Demo app",
      "Key": "key1",
      "Value": "value1",
      "Remarks": "R1",
      "Source": "C",
      "CreatedDate": 1666160856000,
      "UpdatedDate": 1666160856000
    },
    {
      "ID": 1000000010,
      "AppPackageName": "com.pax.demo2",
      "Type": "T",
      "AppName": "Demo2",
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

The type in dataSet is MerchantVariable. And the structure like below.

| Property Name  | Type   | Description                                    |
| -------------- | ------ | ---------------------------------------------- |
| ID             | long   | The id of merchant variable                    |
| AppPackageName | string | The app package name                           |
| AppName        | string | The app name                                   |
| Type           | string | Merchant variable type, T(text) or P(password) |
| Key            | string | Merchant variable key                          |
| Value          | string | Merchant variable value                        |
| Remarks        | string | Comment                                        |
| Source         | String | Source type,M(Market) or C(Merchant)           |
| CreatedDate    | long   |                                                |
| UpdatedDate    | long   |                                                |



### Create a merchant variable

**API**

```
public Result<string> CreateMerchantVariable(MerchantVariableCreateRequest merchantVariableCreateRequest)
```

**Input parameter(s) description**

| Parameter Name                | Type                          | Nullable | Description                                         |
| :---------------------------- | :---------------------------- | :------- | :-------------------------------------------------- |
| merchantVariableCreateRequest | MerchantVariableCreateRequest | false    | the create request object, the structure like below |

Structure of class MerchantVariableCreateRequest

| Property Name | Type | Nullable|Description |
|:--- | :---|:---|:---|
| MerchantId   |long|False|the id of merchant|
|VariableList|List<ParameterVariable>|false|List of parameter variables, the structure like below|

Structure of class ParameterVariable

| Property Name | Type   | Nullable | Description                                                  |
| :------------ | :----- | :------- | :----------------------------------------------------------- |
| PackageName   | string | true     | The app package name                                         |
| Type          | string | true     | Merchant variable type, T(text) or P(password), default T when it is null |
| Key           | string | false    | Merchant variable key                                        |
| Value         | string | true     | Merchant variable value                                      |
| Remarks       | string | false    | Comment                                                      |

**Sample codes**

```
MerchantVariableApi api = new MerchantVariableApi(API_BASE_URL, API_KEY, API_SECRET);
long merchantId = 113781;
MerchantVariableCreateRequest createRequest = new MerchantVariableCreateRequest();
ParameterVariable parameterVariable1 = new ParameterVariable();
parameterVariable1.Key = "testCreateMerchantVariable-key1";
parameterVariable1.Value = "testCreateMerchantVariable-value1";
parameterVariable1.Remarks = "今日头条app testCreateApi3";
ParameterVariable parameterVariable2 = new ParameterVariable();
parameterVariable2.Key = "testCreateMerchantVariable-key2";
parameterVariable2.Value = "testCreateMerchantVariable-value2";
parameterVariable2.PackageName = "";
parameterVariable2.Remarks = "testCreateApi2";
List<ParameterVariable> variableList = new List<ParameterVariable>();
variableList.Add(parameterVariable1);
variableList.Add(parameterVariable2);
createRequest.MerchantId = merchantId;
createRequest.VariableList = variableList;
Result<string> createResult = api.CreateMerchantVariable(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Merchant variable is mandatory!"]
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

> <font color=red>Merchant variable is mandatory!</font>




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
| Type          | string | true     | Merchant variable type, T(text) or P(password) |
| Key           | string | false    | Merchant variable key                          |
| Value         | string | true     | Merchant variable value                        |
| Remarks       | string | true     | Comment                                        |

Note: updateRequest cannot be empty

**Sample codes**

```
MerchantVariableApi api = new MerchantVariableApi(API_BASE_URL, API_KEY, API_SECRET);
long merchantVariableId = 1000000008;
MerchantVariableUpdateRequest updateRequest = new MerchantVariableUpdateRequest();
updateRequest.Key = "testUpdateVariable-key1";
updateRequest.Value = "testUpdateVariable-value1";
updateRequest.Remarks = "updateRemarks1";
updateRequest.PackageName = "com.jbangit.csapp";
Result<string> updateResult = api.UpdateMerchantVariable(merchantVariableId,updateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter merchantCategoryId cannot be null and cannot be less than 1!"]
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
MerchantVariableApi api = new MerchantVariableApi(API_BASE_URL, API_KEY, API_SECRET);
long merchantVariableId = 1000000008L;
Result<string> deleteResult = api.DeleteMerchantVariable(merchantVariableId);
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



### Batch  delete merchant variable

**API**

```
public Result<string> BatchDeleteMerchantVariable(MerchantVariableDeleteRequest batchDeleteMerchantVariableRequest)
```

**Input parameter(s) description**

| Parameter Name       | Type                                   | Nullable | Description                                                 |
| :------------------- | :------------------------------------- | :------- | :---------------------------------------------------------- |
| batchDeleteMerchantVariableRequest | MerchantVariableDeleteRequest | false     | The batchDeletionRequest object. The structure shows below. |

Structure of class MerchantVariableDeleteRequest

| Property Name | Type       | Nullable | Description                     |
| :------------ | :--------- | :------- | :------------------------------ |
| VariableIds  | List\<long\> | false    | The ids of merchant variable id |

**Sample codes**

```
MerchantVariableApi api = new MerchantVariableApi(API_BASE_URL, API_KEY, API_SECRET);
MerchantVariableDeleteRequest batchDeletionRequest = new MerchantVariableDeleteRequest();
List<long> variableIds = new List<>();
variableIds.Add(1000000007);
variableIds.Add(1000000006);
batchDeletionRequest.VariableIds = variableIds;
Result<string> batchDeletionResult = merchantVariableApi.BatchDeleteMerchantVariable(batchDeletionRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["VariableIds cannot be empty!"]
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```

**Possible validation errors**

> <font color=red>Parameter batchDeleteMerchantVariableRequest is mandatory!</font>   
> <font color=red>VariableIds cannot be empty!</font> 



| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 113           | Your request is invalid, please try again or contact marketplace administrator |             |
| 13100         | Variable not found                                           |             |

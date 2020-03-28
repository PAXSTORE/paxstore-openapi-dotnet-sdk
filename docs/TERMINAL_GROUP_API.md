## TerminalGroup API

All APIs related to terminal grouping operations are encapsulated in the class *Paxstore.OpenApi.TerminalGroupApi*.

**Constructors of TerminalGroupApi**

```
public TerminalGroupApi(string baseUrl, string apiKey, string apiSecret)
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|



### Search terminal group

The search terminal group API allows third party system to Search by terminal group status or group name or resellers or models or dynamic group by page.

**API**

```
public Result<TerminalGroup> SearchTerminalGroup(int pageNo, int pageSize, Nullable<TerminalGroupSearchOrderBy> orderBy, Nullable<TerminalGroupStatus> status, string name, string resellerNames, string modelNames, Nullable<bool> isDynamic)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|Nullable<TerminalGroupSearchOrderBy>|true|the sort order of search result. If this parameter is null the search result will order by created date descend. The value of this parameter can be one of TerminalGroupSearchOrderBy.CreatedDate_desc and TerminalGroupSearchOrderBy.CreatedDate_asc and TerminalGroupSearchOrderBy.Name.|
|status|Nullable<TerminalGroupStatus>|true|The value of status can be one of TerminalGroupStatus.PENDING and TerminalGroupStatus.ACTIVE and TerminalGroupStatus.SUSPEND.|
|name|String|true|The name of group|
|resellerNames|String|true|The names of reseller. Multiple names can be separated by ','.                                                           For example, 'resellerName1,resellerName2'|
|modelNames|String|true|The names of model. Multiple names can be separated by ','.                                                           For example, 'modelName1,modelName2'|
|isDynamic|Nullable<bool>|true|Indicate whether to search dynamic group or general group only, if value is null will search both|


**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalGroup> result =  api.searchTerminalGroup(1,5, TerminalGroupSearchOrderBy.CreatedDate_asc, TerminalGroupStatus.ACTIVE,null,"test-8992","A920,E800",true);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["'Page Size' must be less than or equal to '1000'."]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2202,
	"Message": "Terminal group status is invalid"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 5,
		"TotalCount": 1,
		"HasNext": false,
		"DataSet": [{
			"TerminalCount": 0,
            "ResellerName": "test-8992",
            "Description": "reer",
            "UpdatedDate": 1577153179000,
            "ContainSubResellerTerminal": false,
            "ModelName": "A920",
            "CreatedDate": 1574751213000,
            "MerchantNames": ["test-01"],
            "Name": "3RD-Dy-create",
            "Dynamic": true,
            "Id": 16531,
            "Status": "A",
            "CreatedByResellerId": 2
		}]
	}
}
```

The type in dataSet is TerminalGroup. And the structure like below.

| Name                       | Type         | Description                                                  |
| :------------------------- | :----------- | :----------------------------------------------------------- |
| Id                         | long         | the id of terminal group                                     |
| ResellerName               | string       | the name of reseller                                         |
| ModelName                  | string       | the name of model                                            |
| Name                       | string       | the name of terminal group                                   |
| Status                     | string       | the status of terminal group,value can be one of A(Active) and S(Suspend) and P(Pending) |
| Description                | string       | Description of terminal group                                |
| CreatedByResellerId        | long         | the id of the reseller that created the terminal group       |
| CreatedDate                | long         |                                                              |
| UpdatedDate                | long         |                                                              |
| TerminalCount              | int          | Number of terminals in the terminal group                    |
| Dynamic                    | bool      | Dynamic group or general group                               |
| ContainSubResellerTerminal | bool      | Include sub resellers or not                                 |
| merchantNames              | List<String> | the merchant names                                           |

**Possible validation errors**

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font> 

**Possible business codes**

| Business Code | Message                          | Description |
| :------------ | :------------------------------- | :---------- |
| 2202          | Terminal group status is invalid |             |


### Get terminal group

Get terminal group by id.  

**API**

```
public Result<TerminalGroup> GetTerminalGroup(long groupId)
```

**Input parameter(s) description**

| Parameter Name | Type | Nullable | Description              |
| :------------- | :--- | :------- | :----------------------- |
| groupId        | long | false    | the id of terminal group |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalGroup> result = api.GetTerminalGroup(groupId);
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
	"Data": {
		"ModelName": "A920",
		"CreatedDate": 1574751213000,
		"TerminalCount": 0,
		"ResellerName": "test-8992",
		"Name": "3RD-create",
		"Description": "reer",
		"Dynamic": true,
		"Id": 16531,
		"UpdatedDate": 1577153179000,
		"ContainSubResellerTerminal": false,
		"Status": "A",
		"CreatedByResellerId": 2
	}
}
```

The type in data is TerminalGroup. 



**Possible business codes**

| Business Code | Message                  | Description |
| :------------ | :----------------------- | :---------- |
| 2150          | Terminal group not found |             |

### Create a terminal group

**API**

```
public Result<TerminalGroup> CreateTerminalGroup(CreateTerminalGroupRequest createTerminalGroupRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|createTerminalGroupRequest|CreateTerminalGroupRequest|false|the create request object, the structure like below|

Structure of class CreateTerminalGroupRequest

| Property Name              | Type         | Nullable | Description                                                  |
| :------------------------- | :----------- | :------- | :----------------------------------------------------------- |
| Name                       | string       | false    | the name of group                                          |
| modelName                  | string       | false    | the model name, only the same model termina can be add to this group                              |
| resellerName               | string       | false    | reseller name, only the terminals in this reseller can be add to this group          |
| description                | string       | true     |                                                              |
| status                     | string       | true     | the status of terminal group,the values can be 'P' and 'A', if the value is null will create group with default status P(Pending) |
| dynamic                    | Nullable<bool>      | true     | Indicate whether the group is dynamic group or general group, the default value is false (general group)                            |
| containSubResellerTerminal | Nullable<bool>      | true     | Indicate whether to conatin sub reseller's termnal for dynamic group, this property is for dynamic group, if the value is null will use the default value false                                 |
| merchantNameList           | List<string> | true     | merchant names, only terminals belong to those merchant can be add to group, this property is for dynamic group                                                            |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
  CreateTerminalGroupRequest createRequest = new CreateTerminalGroupRequest();
  createRequest.Name = "test-create";
  createRequest.ModelName = "A920";
  createRequest.ResellerName = "test-8992";
  createRequest.Description = "TEST";
  createRequest.Status = "P";
  createRequest.ContainSubResellerTerminal = false;
  List<string> merchantNames = new List<string>();
  merchantNames.Add("test-01");
  merchantNames.Add("test-02");
  createRequest.MerchantNameList = merchantNames;
  createRequest.Dynamic = true;
Result<TerminalGroup> result = terminalGroupApi.CreateTerminalGroup(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["Parameter createTerminalGroupRequest is mandatory!"]
}
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1759,
	"Message": "Reseller doesn't exist"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"TerminalCount": 0,
		"ResellerName": "test-8992",
		"Description": "TEST",
		"UpdatedDate": 1583217982007,
		"ContainSubResellerTerminal": false,
		"ModelName": "A920",
		"CreatedDate": 1583217982007,
		"MerchantNames": ["test-01","test-02"],
		"Name": "test-create",
		"Dynamic": true,
		"Id": 16583,
		"Status": "P",
		"CreatedByResellerId": 2
	}
}
```

The type in data is TerminalGroup. 


**Possible client validation errors**


> <font color="red">Parameter createTerminalGroupRequest is mandatory!</font>

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|1759| Reseller doesn't exist                       ||
|1762| Reseller name is mandatory                   ||
|1720| Merchant doesn't exist                       ||
|1937| Merchant is not belong to the given Reseller ||
|1700| Model doesn't exist                          ||
|2151| Terminal group name is mandatory             ||
|2152| Terminal group model is mandatory            ||
|2153| Terminal group reseller is mandatory         ||
|2154| Terminal group name is too long              ||
|2156| Terminal group name already exists           ||
|2161| Terminal group description is too long       ||
|1854| The type of merchant id  is wrong            ||
|1720| Merchant doesn't exist                       ||
|1737| The associated merchant is not activate      ||
|1713| The associated model is not activate         ||
|1773| The associated reseller is not activate      ||




### Search terminal

This API is provided to the third party system to add terminals and search terminals in the terminal group.


**API**

```
public Result<Terminal> SearchTerminal(int pageNo, int pageSize, Nullable<TerminalSearchOrderBy> orderBy, Nullable<TerminalStatus> status, string modelName, string resellerName, string merchantName, string serialNo, Nullable<bool> excludeGroupId)
```

**Input parameter(s) description**  

| Parameter Name | Type                              | Nullable | Description                                                  |
| :------------- | :-------------------------------- | :------- | :----------------------------------------------------------- |
| pageNo         | int                               | false    | page number, value must >=1                                  |
| pageSize       | int                               | false    | the record number per page, range is 1 to 1000               |
| orderBy        | Nullable<TerminalSearchOrderBy>   | true     | the sort order by of search result, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of TerminalSearchOrderBy.Name and TerminalSearchOrderBy.Tid and TerminalSearchOrderBy.SerialNo. |
| status         | Nullable<TerminalStatus>                    | true     | Terminal status. The value can be one of TerminalStatus.Active and TerminalStatus.Inactive and TerminalStatus.Suspend |
| modelName      | string                            | true     | the model name                                               |
| resellerName   | string                            | true     | the reseller name                                            |
| serialNo       | string                            | true     | the serial number of terminal                                |
| excludeGroupId | string                            | true     | group id which the terminals in this group will be excluded from search result. This search parameter does not support dynamic group|

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<Terminal> result = api.SearchTerminal(1,5, TerminalSearchOrderBy.Name, TerminalStatus.Active,"A920","test-8992",null,null);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["'Page Size' must be less than or equal to '1000'."]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2203,
	"Message": "Terminal status is invalid"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 5,
		"TotalCount": 2,
		"HasNext": false,
		"Dataset": [{
			"ModelName": "A920",
			"ResellerName": "test-8992",
			"Name": "12312SSASAQSAQWS",
			"Location": "",
			"ID": 909753,
			"TID": "12312SSASAQSAQWS",
			"SerialNo": "12312SSASAQSAQWS",
			"Status": "A",
			"MerchantName": "merchant a"
		}, {
			"ModelName": "A920",
			"ResellerName": "test-8992",
			"Name": "test8992",
			"Location": "",
			"ID": 909744,
			"TID": "DONP3PIU",
			"SerialNo": "1223131",
			"Status": "A",
			"MerchantName": "merchant b"
		}]
	}
}
```

The type in dataSet is Terminal. The structure of this object please refer to TerminalApi. And the property GeoLocation, InstalledFirmware and InstalledApks are null in search result.


**Possible validation errors**

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>



### Update terminal group

This api allows the third party system update terminal group.


**API**

```
public Result<TerminalGroup> UpdateTerminalGroup(long groupId, UpdateTerminalGroupRequest updateTerminalGroupRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|groupId|long|false|The id of terminal group|
|updateTerminalGroupRequest|UpdateTerminalGroupRequest|false||


Structure of class UpdateTerminalGroupRequest

|Property Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|Name|string|true|the name of terminal group, if the value is null API won't update the origial value of this property|
|Description|string|true|the description of terminal group|
|ModelName|string|true| the name of model, if the value is null API won't update the origial value of this property|
|ResellerName|string|true|the name of reseller, if the value is null API won't update the origial value of this property|
|MerchantNameList|List<string>|true|the name of merchants|

Note: name, description, modelName, resellerName, and merchantNameList cannot be empty at same time. When it is not inactive, only name and description can be modified.

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
long groupId = 16576;
UpdateTerminalGroupRequest updateRequest = new UpdateTerminalGroupRequest();
updateRequest.Name= "3rdsUpdateTerminalGroupName";
List<string> merchantNamesList = new List<string>();
merchantNamesList.Add("testDelete6");
merchantNamesList.Add("12348");
updateRequest.MerchantNameList = merchantNamesList;
updateRequest.ModelName = "E800";
updateRequest.ResellerName = "Shawn-test-8992";
updateRequest.Description = "test-3rd-api-update-UPDATE";
Result<TerminalGroup> result = api.UpdateTerminalGroup(groupId, updateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter updateTerminalGroupRequest is mandatory!"]
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
	    "ModelName": "E800",
		"CreatedDate": 1583214740000,
		"TerminalCount": 0,
		"ResellerName": "Shawn-test-8992",
		"Name": "3rdsUpdateTerminalGroupName",
		"Description": "test-3rd-api-update-UPDATE",
		"Dynamic": false,
		"Id": 16576,
		"UpdatedDate": 1583375601713,
		"ContainSubResellerTerminal": true,
		"Status": "P",
		"CreatedByResellerId": 2
	}
}
```

**Possible validation errors**

> <font color=red>Parameter updateTerminalGroupRequest is mandatory!</font>  

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2150|Terminal group not found||
|135|Request parameter is missing or invalid||
|1700|Model doesn't exist||
|1759|Reseller doesn't exist||
|1720|Merchant doesn't exist||
|1737|The associated merchant is not activate||
|1813|Push task has already been added, unable to update model||
|1814|Push task has already been added,unable to update reseller||
|1815|Terminal has already been added,unable to update model||
|1816|Terminal has already been added,unable to update reseller||
|1833|Push task has already been added, unable to update merchant||
|1834|Terminal has already been added,unable to update merchant||
|1937|Merchant is not belong to the given Reseller||
|2156|Terminal group name already exists||
|13102|Terminal group is not inactive, only name and description can be modified||



### Active group

This api allows the third party system active terminal group.

**API**

```
public Result<string> ActiveGroup(long groupId)
```

**Input parameter(s) description**  

| Parameter Name | Type | Nullable | Description              |
| :------------- | :--- | :------- | :----------------------- |
| groupId        | long | false    | The id of terminal group |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = api.ActiveGroup(16549);
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
	"BusinessCode": 0
}
```

**Possible business codes**

| Business Code | Message                                 | Description |
| :------------ | :-------------------------------------- | :---------- |
| 2150          | Terminal group not found                |             |
| 1700          | Model doesn't exist                     |             |
| 1713          | The associated model is not activate    |             |
| 1759          | Reseller doesn't exist                  |             |
| 1773          | The associated reseller is not activate |             |
| 2158          | Terminal group has been activated       |             |



### Disable group

This api allows the third party system disable terminal group.

**API**

```
public Result<string> DisableGroup(long groupId)
```

**Input parameter(s) description**  

| Parameter Name | Type | Nullable | Description              |
| :------------- | :--- | :------- | :----------------------- |
| groupId        | long | false    | The id of terminal group |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = api.DisableGroup(16549);
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
	"BusinessCode": 0
}
```

**Possible business codes**

| Business Code | Message                               | Description |
| :------------ | :------------------------------------ | :---------- |
| 2150          | Terminal group not found              |             |
| 2155          | Terminal group has been not activated |             |



### Delete group

This api allows the third party system delete terminal group

**API**

```
public Result<String> DeleteGroup(long groupId)
```

**Input parameter(s) description**  

| Parameter Name | Type | Nullable | Description              |
| :------------- | :--- | :------- | :----------------------- |
| groupId        | long | false    | The id of terminal group |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = api.DeleteGroup(16549L);
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
	"BusinessCode": 0
}
```

**Possible business codes**

| Business Code | Message                                       | Description |
| :------------ | :-------------------------------------------- | :---------- |
| 2150          | Terminal group not found                      |             |
| 2165          | The terminal group is active,unable to delete |             |



### Search terminals in group

This API is provided to the third party system to search terminals in the current terminal group.

**API**

```
public Result<Terminal> SearchTerminalsInGroup(int pageNo, int pageSize, Nullable<TerminalSearchOrderBy> orderBy,long groupId, string serialNo, string merchantNames)
```

**Input parameter(s) description**  

| Parameter Name | Type                              | Nullable | Description                                                  |
| :------------- | :-------------------------------- | :------- | :----------------------------------------------------------- |
| pageNo         | int                               | false    | page number, value must >=1                                  |
| pageSize       | int                               | false    | the record number per page, range is 1 to 1000               |
| orderBy        | Nullable<TerminalSearchOrderBy> | true     | the sort order of search result, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of TerminalSearchOrderBy.Name and TerminalSearchOrderBy.Tid and TerminalSearchOrderBy.SerialNo. |
| groupId        | long                              | false     | the id of terminal group                                     |
| serialNo       | string                            | true     | the serial number of terminal                                |
| merchantNames  | string                            | true     | the name of merchants                                        |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<Terminal> result = api.SearchTerminalsInGroup(1,5, TerminalSearchOrderBy.SerialNo,16541,null,"12343543,123445489");
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
	"PageInfo": {
		"PageNo": 1,
		"Limit": 2,
		"TotalCount": 28,
		"HasNext": true,
		"Dataset":  [{
		"ModelName": "A920",
		"resellerName": "GLOBAL",
		"Name": "ET",
		"ID": 908629,
		"TID": "QAOEF6BX",
		"SerialNo": "90010001",
		"Status": "A",
		"MerchantName": "12343543"
	}, {
		"ModelName": "A920",
		"ResellerName": "GLOBAL",
		"Name": "ET",
		"ID": 908630,
		"TID": "1D6VP0U7",
		"SerialNo": "90010002",
		"Status": "A",
		"MerchantName": "12343543"
	}]}
}
```

The type in dataSet is Terminal. And the structure please refer to TerminalApi.


**Possible validation errors**

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>

**Possible business codes**

| Business Code | Message                                     | Description |
| :------------ | :------------------------------------------ | :---------- |
| 1835          | Dynamic group cannot be queried by merchant |             |
| 2150          | Terminal group not found                    |             |



### Add terminal to group

This api allows the third party system add terminal to group.

**API**

```
public Result<string> AddTerminalToGroup(long groupId, HashSet<long> terminalIds)
```

**Input parameter(s) description**  

| Parameter Name | Type      | Nullable | Description                                    |
| :------------- | :-------- | :------- | :--------------------------------------------- |
| groupId        | long      | false    | The id of terminal group                       |
| terminalIds    | HashSet<long> | false    | Terminal ids to be added to the terminal group |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
long groupId = 1;
HashSet<long> terminalIds = new HashSet<long>();
terminalIds.Add(908654);
terminalIds.Add(908655);
terminalIds.Add(908656);
Result<string> result = api.AddTerminalToGroup(groupId, terminalIds);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["Parameter terminalIds is mandatory!"]
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
	"BusinessCode": 0
}
```

**Possible business codes**

| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 1800          | Terminal not found                                           |             |
| 1810          | Terminal is not active                                       |             |
| 2150          | Terminal group not found                                     |             |
| 2163          | Terminal reseller mismatched                                 |             |
| 2164          | Terminal model mismatched                                    |             |
| 2167          | Terminal group exceeded the max terminal count limit, please create new terminal group to put the terminal |             |
| 2184          | Terminal Ids is mandatory                                    |             |
| 2173          | Dynamic group can not add terminals                          |             |

### Remove terminal out group

This api allows the third party system remove terminal out group.

**API**

```
public Result<string> RemoveTerminalOutGroup(long groupId, HashSet<long> terminalIds)
```

**Input parameter(s) description**  

| Parameter Name | Type      | Nullable | Description                                    |
| :------------- | :-------- | :------- | :--------------------------------------------- |
| groupId        | long      | false    | The id of terminal group                       |
| terminalIds    | HashSet<long> | false    | terminal ids to be added to the terminal group |

**Sample codes**

```
TerminalGroupApi api = new TerminalGroupApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
long groupId = 1;
HashSet<long> terminalIds = new HashSet<long>();
terminalIds.Add(908654);
terminalIds.Add(908655);
terminalIds.Add(908656);
Result<string> result = api.RemoveTerminalOutGroup(groupId, terminalIds);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter terminalIds is mandatory!"]
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
	"BusinessCode": 0
}
```

**Possible business codes**

| Business Code | Message                                | Description |
| :------------ | :------------------------------------- | :---------- |
| 2150          | Terminal group not found               | 1           |
| 2184          | Terminal Ids is mandatory              | 1           |
| 2174          | Dynamic group can not delete terminals | 1           |
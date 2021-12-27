## TerminalGroupRki API

Group RKI(remote key injection) related APIs

**Constructors of TerminalGroupRkiApi**

```
public TerminalGroupRkiApi(string baseUrl, string apiKey, string apiSecret)
```

**Constructor parameters description**

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Search Group Push Rki Task

The search group push rki task API allows third party system to search pushed rki task list to the specified group by page.

**API**

```
public Result<TerminalGroupRkiTask> SearchGroupPushRkiTask(int pageNo, int pageSize, Nullable<SearchOrderBy> orderBy, long groupId, Nullable<bool> pendingOnly, Nullable<bool> historyOnly, string keyWords)
```

**Input parameter(s) description**

| Name        | Type          | Nullable | Description                                                  |
| :---------- | :------------ | :------- | :----------------------------------------------------------- |
| pageNo      | int           | false    | page number, value must >=1                                  |
| pageSize    | int           | false    | the record number per page, range is 1 to 100                |
| orderBy     | Nullable<SearchOrderBy> | true     | the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of SearchOrderBy.CreatedDate_desc and SearchOrderBy.CreatedDate_asc. |
| groupId     | long          | false    | the id of the group                                          |
| pendingOnly | Nullable<bool>       | true     | Indicate whether to search the pending push task only        |
| historyOnly | Nullable<bool>       | true     | Indicate whether to search history push task only            |
| keyWords    | string        | true     | Key words， it will match rkiKey                             |

**Sample codes**

```
TerminalGroupRkiApi terminalGroupRkiApi = new TerminalGroupRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalGroupRkiTask> result = terminalGroupRkiApi.SearchGroupPushRkiTask(
1,12,SearchOrderBy.CreatedDate_desc,null, true, null);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ['Page Size' must be less than or equal to '100'.]
}
```

**Successful sample result**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 12,
		"TotalCount": 2,
		"HasNext": false,
		"DataSet": [{
				"Id": 5,
                "RkiKey": "TEST_KEY1",
                "ActivatedDate": 1632475214000,
                "EffectiveTime": 1632475200000,
                "Status": "S",
                "ActionStatus": 1,
                "ErrorCode": 0,
                "PendingCount": 0,
                "SuccessCount": 0,
                "FailedCount": 1,
                "PushLimit": 0
		},{
                "Id": 4,
                "RkiKey": "TEST_KEY2",
                "ActivatedDate": 1632475179000,
                "EffectiveTime": 1632475140000,
                "Status": "S",
                "ActionStatus": 1,
                "ErrorCode": 0,
                "PendingCount": 0,
                "SuccessCount": 0,
                "FailedCount": 1,
                "PushLimit": 0
		}]
	}
}
```

The type in dataSet is TerminalGroupRkiTask. And the structure like below.

| Name          | Type    | Description                                                  |
| :------------ | :------ | :----------------------------------------------------------- |
| Id            | long    | the id of group push Rki record                              |
| RkiKey        | string  | the key of RKI                                               |
| Status        | string  | the status of push Rki, value can be one of A(Active) and S(Suspend) |
| ActionStatus  | string  | the action status, please refer to [Action Status](APPENDIX.md#action-status) |
| ActivatedDate | Nullable<long>    | the timestamp of the push Rki activated date                                  |
| EffectiveTime | Nullable<long>    | the timestamp of the push Rki effective date                                  |
| Remarks       | String  | the push Rki result remarks                                  |
| ErrorCode     | int     | the error code, please refer to [Action Error Codes](APPENDIX.md#action-error-codes) |
| PendingCount  | int     |                                                              |
| SuccessCount  | int     |                                                              |
| FailedCount   | int     |                                                              |
| Completed     | bool | push complete                                                |
| PushLimit     | int     | the push limit count                                         |

**Possible client validation errors**  

> <font color="red">'Page Size' must be less than or equal to '100'.</font>  
> <font color="red">'Page No' must be greater than '0'.</font>  
> <font color="red">'Page Size' must be greater than '0'.</font>  





### Get group push rki task by id

Get group push rki task by id.

**API**

```
public Result<TerminalGroupRkiTask> GetGroupPushRkiTask(long groupRkiPushTaskId)
```

**Input parameter(s) description**

| Parameter Name     | Type | Nullable | Description                   |
| :----------------- | :--- | :------- | :---------------------------- |
| groupPushRkiTaskId | long | false    | the id of group push rki task |

**Sample codes**

```
TerminalGroupRkiApi terminalGroupRkiApi = new TerminalGroupRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalGroupRkiTask> result = terminalGroupRkiApi.GetGroupPushRkiTask(6);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 18002,
	"Message": "RKI push task not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
            "Id": 6,
            "RkiKey": "TEST_8",
            "ActivatedDate": 1632648236000,
            "EffectiveTime": 1632648180000,
            "Status": "A",
            "ActionStatus": 1,
            "ErrorCode": 0,
            "PendingCount": 1,
            "SuccessCount": 0,
            "FailedCount": 0,
            "Completed": false,
            "PushLimit": 0
		}
}
```

<br>

The type of data is TerminalGroupRkiTask, and Refer to search group push rki task Api for structure .


**Possible business codes**

| Business Code | Message                     | Description |
| :------------ | :-------------------------- | :---------- |
| 2051          | Terminal Rki task not found | &nbsp;      |

**Possible action status**

| action status | status  | Description                            |
| :------------ | :------ | :------------------------------------- |
| 0             | None    | The push task no start                 |
| 1             | Pending | The push task staring                  |
| 2             | Succeed | The push task is succeed               |
| 3             | Failed  | The push task is failed                |
| 4             | Watting | The push task is watting, no need push |

**Possible error codes**

| Error Code | Description                     |
| :--------- | :------------------------------ |
| 1          | Download error                  |
| 2          | Install error                   |
| 12         | The push is disabled            |
| 27         | Unable To Bind Terminal RKI Key |



### Push rki to group

Push rki to group API allow the third party system push a Rki to group.

**API**

```
Result<TerminalGroupRkiTask> PushRkiKey2Group(CreateTerminalGroupRkiTaskRequest createTerminalGroupRkiTaskRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|createTerminalGroupRkiTaskRequest|CreateTerminalGroupRkiTaskRequest|false|The create request object. The structure shows below.|


Structure of class CreateTerminalGroupRkiRequest

|Property Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|GroupId|long|false|The id of group|
|RkiKey|string|false|The rki key which indicate you want to push RKI Key template to the terminal|

**Sample codes**

```
TerminalGroupRkiApi terminalGroupRkiApi = new TerminalGroupRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
CreateTerminalGroupRkiTaskRequest createRequest = new CreateTerminalGroupRkiTaskRequest();
createRequest.GroupId = 16601;
createRequest.RkiKey = "TEST_8";
Result<TerminalGroupRkiTask> result = terminalGroupRkiApi.PushRkiKey2Group(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter RkiKey is mandatory!"]
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
            "Id": 6,
            "RkiKey": "TEST_8",
            "ActivatedDate": 1632648236000,
            "EffectiveTime": 1632648180000,
            "Status": "A",
            "ActionStatus": 1,
            "ErrorCode": 0,
            "PendingCount": 1,
            "SuccessCount": 0,
            "FailedCount": 0,
            "Completed": false,
            "PushLimit": 0
	}
}
```


**Possible validation errors**

> <font color=red>Parameter GroupId must grate than 0!</font>
> <font color=red>Parameter RkiKey is mandatory!</font>  

<br>

The type of data is TerminalGroupRkiTask, and Refer to search group push rki task Api for structure .


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|131|Insufficient access right||
|2150|Terminal group not found|&nbsp;|
|2060|Failed to activate push task: PAX Auth System Connect Error||
|9293|Reseller RKI user token is not ready||
|18002|RKI push task not found||
|18004|Pending RKI push task already exist|&nbsp;|
|18005|Active RKI push task already exist|&nbsp;|
|18006|Invalid RKI push task status||



### Disable Rki push by group push rki task id

This api allows the third Party system disable an exist push by  group push rki task id.

**API**

```
public Result<TerminalGroupRkiTask> DisableGroupRkiPushTask(long groupRkiPushTaskId)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|groupRkiPushTaskId|long|false|the id of group push rki task|

**Sample codes**

```
TerminalGroupRkiApi terminalGroupRkiApi = new TerminalGroupRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalGroupRkiTask> result = terminalGroupRkiApi.DisableGroupRkiPushTask(6);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 18002,
	"Message": "RKI push task not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"Id": 6,
		"RkiKey": "TEST_8",
		"ActivatedDate": 1632648236000,
		"EffectiveTime": 1632648180000,
		"Status": "S",
		"ActionStatus": 1,
		"ErrorCode": 0,
		"PendingCount": 0,
		"SuccessCount": 0,
		"FailedCount": 0,
		"PushLimit": 0
	}
}
```



**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|18002|RKI push task not found||
|18003|RKI push task not active||

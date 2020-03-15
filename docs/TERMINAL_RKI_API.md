## TerminalRki API

The push RKI to terminal related APIs are encapsulated in the class *Paxstore.OpenApi.TerminalRkiApi*.

**Constructors of TerminalRkiApi**

```
public TerminalRkiApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

|Name|Type|Description|
|:----|:----|:----|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Push Rki

Push Rki API allow the third party system push a Rki to terminal.


**API**

```
public Result<TerminalRkiTaskInfo> PushRkiKey2Terminal(PushRki2TerminalRequest pushRki2TerminalRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pushRki2TerminalRequest|PushRki2TerminalRequest|false|The push Rki request object. The structure shows below.|


Structure of class PushRki2TerminalRequest

|Property Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|Tid|string|true|The tid of terminal|
|SerialNo|string|true|The serial number of terminal|
|RkiKey|string|false|The rki key which indicate you want to push RKI Key template to the terminal|

Note: tid and serialNo cannot be empty at same time.


**Sample codes**

```
TerminalRkiApi terminalRkiApi = new TerminalRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
PushRki2TerminalRequest pushRki2TerminalRequest = new PushRki2TerminalRequest();
pushRki2TerminalRequest.Tid="ABC09098989";
pushRki2TerminalRequest.RkiKey="PIN_TEST";
terminalRkiApi.PushRki2Terminal(pushRki2TerminalRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Rkikey is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2028,
	"Message": "Terminal not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
        "id": 51741,	
    }
}
```


**Possible validation errors**

> <font color=red>Parameter pushRki2TerminalRequest is mandatory!</font>  
> <font color=red>SerialNo or Tid is mandatory!</font> 
> <font color=red>Rkikey is mandatory!</font> 


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2028|Terminal not found|Please check the value of tid or serialNo|
|2056|The rkiKey cannot be empty|&nbsp;|
|2026|Tid and serialNo cannot empty at same time|&nbsp;|
|9293|Reseller RKI user token is not ready|&nbsp;|
|2053|Pending push RKI task for this terminal already exists|&nbsp;|
|2054|Active push RKI task for this terminal already exists|&nbsp;|


### Search Rki push history

The search Rki push history API allows third party system to search pushed rki task list to the specified terminal by page.
**API**

```
public Result<TerminalRkiTaskInfo> SearchPushRkiTasks(int pageNo, int pageSize, Nullable<SearchOrderBy> orderBy,
                                                                   string terminalTid, string rkiKey, PushStatus status
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|SearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of SearchOrderBy.CreatedDate_desc and SearchOrderBy.CreatedDate_asc.|
|terminalTid|string|false|search filter by terminal tid|
|rkiKey|string|true|search filter by rki key|
|status|PushStatus|false|the push status<br/> the value can be PushStatus.Active, PushStatus.Suspend, PushStatus.All|

**Sample codes**

```
TerminalRkiApi terminalRkiApi = new TerminalRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalRkiTaskInfo> result = terminalRkiApi.SearchPushRkiTasks(1,12,SearchOrderBy.CreatedDate_desc,
                                terminalTid, rkiKey, PushStatus.Active);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter terminalTid is mandatory!"]
}
```

**Successful sample result**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 12,
		"TotalCount": 1,
		"HasNext": false,
		"DataSet": [{
			"Id": 17850,
            "RkiKey": "PIN_TEST",
            "TerminalSN": "87879696",
            "Status": "A",
            "ActionStatus": 2,
            "ErrorCode": ""
		}]
	}
}
```

The type in DataSet is TerminalRkiTaskInfo. And the structure like below.

|Name|Type|Description|
|:---|:---|:---|
|Id|long|the id of push Rki record|
|RkiKey|string|the key of RKI|
|TerminalSN|string|the serialNo of terminal|
|Status|string|the status of push Rki, value can be one of A(Active) and S(Suspend)|
|ActionStatus|string|the push status, 2(Success), 3(Failed)|
|ActivatedDate|long|timestamp(milliseconds) of the push Rki activated date|
|ErrorCode|string|the error code of push task|
|Remarks|string|the push Rki result remarks|

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
|12|The push is disabled|
|27|Unable To Bind Terminal RKI Key|

**Possible client validation errors**  

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>
> <font color="red">Parameter terminalTid is mandatory!  


### Get push Rki history by id

Get terminal push Rki history by id.


**API**

```
public Result<TerminalRkiTaskInfo> GetPushRkiTask(long pushRkiTaskId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pushRkiTaskId|long|false|the id of push Rki|

**Sample codes**

```
TerminalRkiApi terminalRkiApi = new TerminalRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalRkiTaskInfo> result = terminalRkiApi.GetPushRkiTask(17850);
```



**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2051,
	"Message": "Terminal Rki task not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"Id": 17850,
        "RkiKey": "PIN_TEST",
        "TerminalSN": "87879696",
        "Status": "A",
        "ActionStatus": 2,
        "ErrorCode": ""
	}
}
```

<br>
The type of data is TerminalRkiTaskInfo


**Possible client validation errors**


> <font color="red">Parameter pushRkiTaskId cannot be null and cannot be less than 1!</font>


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2051|Terminal Rki task not found|&nbsp;|




### Disable Rki push by serial number(TID) and Rki name

This api allows the third Party system disable an exist push by specifying the serial number of terminal and the Rki name. 


**API**

```
public Result<String> DisablePushRkiTask(DisablePushRkiTaskRequest disablePushRkiTaskRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|disablePushRkiTaskRequest|DisablePushRkiTaskRequest|false|The disable request object. The structure shows below.|


Structure of class DisablePushRkiTask

|Property Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|Tid|string|true|The tid of terminal|
|SerialNo|string|true|The serial number of terminal|
|RkiKey|string|false|The rki key which indicate you want to suspend the terminal push rki task|

Note: tid and serialNo cannot be empty at same time.


**Sample codes**

```
TerminalRkiApi terminalRkiApi = new TerminalRkiApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
DisablePushRkiTaskRequest disablePushRkiTaskRequest = new DisablePushRkiTaskRequest();
disablePushRkiTaskRequest.Tid="ABC09098989";
disablePushRkiTaskRequest.RkiKey="PIN_TEST";
terminalRkiApi.DisablePushRkiTask(disablePushRkiTaskRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Rkikey is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2057,
	"Message": "Unfinished terminal push Rki not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```


**Possible validation errors**

> <font color=red>Parameter disablePushRkiTaskRequest is mandatory!</font>  
> <font color=red>Rkikey is mandatory!</font> 
> <font color=red>SerialNo or Tid is mandatory!</font>


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2028|Terminal not found|Please check the value of tid or serialNo|
|2026|Tid and serialNo cannot empty at same time||
|2056|The rkiKey cannot be empty||
|2057|Unfinished terminal push Rki not found|

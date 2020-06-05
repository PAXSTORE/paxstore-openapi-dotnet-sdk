## TerminalFirmware API

All the terminalFirmware related APIs are encapsulated in the class *Paxstore.OpenApi.TerminalFirmwareApi*.

**Constructors of TerminalFirmwareApi**

```
public TerminalFirmwareApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

| Name | Type | Description |
| :--- | :--- | :--- |
| baseUrl | string | the base url of REST API |
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Push firmware to terminal by TID and firmware name

This API allows the thirdparty system to push a firmware to a terminal


**API**

```
public Result<string> PushFirmware2TerminalByTidAndFirmwareName(string tid, string firmwareName)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|tid|string|false|The TID of terminal|
|firmwareName|string|false|The firmware name|




**Sample codes**

```
TerminalFirmwareApi terminalFirmwareApi = new TerminalFirmwareApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
terminalFirmwareApi.PushFirmware2TerminalByTidAndFirmwareName("ABC09098989", "A920_PayDroid_4.4.2_Capricorn_V01.1.10_20171226_OTA");
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter tid is mandatory!"]
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
	"BusinessCode": 0
}
```



**Possible validation errors**

> <font color=red>Parameter tid is mandatory!</font>  
> <font color=red>Parameter firmwareName is mandatory!</font> 



**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2028|Terminal not found|Please check the value of tid or serialNo|
|2033|Firmware name cannot be empty|&nbsp;|
|2034|Firmware not found|Cannot find firmware by fmName|
|2035|Firmware status not online|&nbsp;|
|2036|Firmware model mismatch with terminal model|&nbsp;|
|2026|Tid and serialNo cannot empty at same time|The parameter tid is empty, please use a valid tid|
|8112|Same version of pending terminal firmware already exists|&nbsp;|
|8113|Same version of active terminal firmware already exists|&nbsp;|


### Push firmware to terminal by serial number and firmware name

The function of this API is same as above one


**API**

```
public Result<string> PushFirmware2TerminalBySnAndFirmwareName(string serialNo, string firmwareName)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|serialNo|string|false|The serial number of terminal|
|firmwareName|string|false|The firmware name|



### Search firmware push history

The search firmware push history API allows third party system to search pushed firmwares to the specified terminal by page.  
**API**

```
public Result<PushFirmwareTaskInfo> SearchPushFirmwareTasks(int pageNo, int pageSize, SearchOrderBy orderBy,
                                                               string terminalTid, string fmName, PushStatus status)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|SearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of SearchOrderBy.CreatedDate_desc and SearchOrderBy.CreatedDate_asc.|
|terminalTid|string|false|search filter by terminal tid|
|fmName|string|true|search filter by firmware name|
|status|PushStatus|true|the push status<br/> the value can be PushStatus.Active, PushStatus.Suspend, PushStatus.All|

**Sample codes**

```
TerminalFirmwareApi terminalFirmwareApi = new TerminalFirmwareApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
terminalFirmwareApi.SearchPushFirmwareTasks(1, 10, SearchOrderBy.CreatedDate_desc, "I1TF6LA2", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012", PushStatus.All);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["pageNo:must be greater than or equal to 1"]
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
			"ID": 17850,
            "FmName": "PayDroid_5.1.1_Aquarius_V09.0.00_20190508",
            "TerminalSN": "87879696",
            "Status": "A",
            "ActionStatus": 2,
            "ErrorCode": ""
		}]
	}
}
```

The type in dataSet is PushFirmwareTaskInfo. And the structure like below.

|Name|Type|Description|
|:---|:---|:---|
|ID|Long|the id of push firmware record|
|FmName|string|the name of firmware|
|TerminalSN|string|the serialNo of terminal|
|Status|string|the status of push firmware, value can be one of A(Active) and S(Suspend)|
|ActionStatus|string|the push status|
|ErrorCode|string|the error code of push task|
|ActivatedDate|long|the push firmware activated date|

**Possible client validation errors**  

> <font color=red>pageNo:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be less than or equal to 1000</font>  

### Get push firmware history by id

Get terminal push firmware history by id.


**API**

```
public Result<PushFirmwareTaskInfo> GetPushFirmwareTask(long pushFirmwareTaskId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|pushFirmwareTaskId|Long|false|the task id of push firmware|

**Sample codes**

```
TerminalFirmwareApi terminalFirmwareApi = new TerminalFirmwareApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
terminalFirmwareApi.GetPushFirmwareTask(1000012895);
```



**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 8108,
	"message": "Terminal firmeware not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0,
	"data": {
		"id": 17850,
        "fmName": "PayDroid_5.1.1_Aquarius_V09.0.00_20190508",
        "terminalSN": "87879696",
        "status": "A",
        "actionStatus": 2,
        "errorCode": ""
	}
}
```

<br>






**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|8101|Terminal firmware not found|&nbsp;|

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
|13|The firmware is duplicate|
|14|The firmware status invalid|
|15|The firmware model pn mismatch with terminal|
|16|The firmware version mismatch|
|17|The firmware model mismatch with terminal|
|18|The terminal no right to download this firmware|
|19|The firmware already installed|
|20|The firmware file version too low|
|22|The firmware file deleted by user|
|25|The firmware resource mismatch|


### Disable firmware push by serial number and firmware name

This API allows the third Party system disable an exist firmware push by serial number or TID of terminal and the firmware name. 


**API**

```
public Result<string> DisablePushFirmwareTaskBySnAndFirmwareName(string serialNo, string firmwareName)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|serialNo|string|false|The serial number of terminal|
|firmwareName|string|false|The name of firmware|



**Sample codes**

```
TerminalFirmwareApi terminalFirmwareApi = new TerminalFirmwareApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
terminalFirmwareApi.DisablePushFirmwareTaskBySnAndFirmwareName("0820881219", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012");
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter serialNo is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2040,
	"Message": "Unfinished terminal push firmware not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```


**Possible validation errors**

> <font color=red>Parameter serialNo is mandatory!</font>  
> <font color=red>Parameter firmwareName is mandatory!</font> 



**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2028|Terminal not found|Please check the value of tid or serialNo|
|2026|Tid and serialNo cannot empty at same time||
|2033|FmName cannot be empty||
|2034|Firmware not found||
|2040|Unfinished terminal push firmware not found|


### Disable firmware push by TID and firmware name

This API allows the third Party system disable an exist firmware push by TID of terminal and the firmware name. 


**API**

```
public Result<string> DisablePushFirmwareTaskBySnAndFirmwareName(string serialNo, string firmwareName)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|serialNo|string|false|The serial number of terminal|
|firmwareName|string|false|The name of firmware|



**Sample codes**

```
TerminalFirmwareApi terminalFirmwareApi = new TerminalFirmwareApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
terminalFirmwareApi.DisablePushFirmwareTaskByTidAndFirmwareName("I1TF6LA2", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012");
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter tid is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 2040,
	"Message": "Unfinished terminal push firmware not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```


**Possible validation errors**

> <font color=red>Parameter tid is mandatory!</font>  
> <font color=red>Parameter firmwareName is mandatory!</font> 



**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2028|Terminal not found|Please check the value of tid or serialNo|
|2026|Tid and serialNo cannot empty at same time||
|2033|FmName cannot be empty||
|2034|Firmware not found||
|2040|Unfinished terminal push firmware not found|
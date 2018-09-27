## Terminal APIs

Terminal APIs allow thirdparty system search terminals, get a terminal, create a terminal, update a terminal, activate a terminal, disable a terminal and delete a exist terminal.

All the terminal APIs are in the class *Paxstore.OpenApi.TerminalApi*.   

**Constructors of ResellerAPI**

```
public TerminalApi(string baseUrl, string apiKey, string apiSecret)
```

**Constructor parameters description**

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|



### Search terminals

The search terminal API allow the thirdparty system search terminals by page. 

**API**

```
public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, string snNameTID)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|TerminalSearchOrderBy|false|the field name of sort order by. Value can be one of TerminalSearchOrderBy.Name, TerminalSearchOrderBy.TID and TerminalSearchOrderBy.SerialNo.|
|status|TerminalStatus|false|the terminal status<br/> the value can be TerminalStatus.Active, TerminalStatus.Inactive, TerminalStatus.Suspend and TerminalStatus.All. If the value is TerminalStatus.All it will return terminals of all status|
|snNameTID|String|true|search filter by serial number,name or TID|

**Sample codes**

```
TerminalApi api = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<Terminal> result = API.SearchTerminal(1, 10, TerminalSearchOrderBy.SerialNo, TerminalStatus.All, null);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["'Page No' must be greater than '0'."],
	"Data": null,
	"PageInfo": null
}

**Succssful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 10,
		"TotalCount": 1,
		"HasNext": false,
		"DataSet": [{
			"ID": 1000159335,
			"Name": "AjTest",
			"TID": "PO8KFHHY",
			"SerialNo": "etqteqt3",
			"Status": "P",
			"MerchantName": null,
			"ModelName": "A90",
			"ResellerName": "Pine Labs"
		}]
	}
}
```

The type in dataSet of result is Terminal. The structure shows below.

Structure of class Terminal

|Property Name|Type|Description|
|:--|:--|:--|
|ID|long|Terminal's id.|
|Name|string|Terminal's name.|
|TID|string|Terminal's tid.|
|SerialNo|string|Serial number of terminal.|
|Status|string|Status of terminal. Value can be one of A(active), P(Pendding) and S(Suspend).|
|MerchantName|string|The merchant of terminal belongs to.|
|ModelName|string|Model name of terminal.|
|ResellerName|string|The reseller of terminal belongs to.|

**Possible validation errors**

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font> 


### Get a terminal

The get terminal API allows the thirdparty system get a terminal by terminal id. If the termianl does not exist the data field in result is null.

**API**

```
public Result<Terminal> GetTerminal(long terminalId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|The terminal id.|


**Sample codes**

```
TerminalApi api = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<Terminal> result = API.GetTerminal(100);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter terminalId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1801,
	"Message": "Terminal doesn't exist",
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": {
		"ID": 1000159335,
		"Name": "AjTest",
		"TID": "PO8KFHHY",
		"SerialNo": "etqteqt3",
		"Status": "P",
		"MerchantName": null,
		"ModelName": "A90",
		"ResellerName": "Pine Labs"
	},
	"PageInfo": null
}
```

The type of data in result is Terminal. Its structure already shows in search terminal API.


**Possible validation errors**

> <font color=red>Parameter terminalId cannot be null and cannot be less than 1!</font>  

**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1801|Terminal doesn't exist|&nbsp;|


### Create a terminal

Create merchant API allow the thirdparty system create a terminal.


**API**

```
public Result<Terminal> CreateTerminal(TerminalCreateRequest terminalCreateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalCreateRequest|TerminalCreateRequest|false|The create request object. The structure shows below.|

Structure of class TerminalCreateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|The name of terminal, max length is 64.|
|TID|string|true|The tid of terminal. If it is empty system will generate a tid when creating. And the length range is from 8 to 16.|
|SerialNo|string|true|The serial number of terminal. If the status is active the serial number is mandatory.|
|MerchantName|string|true|The merchant of terminal belongs to. If the initial is active then merchantName is mandatory. The max length is 64. Make sure the merchant belongs to the given reseller|
|ResellerName|string|false|The reseller of terminal belongs to. Max length is 64.|
|ModelName|string|false|The model name of terminal. Max length is 64.|
|Location|string|true|The location of terminal, max length is 32.|
|Status|string|true|Status of terminal, valus can be one of A(Active) and P(Pendding). If status is null the initial status is P(Pendding) when creating.|


**Sample codes**

```
TerminalApi api = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
TerminalCreateRequest createRequest = new TerminalCreateRequest();
createRequest.Name = "Terminal 1";
createRequest.ResellerName = "reseller_002";
createRequest.MerchantName = "KFC";
createRequest.SerialNo = "sn021215";
createRequest.ModelName = "A920";
Result<Terminal> result = api.CreateTerminal(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["'Name' should not be empty.", "'Reseller Name' should not be empty.", "'Model Name' should not be empty.", "'Status' must be 'A' or 'P'."],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 1740,
	"message": "Your terminal (SN:sn0101012237) already exists",
    "ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": {
		"ID": 1000160042,
		"Name": "Terminal 1",
		"TID": "3JYIRMXM",
		"SerialNo": "sn021215",
		"Status": "P",
		"MerchantName": "KFC",
		"ModelName": "A920",
		"ResellerName": "reseller_002"
	},
	"PageInfo": null
}
```

The type of data in result is same as search terminal API.

**Possible validation errors**


> <font color=red>Parameter terminalCreateRequest cannot be null!</font>  
> <font color=red>'Name' should not be empty.</font>  
> <font color=red>'Reseller Name' should not be empty.</font>  
> <font color=red>'Model Name' should not be empty.</font>  
> <font color=red>The length of 'Name' must be 64 characters or fewer. You entered 144 characters.</font>  
> <font color=red>The length of 'TID' must be 16 characters or fewer. You entered 19 characters.</font>  
> <font color=red>The length of 'TID' must be at least 8 characters. You entered 4 characters.</font>
> <font color=red>The length of 'Serial No' must be 32 characters or fewer. You entered 35 characters.</font>  
> <font color=red>The length of 'Merchant Name' must be 64 characters or fewer. You entered 65 characters.</font> 
> <font color=red>The length of 'Reseller Name' must be 64 characters or fewer. You entered 65 characters.</font>
> <font color=red>The length of 'Model Name' must be 64 characters or fewer. You entered 65 characters.</font>
> <font color=red>The length of 'Location' must be 32 characters or fewer. You entered 40 characters.</font>
> <font color=red>'Status' must be 'A' or 'P'.</font>  




**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1740|Your terminal (SN:xxxxxx) already exists|&nbsp;|
|1759|Reseller doesn't exist|&nbsp;|
|1720|Merchant doesn't exist|&nbsp;|
|1937|Merchant is not belong to the given Reseller!|&nbsp;|
|1700|Model doesn't exist|&nbsp;|
|1817|Terminal name is mandatory|&nbsp;|
|1818|Terminal name is too long|&nbsp;|
|1803|Terminal model is mandatory|&nbsp;|
|2326|Terminal reseller is mandatory|&nbsp;|
|1806|Terminal SN is too long|&nbsp;|
|2312|Terminal Serial No. accept alphanumeric|Alphanumeric and max length is 16|
|1807|Terminal model is too long|&nbsp;|
|1808|Terminal merchant is too long|&nbsp;|
|1809|Terminal location is too long|&nbsp;|
|1804|Terminal merchant is mandatory|&nbsp;|
|1802|Terminal SN is mandatory|&nbsp;|
|1828|TID already used|&nbsp;|
|2349|Terminal TID length is 8 to 15|&nbsp;|
|1737|The associated merchant is not activate|&nbsp;|
|1773|The associated reseller is not activate|&nbsp;|


### Update a terminal

Update terminal API allows the thirdparty system update a exist terminal by terminal id.

**API**

```
public Result<Terminal> UpdateTerminal(long terminalId,TerminalUpdateRequest terminalUpdateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|Terminal's id.|
|terminalUpdateRequest|TerminalUpdateRequest|false|Update terminal request object. The structure shows below.|

Structure of class TerminalUpdateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|String|false|The name of terminal, max length is 64.|
|TID|String|true|The tid of terminal. If it is empty system will generate a tid when creating. And the length range is from 8 to 15.|
|SerialNo|String|true|The serial number of terminal. If the status is active the serial number is mandatory.|
|MerchantName|String|true|The merchant of terminal belongs to. If the initial is active then merchantName is mandatory. The max length is 64. Make sure the merchant belongs to the given reseller|
|ResellerName|String|false|The reseller of terminal belongs to. Max length is 64.|
|ModelName|String|false|The model name of terminal. Max length is 64.|
|Location|String|true|The location of terminal, max length is 32.|


**Sample codes**

```
TerminalApi api = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
TerminalUpdateRequest updateRequest = new TerminalUpdateRequest();
updateRequest.Name = "Terminal 1";
updateRequest.Location = "Suzhou";
updateRequest.MerchantName = "KFC";
updateRequest.ResellerName = "reseller_002";
updateRequest.SerialNo = "sn021215";
updateRequest.ModelName = "A920";
Result<Terminal> updateResult = api.UpdateTerminal(1000160042, updateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
    "message": null,
	"validationErrors": ["Parameter terminalId cannot be null and cannot be less than 1!"],
    "Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
    "businessCode": -1804,
    "message": "Terminal merchant is mandatory",
	"validationErrors": null,
    "Data": null,
	"PageInfo": null
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0,
    "message": null,
    "validationErrors": null,
	"data": {
		"id": 907560,
		"name": "KFC-TML-001",
		"tid": "53K5M9OS",
		"serialNo": "sn010101211226",
		"status": "S",
		"merchantName": "KFC",
		"modelName": "A920",
		"resellerName": "New York"
	},
    "PageInfo": null
}
```

The type of data in result is same as search terminal API.

**Possible client validation errors**


> <font color=red>Parameter terminalUpdateRequest cannot be null!</font>  
> <font color=red>Parameter terminalId cannot be null and cannot be less than 1!</font>
> <font color=red>'Name' should not be empty.</font>  
> <font color=red>'Reseller Name' should not be empty.</font>  
> <font color=red>'Model Name' should not be empty.</font>  
> <font color=red>The length of 'Name' must be 64 characters or fewer. You entered 144 characters.</font>  
> <font color=red>The length of 'TID' must be 16 characters or fewer. You entered 19 characters.</font>  
> <font color=red>The length of 'TID' must be at least 8 characters. You entered 4 characters.</font>
> <font color=red>The length of 'Serial No' must be 32 characters or fewer. You entered 35 characters.</font>  
> <font color=red>The length of 'Merchant Name' must be 64 characters or fewer. You entered 65 characters.</font> 
> <font color=red>The length of 'Reseller Name' must be 64 characters or fewer. You entered 65 characters.</font>
> <font color=red>The length of 'Model Name' must be 64 characters or fewer. You entered 65 characters.</font>
> <font color=red>The length of 'Location' must be 32 characters or fewer. You entered 40 characters.</font>


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1759|Reseller doesn't exist|&nbsp;|
|1720|Merchant doesn't exist|&nbsp;|
|1937|Merchant is not belong to the given Reseller!|&nbsp;|
|1700|Model doesn't exist|&nbsp;|
|1800|Terminal not found|&nbsp;|
|1817|Terminal name is mandatory|&nbsp;|
|1818|Terminal name is too long|&nbsp;|
|1803|Terminal model is mandatory|&nbsp;|
|2326|Terminal reseller is mandatory|&nbsp;|
|1806|Terminal SN is too long|&nbsp;|
|2312|Terminal Serial No. accept alphanumeric|Alphanumeric and max length is 16|
|1807|Terminal model is too long|&nbsp;|
|1808|Terminal merchant is too long|&nbsp;|
|1809|Terminal location is too long|&nbsp;|
|2401|Terminal TID is invalid|&nbsp;|
|1929|The terminal is not inactive,model cannot be updated!|&nbsp;|
|1811|The terminal has already been activated,unable to update reseller.|&nbsp;|
|1928|The terminal is active,terminal SN cannot be updated!|&nbsp;|
|1804|Terminal merchant is mandatory|&nbsp;|
|1737|The associated merchant is not activate|&nbsp;|
|1813|Push task has already been added, unable to update model.|&nbsp;|
|1814|Push task has already been added,unable to update reseller.|&nbsp;|
|1828|TID already used|&nbsp;|
|2349|Terminal TID length is 8 to 15|&nbsp;|
|1737|The associated merchant is not activate|&nbsp;|
|1773|The associated reseller is not activate|&nbsp;|


### Activate a terminal

Activate terminal API allows the thirdparty system to activate a terminal by terminal id.


**API**

```
public Result<string> ActivateTerminal(long terminalId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|The terminal id.|


**Sample codes**

```
TerminalApi api = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<string> result = terminalApi.ActivateTerminal(907560L);
```

**Client validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter terminalId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": 1800,
	"Message": "Terminal not found",
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Possible client validation errors**

> <font color=red>Parameter terminalId cannot be null and cannot be less than 1!</font>  

**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1800|Terminal not found|&nbsp;|
|1893|The terminal has already been activated!|&nbsp;|
|1802|Terminal SN is mandatory|&nbsp;|
|1804|Terminal merchant is mandatory|&nbsp;|
|1700|Model doesn't exist|&nbsp;|
|1713|The associated model is not activate|&nbsp;|


### Disable a terminal  

The disable terminal API allows the thirdparty system disable a terminal by terminal id.
If disable successfully there's not response content from remote server.

**API**

```
public Result<string> DisableTerminal(long terminalId)
```


**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|The terminal id.|


**Sample codes**

```
TerminalApi api = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<string> result = terminalApi.DisableTerminal(907560L);
```

**Client validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter terminalId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": -1888,
	"Message": "The terminal is not active,unable to disable!",
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Successful sample result(JSON formatted)**

```
{
    "BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Possible client validation errors**

> <font color=red>Parameter terminalId cannot be null and cannot be less than 1!</font>  

**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1800|Terminal not found|&nbsp;|
|1888|The terminal is not active,unable to disable!|&nbsp;|


### Delete a terminal  

Delete terminal API allows the thirdparty system delete a exist terminal by terminal id.
If delete successfully there's no response content from remote server.

**API**

```
public Result<string> DeleteTerminal(long terminalId)
```


**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|The terminal id.|


**Sample codes**

```
TerminalApi api = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
Result<string> result = terminalApi.DeleteTerminal(907560L);
```

**Client validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter terminalId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": 1877,
	"Message": "The terminal is active,unable to delete!",
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Successful sample result(JSON formatted)**

```
{
    "BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Possible client validation errors**

> <font color=red>Parameter terminalId cannot be null and cannot be less than 1!</font>  

**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1800|Terminal not found|&nbsp;|
|1877|The terminal is active,unable to delete!|&nbsp;|






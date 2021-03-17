## Terminal APIs

Terminal APIs allow thirdparty system search terminals, get a terminal, create a terminal, update a terminal, activate a terminal, disable a terminal and delete a exist terminal.

All the terminal APIs are in the class *Paxstore.OpenApi.TerminalApi*.   

**Constructors of TerminalApi**

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
Note: This result of this API does not include the geolocation, firmware and installed application, if you need those 3 information in result please use another search terminals API

**API**

```
public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, string snNameTID)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 100|
|orderBy|TerminalSearchOrderBy|false|the field name of sort order by. Value can be one of TerminalSearchOrderBy.Name, TerminalSearchOrderBy.TID and TerminalSearchOrderBy.SerialNo.|
|status|TerminalStatus|false|the terminal status<br/> the value can be TerminalStatus.Active, TerminalStatus.Inactive, TerminalStatus.Suspend and TerminalStatus.All. If the value is TerminalStatus.All it will return terminals of all status|
|snNameTID|string|true|search filter by serial number,name or TID|

**Sample codes**

```
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
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
```

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
			"ResellerName": "Pine Labs",
			"Location": "USA"
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
|Location|string|The location.|
|GeoLocation|TerminalLocation| The geography location of the terminal|
|InstalledFirmware|TerminalInstalledFirmware| The installed firmware of the terminal|
|InstalledApks|List\<TerminalInstalledApk\>| The installed applications of the terminal|
|TerminalDetail|TerminalDetail| The terminal detail information |
|TerminalAccessory|TerminalAccessory| The terminal accessory information |


Structure of class TerminalLocation

|Property Name|Type|Description|
|:---|:---|:---|
|Lat|Double|The latitude of geography location|
|Lng|Double|The longitude of geography location|

Structure of class TerminalInstalledFirmware 

|Property Name|Type|Description|
|:---|:---|:---|
|FirmwareName|string|Firmware name|
|InstallTime|Nullable<DateTime>|Firmware installed date|


Structure of class TerminalInstalledApk  

|Property Name|Type|Description|
|:---|:---|:---|
|AppName|string|Application name|
|PackageName|string|Package name of application|
|VersionName|string|Version name of application|
|VersionCode|long|Version code of application|
|InstallTime|Nullable<DateTime>|Installed time of application|

Structure of class TerminalDetail

| Property Name    | Type   | Description                  |
| :--------------- | :----- | :--------------------------- |
| PN               | string | Terminal's pn                |
| OSVersion        | string | Terminal's android version   |
| IMEI             | string | Terminal's IMEI              |
| ScreenResolution | string | Terminal's screen resolution |
| Language         | string | Terminal's language          |
| IP               | string | Terminal's network ip        |
| TimeZone         | string | Terminal's time zone         |
| MacAddress       | string | Terminal's MAC address       |
| ICCID            | string | Terminal's ICCID             |
| CellId           | string | Terminal's Cellid            |

Structure of class TerminalAccessory

| Property Name       | Type                     | Description                                         |
| :------------------ | :----------------------- | :-------------------------------------------------- |
| relatedTerminalName | String                   | The accessory information terminal name             |
| Basic               | TerminalDeviceInfo  | The basic information of the accessory device       |
| Hardware            | TerminalDeviceInfo  | The hardware information of the accessory device    |
| InstallApps         | TerminalDeviceInfo  | The installApps information of the accessory device |
| History             | TerminalDeviceHistory | The history information of the accessory device     |

Structure of class TerminalDeviceInfo

| Property Name | Type   | Description                       |
| :------------ | :----- | :-------------------------------- |
| Name          | string | The accessory information name    |
| Content       | string | The accessory information content |

Structure of class TerminalDeviceHistory

| Property Name | Type   | Description                                                  |
| :------------ | :----- | :----------------------------------------------------------- |
| Name          | string | The accessory information name                               |
| Version       | string | The accessory information version                            |
| Status        | string | The status of the related historical push of the accessory device |
| InstallTime   | Nullable<DateTime>   | The accessory information install time                       |
| FileSize      | Nullable<long>  | The size of the file pushed by the accessory device          |
| FileType      | string | The type of the file pushed by the accessory device          |
| Source        | string | The file source                                              |
| Remarks       | string | The remarks information 

**Possible validation errors**

> <font color="red">'Page Size' must be less than or equal to '100'.</font>  
> <font color="red">'Page No' must be greater than '0'.</font>  
> <font color="red">'Page Size' must be greater than '0'.</font>  


### Search terminals include geo location, installed app and firmware  
This API is similar to the search terminals API, it has additional 3 parameters, the details please refer to the Input parameter(s) description

**API**  
```
public Result<Terminal> SearchTerminal(int pageNo, int pageSize, TerminalSearchOrderBy orderBy, TerminalStatus status, string snNameTID, bool includeGeoLocation, bool includeInstalledApks, bool includeInstalledFirmware)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:---- | :----|:----|:----|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 100|
|orderBy|TerminalSearchOrderBy|true|the sort order by field name, value can be one of TerminalSearchOrderBy.Name, TerminalSearchOrderBy.Tid and TerminalSearchOrderBy.SerialNo. If pass null parameter the search result will order by id by default.|
|status|TerminalStatus|true|the terminal status<br/> the value can be TerminalStatus.Active, TerminalStatus.Inactive, TerminalStatus.Suspend|
|snNameTID|String|true|search by serial number,name and TID|
|includeGeoLocation|boolean|true|whether to include geo location information in search result|
|includeInstalledApks|boolean|true|whether to include install applications in search result|
|includeInstalledFirmware|boolean|true|whether to include the firmware version of the terminal in search result|


**Sample codes**

```
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
Result<Terminal> result = API.SearchTerminal(1, 10, TerminalSearchOrderBy.SerialNo, TerminalStatus.All, null, true, true, true);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["pageSize:must be greater than or equal to 1", "pageNo:must be greater than or equal to 1"]
}
```

**Succssful sample result(JSON formatted)**

```  
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 10,
		"TotalCount": 1,
		"HasNext": false,
		"DataSet": [{
			"Id": 907558,
			"Name": "testcreateterminal_023",
			"Tid": "FATIAP0T",
			"SerialNo": "sn0101012225",
			"Status": "A",
			"MerchantName": "KFC",
			"ModelName": "A920",
			"ResellerName": "New York",
			"Location": "USA",
			"GeoLocation": {
				"lng": 120.77595,
				"lat": 31.308021
			},
			"InstalledFirmware": {"firmwareName": "A930_PayDroid_7.1.1_Virgo_customer_res_pax_20180925",
				"installTime": null
			},
			"InstalledApks": [{
				"AppName": "WSPLink",
				"PackageName": "com.soundpayments.wsplink",
				"InstallTime": 1563639530000,
				"VersionName": "10.01.00.00",
				"VersionCode": 10010000
			}, {
				"AppName": "NeptuneService",
				"PackageName": "com.pax.ipp.neptune",
				"InstallTime": 1230692400000,
				"VersionName": "V3.05.00_20190523",
				"VersionCode": 33
			}, {
				"AppName": "releasedemo1",
				"PackageName": "com.pax.new.release.demo1",
				"InstallTime": 1563639280000,
				"VersionName": "V3.02.00_20190129",
				"VersionCode": 11
			}
			]
		}]
	}
}
```

The type in dataSet of result is Terminal.



**Possible validation errors**

> <font color=red>pageSize:must be greater than or equal to 1</font>   
> <font color=red>pageNo:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be less than or equal to 100</font>  

### Get a terminal

The get terminal API allows the thirdparty system get a terminal by terminal id. If the termianl does not exist the data field in result is null.

**API**

```
public Result<Terminal> GetTerminal(long terminalId)

public Result<Terminal> GetTerminal(long terminalId, bool includeDetailInfo);
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|The terminal id.|
|includeDetailInfo|bool|false|Whether to include terminal details in result|


**Sample codes**

```
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
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
		"ResellerName": "Pine Labs",
		"Location": "USA"
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
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
TerminalCreateRequest createRequest = new TerminalCreateRequest();
createRequest.Name = "Terminal 1";
createRequest.ResellerName = "reseller_002";
createRequest.MerchantName = "KFC";
createRequest.SerialNo = "sn021215";
createRequest.ModelName = "A920";
createRequest.Location = "USA";
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
		"ResellerName": "reseller_002",
		"Location": "USA"
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
|2412|Your terminal SN not exist in asset|&nbsp;|


### Update a terminal

Update terminal API allows the thirdparty system update a exist terminal by terminal id.

**API**

```
public Result<Terminal> UpdateTerminal(long terminalId,TerminalUpdateRequest terminalUpdateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|Terminal's id(Get from the result of create terminal, not the TID of terminal)|
|terminalUpdateRequest|TerminalUpdateRequest|false|Update terminal request object. The structure shows below.|

Structure of class TerminalUpdateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|The name of terminal, max length is 64.|
|TID|string|true|The tid of terminal. If it is empty system will generate a tid when creating. And the length range is from 8 to 16.|
|SerialNo|string|true|The serial number of terminal. If the status is active the serial number is mandatory.|
|MerchantName|string|true|The merchant of terminal belongs to. If the initial is active then merchantName is mandatory. The max length is 64. Make sure the merchant belongs to the given reseller|
|ResellerName|string|false|The reseller of terminal belongs to. Max length is 64.|
|ModelName|string|false|The model name of terminal. Max length is 64.|
|Location|string|true|The location of terminal, max length is 32.|


**Sample codes**

```
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
TerminalUpdateRequest updateRequest = new TerminalUpdateRequest();
updateRequest.Name = "Terminal 1";
updateRequest.Location = "Suzhou";
updateRequest.MerchantName = "KFC";
updateRequest.ResellerName = "reseller_002";
updateRequest.SerialNo = "sn021215";
updateRequest.ModelName = "A920";
updateRequest.Location = "China";
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
		"resellerName": "New York",
		"Location": "China"
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
|1740|Your terminal (SN:{0}) already exists|&nbsp;|

Note: The string {0} in the message of business code 1740 will be replaced by the SN in request.


### Activate a terminal

Activate terminal API allows the thirdparty system to activate a terminal by terminal id.


**API**

```
public Result<string> ActivateTerminal(long terminalId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|terminalId|long|false|The terminal id(Get from the result of create terminal, not the TID of terminal)|


**Sample codes**

```
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
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
|terminalId|long|false|The terminal id(Get from the result of create terminal, not the TID of terminal)|


**Sample codes**

```
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
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
|terminalId|long|false|The terminal id(Get from the result of create terminal, not the TID of terminal)|


**Sample codes**

```
TerminalApi api = new TerminalApi(API_BASE_URL, API_KEY, API_SECRET);
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



### Batch add terminal to group  

Batch add terminal to group API allows the thirdparty system to add terminals to one or more groups.

**API**

```
public Result<string> BatchAddTerminalToGroup(TerminalGroupRequest batchAddTerminalToGroupRequest)
```

**Input parameter(s) description**

| Parameter Name | Type                 | Nullable | Description                                                  |
| :------------- | :------------------- | :------- | :----------------------------------------------------------- |
| batchAddTerminalToGroupRequest   | TerminalGroupRequest | false    | add terminals to group request object. The structure shows below. |

Structure of class TerminalGroupRequest

| Property Name | Type      | Nullable | Description      |
| :------------ | :-------- | :------- | :--------------- |
| TerminalIds   | HashSet\<long\> | false    | terminal ids |
| GroupIds      | HashSet\<long\> | false    | group ids    |

**Sample codes**

```
TerminalApi terminalApi = new TerminalApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
TerminalGroupRequest batchAddTerminalToGroupRequest = new TerminalGroupRequest();

HashSet<long> groupIds = new HashSet<long>();
groupIds.Add(16529);
groupIds.Add(16527);
HashSet<long> terminalIds = new HashSet<long>();
terminalIds.Add(909744);
terminalIds.Add(909742);
request.GroupIds = groupIds;
request.TerminalIds = terminalIds;
Result<string> result = API.BatchAddTerminalToGroup(request);
```

**Client validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter batchAddTerminalToGroupRequest is mandatory!"]
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

**Possible client validation errors**

> <font color=red>Parameter batchAddTerminalToGroupRequest is mandatory!</font>  

**Possible business codes**

| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 1800          | Terminal not found                                           |             |
| 1810          | Terminal is not active                                       |             |
| 2150          | Terminal group not found                                     |             |
| 2163          | Terminal reseller mismatched                                 |             |
| 2164          | Terminal model mismatched                                    |             |
| 2167          | Terminal group exceeded the max terminal count limit, please create new terminal group to put the terminal |             |


### Update terminal configuration

Update terminal configuration like whether allow terminal replacement by API or input serial number on terminal.

**API**

```
public Result<string> UpdateTerminalConfig(long terminalId, TerminalConfigUpdateRequest terminalConfigUpdateRequest)
```

**Input parameter(s) description**

| Parameter Name              | Type                        | Nullable | Description                                                  |
| :-------------------------- | :-------------------------- | :------- | :----------------------------------------------------------- |
| terminalId                  | long                        | false    | Terminal's id.                                               |
| terminalConfigUpdateRequest | TerminalConfigUpdateRequest | false    | Update terminal config request object. The structure shows below. |

Structure of class TerminalRemoteConfigRequest

| Property Name    | Type    | Nullable | Description                                                  |
| :--------------- | :------ | :------- | :----------------------------------------------------------- |
| AllowReplacement | bool | false    | Whether allow terminal replacement by API or input serial number on termial |

**Sample codes**

```
TerminalApi terminalApi = new TerminalApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
long terminalId = 909744;
TerminalConfigUpdateRequest terminalConfigUpdateRequest = new TerminalConfigUpdateRequest();
terminalConfigUpdateRequest.AllowReplacement = true;
Result<string> result = terminalApi.UpdateTerminalConfig(terminalId,terminalConfigUpdateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter terminalConfigUpdateRequest is mandatory!"]
}
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
	"BusinessCode": 0
}
```

**Possible client validation errors**

> <font color=red>Parameter terminalConfigUpdateRequest is mandatory!</font>  

**Possible business codes**

| Business Code | Message                                                      | Description |
| :------------ | :----------------------------------------------------------- | :---------- |
| 1800          | Terminal not found                                           |             |
| 1838          | It is not allowed to change the terminal level "Terminal Replacement" status. please make sure reseller level terminal replacement settings are enabled. |             |

### Get terminal configuration

Get terminal configuration.

**API**

```
public Result<TerminalConfig> GetTerminalConfig(long terminalId)
```

**Input parameter(s) description**

| Parameter Name | Type | Nullable | Description    |
| :------------- | :--- | :------- | :------------- |
| terminalId     | long | false    | Terminal's id. |

**Sample codes**

```
TerminalApi terminalApi = new TerminalApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalConfig> result = terminalApi.GetTerminalConfig(909744);
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
	"Data": {
		"AllowReplacement": true
	}
}
```

 

**Possible business codes**

| Business Code | Message                | Description |
| :------------ | :--------------------- | :---------- |
| 1800          | Terminal not found     |             |
| 1801          | Terminal doesn't exist |             |

### Get terminal PED information  

Get terminal PED information by terminal id.

**API**

```
public Result<TerminalPED> GetTerminalPED(long terminalId)
```

**Input parameter(s) description**

| Parameter Name | Type | Nullable | Description    |
| :------------- | :--- | :------- | :------------- |
| terminalId     | long | false    | Terminal's id |

**Sample codes**

```
TerminalApi terminalApi = new TerminalApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<TerminalPED> result = terminalApi.GetTerminalPED(909755);
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
	"Data": {
		"info": "[{\"SK/TPK\": [{\"kcv\": \"B03273DC0000000000\", \"slot\": \"2\"}]}, {\"TDES DUKPT\": [{\"ksi\": \"FFFF539SD99336FCA00001\", \"slot\": \"1\"}, {\"ksi\": \"FFFF98765D400CB600001\", \"slot\": \"3\"}, {\"ksi\": \"FFFF9876D5400CB200001\", \"slot\": \"4\"}, {\"ksi\": \"FFFF98765D43347000001\", \"slot\": \"5\"}, {\"ksi\": \"FFFF987654C92DA200001\", \"slot\": \"11\"}]}]"
	}
}
```


**Possible business codes**

| Business Code | Message            | Description |
| :------------ | :----------------- | :---------- |
| 1800          | Terminal not found |             |


### Move terminal

Move a terminal to another reseller and merchant

**API**

```
public Result<string> MoveTerminal(long terminalId, string resellerName, string merchantName)
```

**Input parameter(s) description**

| Parameter Name | Type | Nullable | Description    |
| :------------- | :--- | :------- | :------------- |
| terminalId     | long | false    | Terminal's id. |
| resellerName| string | false | The target reseller name the terminal move to|
| merchantName| string | false | The target merchant name the terminal move to|



**Sample codes**

```
TerminalApi terminalApi = new TerminalApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = terminalApi.MoveTerminal(terminalId, "PAX", "6666");
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter resellerName is mandatory!"]
}
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
}
```

**Possible client validation errors**

> <font color=red>Parameter resellerName is mandatory!</font>  
> <font color=red>Parameter merchantName is mandatory!</font> 

**Possible business codes**

| Business Code | Message            | Description |
| :------------ | :----------------- | :---------- |
| 1800          | Terminal not found |             |
| 1759          | Reseller doesn't exist |             |
| 1720          | Merchant doesn't exist |             |
| 1937          | Merchant is not belong to the given Reseller! |             |


### Push Command to Terminal

Push lock, unlock and restart command to terminal

**API**

```
public Result<string> PushCmdToTerminal(long terminalId, TerminalPushCmd command)
```

**Input parameter(s) description**

| Parameter Name              | Type                        | Nullable | Description                     |
| :-------------------------- | :-------------------------- | :------- | :------------------------------ |
| terminalId                  | long                        | false    | Terminal's id.                  |
| command | TerminalPushCmd | false    | Value can be TerminalPushCmd.Lock, TerminalPushCmd.Unlock and TerminalPushCmd.Restart |

**Sample codes**

```
TerminalApi terminalApi = new TerminalApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = terminalApi.PushCmdToTerminal(terminalId, TerminalPushCmd.Lock);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1801,
	"Message": "Terminal doesn't exist"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
}
```

**Possible business codes**

| Business Code | Message                                  | Description |
| :------------ | :--------------------------------------- | :---------- |
| 135           | Request parameter is missing or invalid  |             |
| 997           | Malformed or illegal request             |             |
| 1801          | Terminal doesn't exist                   |             |
| 1896          | The terminal is offline                  |             |
| 15094         | Terminal is locked                       |             |
| 15095         | Terminal has been unlocked               |             |
| 15096         | The terminal is being locked or unlocked |             |
| 15099         | Terminal restart in progress             |             |


## TerminalFirmware API

All the terminalFirmware related APIs are encapsulated in the class *Paxstore.OpenApi.TerminalFirmwareApi*.

**Constructors of TerminalFirmwareApi**

```
public TerminalFirmwareApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

| Name | Type | Description |
| :--- | :--- | :--- |
| baseUrl | String | the base url of REST API |
|apiKey|String|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|String|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


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
	"businessCode": -1,
	"validationErrors": ["Parameter tid is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 2028,
	"message": "Terminal not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0
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
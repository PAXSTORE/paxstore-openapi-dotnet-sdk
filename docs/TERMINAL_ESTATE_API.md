## Terminal Estate APIs

Terminal Estate APIs allow thirdParty system verify terminal estate by serialNo.

The related API(s) is in the class *Paxstore.OpenApi.TerminalEstateApi*.   

**Constructors of TerminalEstateApi**

```
public TerminalEstateApi(string baseUrl, string apiKey, string apiSecret);
```

**Constructor parameters description**

|Name|Type|Description|
|:----|:----|:----|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Verify Terminal Estate  

The verify terminal estate API allows the thirdParty system verify terminal estate by terminal serialNo.
If terminal estate exist there's not response content from remote server. 

**API**

```
public Result<string> VerifyTerminalEstate(string serialNo)
```


**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|serialNo|string|false|The terminal serialNo.|


**Sample codes**

```
TerminalEstateApi api = new TerminalEstateApi(API_BASE_URL, API_KEY, API_SECRET);
Result<string> result = api.VerifyTerminalEstate("1350001853");
```

If the property BusinessCode of result is 0, then means the terminal exist in terminal estate.

Client validation failed sample result(JSON formatted)**

```
{
	"businessCode": -1,
	"validationErrors": ["Serial number is mandatory!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"businessCode": 2412,
	"message": "Your terminal (SN:1350001853) does not exist in estate list"
}
```

**Successful sample result(JSON formatted)**

```
{
	"businessCode": 0
}
```

**Possible client validation errors**

> <font color=red>Serial number is mandatory!</font>  

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|2412|Your terminal (SN:{0}) does not exist in estate list|&nbsp;|
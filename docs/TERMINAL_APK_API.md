## TerminalApk API

All the terminalApk related APIs are encapsulated in the class *Paxstore.OpenApi.TerminalApkApi*.

**Constructors of TerminalApkApi**

```
public TerminalApkApi(string baseUrl, string apiKey, string apiSecret)
```

**Constructor parameters description**

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Create terminalApk

Create terminalApk API allow the thirdparty system create a terminalApk (create a push apk task for the specified terminal).


**API**

```
public Result<string> CreateTerminalApk(CreateTerminalApkRequest createTerminalApkRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|createTerminalApkRequest|CreateTerminalApkRequest|false|The create request object. The structure shows below.|


Structure of class TerminalCreateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|TID|string|true|The TID of terminal|
|SerialNo|string|true|The serial number of terminal|
|PackageName|string|false|The package name which indicate the application you want to push to the terminal|
|Version|string|true|The version name of application which you want to push, if it is blank API will use the latest version|
|TemplateName|string|true|The template name of paramter. If user want to push more than one template the please use &#124; to concact the different template names like tempate1&#124;template2&#124;template3, the max size of template names is 10.|
|Parameters|Dictionary&lt;string, string&gt;|false|The parameter key and value, the key the the PID in template|

Note: TID and serialNo cannot be empty at same time.


**Sample codes**

```
TerminalApkApi api = new TerminalApkApi(API_BASE_URL, API_KEY, API_SECRET);
CreateTerminalApkRequest createTerminalApkRequest = new CreateTerminalApkRequest();
createTerminalApkRequest.TID = "ABC09098989";
createTerminalApkRequest.PackageName = "com.baidu.map";
createTerminalApkRequest.TemplateName = "template_map";
Dictionary<string, string> parameters = new Dictionary<string, string>();
parameters.Add("PID.locationCode", "cn_js_sz");
parameters.Add("PID.showtraffic", "true");
createTerminalApkRequest.Parameters = parameters;
Result<string> result = api.CreateTerminalApk(createTerminalApkRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": -1,
"Message": null,
"ValidationErrors": ["The property SerialNo and TID in createTerminalApkRequest cannot be blank at same time!"],
"Data": null,
"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": 2028,
  "Message": "TerminalApk not found",
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



**Possible validation errors**

> <font color=red>Parameter createTerminalApkRequest cannot be null!</font>  
> <font color=red>The property parameters of createTerminalApkRequest cannot be empty!</font>  
> <font color=red>The property serialNo and tid in createTerminalApkRequest cannot be blank at same time!</font> 
> <font color=red>'Package Name' should not be empty.</font> 
> <font color="red">The max size of template names is 10!</font>


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|2028|Terminal not found|Please check the value of tid or serialNo|
|2029|Apk not found|Cannot find apk by packagename and version|
|2030|Parameter template not found|The given template name(s) not exist in system|
|13100|Invalid application parameter variables||
|2026|Tid and serialNo cannot empty at same time||
|2027|Package name cannot be empty||
|2001|Terminal app not found||
|2000|Terminal app status is invalid||
|9306|App is not available||
|2022|Same version of pending terminal app already exists||
|2023|Same version of active terminal app already exists||
|1905|Terminal task app parameter is invalid||
|13100|Invalid application parameter variables||
|1111|Selected parameter templates exceeded the max limit||
|2031|Templatename cannot be empty|&nbsp;|


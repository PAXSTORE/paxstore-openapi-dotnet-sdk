## Application APIs

The app APIs allow thirdparty system search apps.
All the app APIs are in the class *Paxstore.OpenApi.AppApi*.   

User can customize the additional attributes for app. To featch app's additional entity attributes please using marketplace admin login and go to page via General Setting -> Entity Attribute Setting.  


**Constructors of AppAPI**

```
public AppApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null)
public AppApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo)
public AppApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy)
public AppApi(string baseUrl, string apiKey, string apiSecret, int timeout)
```

Constructor parameters description   

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|String|the base url of REST API|
|apiKey|String|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|String|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Search apps

The search apps API allows thirdparty system to search apps for page.   

**API**

```
public Result<PagedApp> SearchApp(
    int pageNo, int pageSize, Nullable<AppSearchOrderBy> orderBy,
        string name,
        AppOsType? osType,
        AppChargeType? chargeType,
        AppBaseType? baseType,
        AppStatus? appStatus,
        ApkStatus? apkStatus,
        bool? specificReseller = false,
        bool? specificMerchantCategory = false,
        bool? includeSubscribedApp = false,
        string resellerName = null,
        string modelName = null)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 100|
|orderBy|AppSearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of AppSearchOrderBy.AppName_desc, AppSearchOrderBy.AppName_asc, AppSearchOrderBy.Emial_desc, AppSearchOrderBy.Emial_asc, AppSearchOrderBy.UpdatedDate_desc and AppSearchOrderBy.UpdatedDate_asc.|
|name|string|true|search filter by app name(parsed from apk file）, package name or the developer's name|
|appStatus|AppStatus|true|the app status<br/> the value can be AppStatus.Active, AppStatus.Suspend|
|apkStatus|ApkStatus|true|the apk status<br/> the value can be ApkStatus.Pending, ApkStatus.Online, ApkStatus.Rejected, ApkStatus.Offline|
|osType|AppOsType|true|the app osType<br/> the value can be AppOsType.Android, AppOsType.Traditional|
|baseType|AppBaseType|true|the app baseType<br/> the value can be AppBaseType.Normal, AppBaseType.Parameter|
|chargeType|AppChargeType|true|the app chargeType<br/> the value can be AppChargeType.Free, AppChargeType.Charging|
|specificReseller|bool|true|specific reseller<br/> make app private to some reseller, the value can be true or false|
|specificMerchantCategory|bool|true|sperific merchant category<br/> make app only visible by specific merchants in store client, the value can be true or false|
|includeSubscribedApp|bool|true|whether to include the subscribed app, default value is false|
|resellerName|string|true|search filter by reseller name, search out the app to which the reseller belongs to|
|modelName|string|true|search filter by model name, search out the app to which the model belongs to|



**Sample codes**

```
AppApi AppApi = new  AppApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<PagedApp> result = AppApi.SearchApp(1, 10, AppSearchOrderBy.UpdatedDate_desc,
                                                 "", AppOsType.Android, AppChargeType.Free, AppBaseType.Normal, 
                                                 AppStatus.Active, ApkStatus.Online, false, false);
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
		"Limit": 10,
		"TotalCount": 1,
		"HasNext": false,
		"DataSet": [{
			"ID":1000006322,
            "Name":"sample",
            "PackageName":"com.snatik.storage.sample",
            "Status":"A",
            "OsType":"A",
            "SpecificReseller":null,
            "ChargeType":0,
            "Price":null,
            "Downloads":4,
            "Developer":{
                "RealName":null,
                "Nickname":"ssy",
                "Phone":null,
                "Email":"heibai8054@163.com",
                "CompanyName":null
            },
            "ApkList":[
                 {
                    "Status":"O",
                    "VersionCode":1,
                    "VersionName":null,
                    "ApkType":"N",
                    "ApkFileType":"A",
                    "ApkFile":{
                        "Permissions":"WRITE_EXTERNAL_STORAGE,MAGCARD,UPDATE_APP,PRINTER,UPDATE_FIRM,ICC,SYSSIG,PICC,RECV_BOOT_COMPLETED,PED",
                        "PaxPermission":null
                    },
                    "OsType":"A"
                 }
            ]
		}]
	}
}
```

The type in dataSet is PagedApp. And the structure like below.

|Property Name|Type|Description|
|:---|:---|:---|
|ID|long|The id of app.|
|Name|string|The name of app.|
|PackageName|string|The packageName of app.|
|Status|string|Status of app. Value can be one of A(Active) and S(Suspend)|
|OsType|string|OsType of app. Value can be one of A(Android) and T(Traditional)|
|ChargeType|int|chargeType of app. Value can be one of 0(Free) and 1(Charing)|
|SpecificReseller|bool|whether make app specific reseller.|
|Downloads|long|downloads of app.|
|Developer|Developer|The developer of the app belongs to.|
|ApkList|List\<Apk\>|App version list.|

The structure of class Apk

|Property Name|Type|Description|
|:---|:---|:---|
|Name|string|Apk name|
|FileSize|long|Apk file size(byte)|
|Status|string|Status of apk. Value can be one of P(Pending), O(Online), R(Rejected) and U(Offline)|
|VersionCode|long|version code of apk.|
|VersionName|string|version name of apk.|
|ApkType|string|base type of apk. Value can be one of N(Normal) and P(Parameter)|
|ApkFileType|string|file type of apk. Value can be one of A(Android), P(Prolin) and B(Broadpos)|
|ApkFile|ApkFile|install package file of apk.|

The structure of class ApkFile

|Property Name|Type|Description|
|:---|:---|:---|
|Permissions|string|Android OS Authorization.|
|PaxPermission|string|Paydroid Authorizationr.|

The structure of class Developer

|Property Name|Type|Description|
|:---|:---|:---|
|RalName|string|The real name of developer.|
|Nickname|string|The nick name of developer.|
|CompanyName|string|Company name of developer.|
|Email|string|Email of developer.|
|Phone|string|Phone number of developer.|


**Possible client validation errors**  

> <font color=red>pageNo:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be greater than or equal to 1</font>   
> <font color=red>pageSize:must be less than or equal to 100</font>  

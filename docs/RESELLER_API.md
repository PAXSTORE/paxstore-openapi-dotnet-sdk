## Manage resellers

All the reseller related APIs are encapsulated in the class *Paxstore.OpenApi.ResellerApi*.

**Constructors of ResellerAPI**

```
public ResellerApi(string baseUrl, string apiKey, string apiSecret)
```

**Constructor parameters description**

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


<br>

### Search resellers

**API**

```
public Result<PagedReseller> SearchReseller(int pageNo, int pageSize, ResellerSearchOrderBy orderBy, string name, ResellerStatus status)
```

<br>

**Input parameter(s) description**


| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|ResellerSearchOrderBy|false|the field name of sort order by. The value of the parameter can be one of ResellerSearchOrderBy.Name, ResellerSearchOrderBy.Phone and ResellerSearchOrderBy.Contact|
|name|string|true|search filter by reseller name|
|status|ResellerStatus|false|the reseller status<br/> the value can be ResellerStatus.All, ResellerStatus.Active, ResellerStatus.Inactive, ResellerStatus.Suspend. If the value is ResellerStatus.All it will return the resellers of all status|

<br/>

**Sample codes**


```
ResellerApi api = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
Result<PagedReseller> result = api.SearchReseller(1, 10, ResellerSearchOrderBy.Name, null, ResellerStatus.All);
```    



**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["'Page No' must be greater than '0'."]
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 10,
		"TotalCount": 2,
		"HasNext": false,
		"DataSet": [{
			"ID": 1000000211,
			"Name": "Pine Labs",
			"Phone": "",
			"Country": "",
			"Postcode": "",
			"Address": "",
			"Company": "",
			"Contact": "",
			"Email": "chenlb@paxsz.com",
			"Status": "A"
		}, {
			"ID": 1000000225,
			"Name": "reseller_002",
			"Phone": "89894545",
			"Country": "CN",
			"Postcode": "8954",
			"Address": "JiangSu Suzhou city xinghujie 203#",
			"Company": "pax",
			"Contact": "sam",
			"Email": "sam2@gmail.com",
			"Status": "A"
		}]
	}
}
```

<br>

The type in dataSet of is PagedReseller. And the structure shows like below.

|Property Name|Type|Description|
|:--|:--|:--|
|ID|long|the id of reseller|
|Name|string|the name of reseller|
|Phone|string|the phone number of reseller|
|Country|string|the country code|
|Postcode|string|the postcode of reseller|
|Email|string|the email of reseller|
|Status|string|the status of reseller, value can be one of A(Active), P(Pendding) and S(Suspend)|
<br>

**Possible client validation errors**

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>




### Get a reseller

Get reseller by reseller id. 

**API**

```
public Result<Reseller> GetReseller(long resellerId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|resellerId|long|false|the id of reseller|

**Sample codes**

```
ResellerApi api = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
Result<Reseller> result = api.GetReseller(1000000211);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter resellerId cannot be null and cannot be less than 1!"]
}
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1760,
	"Message": "Reseller name already exists"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"EntityAttributeValues": {
			"111": "hello"
		},
		"Parent": {
			"ID": 4151,
			"Name": "New York"
		},
		"ID": 1000000211,
		"Name": "FVFFF",
		"Phone": "87879696",
		"Country": "CN",
		"Contact": "FFF",
		"Email": "sum@gmail.com",
		"Status": "S"
	}
}
```

<br>
The type of data is Reseller, and the structure shows below.

|Name|Type|Description|
|:--|:--|:--|
|ID|long|the id of reseller|
|Name|string|the name of reseller|
|Phone|string|the phone number of reseller|
|Country|string|the country code|
|Postcode|string|the postcode of reseller|
|Email|string|the email of reseller|
|Status|string|the status of reseller, value can be one of A(Active), P(Pendding) and S(Suspend)|
|Parent|SimpleReseller|reseller's parent|
|EntityAttributeValues|Dictionary&lt;string, string&gt;|dynamic attributes|
<br>
Structure of SimpleReseller

|Name|Type|Description|
|:--|:--|:--|
|ID|long|the id of reseller|
|Name|string|the name of reseller|



**Possible client validation errors**


> <font color="red">Parameter resellerId cannot be null and cannot be less than 1!</font>


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1759|Reseller doesn't exist|&nbsp;|

### Create a reseller

**API**

```
public Result<Reseller> CreateReseller(ResellerCreateRequest resellerCreateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|resellerCreateRequest|ResellerCreateRequest|false|the create request object, the structure like below|

Structure of class ResellerCreateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|Name of reseller, max length is 64.|
|Email|string|false|Email of reseller, max length is 255.|
|Country|string|false|Country code of reseller, max length is 64.|
|Contact|string|false|contact of reseller, max length is 64.|
|Phone|string|false|Phone number of reseller, max length is 32. Sample value 400-86554555.|
|Postcode|string|true|Post code, max length is 32. Sample value 510250.|
|Address|string|true|Address of reseller, max length is 255.|
|Company|string|true|Company of reseller, max length is 255.|
|ParentResellerName|string|true|Parent reseller name, if it is empty will set the root reseller of current marketplace as the parent reseller|
|EntityAttributeValues|Dictionary&lt;string, string&gt;|false|Dynamic attributes. Whether the attributes is required or not depends on the attributes configuration.|

**Sample codes**

```
ResellerApi api = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
ResellerCreateRequest request = new ResellerCreateRequest();
request.Name = "Reseller For Test";
request.Address = "suzhou";
request.Email = "zhangsan@163.com";
request.Country = "CN";
request.Contact = "ZhangSan";
request.Phone = "88889999";
Result<Reseller> result = api.CreateReseller(request);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["'Email' should not be empty.","'Country' should not be empty.","'Contact' should not be empty.","'Phone' should not be empty."]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1760,
	"Message": "Reseller name already exists"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"DntityAttributeValues": {
			"111": "tan2"
		},
		"Parent": {
			"ID": 4151,
			"Name": "New York"
		},
		"ID": 51741,
		"Name": "reseller_abc",
		"Phone": "87879696",
		"Country": "CN",
		"Contact": "Sam",
		"Email": "sam@gmail.com",
		"Status": "P"
	}
}
```

Type of data is Reseller, same as the API get reseller.

**Possible client validation errors**


> <font color="red">Parameter resellerCreateRequest cannot be null!</font><br/>
> <font color="red">'Name' should not be empty.</font><br/>
> <font color="red">'Email' should not be empty.</font><br/>
> <font color="red">'Country' should not be empty.</font><br/>
> <font color="red">'Contact' should not be empty.</font><br/>
> <font color="red">'Phone' should not be empty.</font><br/>
> <font color="red">'Email' is not a valid email address.</font><br/>
> <font color="red">The length of 'Name' must be 64 characters or fewer. You entered 100 characters.</font><br/>
> <font color="red">The length of 'Email' must be 255 characters or fewer. You entered 256 characters.</font><br/>
> <font color="red">The length of 'Country' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Contact' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Phone' must be 32 characters or fewer. You entered 60 characters.</font><br/>
> <font color="red">The length of 'Postcode' must be 16 characters or fewer. You entered 20 characters.</font><br/>
> <font color="red">The length of 'Address' must be 255 characters or fewer. You entered 300 characters.</font><br/>
> <font color="red">The length of 'Company' must be 255 characters or fewer. You entered 300 characters.</font><br/>
> <font color="red">The length of 'Parent Reseller Name' must be 64 characters or fewer. You entered 70 characters.</font><br/>


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1778|Parent reseller not found|&nbsp;|
|1779|Parent reseller is not active|&nbsp;|
|1782|At most 10 level resellers are supported|&nbsp;|
|1762|Reseller name is mandatory|&nbsp;|
|1763|Reseller contact is mandatory|&nbsp;|
|1764|Reseller phone is mandatory|&nbsp;|
|1765|Reseller email is mandatory|&nbsp;|
|1606|Country is mandatory|&nbsp;|
|1767|Reseller name is too long|&nbsp;|
|1768|Reseller contact is too long|&nbsp;|
|1769|Reseller phone is too long|&nbsp;|
|1770|Reseller email is too long|&nbsp;|
|1618|Postcode is too long|&nbsp;|
|1619|Address is too long|&nbsp;|
|1771|Reseller company is too long|&nbsp;|
|1105|Email is invalid|&nbsp;|
|1112|Phone No. is invalid|&nbsp;|
|1624|The name cannot contain special characters|Name can contain the characters 0-9, a-z, A-Z, space, Chinese characters,(,),_,.|
|3400|Country code is invalid|&nbsp;|

### Update a reseller

**API**

```
public Result<Reseller> UpdateReseller(long resellerId, ResellerUpdateRequest resellerUpdateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|resellerId|long|false|Reseller's id.|
|resellerUpdateRequest|ResellerUpdateRequest|false|The update request object, the structure like below|

Structure of class ResellerUpdateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|Name of reseller, max length is 64.|
|Email|string|false|Email of reseller, max length is 255.|
|Country|string|false|Country code of reseller, max length is 64.|
|Contact|string|false|contact of reseller, max length is 64.|
|Phone|string|false|Phone number of reseller, max length is 32. Sample value 400-86554555.|
|Postcode|string|true|Post code, max length is 32. Sample value 510250.|
|Address|string|true|Address of reseller, max length is 255.|
|Company|string|true|Company of reseller, max length is 255.|
|ParentResellerName|string|true|Parent reseller name, if it is empty will set the root reseller of current marketplace as the parent reseller. If the status of the updated reseller is active or suspend the parent cannot be changed.|
|EntityAttributeValues|Dictionary&lt;string, string&gt;|false|Dynamic attributes. Whether the attributes is required or not depends on the attributes configuration.|  


**Sample codes**

```
ResellerApi api = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
ResellerUpdateRequest updateRequest = new ResellerUpdateRequest();
updateRequest.Name = "Reseller For Test";
updateRequest.Address = "suzhou2";
updateRequest.Email = "zhangsan@163.com";
updateRequest.Country = "CN";
updateRequest.Contact = "ZhangSan2";
updateRequest.Phone = "44445555";
Result<Reseller> updateResult = api.UpdateReseller(resellerId, updateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["'Email' should not be empty.","'Country' should not be empty.","'Contact' should not be empty.","'Phone' should not be empty."]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 12000,
	"Message": "code is mandatory"
}
```

Note: the code in message is the dynamic attribute for the above failed sample result   


**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"EntityAttributeValues": {
			"code": "XY"
		},
		"Parent": {
			"ID": 4151,
			"Name": "New York"
		},
		"ID": 17850,
		"Name": "FVFFF",
		"Phone": "87879696",
		"Country": "CN",
		"Contact": "FFF",
		"Email": "FF@1234.COM",
		"Status": "S"
	}
}
```

Type of data is Reseller, same as the API get reseller.

**Possible client validation errors**  

> <font color="red">Parameter resellerId cannot be null and cannot be less than 1!</font><br/>
> <font color="red">Parameter resellerUpdateRequest cannot be null!</font><br/>
> <font color="red">'Name' should not be empty.</font><br/>
> <font color="red">'Email' should not be empty.</font><br/>
> <font color="red">'Country' should not be empty.</font><br/>
> <font color="red">'Contact' should not be empty.</font><br/>
> <font color="red">'Phone' should not be empty.</font><br/>
> <font color="red">'Email' is not a valid email address.</font><br/>
> <font color="red">The length of 'Name' must be 64 characters or fewer. You entered 100 characters.</font><br/>
> <font color="red">The length of 'Email' must be 255 characters or fewer. You entered 256 characters.</font><br/>
> <font color="red">The length of 'Country' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Contact' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Phone' must be 32 characters or fewer. You entered 60 characters.</font><br/>
> <font color="red">The length of 'Postcode' must be 16 characters or fewer. You entered 20 characters.</font><br/>
> <font color="red">The length of 'Address' must be 255 characters or fewer. You entered 300 characters.</font><br/>
> <font color="red">The length of 'Company' must be 255 characters or fewer. You entered 300 characters.</font><br/>
> <font color="red">The length of 'Parent Reseller Name' must be 64 characters or fewer. You entered 70 characters.</font>




<br>

**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1759|Reseller doesn't exist|&nbsp;|
|1778|Parent reseller not found|&nbsp;|
|1762|Reseller name is mandatory|&nbsp;|
|1764|Reseller phone is mandatory|&nbsp;|
|1606|Country is mandatory|&nbsp;|
|1763|Reseller contact is mandatory|&nbsp;|
|1765|Reseller email is mandatory|&nbsp;|
|1767|Reseller name is too long|&nbsp;|
|1769|Reseller phone is too long|&nbsp;|
|1768|Reseller contact is too long|&nbsp;|
|1618|Postcode is too long|&nbsp;|
|1619|Address is too long|&nbsp;|
|1771|Reseller company is too long|&nbsp;|
|1770|Reseller email is too long|&nbsp;|
|1105|Email is invalid|&nbsp;|
|1112|Phone No. is invalid|&nbsp;|
|1624|The name cannot contain special characters|Name can contain the characters 0-9, a-z, A-Z, space, Chinese characters,(,),_,.|
|3400|Country code is invalid|&nbsp;|
|1938|Reseller is active or suspend, parent cannot be updated!|&nbsp;|





### Activate a reseller

**API**

If activate reseller successfully there's not response content from remote server. So the data field in result is null whether activate sucessfully not not.

```
public Result<string> ActivateReseller(long resellerId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|resellerId|long|false|The reseller's id.|

**Sample codes**

```
ResellerApi api = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
Result<string> result = api.ActivateReseller(51739L);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter resellerId cannot be null and cannot be less than 1!"]
}
```

**Server side validation failed sample result(JSON formaatted)**

```
{
	"BusinessCode": 1891,
	"Message": "The reseller has already been activated!"
}
```

**Succssful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```


**Possible client validation errors**

> <font color="red">Parameter resellerId cannot be null and cannot be less than 1!</font>



**Possible business codes**

|BusinessCode|Message|Description|
|:--|:--|:--|
|1759|Reseller doesn't exist|The input reseller id not correct.|
|1891|The reseller has already been activated!|&nbsp;|
|1894|The reseller's parent is not active|&nbsp;|



### Disable a reseller

**API**

If disable successfully there's no response content from remote server. So the data field in result is null whether disable successfully or not.

```
public Result<string> DisableReseller(long resellerId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|resellerId|long|false|The reseller's id.|

**Sample codes**

```
ResellerApi api = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
Result<string> result = api.DisableReseller(51739L);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter resellerId cannot be null and cannot be less than 1!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1886,
	"Message": "The reseller is not active,unable to disable!"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```



**Possible client validation errors**

> <font color="red">Parameter resellerId cannot be null and cannot be less than 1!</font>



**Possible business codes**

|BusinessCode|Message|Description|
|:--|:--|:--|
|1759|Reseller doesn't exist|The input reseller id not correct.|
|1886|The reseller is not active,unable to disable!|&nbsp;|
|1793|The reseller has active merchants|&nbsp;|
|1794|The reseller has active terminals|&nbsp;|
|1795|The reseller has active terminal groups|&nbsp;|
|1781|The reseller has active sub-resellers|&nbsp;|

### Delete a reseller

**API**

If delete reseller successfully there's not response content from remote server. And the data field in result is always null.

```
public Result<string> DeleteReseller(long resellerId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|resellerId|long|false|The reseller's id.|

**Sample codes**

```
ResellerApi api = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
Result<string> result = api.DeleteReseller(51739L);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter resellerId cannot be null and cannot be less than 1!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1759,
	"Message": "Reseller doesn't exist"
}
```

**Successful sample result**

```
{
	"BusinessCode": 0
}
```


**Possible client validation errors**

> <font color="red">Parameter resellerId cannot be null and cannot be less than 1!</font>

**Possible business codes**

|BusinessCode|Message|Description|
|:--|:--|:--|
|1759|Reseller doesn't exist|The input reseller id not correct.|
|1875|The reseller is active,unable to delete!|&nbsp;|
|1775|Not allowed to delete the reseller of current user|&nbsp;|
|1761|Reseller has been used by merchant|&nbsp;|
|1785|The reseller has been used by terminal|&nbsp;|
|1788|The reseller has been used by terminal group|&nbsp;|
|1780|The reseller has sub-resellers|&nbsp;|

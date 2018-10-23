## Merchant APIs

The merchant APIs allow thirdparty system search merchants, get a merchant, create/update a merchant, activate/disable a merchant and delete a exist merchant.
All the merchant APIs are in the class *Paxstore.OpenApi.MerchantApi*.    

**Constructors of MerchantApi**

```
public MerchantApi(string baseUrl, string apiKey, string apiSecret)
```

Constructor parameters description   

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Search merchants

The search merchants API allows thirdparty system to search merchants by page.   

**API**

```
public Result<PagedMerchant>  SearchMerchant(int pageNo, int pageSize, MerchantSearchOrderBy orderBy, String name, MerchantStatus status)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|MerchantSearchOrderBy|false|the field name of sort order by. The value of this parameter can be one of MerchantSearchOrderBy.Name, MerchantSearchOrderBy.Phone and MerchantSearchOrderBy.Contact.|
|name|string|true|search filter by merchant name|
|status|MerchantStatus|false|the reseller status<br/> the value can be MerchantStatus.All, MerchantStatus.Active, MerchantStatus.Inactive, MerchantStatus.Suspend. If the value is MerchantStatus.All it will return merchant of all status|

**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
Result<PagedMerchant> result = API.SearchMerchant(1, 10, MerchantSearchOrderBy.Name, null, MerchantStatus.All);
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

**Successful sample result**

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
			"ID": 1000000134,
			"Name": "KFC",
			"Reseller": {
				"ID": 1000000225,
				"Name": "reseller_002"
			},
			"Country": "CN",
			"Postcode": null,
			"Address": null,
			"Contact": "tan",
			"Email": "abc@163.com",
			"Phone": "23231515",
			"Status": "A",
			"Description": "Merchant KFC"
		}]
	}
}
```

The type in dataSet is PagedMerchant. And the structure like below.

|Property Name|Type|Description|
|:--|:--|:--|
|ID|long|The id of merchant.|
|Name|string|The name of merchant.|
|Reseller|SimpleReseller|The reseller of the merchant belongs to.|
|Country|string|Country code of merchant.|
|Contact|string|Contact of merchant.|
|Email|string|Email of merchant.|
|Phone|string|Phone number of merchant.|
|Status|string|Status of merchant. Value can be one of A(Active), P(Pendding) and S(Suspend)|

The structure of class SimpleReseller

|Property Name|Type|Description|
|:--|:--|:--|
|ID|long|The id of reseller.|
|Name|string|The name of reseller.|


**Possible client validation errors**  

> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>


### Get a merchant

The get merchant API allows the thirdparty system get a merchant by merchant ID.
If the merchant does not exist the data field in result is null. 

**API**

```
public Result<Merchant>  GetMerchant(long merchantId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantId|long|false|The merchant id.|

**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
Result<Merchant> result = api.GetMerchant(72590);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1720,
	"Message": "Merchant doesn't exist",
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
		"EntityAttributeValues": null,
		"MerchantCategory": [],
		"ID": 1000000134,
		"Name": "KFC",
		"Reseller": {
			"ID": 1000000225,
			"Name": "reseller_002"
		},
		"Country": "CN",
		"Postcode": null,
		"Address": null,
		"Contact": "tan",
		"Email": "abc@163.com",
		"Phone": "23231515",
		"Status": "A",
		"Description": "Merchant KFC"
	},
	"PageInfo": null
}
```

The type of data in result is Merchant, and the structure shows below.

|Property Name|Type|Description|
|:--|:--|:--|
|ID|long|The id of merchant.|
|Name|string|The name of merchant.|
|Reseller|SimpleReseller|The reseller of the merchant belongs to.|
|Country|string|Country code of merchant.|
|Contact|string|Contact of merchant.|
|Email|string|Email of merchant.|
|Phone|string|Phone number of merchant.|
|Status|string|Status of merchant. Value can be one of A(Active), P(Pendding) and S(Suspend)|
|EntityAttributeValues|Dictionary&lt;string, string&gt;|Dynamic attributes of merchant.|
|MerchantCategory|List&lt;MerchantCategory&gt;|Categories of merchant belongs to.|

The structure of SimpleReseller already described in Search Merchants chapter.

**Possible client validation errors**

> <font color=red>Parameter merchantId cannot be null and cannot be less than 1!</font>  

**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1720|Merchant doesn't exist|&nbsp;|


### Create a merchant

Create merchant API allows thirdparty system create a merchant. If create successful SDK will return the created merchant in result.

**API**

```
public Result<Merchant>  CreateMerchant(MerchantCreateRequest merchantCreateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantCreateRequest|MerchantCreateRequest|false|The object of create request. The structure refer to below.|

Structure of class MerchantCreateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|Merchant name, max length is 64.|
|Email|string|false|Email of merchant, max length is 255.|
|ResellerName|string|false|Reseller name of merchant, max length is 64. Make sure the reseller exist.|
|Contact|string|false|Contact of merchant, max length is 64.|
|Country|string|false|Country code of merchant, max length is 64. Please refer to country codes table.|
|Phone|string|false|Phone number of merchant, max length is 32.|
|Postcode|string|true|Postcode of merchant, max length is 16.|
|Address|string|true|Address of merchant, max length is 255.|
|Description|string|true|Description of merchant, max length is 3000.|
|CreateUserFlag|bool|true|Indicate whether to create user when activate the merchant, won't create user when activate if this value is empty|
|MerchantCategoryNames|List&lt;string&gt;|true|Merchant categories. Make sure the categories are available.|
|EntityAttributeValues|Dictionary&lt;string, string&gt;|true|Dynamic attributes of merchant. Whether the attribute is required or not depend on the configuration of attribute.|


**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
MerchantCreateRequest merchantCreateRequest = new MerchantCreateRequest();
merchantCreateRequest.Name = "hrmj";
merchantCreateRequest.Email = "haoren@163.com";
merchantCreateRequest.ResellerName = "Pine Labs";
merchantCreateRequest.Contact = "haoren";
merchantCreateRequest.Country = "CN";
merchantCreateRequest.Description = "merchant hrmj";
merchantCreateRequest.Phone = "0512-59564515";
Result<Merchant> result = API.CreateMerchant(merchantCreateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["'Name' should not be empty.", "'Email' should not be empty.", "'Reseller Name' should not be empty.", "'Contact' should not be empty.", "'Country' should not be empty.", "'Phone' should not be empty."],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1721,
	"Message": "Merchant name already exists",
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
		"EntityAttributeValues": null,
		"MerchantCategory": [],
		"ID": 1000000155,
		"Name": "hrmj",
		"Reseller": {
			"ID": 1000000211,
			"Name": "Pine Labs"
		},
		"Country": "CN",
		"Postcode": null,
		"Address": null,
		"Contact": "haoren",
		"Email": "haoren@163.com",
		"Phone": "0512-59564515",
		"Status": "P",
		"Description": "merchant hrmj"
	},
	"PageInfo": null
}
```

The type of data in result is same as the get reseller API.

**Possible client validation errors**


> <font color="red">Parameter merchantCreateRequest cannot be null!</font><br/>
> <font color="red">'Name' should not be empty.</font><br/>
> <font color="red">'Email' should not be empty.</font><br/>
> <font color="red">'Reseller Name' should not be empty.</font><br/>
> <font color="red">'Country' should not be empty.</font><br/>
> <font color="red">'Contact' should not be empty.</font><br/>
> <font color="red">'Phone' should not be empty.</font><br/>
> <font color="red">'Email' is not a valid email address.</font><br/>
> <font color="red">The length of 'Name' must be 64 characters or fewer. You entered 100 characters.</font><br/>
> <font color="red">The length of 'Reseller Name' must be 64 characters or fewer. You entered 100 characters.</font><br/>
> <font color="red">The length of 'Email' must be 255 characters or fewer. You entered 256 characters.</font><br/>
> <font color="red">The length of 'Country' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Contact' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Phone' must be 32 characters or fewer. You entered 60 characters.</font><br/>
> <font color="red">The length of 'Postcode' must be 16 characters or fewer. You entered 20 characters.</font><br/>
> <font color="red">The length of 'Address' must be 255 characters or fewer. You entered 300 characters.</font><br/>
> <font color="red">The length of 'Description' must be 3000 characters or fewer. You entered 3008 characters.</font><br/>




**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1721|Merchant name already exists|&nbsp;|
|1759|Reseller doesn't exist|&nbsp;|
|16000|Merchant category not found|&nbsp;|
|1723|Merchant name is mandatory|&nbsp;|
|1725|Merchant reseller is mandatory|&nbsp;|
|1606|Country is mandatory|&nbsp;|
|1726|Merchant contact is mandatory|&nbsp;|
|1727|Merchant email is mandatory|&nbsp;|
|1728|Merchant phone is mandatory|&nbsp;|
|1729|Merchant name is too long|&nbsp;|
|1731|Merchant reseller is too long|&nbsp;|
|1618|Postcode is too long|&nbsp;|
|1619|Address is too long|&nbsp;|
|1732|Merchant contact is too long|&nbsp;|
|1733|Merchant email is too long|&nbsp;|
|1734|Merchant phone is too long|&nbsp;|
|1736|Merchant description is too long|&nbsp;|
|1105|Email is invalid|&nbsp;|
|1112|Phone No. is invalid|&nbsp;|
|3400|Country code is invalid|&nbsp;|
|1773|The associated reseller is not activate|&nbsp;|


### Update a merchant

Update merchant API allows the thirdparty system update a exist merchant.

**API**

```
public Result<Merchant>  UpdateMerchant(long merchantId, MerchantUpdateRequest merchantUpdateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantId|long|false|The id of merchant.|
|merchantUpdateRequest|MerchantUpdateRequest|false|The update request object. The structure shows below.|

Structure of class MerchantUpdateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|Merchant name, max length is 64.|
|Email|string|false|Email of merchant, max length is 255.|
|ResellerName|string|false|Reseller name of merchant, max length is 64. Make sure the reseller exist.|
|Contact|string|false|Contact of merchant, max length is 64.|
|Country|string|false|Country code of merchant, max length is 64. Please refer to country codes table.|
|Phone|string|false|Phone number of merchant, max length is 32.|
|Postcode|string|true|Postcode of merchant, max length is 16.|
|Address|string|true|Address of merchant, max length is 255.|
|Description|string|true|Description of merchant, max length is 3000.|
|CreateUserFlag|bool|true|Indicate whether to create user when activate the merchant, won't create user if this value is empty|
|MerchantCategoryNames|List&lt;string&gt;|true|Merchant categories. Make sure the categories are available.|
|EntityAttributeValues|Dictionary&lt;string, string&gt;|true|Dynamic attributes of merchant. Whether the attribute is required or not depend on the configuration of attribute.|


**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
MerchantUpdateRequest merchantUpdateRequest = new MerchantUpdateRequest();
merchantUpdateRequest.Name = "hrmj2";
merchantUpdateRequest.Email = "haoren2@163.com";
merchantUpdateRequest.ResellerName = "Pine Labs";
merchantUpdateRequest.Contact = "haoren2";
merchantUpdateRequest.Country = "CN";
merchantUpdateRequest.Description = "merchant hrmj2";
merchantUpdateRequest.Phone = "0512-88889999";
Result<Merchant> updateResult = api.UpdateMerchant(1000000155, merchantUpdateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["'Reseller Name' should not be empty.", "'Contact' should not be empty.", "'Country' should not be empty.", "'Phone' should not be empty."],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1720,
	"Message": "Merchant doesn't exist",
	"ValidationErrors": null,
	"Data": null,
	"PageInfo": null
}
```

**Succsssful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Message": null,
	"ValidationErrors": null,
	"Data": {
		"EntityAttributeValues": null,
		"MerchantCategory": [],
		"ID": 1000000155,
		"Name": "hrmj2",
		"Reseller": {
			"ID": 1000000211,
			"Name": "Pine Labs"
		},
		"Country": "CN",
		"Postcode": null,
		"Address": null,
		"Contact": "haoren2",
		"Email": "haoren2@163.com",
		"Phone": "0512-88889999",
		"Status": "P",
		"Description": "merchant hrmj2"
	},
	"PageInfo": null
}
```



The data type in result is same as get merchant API.

**Possible client validation errors**

> <font color="red">Parameter merchantId cannot be null and cannot be less than 1!</font><br/>
> <font color="red">Parameter merchantUpdateRequest cannot be null!</font><br/>
> <font color="red">'Name' should not be empty.</font><br/>
> <font color="red">'Email' should not be empty.</font><br/>
> <font color="red">'Reseller Name' should not be empty.</font><br/>
> <font color="red">'Country' should not be empty.</font><br/>
> <font color="red">'Contact' should not be empty.</font><br/>
> <font color="red">'Phone' should not be empty.</font><br/>
> <font color="red">'Email' is not a valid email address.</font><br/>
> <font color="red">The length of 'Name' must be 64 characters or fewer. You entered 100 characters.</font><br/>
> <font color="red">The length of 'Reseller Name' must be 64 characters or fewer. You entered 100 characters.</font><br/>
> <font color="red">The length of 'Email' must be 255 characters or fewer. You entered 256 characters.</font><br/>
> <font color="red">The length of 'Country' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Contact' must be 64 characters or fewer. You entered 70 characters.</font><br/>
> <font color="red">The length of 'Phone' must be 32 characters or fewer. You entered 60 characters.</font><br/>
> <font color="red">The length of 'Postcode' must be 16 characters or fewer. You entered 20 characters.</font><br/>
> <font color="red">The length of 'Address' must be 255 characters or fewer. You entered 300 characters.</font><br/>
> <font color="red">The length of 'Description' must be 3000 characters or fewer. You entered 3008 characters.</font><br/>


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1720|Merchant doesn't exist|&nbsp;|
|1721|Merchant name already exists|&nbsp;|
|1759|Reseller doesn't exist|&nbsp;|
|16000|Merchant category not found|&nbsp;|
|1723|Merchant name is mandatory|&nbsp;|
|1606|Country is mandatory|&nbsp;|
|1726|Merchant contact is mandatory|&nbsp;|
|1727|Merchant email is mandatory|&nbsp;|
|1728|Merchant phone is mandatory|&nbsp;|
|1725|Merchant reseller is mandatory|&nbsp;|
|1729|Merchant name is too long|&nbsp;|
|1618|Postcode is too long|&nbsp;|
|1619|Address is too long|&nbsp;|
|1732|Merchant contact is too long|&nbsp;|
|1733|Merchant email is too long|&nbsp;|
|1734|Merchant phone is too long|&nbsp;|
|1736|Merchant description is too long|&nbsp;|
|1105|Email is invalid|&nbsp;|
|1112|Phone No. is invalid|&nbsp;|
|3400|Country code is invalid|&nbsp;|
|1927|The merchant is not inactive,reseller cannot be updated!|&nbsp;|
|1759|Reseller doesn't exist|&nbsp;|
|1773|The associated reseller is not activate|&nbsp;|
|1936|The merchant is not inactive,merchant email cannot be updated!|Only the pending merchant can update the email|





### Activate a merchant

Activate merchant API allows the thirdparty system activate a inactive merchant. 
If activate successfully there's no response content from remote server.

**API**

```
public Result<string> ActivateMerchant(long merchantId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantId|Long|false|The merchant id.|

**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
Result<string> result = api.ActivateMerchant(72590);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1892,
	"Message": "The merchant has already been activated!",
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
> <font color=red>Parameter merchantId cannot be null and cannot be less than 1!</font>  


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1720|Merchant doesn't exist|&nbsp;|
|1759|Reseller doesn't exist|&nbsp;|
|1773|The associated reseller is not activate|&nbsp;|
|1892|The merchant has already been activated!|&nbsp;|


### Disable a merchant

Disable merchant API allows the thirdparty system disable a Active/Pendding merchant.
If disable successfully there's not response content from remote server.

**API**

```
public Result<string> DisableMerchant(long merchantId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantId|Long|false|The merchant id.|


**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
Result<string> result = api.DisableMerchant(72594);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1887,
	"Message": "The merchant is not active,unable to disable!",
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

> <font color=red>Parameter merchantId cannot be null and cannot be less than 1!</font>  


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1720|Merchant doesn't exist|&nbsp;|
|1887|The merchant is not active,unable to disable!|&nbsp;|
|1797|The merchant has active terminals|&nbsp;|



### Delete a merchant

Delete merchant API allows the thirdparty system delete a exist merchant.
If delete successfully there's no response content from remote server.

**API**

```
public Result<string> DeleteMerchant(long merchantId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantId|Long|false|The merchant id.|


**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
Result<string> result = api.DeleteMerchant(72593);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1720,
	"Message": "Merchant doesn't exist",
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

> <font color=red>Parameter merchantId cannot be null and cannot be less than 1!</font>  


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1720|Merchant doesn't exist|&nbsp;|
|1876|The merchant is active,unable to delete!|&nbsp;|
|1786|The merchant has been used by terminal|&nbsp;|



### Replace merchant email

This API is used to update the email of active merchant

**API**

```
public Result<string> ReplaceMerchantEmail(long merchantId, string email, bool createUser)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantId|long|false|The merchant id|
|email|string|false|The new email|
|createUser|bool|false|Indicate whether to create user when replace the email|


**Sample codes**

```
MerchantApi api = new MerchantApi(API_BASE_URL, API_KEY, API_SECRET);
Result<string> result = api.ReplaceMerchantEmail(72593, "zhangsan@pax.com", true);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantId cannot be null and cannot be less than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 1720,
	"Message": "Merchant doesn't exist",
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

> <font color=red>Parameter merchantId cannot be null and cannot be less than 1!</font>  
> <font color="red">'Email' should not be empty.</font>
> <font color="red">The length of 'Email' must be 255 characters or fewer. You entered 256 characters.</font>  
> <font color="red">'Email' is not a valid email address.</font>




**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|1720|Merchant doesn't exist|&nbsp;|
|1934|The merchant is not active,unable to replace user!|This API can only update the email of active merchants|
|1105|Email is invalid|The inputted email address is invalid|
|1933|The user email not update.|The inputted email address is same as the original email|


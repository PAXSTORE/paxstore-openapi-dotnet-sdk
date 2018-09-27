## Merchant Category APIs


The merchant category APIs allow thirdparty system get merchant categories by name, create merchante category, update merchant category, delete merchant category and batch create merchant categories.  

All the merchant APIs are in the class *Paxstore.OpenApi.MerchantCategoryApi*.   

**Constructors of MerchantCategoryApi**

```
public MerchantCategoryApi(string baseUrl, string apiKey, string apiSecret)
```

Constructor parameters description   

|Name|Type|Description|
|:--|:--|:--|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|


### Get merchant categories

The get merchant categories API allows thirdparty system to search the merchant categories by name. 

**API**

```
public Result<List<MerchantCategory>> GetMerchantCategories(string name)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|name|string|true|name of merchant category, if name is null API will return all the merchant categories|


**Sample codes**

```
MerchantCategoryApi api = new MerchantCategoryApi(API_BASE_URL, API_KEY, API_SECRET);
Result<List<MerchantCategory>> result = API.GetMerchantCategories("Fast food");
```



**Successful sample result(JSON formatted)**

```
{
    "BusinessCode": 0,
    "Message": null,
    "ValidationErrors": null,
    "Data": [{
        "ID": 2555,
        "Name": "Fast food",
        "Remarks": "fast food category"
    }],
    "PageInfo": null
}
```

The structure of the class MerchantCategory like below.


|Property Name|Type|Description|
|:--|:--|:--|
|ID|long|The id of merchant category.|
|Name|string|The name of merchant category.|
|Remarks|string|The remarks of merchant category.|








### Create a merchant category



**API**

```
public Result<MerchantCategory> CreateMerchantCategory(MerchantCategoryCreateRequest merchantCategoryCreateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantCategoryCreateRequest|MerchantCategoryCreateRequest|false|The object of create request. The structure refer to below.|

Structure of class MerchantCategoryCreateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|Name of merchant category,max length is 128.|
|Remarks|string|true|Remarks of merchant category, max length is 255.|



**Sample codes**

```
MerchantCategoryApi api = new MerchantCategoryApi(API_BASE_URL, API_KEY, API_SECRET);
MerchantCategoryCreateRequest createRequest = new MerchantCategoryCreateRequest();
createRequest.Name = "test";
createRequest.Remarks = "testdesc";
Result<MerchantCategory> createResult = api.CreateMerchantCategory(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantCategoryCreateRequest cannot be null!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 16001,
	"Message": "Merchant category name already exists",
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
		"ID": 2597,
		"Name": "test",
		"Remarks": "testdesc"
	},
	"PageInfo": null
}
```



**Possible client validation errors**
> <font color=red>Parameter merchantCategoryCreateRequest cannot be null!</font>  
> <font color=red>'Name' should not be empty.</font>  
> <font color=red>The length of 'Name' must be 128 characters or fewer. You entered 222 characters.</font>  
> <font color=red>The length of 'Remarks' must be 255 characters or fewer. You entered 280 characters.</font>  
 

**Possible business codes**  

|Business Code|Message|Description|
|:--|:--|:--|
|16001|Merchant category name already exists|&nbsp;|
|16002|Merchant category name is mandatory|&nbsp;|
|16003|Merchant category name is too long|&nbsp;|
|16004|Merchant category remarks is too long|&nbsp;|


### Update a merchant category

Update merchant category API allows the thirdparty system update a exist merchant category by id.

**API**

```
public Result<MerchantCategory> UpdateMerchantCategory(long merchantCategoryId, MerchantCategoryUpdateRequest merchantCategoryUpdateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantCategoryId|long|false|The id of merchant category.|
|merchantCategoryUpdateRequest|MerchantCategoryUpdateRequest|false|The update request object. The structure shows below.|

Structure of class MerchantCategoryUpdateRequest

|Property Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|Name|string|false|Merchant category name, max length is 128.|
|Remarks|string|true|Remarks of merchant category, max length is 255.|



**Sample codes**

```
MerchantCategoryApi api = new MerchantCategoryApi(API_BASE_URL, API_KEY, API_SECRET);
MerchantCategoryUpdateRequest updateRequest = new MerchantCategoryUpdateRequest();
updateRequest.Name = "test2";
updateRequest.Remarks = "test2desc";
Result<MerchantCategory> updateResult = api.UpdateMerchantCategory(2597, updateRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantCategoryId cannot be null and cannot be lese than 1"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 16001,
	"Message": "Merchant category name already exists",
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
		"ID": 2,
		"Name": "Retail",
		"Remarks": "This is a retail category"
	},
	"PageInfo": null
}
```





**Possible client validation errors**

> <font color=red>Parameter merchantCategoryId cannot be null and cannot be lese than 1</font>  
> <font color=red>Parameter merchantCategoryUpdateRequest cannot be null!</font>  
> <font color=red>'Name' should not be empty.</font>  
> <font color=red>The length of 'Name' must be 128 characters or fewer. You entered 222 characters.</font>  
> <font color=red>The length of 'Remarks' must be 255 characters or fewer. You entered 280 characters.</font>  




**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|16000|Merchant category not found|&nbsp;|
|16001|Merchant category name already exists|&nbsp;|
|16002|Merchant category name is mandatory|&nbsp;|
|16003|Merchant category name is too long|&nbsp;|
|16004|Merchant category remarks is too long|&nbsp;|







### Delete a merchant category

Delete merchant category API allows the thirdparty system delete a exist merchant category by id.


**API**  

```
public Result<string> DeleteMerchantCategory(long merchantCategoryId)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantCategoryId|long|false|The merchant category id.|


**Sample codes**

```
MerchantCategoryApi api = new MerchantCategoryApi(API_BASE_URL, API_KEY, API_SECRET);
Result<string> deleteResult = api.DeleteMerchantCategory(2597);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"Message": null,
	"ValidationErrors": ["Parameter merchantCategoryId cannot be null and cannot be lese than 1!"],
	"Data": null,
	"PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 16000,
	"Message": "Merchant category not found",
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

> <font color=red>Parameter merchantCategoryId cannot be null and cannot be lese than 1!</font>  


**Possible business codes**

|Business Code|Message|Description|
|:--|:--|:--|
|16000|Merchant category not found|&nbsp;|





### Batch create a merchant categories



**API**

```
public Result<List<MerchantCategory>> BatchCreateMerchantCategory(List<MerchantCategoryCreateRequest> merchantCategoryBatchCreateRequest, bool skipExist)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:--|:--|:--|:--|
|merchantCategoryBatchCreateRequest|List&lt;MerchantCategoryCreateRequest&gt;|false|List of merchant category to create|
|skipExist|bool|true|if the value is true, then the name of category in create list exist in system will skip when create, if the value is false then all the categories in create list won't created in system. The default value is false.|





**Sample codes**

```
MerchantCategoryApi api = new MerchantCategoryApi(API_BASE_URL, API_KEY, API_SECRET);
List<MerchantCategoryCreateRequest> createList = new List<MerchantCategoryCreateRequest>();
MerchantCategoryCreateRequest createRequest1 = new MerchantCategoryCreateRequest();
createRequest1.Name = "test_1";
createRequest1.Remarks = "testdesc_1";
MerchantCategoryCreateRequest createRequest2 = new MerchantCategoryCreateRequest();
createRequest2.Name = "test_2";
createRequest2.Remarks = "testdesc_2";
MerchantCategoryCreateRequest createRequest3 = new MerchantCategoryCreateRequest();
createRequest3.Name = "test_3";
createRequest3.Remarks = "testdesc_3";
createList.Add(createRequest1);
createList.Add(createRequest2);
createList.Add(createRequest3);
Result<List<MerchantCategory>> result = api.BatchCreateMerchantCategory(createList, true);
```

**Client side validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": -1,
    "Message": null,
    "ValidationErrors": ["All the category names in the list cannot be blank!"],
    "Data": null,
    "PageInfo": null
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
    "BusinessCode": 16009,
    "Message": "Merchant name(s) Retail in create list already exists in system",
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
    "Data": [{
        "ID": 1,
        "Name": "restaurant",
        "Remarks": "restaurant"
    }, {
        "ID": 2,
        "Name": "Fast Foods",
        "Remarks": "Fast Food remarks"
    }, {
        "ID": 3,
        "Name": "Retail",
        "Remarks": "Retail"
    }],
    "PageInfo": null
}
```



**Possible client validation errors**
> <font color=red>Parameter merchantCategoryBatchCreateRequest cannot be null and empty!</font>  
> <font color=red>All the category names in the list cannot be blank!</font>  
> <font color=red>Merchant category name '[NAME]' is too long!</font>  
> <font color=red>Merchant category remarks '[REMARKS]' is too long!</font>  

Note: [NAME] and [REMARKS] will be replaced by name and remarks of merchant category in batch create list.
 

**Possible business codes**    

|:--|:--|:--|  
|Business Code|Message|Description|
|16005|Batch create merchant category list is empty|&nbsp;|
|16006|All the names in batch create list cannot be empty|&nbsp;|
|16007|Merchant category names {0} in create list is too long|Note: {0} in message will be replaced by category name(s)|
|16008|Merchant category remarks {0} in create list is too longg|Note: {0} in message will be replaced by category remarks(s)|
|16009|Merchant category name(s) {0} in create list already exists in system|Note: {0} in the message will be replaced by merchante category name in the create list which is already exist in system.|

















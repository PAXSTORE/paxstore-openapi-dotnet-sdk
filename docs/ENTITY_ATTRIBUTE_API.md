## EntityAttributeAPI

All the entity attribute related APIs are encapsulated in the class *Paxstore.OpenApi.EntityAttributeApi*.

**Constructors of EntityAttribute**

```
public EntityAttributeApi(string baseUrl, string apiKey, string apiSecret) 
```

**Constructor parameters description**

|Name|Type|Description|
|:---|:---|:---|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin console, refe to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin console, refer to chapter Apply access rights|



### Get entity attribute

Get entity attribute by attributeId .


**API**

```
public Result<EntityAttribute> GetEntityAttribute(long attributeId)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|attributeId|long|false|the id of entity attribute|

**Sample codes**

```
EntityAttributeApi api = new EntityAttributeApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<EntityAttribute> result = api.GetEntityAttribute(1000000218);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": [Parameter attributeId cannot be less than 1!"]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 11000,
	"Message": "Entity attribute not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data":  {
		"EntityType": "Merchant",
		"MinLength": 2,
		"EntityAttributeLabelList": [{
			"Label": "testCreateApi-label-en-test",
			"Locale": "en"
		}, {
			"Label": "テストラベル-jp",
			"Locale": "jp"
		}, {
			"Label": "testCreateApi-label-update",
			"Locale": "zh_CN"
		}],
		"Index": 0,
		"InputType": "TEXT",
		"DefaultLabel": "testCreateApi-label-update",
		"ID": 1000000218,
		"MaxLength": 6,
		"Required": false,
		"Key": "123"
	}
}
```

The type in dataSet is EntityAttribute. And the structure like below.

| Name                     | Type                           | Description                                        |
| :----------------------- | :----------------------------- | :------------------------------------------------- |
| ID                       | long                           | the id of entity attribute                         |
| EntityType               | string                         | the type of entity attribute type                  |
| InputType                | string                         | the type of entity input type, value can be TEXT or SELECTOR                      |
| MinLength                | int                            | Minimal length of attribute value, this property is for TEXT input type entity attribute|
| MaxLength                | int                            | Maximal length of attribute value, this property is for TEXT input type entity attribute|
| Required                 | bool                           | Wether the value of the entity attribute is required or not
| Selector                 | string                         | The select options for SELECTOR input type entity attribute, the value msut be json formatted                                                   |
| Key                      | string                         | The key of the entity attribute                                                   |
| Index                    | int                            |                                                    |
| DefaultLabel             | string                         | The default of the entity attribute                                                   |
| EntityAttributeLabelList | IList\<EntityAttributeLabelInfo\> | the EntityAttributeLabelInfo structure like below. |

The type in data is EntityAttributeLabelInfo. And the structure like below.

| Name   | Type   | Description                 |
| :----- | :----- | :-------------------------- |
| Locale | string | the locale of language type |
| Label  | string |                             |

**Possible validation errors**

> <font color="red">Parameter attributeId cannot be less than 1!</font>

**Possible business codes**

| Business Code | Message                    | Description |
| :------------ | :------------------------- | :---------- |
| 11000         | Entity attribute not found |             |



### Search entity attributes

The search entity attributes API allows third party system to search entity attribute to the current market by page.
**API**

```
public Result<EntityAttribute> SearchEntityAttributes(int pageNo, int pageSize, Nullable<EntityAttributeSearchOrderBy> orderBy, string key, Nullable<EntityAttributeType> entityType)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:--- | :---|:---|:---|
|pageNo|int|false|page number, value must >=1|
|pageSize|int|false|the record number per page, range is 1 to 1000|
|orderBy|EntityAttributeSearchOrderBy|true|the sort order by field name, if this parameter is null the search result will order by created date descend. The value of this parameter can be one of EntityAttributeSearchOrderBy.EntityType_desc and EntityAttributeSearchOrderBy.EntityType_asc.|
|key|string|true|the value of the enity attribute key|
|entityType|EntityAttributeType|true|The value of this parameter can be one of EntityAttributeType.Merchant and EntityAttributeType.Reseller|

**Sample codes**

```
EntityAttributeApi api = new EntityAttributeApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<EntityAttribute> result = api.SearchEntityAttributes(1,1,EntityAttributeSearchOrderBy.EntityType_asc,null,null);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["'Page No' must be greater than '0'."]
}
```

**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 11002,
	"Message": "Invalid entity type"
}
```

**Successful sample result**

```
{
	"BusinessCode": 0,
	"PageInfo": {
		"PageNo": 1,
		"Limit": 1,
		"TotalCount": 14,
		"HasNext": true,
		"DataSet": [{
		"EntityType": "Merchant",
		"MinLength": 2,
		"EntityAttributeLabelList": [{
			"Label": "testCreateApi-label-en-test",
			"Locale": "en"
		}, {
			"Label": "テストラベル-jp",
			"Locale": "jp"
		}, {
			"Label": "testCreateApi-label-update",
			"Locale": "zh_CN"
		}],
		"Index": 0,
		"InputType": "TEXT",
		"DefaultLabel": "testCreateApi-label-update",
		"ID": 1000000218,
		"MaxLength": 6,
		"Required": false,
		"Key": "123"
	}]
	}
}
```

The type of data is EntityAttribute,EntityAttributeLabelInfo, and Refer to get entity attribute Api for structure .

**Possible client validation errors**  


> <font color="red">'Page Size' must be less than or equal to '1000'.</font><br>
> <font color="red">'Page No' must be greater than '0'.</font><br>
> <font color="red">'Page Size' must be greater than '0'.</font>

**Possible business codes**

| Business Code | Message             | Description |
| :------------ | :------------------ | :---------- |
| 11002         | Invalid entity type |             |



### Create entity attribute

This api allows the third party system create entity attribute  by EntityAttributeCreateRequest.

**API**

```
public Result<EntityAttribute> CreateEntityAttribute(EntityAttributeCreateRequest entityAttributeCreateRequest)
```

**Input parameter(s) description**

|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|entityAttributeCreateRequest|EntityAttributeCreateRequest|false|The create request|

Structure of class EntityAttributeCreateRequest.

| Property Name | Type                | Nullable | Description                                                  |
| :------------ | :------------------ | :------- | :----------------------------------------------------------- |
| EntityType    | EntityAttributeType | false    | The value of this parameter can be one of EntityAttributeType.Merchant and EntityAttributeType.Reseller |
| InputType     | EntityAttributeInputType     | false    | The value of this parameter can be one of EntityAttributeInputType.Text and EntityAttributeInputType.Selector |
| MinLength     | int             | true     | The property is for TEXT input type entity attribute                                                              |
| MaxLength     | int             | true     | The property is for TEXT input type entity attribute|
| Rrequired     | bool             | false    |                                                              |
| Selector      | string              | true     | The select options for Selector input type attribute, the value must be json format                                     |
| Key           | string              | false    | The key of entity attribute                               |
| DefaultLabel  | string              | false    | The default label of entity attribute                                                             |

**Sample codes**

```
EntityAttributeApi api = new EntityAttributeApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
EntityAttributeCreateRequest createRequest = new EntityAttributeCreateRequest();       
createRequest.EntityType = EntityAttributeType.Reseller;
createRequest.DefaultLabel = "testCreateApi-label-1";
createRequest.InputType = EntityAttributeInputType.Text);
createRequest.Key = "testCreateApi-key-01";
createRequest.MaxLength = 5;
createRequest.MinLength = 1;
createRequest.Required = false;
Result<EntityAttribute> result = api.CreateEntityAttribute(createRequest);
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": -1,
	"ValidationErrors": ["Parameter entityAttributerCreateRequest is mandatory!"]
}
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 11001,
	"Message": "Entity type is mandatory"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
		"EntityType": "Reseller",
		"MinLength": 1,
		"EntityAttributeLabelList": [{
			"Label": "testCreateApi-label-1",
			"Locale": "en"
		}, {
			"Label": "testCreateApi-label-1",
			"Locale": "zh_CN"
		}, {
			"Label": "testCreateApi-label-1",
			"Locale": "jp"
		}],
		"Index": 0,
		"InputType": "TEXT",
		"DefaultLabel": "testCreateApi-label-1",
		"ID": 1000000416,
		"MaxLength": 5,
		"Required": false,
		"Key": "testCreateApi-key-01"
	}
}
```

<br>
The type of data is EntityAttribute,EntityAttributeLabelInfo, and Refer to get entity attribute Api for structure .

**Possible client validation errors**


> <font color="red">Parameter entityAttributerCreateRequest is mandatory!</font>

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
| 135           |Request parameter is missing or invalid|&nbsp;|
| 5000 |Language not supported||
| 11001     |Entity type is mandatory||
| 11002     |Invalid entity type||
| 11003     |Input type is mandatory||
| 11004     |Invalid input type||
| 11005 |Entity attribute label is mandatory||
| 11006 |Entity attribute label is too long||
| 11007 |Select options is mandatory||
| 11008 |Invalid select options||
| 11009 |Invalid min or max length||
| 11010     |Entity attribute key is mandatory||
| 11011     |Entity attribute key is too long||
| 11012 |Entity attribute key is already exist||
| 11013 |Invalid regular expression||
| 11005 |Entity attribute label is mandatory||




### Update entity attribute by id

This api allows the third party system update entity attribute by attributeId

**API**

```
public Result<EntityAttribute> UpdateEntityAttribute(long attributeId, EntityAttributeUpdateRequest entityAttributeUpdateRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|attributeId|long|false|the id of the entity attribute|
|entityAttributeUpdateRequest|EntityAttributeUpdateRequest|false|The request body|

Structure of class entityAttributeUpdateRequest.

| Property Name | Type            | Nullable | Description                                                  |
| :------------ | :-------------- | :------- | :----------------------------------------------------------- |
| InputType     | EntityAttributeInputType | false    | The value of this parameter can be one of EntityAttributeInputType.Text and EntityAttributeInputType.Selector. |
| MinLength     | int         | true     | If the value is null API won't update it                                                             |
| MaxLength     | int         | true     | If the value is null API won't update it                                                             |
| Required      | bool         | false    |                                                              |
| Selector      | string          | true     | The select options                                     |
| DefaultLabel  | string          | false    | If the value is null API won't update it                                                             |

**Sample codes**

```
EntityAttributeApi api = new EntityAttributeApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
EntityAttributeLabelUpdateRequest updateRequest = new EntityAttributeLabelUpdateRequest();
updateRequest.DefaultLabel = "testCreateApi-label-update";
updateRequest.InputType = EntityAttributeInputType.Text;
updateRequest.MaxLength = 6;
updateRequest.MinLength = 2;
updateRequest.Required = false;
Result<EntityAttribute> result = api.UpdateEntityAttribute(1000000416L,updateRequest);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 11003,
	"Message": "Input type is mandatory"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```


**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|135| Request parameter is missing or invalid ||
|5000| Language not supported                  ||
|11000| Entity attribute not found              ||
|11001| Entity type is mandatory                ||
|11002| Invalid entity type                     ||
| 11003         |Input type is mandatory||
| 11004         |Invalid input type||
| 11005         |Entity attribute label is mandatory||
| 11006         |Entity attribute label is too long||
| 11007         |Select options is mandatory||
| 11008         |Invalid select options||
| 11009         |Invalid min or max length||
| 11010         |Entity attribute key is mandatory||
| 11011         |Entity attribute key is too long||
| 11012         |Entity attribute key is already exist||
| 11013         |Invalid regular expression||



### Update entity attribute label

This api allows the third party system  update entity attribute label.


**API**

```
public Result<string> UpdateEntityAttributeLabel(long attributeId, EntityAttributeLabelUpdateRequest updateLabelRequest)
```

**Input parameter(s) description**  


|Parameter Name|Type|Nullable|Description|
|:---|:---|:---|:---|
|attributeId|long|false|the id of the entity attribute|
|updateLabelRequest|EntityAttributeLabelUpdateRequest|false||

Structure of class EntityAttributeLabelUpdateRequest

| Property Name            | Type                           | Nullable | Description |
| :----------------------- | :----------------------------- | :------- | :---------- |
| EntityAttributeLabelList | List<EntityAttributeLabelInfo> | false    |             |

Structure of class EntityAttributeLabelInfo.

| Property Name | Type   | Nullable | Description |
| :------------ | :----- | :------- | :---------- |
| Locale        | string | false    |             |
| Label         | string | false    |             |

**Sample codes**

```
EntityAttributeApi api = new EntityAttributeApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");

EntityAttributeLabelInfo labelInfoJp = new EntityAttributeLabelInfo();
EntityAttributeLabelInfo labelInfoEn = new EntityAttributeLabelInfo();
EntityAttributeLabelInfo labelInfoZh = new EntityAttributeLabelInfo();
labelInfoJp.Label = "テストラベル-jp";
labelInfoJp.Locale = "jp";
labelInfoEn.Label = "testCreateApi-label-en";
labelInfoEn.Locale ="en";
labelInfoZh.Label = "测试updateAPI-zh";
labelInfoZh.Locale = "zh_CN";

List<EntityAttributeLabelInfo> entityAttributeLabelList = new ArrayList<>();
entityAttributeLabelList.Add(labelInfoJp);
entityAttributeLabelList.Add(labelInfoEn);
entityAttributeLabelList.Add(labelInfoZh);
EntityAttributeLabelUpdateRequest updateRequest = new EntityAttributeLabelUpdateRequest();
updateRequest.EntityAttributeLabelList = entityAttributeLabelList;

Result<string> result = api.UpdateEntityAttributeLabel(1000000416L,updateRequest);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 11000,
	"Message": "Entity attribute not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|135|Request parameter is missing or invalid||
|5000|Language not supported||
|11000|Entity attribute not found||
|11005|Entity attribute label is mandatory||
|11006|Entity attribute label is too long||



### Delete entity attribute

This api allows the third party system  delete entity attribute.

**API**

```
public Result<string> DeleteEntityAttribute(long attributeId)
```

**Input parameter(s) description**  

| Parameter Name | Type | Nullable | Description                    |
| :------------- | :--- | :------- | :----------------------------- |
| attributeId    | long | false    | the id of the entity attribute |

**Sample codes**

```
EntityAttributeApi api = new EntityAttributeApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN");
Result<string> result = api.DeleteEntityAttribute(1000000416);
```


**Server side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 11000,
	"Message": "Entity attribute not found"
}
```

**Successful sample result(JSON formatted)**

```
{
	"BusinessCode": 0
}
```
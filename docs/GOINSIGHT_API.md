## GoInsight APIs

GoInsight APIs allow third party system search app bizData form goInsight.

  

**Constructors of GoInsightApi**

```
public GoInsightApi(string baseUrl, string apiKey, String apiSecret);
public GoInsightApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo)
```

**Constructor parameters description**

|Name|Type|Description|
|:----|:----|:----|
|baseUrl|string|the base url of REST API|
|apiKey|string|the apiKey of marketplace, get this key from PAXSTORE admin/reseller console, refer to chapter Apply access rights|
|apiSecret|string|apiSecret, get api secret from PAXSTORE admin/reseller console, refer to chapter Apply access rights|
|timeZoneInfo|TimeZoneInfo|the timeZone, the default timeZone is TimeZoneInfo.Local, the biz data in return object depend on timeZone|


### Search APP BizData

The search app bizData API allow the third party system search data.  
Note: This result of this API depends on the API query settings in GoInsight. Paging needs to be set when the query result set type is a details data.

**API**

```
public Result<DataQueryResult> FindDataFromInsight(string queryCode)

public Result<DataQueryResult> FindDataFromInsight(string queryCode, TimeRange timeRange)
   
public Result<DataQueryResult> FindDataFromInsight(string queryCode, TimeRange timeRange, Nullable<int> pageNo, Nullable<int> pageSize)
```

**Input parameter(s) description**

| Name| Type | Nullable|Description |
|:---- | :----|:----|:----|
|queryCode|string|false|search by GoInsight api query code|
|rangeType|TimeRange|true|you can choose the range of data results for search|
|pageNo|Nullable<int>|true|page number, value must >= 1|
|pageSize|Nullable<int>|true|the record number per page, range is 1 to 100 for details data query, range is 1 to 1000 for statistics data query|

Note: The pageNo param will be ignore when your query result set type is statistics chart.

Value of enum TimeRange

| Value | Description |
|:---- |:----|
|YESTERDAY|Yesterday|
|LAST_WEEK|Last Week|
|LAST_MONTH|Last Month|
|LAST_YEAR|Last Year|
|RECENT_DAY|Recent Day|
|RECENT_WEEK|Recent Week|
|RECENT_MONTH|Recent Month|
|RECENT_YEAR|Recent Year|
|TODAY|Today|
|THIS_WEEK|This Week|
|THIS_MONTH|This Month|
|THIS_YEAR|This Year|


**Sample codes**

```
GoInsightApi goInsightApi = new GoInsightApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN", TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
Result<DataQueryResult> resultData = goInsightApi.FindDataFromInsight("ahh3y62t");
```

**Client side validation failed sample result(JSON formatted)**

```
{
	"BusinessCode": 36000,
	"ValidationErrors": ["The query code is not found"]
}
```

**Succssful sample result(JSON formatted)**

```
{
	"BusinessCode": 0,
	"Data": {
        "WorksheetName": "Merchant transaction amount trend (This Year)",
		"Columns": [{
			"ColName": "acquirer_type",
			"DisplayName": "Acquirer Type",
            "Type": "Dimension"
		}, {
			"ColName": "currency",
			"DisplayName": "Currency",
            "Type": "Dimension"
		}, {
			"ColName": "purchase_id",
			"DisplayName": "Purchase ID",
            "Type": "Dimension"
		}, {
			"ColName": "amount",
			"DisplayName": "Amount",
            "Type": "Measure"
		}, {
			"ColName": "tax",
			"DisplayName": "Tax",
            "Type": "Measure"
		}, {
			"ColName": "_sys_marketid",
			"DisplayName": "Marketplace",
            "Type": "Dimension"
		}, {
			"ColName": "_sys_merchantid",
			"DisplayName": "Merchant",
            "Type": "Dimension"
		}, {
			"ColName": "_sys_terminalid",
			"DisplayName": "Terminal",
            "Type": "Dimension"
		}],
		"Rows": [
			[{
				"ColName": "acquirer_type",
				"Value": "ZTO"
			}, {
				"ColName": "currency",
				"Value": "USD"
			}, {
                "ColName": "purchase_id",
				"Value": "15851195134847"
			}, {
                "ColName": "amount",
				"Value": "169.15"
			}, {
                "ColName": "tax",
				"Value": "64.38"
			}, {
				"ColName": "_sys_marketid",
				"Value": "demo"
			}, {
				"ColName": "_sys_merchantid",
				"Value": "Macy’s"
			}, {
				"ColName": "_sys_terminalid",
				"Value": "0820087295"
			}],
            [{
				"ColName": "acquirer_type",
				"Value": "ZTO"
			}, {
				"ColName": "currency",
				"Value": "USD"
			}, {
                "ColName": "purchase_id",
				"Value": "15851135975100"
			}, {
                "ColName": "amount",
				"Value": "2990.09"
			}, {
                "ColName": "tax",
				"Value": "64.12"
			}, {
				"ColName": "_sys_marketid",
				"Value": "demo"
			}, {
				"ColName": "_sys_merchantid",
				"Value": "Macy’s"
			}, {
				"ColName": "_sys_terminalid",
				"Value": "0820087295"
			}]
		],
        "Offset": 10,
		"Limit": 10,
		"HasNext": true,
	},
	"RateLimitRemain": ""
}
```

The type in dataSet of result is DataQueryResult. The structure shows below.

Structure of class DataQueryResult

|Property Name|Type|Description|
|:---|:---|:---|
|WorksheetName|string|The result set worksheet name.|
|Columns|List<Column>|The result set column.|
|Rows|List<List<Row>>|The result set.|
|HasNext|Nullable<bool>|Is there any data.|
|Offset|Nullable<long>|Rows offset if exit page info.|
|Limit|Nullable<int>|Rows page size if exit page info.|

Structure of class Column

|Property Name|Type|Description|
|:---|:---|:---|
|ColName|string|The dataset filed name in GoInsight|
|DisplayName|string|The dataset filed's display name|
|Type|string|Data analysis type of dataset field. Value can be one of Dimension, Measure and Image|

Structure of class Row  

|Property Name|Type|Description|
|:---|:---|:---|
|ColName|string|The dataset filed name in GoInsight|
|Value|string|The dataset filed's value|

**Possible client validation errors**
 
> <font color=red>Parameter queryCode is mandatory!</font>  
> <font color=red>The length of parameter queryCode must 8!</font>
> <font color=red>Parameter pageSize must be range is 1 to 1000</font>

**Possible business codes**

|Business Code|Message|Description|
|:---|:---|:---|
|36000|The query code is not found|&nbsp;|
|36001|The query code is not active|&nbsp;|
|36002|Query failed, please try again|&nbsp;|
|36003|The query is timeout, please try again|&nbsp;|
|36004|Insufficient permissions|&nbsp;|
|36005|Invalid pageNo|&nbsp;|
|36006|Invalid pageSize|&nbsp;|
|36008|Query failed, please contact administrator|&nbsp;|
|36009|Too many request, please try again later|&nbsp;

**Possible abnormal http codes**

|Http Code|Message|Description|
|:---|:---|:---|
|429|Too many request, please try again in one minute, two minutes or whatever|&nbsp;

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
String[] ids = TimeZone.getAvailableIDs();
//TimeZone tz1 = TimeZone.getTimeZone(ids[2]);
TimeZone tz = TimeZone.getTimeZone("Etc/GMT-1");
GoInsightApi goInsightApi = new GoInsightApi("https://api.whatspos.com/p-market-api", "RCA9MDH6YN3WSSGPW6TJ", "TUNLDZVZECHNKZ4FW07XFCKN2W0N8ZDEA5ENKZYN", tz);
Result<DataQueryResultDTO> resultData = goInsightApi.findDataFromInsight("ahh3y62t");
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
	"businessCode": 0,
	"data": {
        "worksheetName": "Merchant transaction amount trend (This Year)",
		"columns": [{
			"colName": "acquirer_type",
			"displayName": "Acquirer Type",
            "type": "Dimension"
		}, {
			"colName": "currency",
			"displayName": "Currency",
            "type": "Dimension"
		}, {
			"colName": "purchase_id",
			"displayName": "Purchase ID",
            "type": "Dimension"
		}, {
			"colName": "amount",
			"displayName": "Amount",
            "type": "Measure"
		}, {
			"colName": "tax",
			"displayName": "Tax",
            "type": "Measure"
		}, {
			"colName": "_sys_marketid",
			"displayName": "Marketplace",
            "type": "Dimension"
		}, {
			"colName": "_sys_merchantid",
			"displayName": "Merchant",
            "type": "Dimension"
		}, {
			"colName": "_sys_terminalid",
			"displayName": "Terminal",
            "type": "Dimension"
		}],
		"rows": [
			[{
				"colName": "acquirer_type",
				"value": "ZTO"
			}, {
				"colName": "currency",
				"value": "USD"
			}, {
                "colName": "purchase_id",
				"value": "15851195134847"
			}, {
                "colName": "amount",
				"value": "169.15"
			}, {
                "colName": "tax",
				"value": "64.38"
			}, {
				"colName": "_sys_marketid",
				"value": "demo"
			}, {
				"colName": "_sys_merchantid",
				"value": "Macy’s"
			}, {
				"colName": "_sys_terminalid",
				"value": "0820087295"
			}],
            [{
				"colName": "acquirer_type",
				"value": "ZTO"
			}, {
				"colName": "currency",
				"value": "USD"
			}, {
                "colName": "purchase_id",
				"value": "15851135975100"
			}, {
                "colName": "amount",
				"value": "2990.09"
			}, {
                "colName": "tax",
				"value": "64.12"
			}, {
				"colName": "_sys_marketid",
				"value": "demo"
			}, {
				"colName": "_sys_merchantid",
				"value": "Macy’s"
			}, {
				"colName": "_sys_terminalid",
				"value": "0820087295"
			}]
		],
        "offset": 10,
		"limit": 10,
		"hasNext": true,
	},
	"rateLimitRemain": ""
}
```

The type in dataSet of result is DataQueryResultDTO. The structure shows below.

Structure of class TerminalDTO

|Property Name|Type|Description|
|:---|:---|:---|
|worksheetName|String|The result set worksheet name.|
|columns|List<Column>|The result set column.|
|rows|List<List<Row>>|The result set.|
|hasNext|Boolean|Is there any data.|
|offset|int|Rows offset if exit page info.|
|limit|int|Rows page size if exit page info.|

Structure of class Column

|Property Name|Type|Description|
|:---|:---|:---|
|colName|String|The dataset filed name in GoInsight|
|displayName|String|The dataset filed's display name|
|type|String|Data analysis type of dataset field. Value can be one of Dimension, Measure and Image|

Structure of class Row  

|Property Name|Type|Description|
|:---|:---|:---|
|colName|String|The dataset filed name in GoInsight|
|value|String|The dataset filed's value|

**Possible client validation errors**
 
> <font color=red>Parameter queryCode cannot be null</font>  
> <font color=red>Parameter queryCode length must is 8</font>
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

# Changelog  

## 9.2.0  
### New Feature  
* TerminalApi supports get terminal network information
* TerminalApi supports copy a exist terminal
* Support specify model name as filter when search application
* Add MerchantVariableApi which includes merchant variable related opertions
* Thirdparty dependencies are updated, netlog was changed to Serilog 3.1.1. FluentValidation was updated to 11.8.0. Newtonsoft.Json was updated to 13.0.3. RestSharp was updated to 110.2.0

### Bug Fix
* Some errors in the document were updated.

### Breaking Changes
* TargetFrameworks was updated to netstandard2.0;netstandard2.1;net6.0;net462;net471. Please check the compatibility before update SDK.
* Proxy setting was add to the constructor parameters instead of separate method.
* Separate methods to set connectionTimeoutTime and readWriteTimeoutTime were removed. Please use constructor parameter 'timeout' to set the request timeout value. The default value is 5000 miliseconds. 


## 8.6.0
### Improvement
* The fields such as effectiveTime and expiredTime are added respectively for terminal and group push related tasks.
* Search terminals API is updated, add resellerName and merchantName parameters.

### Breaking Changes
* The original methods of timeout setting and proxy setting are not supported. Both timeout and proxy setting are moved to the API constructor. Please refer to the API document.


## 8.5.0  
### Improvement  
* Updated get terminal details API to support return accessory list  
* Updated create merchant API and update merchant API, fields email, contact, country and phone are not mandatory anymore when create/update merchant.

### Breaking Changes
* The API TerminalAPI.GetTerminal(long terminalId, boolean includeDetailInfoList) is not compatible with the old API, 
the structure of the return result is not same as before. It will cause compile fail if developer do not change codes after upgrading SDK to 8.5.0.
Please refer to the API document.   


## 8.3.1  
### Bug Fix  
* Fix TID, serial No. bug in create terminal variable API  


## 8.3.0  
### New Feature  
* Add group RKI related APIs  


## 8.2  

### New Feature
* Terminal added new property 'Remark'  

### Improvement  
* The max length of Location property in terminal increased to 64  


## 8.1  

### New Feature   
* PushHistoryApi supports search latest parameter push history
* PushHistoryApi supports search optimized parameter push history
* PushHistoryApi supports search latest optimized parameter push history

### Improvement  
* Add new search app API to support search apps filter by reseller
* Add new search push firmware task API to support search by tasks filter by serial number
* TerminalApkApi supports push a parameter app inherite parameters from the lastest success push history
* EntityAttributeApi does not support regular expression anymore
* TerminalVariableApi does not support specify the version when create variable

### Breaking Changes
* GROUP is removed from enum VariableSource, if you are using it when search terminal variables you need to change codes.
* Version property is removed from class ParameterVariable.

## 8.0  
### New Feature  
* Terminal API supports get details when get single terminal  

## 7.4.0  
### New Feature  
* Terminal API supports push command(lock, unlock, restart) to terminal  

## 7.3.2 

### Bug fix    
* Fix SDK version incorrect issue

## 7.3.1 

### Improvement  
* Add Name and FileSize properties to ApkFile in search application result


## 7.3.0  

### Bug fix
* Fixed the pagination issue for search app API

### Breaking Changes  
* Change property status to Status in the class PagedApp
* Max records per page is reduced from 1000 to 100 (except GoInsightApi). If pass a value greate than 100 SDK validation will fail.
* Developer inforamtion is removed from search result(PagedApp class) of AppApi


## 7.2.0  
### New features(To support these new features server side application must upgrade to version 7.2.0)  

* Merchant supports new properties 'city' and 'province'
* Add new terminal related APIs in TerminalApi
  1. Move terminal to another reseller and merchant  ```MoveTerminal(long terminalId, string resellerName, string merchantName) ```  
  2. Get terminal PED information  ```GetTerminalPED(long terminalId)```  
  3. Get terminal configuration(allow replacement)  ```GetTerminalConfig(long terminalId)```  
  4. Update terminal configuration(allow replacement)  ```UpdateTerminalConfig(long terminalId, TerminalConfigUpdateRequest terminalConfigUpdateRequest)```
* Add GoInsight related APIs
  1. Find app business data by query code  ```FindDataFromInsight(string queryCode)```  
  2. Find app business data by query code and date range  ```FindDataFromInsight(string queryCode, TimeRange timeRange)```  
  3. Find app business data by query code, date range and page  ```FindDataFromInsight(string queryCode, TimeRange timeRange, int pageNo, int pageSize)```


## 7.1.0  
### New features(To support these new feature Paxstore must upgrade to version 7.1)  
* Add entity attribute APIs (EntityAttributeApi)  
  1. Get entity attribute by id  ```GetEntityAttribute(long attributeId)```  
  2. Search entity attribute  ```SearchEntityAttributes(int pageNo, int pageSize, Nullable<EntityAttributeSearchOrderBy> orderBy, string key, Nullable<EntityAttributeType> entityType)```  
  3. Create entity attribute(this API is market level, reseller has not permission)  ```CreateEntityAttribute(EntityAttributeCreateRequest entityAttributeCreateRequest)```  
  4. Update entity attribute(this API is market level, reseller has not permission)  ```UpdateEntityAttribute(long attributeId, EntityAttributeUpdateRequest entityAttributeUpdateRequest)```  
  5. Update entity attribute label(this API is market level, reseller has not permission)  ```UpdateEntityAttributeLabel(long attributeId, EntityAttributeLabelUpdateRequest updateLabelRequest)```  
  6. Delete entity attribute(this API is market level, reseller has not permission)  ```DeleteEntityAttribute(long attributeId)```  
  <br>
* Add terminal group related APIs (TerminalGroupApi)  
  1. Search terminal group  ```SearchTerminalGroup(int pageNo, int pageSize, Nullable<TerminalGroupSearchOrderBy> orderBy, Nullable<TerminalGroupStatus> status, string name, string resellerNames, string modelNames, Nullable<bool> isDynamic)```   
  2. Get terminal group by id  ```GetTerminalGroup(long groupId)```  
  3. Create terminal group  ```CreateTerminalGroup(CreateTerminalGroupRequest createTerminalGroupRequest)```  
  4. Search terminal  ```SearchTerminal(int pageNo, int pageSize, Nullable<TerminalSearchOrderBy> orderBy, Nullable<TerminalStatus> status, string modelName, string resellerName, string merchantName, string serialNo, Nullable<bool> excludeGroupId)```  
  5. Update terminal group  ```UpdateTerminalGroup(long groupId, UpdateTerminalGroupRequest updateTerminalGroupRequest)```  
  6. Activate terminal group```ActiveGroup(long groupId)```  
  7. Disable terminal group  ```DisableGroup(long groupId)```  
  8. Delete terminal group  ```DeleteGroup(long groupId)```  
  9. Search terminals in group  ```SearchTerminalsInGroup(int pageNo, int pageSize, Nullable<TerminalSearchOrderBy> orderBy, long groupId, string serialNo, string merchantNames)```  
  10. Add terminal(s) to group  ```AddTerminalToGroup(long groupId, HashSet<long> terminalIds)```  
  11. Remove terminal(s) out of group  ```RemoveTerminalOutGroup(long groupId, HashSet<long> terminalIds)```  
  <br>
* Add terminal group push related APIs (TerminalGroupApkApi)   
  1. Get terminal group push task and include specified parameters in result  ```GetTerminalGroupApk(long groupApkId, List<string> pidList)```  
  2. Search terminal group push task  ```SearchTerminalGroupApk(int pageNo, int pageSize, Nullable<TerminalGroupApkSearchOrderBy> orderBy, long groupId, Nullable<bool> pendingOnly, Nullable<bool> historyOnly, string keyWords)```  
  3. Create terminal group push task  ```CreateAndActiveGroupApk(CreateTerminalGroupApkRequest createTerminalGroupApkRequest)```  
  4. Suspend terminal group push task  ```SuspendTerminalGroupApk(long groupApkId)```  
  5. Delete terminal group push task  ```DeleteTerminalGroupApk(long groupApkId)```  
  <br>
* Add terminal RKI(remote key injection) APIs (TerminalRkiApi)  
  1. Push RKI key to terminal  ```PushRkiKey2Terminal(PushRki2TerminalRequest pushRki2TerminalRequest)```  
  2. Search RKI push task  ```SearchPushRkiTasks(int pageNo, int pageSize, Nullable<SearchOrderBy> orderBy, string terminalTid, string rkiKey, PushStatus status)```  
  3. Get RKI push task  ```GetPushRkiTask(long pushRkiTaskId)```  
  4. Disable RKI push task  ```DisablePushRkiTask(DisablePushRkiTaskRequest disablePushRkiTaskRequest)```  
  <br>
* Update terminal APIs (TerminalApi)  
  1. Batch add terminal(s) to group(s)  ```BatchAddTerminalToGroup(TerminalGroupRequest batchAddTerminalToGroupRequest)```
  <br>
* Update reseller APIs (ResellerApi)  
  1. Search reseller RKI keys  ```SearchResellerRkiKeyList(long resellerId, int pageNo, int pageSize, string rkiKey)```  

## 7.0.1  

The search API in PushHistoryAPI is removed which provided in version 7.0. Please use the latest SDK 7.0.1


## 7.0  
### New features
* Add new search terminal API to include geo location, firmware and installed app in search result
* Add push template related APIs
* Add terminal variable related APIs
* Add search push history result API
* Add API to verify terminal estate
* Support push file type parameters when push app and parameter to terminal

## 6.3.3  

### New features

*  Support proxy

## 6.3.2  

### Bug fixes
* Fix dupliated businessCode issue (16111, 16104, 16000)  
As above 3 business codes are generated at SDK side not from server response. So they may be same as the business code returned by server. Currently we found the SDK generated business code 16000 same as the one from server response 16000 (Merchant category not found).

Now BusinessCode 16111 changed to -2, 16104 changed to -3, 16000 changed to -2



## 6.3.1  

### New features

* Add search firmware push history API
* Add get single firmware push history API
* Add suspend firmware push API

## 6.3.0  

### New features

* Add search app API
* Add push firmware to terminal API
* Add suspend APK push API
* Add uninstall APP API


### Improvement

* Add request rate limit feature
* Add set connectionTimeout and readWriteTimeout method for API


## 6.2.2

* Add 2 APIs to allow third party system search push apk history and get push apk history by id


## 6.2.1  

### Improvement

* Add location field in response of terminal related APIs

## 6.2

### Improvement  

* Support activate the reseller when create reseller, this improvement only works with PAXSTORE v6.2 and later.
* Support activate the merchant when create merchant, this improvement only works with PAXSTORE v6.2 and later.

### Bug fixes  

### Breaking changes  
NA


## 6.1 

### Improvement

* Separate replace merchant email from update merchant API, add additional replace merchant email API.
* Separate replace reseller email from update merchant API, add additional replace reseller email API.
* Do not create user when create reseller but in activate reseller step. Won't affect code.
* *Email* is not mandatory for updating merchant and reseller. If *Email* is empty when call update merchant or reseller API the email won't be updated.
* *ResellerName* is not mandatory for updating merchant. If *ResellerName* is empty when call update merchant API the merchant's reseller won't be updated.
* *ParentResellerName* is not mandatory for updating reseller and we suggest the developers pass null when updating reseller as the API does not support changing reseller's parent. To reserve this parameter is to make sure SDK is compatible with old version.   

### Bug fixes

### Breaking changes

* Add a new property *createUserFlag* in the MerchantCreateRequest and MerchantUpdateRequest to indicate whether to create user, the default value is false. The old version API will create user in create merchant step. The impact is if user udpated SDK to this version and does not do any code change, the API won't create user when creating merchant and activating merchant. If user still need create user for the created merchant he need to set *createuserFlag* to true when creating or updating a merchant.
 
* The update reseller API cannot update the parent anymore. 

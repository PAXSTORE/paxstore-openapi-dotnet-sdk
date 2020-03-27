# Changelog  

## 7.1.0  
### New features(To support these new feature Paxstore must upgrade to version 7.1)  
* Add entity attribute APIs (EntityAttributeApi)  
  1. Get entity attribute by id  ```getEntityAttribute(Long attributeId)```  
  2. Search entity attribute  ```searchEntityAttributes(int pageNo, int pageSize, SearchOrderBy orderBy, String key , EntityAttributeType entityType)```  
  3. Create entity attribute(this API is market level, reseller has not permission)  ```createEntityAttribute(EntityAttributeCreateRequest createRequest)```  
  4. Update entity attribute(this API is market level, reseller has not permission)  ```updateEntityAttribute(Long attributeId, EntityAttributeUpdateRequest updateRequest)```  
  5. Update entity attribute label(this API is market level, reseller has not permission)  ```updateEntityAttributeLabel(Long attributeId, EntityAttributeLabelUpdateRequest updateLabelRequest)```  
  6. Delete entity attribute(this API is market level, reseller has not permission)  ```deleteEntityAttribute(Long attributeId)```  
  <br>
* Add terminal group related APIs (TerminalGroupApi)  
  1. Search terminal group  ```searchTerminalGroup(int pageNo, int pageSize,TerminalGroupSearchOrderBy orderBy, TerminalGroupStatus status, String name,String resellerNames,String modelNames, Boolean isDynamic)```   
  2. Get terminal group by id  ```getTerminalGroup(Long groupId)```  
  3. Create terminal group  ```createTerminalGroup(CreateTerminalGroupRequest createRequest)```  
  4. Search terminal  ```searchTerminal(int pageNo, int pageSize, TerminalApi.TerminalSearchOrderBy orderBy, TerminalStatus status, String modelName, String resellerName, String serialNo, String excludeGroupId)```  
  5. Update terminal group  ```updateTerminalGroup(Long groupId ,UpdateTerminalGroupRequest updateRequest)```  
  6. Activate terminal group```activeGroup(Long groupId)```  
  7. Disable terminal group  ```disableGroup(Long groupId)```  
  8. Delete terminal group  ```deleteGroup(Long groupId)```  
  9. Search terminals in group  ```searchTerminalsInGroup(int pageNo, int pageSize, TerminalApi.TerminalSearchOrderBy orderBy, Long groupId, String serialNo, String merchantNames)```  
  10. Add terminal(s) to group  ```addTerminalToGroup(Long groupId, Set<Long> terminalIds)```  
  11. Remove terminal(s) out of group  ```removeTerminalOutGroup(Long groupId, Set<Long> terminalIds)```  
  <br>
* Add terminal group push related APIs (TerminalGroupApkApi)  
  1. Get terminal group push task  ```getTerminalGroupApk(Long groupApkId)```  
  2. Get terminal group push task and include specified parameters in result  ```getTerminalGroupApk(Long groupApkId, List<String> pidList)```  
  3. Search terminal group push task  ```searchTerminalGroupApk(int pageNo, int pageSize, SearchOrderBy orderBy , Long groupId, Boolean pendingOnly, Boolean historyOnly, String keyWords)```  
  4. Create terminal group push task  ```createAndActiveGroupApk(CreateTerminalGroupApkRequest createRequest)```  
  5. Suspend terminal group push task  ```suspendTerminalGroupApk(Long groupApkId)```  
  6. Delete terminal group push task  ```deleteTerminalGroupApk(Long groupApkId)```  
  <br>
* Add terminal RKI(remote key injection) APIs (TerminalRkiApi)  
  1. Push RKI key to terminal  ```pushRkiKey2Terminal(PushRki2TerminalRequest pushRki2TerminalRequest)```  
  2. Search RKI push task  ```searchPushRkiTasks(int pageNo, int pageSize, SearchOrderBy orderBy, String terminalTid, String rkiKey, PushStatus status)```  
  3. Get RKI push task  ```getPushRkiTask(Long pushRkiTaskId)```  
  4. Disable RKI push task  ```disablePushRkiTask(DisablePushRkiTask disablePushRkiTask)```  
  <br>
* Update terminal APIs (TerminalApi)  
  1. Batch add terminal(s) to group(s)  ```batchAddTerminalToGroup(TerminalGroupRequest groupRequest)```
  <br>
* Update reseller APIs (ResellerApi)  
  1. Search reseller RKI keys  ```searchResellerRkiKeyList(Long resellerId, int pageNo, int pageSize, String rkiKey)```  

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

# Changelog  

## 6.3  

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
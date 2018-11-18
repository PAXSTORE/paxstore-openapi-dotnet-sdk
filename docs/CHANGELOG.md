# Changelog  


## 6.1 

### Improvement

* Separate replace merchant email from update merchant API, add additional replace merchant email API.
* Separate replace reseller email from update merchant API, add additional replace reseller email API.
* Do not create user when create reseller but in activate reseller step. Won't affect code.
* *Email* is not mandatory for updating merchant and reseller. If *Email* is empty when call update merchant or reseller API the email won't be updated.
* *ResellerName* is not mandatory for updating merchant. If *ResellerName* is empty when call update merchant API the merchant's reseller won't be updated.
* *ParentResellerName* is not mandatory for updating reseller and we suggest the developers pass null when updating reseller as the API does not support changing reseller's parent. To reserve this parameter is to make sure SDK is compatible with old version. 
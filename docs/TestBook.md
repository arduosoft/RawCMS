This is the test book.

## Authentications tests
Feature  | Description  | Expected Result |  Passed
------------- | ------------- | -------------  | -------------
Apikey login  | adding an apykey on auth config, you will be identified as ApiKey user (calling UserInfo lambda) | if key is empty, user wont be authenticated. If key is provided and matches authorize header, user is authenticated. | TODO 
Apikey Admin login  | adding an apykey admin on auth config, you will be identified as ApiKey user (calling UserInfo lambda) | if key is empty, user wont be authenticated. If key is provided and matches authorize header, user is authenticated. | TODO 
Oauth login  | enabling oauth configuration, user is able to be authenticate using password flow <br />- add user to _user collections (initial user can be used as template) - call userinfo | user will be authenticated. All user field will be available as user claims | TODO 
External  login  | setup a identity server, create user on it, get the token, then authenticate on rawcms | user will be authenticated. All user field exposed by userinfo of external service will be available as user claims | TODO 



## CRUD Test

Feature  | Description  | Expected Result |  Passed
------------- | ------------- | -------------  | -------------
CRUD  | Test insert, update, delete on collection, using API | data will be saved | TODO 
Automatic collection creation  | inserting a item on a collection, if collection doesn't exist, it is created | collection will be created | TODO 
Schema  | Adding a row on _schemas table, schema will be used to validate data | it is not possible inserd unvalid data. it will be possible add valid data | TODO 
Alter query lambda  | Creating a class that implements Alter query allow user to perfom data filtering | query returns expected data. lambda works only in collection that meets lambda configuration | TODO 
Presave Lambda  | adding a presave lambda, this lambda is called before saving data | it is possible to alter data ** before save ** | TODO 
Postsave  Lambda  | adding a postsave lambda, this lambda is called before saving data | it is possible to be notified after data is saved | TODO 


## Admin Test

Feature  | Description  | Expected Result |  Passed
------------- | ------------- | -------------  | -------------
CRUD  | save data on _ tables, | Same of public CRUD api | TODO 
Authorization  | test separation of public and admin CRUD. Test token usage | admin service are protected and cannot be called without apikey (if provided). public api cannot write into collection with _ prefix. admin api cannot write on public collections | TODO 

## Lambda

Feature  | Description  | Expected Result |  Passed
------------- | ------------- | -------------  | -------------
Http Lambda  | create an http lambda | hitting lambda url produce expeced result | TODO 
Rest lambda  | create a Rest lambda | hitting lambda url produce expeced result | TODO 

## Extension

Feature  | Description  | Expected Result |  Passed
------------- | ------------- | -------------  | -------------
Plugin  | Referencing a dll that implements plugin, plugin is loaded | See log from plugin | TODO 
  |  |  |  


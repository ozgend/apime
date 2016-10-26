# apime
a lightweight .net-core api host for custom assemblies 


### sample project
- clone apime
- explore `SampleApi` project

### usage
- create a netcore class library named `YourApi`
- add `YourClass`, implement `Apime.Sdk.Contracts.IApiInterface` and create your methods
- copy `YourApi` assemblies into `Apime.Host` output directory, name your folder as `your-api-key`
- prepare a post request to `http://host:port/YourClass/YourAction` with a X-ApiKey header `your-api-key`
- put your request string as json, make the request


### + todo
+ exception handling
+ logging
+ authetication via apikey & apisecret 
+ nosql repository (authentication data, logs, exceptions)
+ ...
+ ...
+ ...


##

*apime*
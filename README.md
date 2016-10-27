# apime
a lightweight .net-core api host for custom assemblies 


### sample project
- clone apime.
- restore & build `Apime.Host` and `SampleApi` : `dotnet restore` & `dotnet build` 
- create a directory to use as [plugin-path].
- copy `SampleApi` assemblies to a folder named sampleapi under [plugin-path].
- start `Apime.Host`: `dotnet run [port] [plugin-path]`
- explore [sample api-calls](https://github.com/ozgend/apime/tree/master/api-calls)


### usage
- create a netcore class library named `YourApi`
- add `YourClass`, implement `Apime.Sdk.Contracts.IApiInterface` and create your public methods
- copy `YourApi` assemblies to a folder named `your-api-key` under [plugin-path].
- start `Apime.Host`: `dotnet run [port] [plugin-path]`
- prepare a post request to `http://host:port/YourClass/YourAction` with a X-ApiKey header `your-api-key`
- send request body as json, make the request


### + todo
+ exception handling
+ logging
+ directory security & sandboxing
+ authetication via apikey & apisecret 
+ nosql repository (authentication data, logs, exceptions)
+ ...
+ ...
+ ...


##

*apime*

# Gateway

This plugin is an example of RAWCMS configurable plugin that enable basic Gateway capability like:
* Balancer
* Proxy
* Cache
* Logging

## Configuration
Like all configurable plugin, graphQL have a json stored on _configuration collection
```json
{
   "_id":"5db12fb8791f8e6574b62821",
   "plugin_name":"RawCMS.Plugins.ApiGateway.ApiGatewayPlugin",
   "data":{
      "Balancer":[
         {
            "Host":"localhost:64516",
            "Port":64516,
            "Scheme":"http",
            "Path":"^(.*)$",
            "Nodes":[
               {
                  "Host":"test2.com",
                  "Port":443,
                  "Scheme":"https",
                  "Enable":true
               },
               {
                  "Host":"test1.com",
                  "Port":443,
                  "Scheme":"https",
                  "Enable":true
               }
            ],
            "Policy":"RoundRobin",
            "Enable":false
         }
      ],
      "Proxy":[
         {
            "Host":"localhost:64516",
            "Port":64516,
            "Scheme":"http",
            "Path":"^(.*)$",
            "Node":{
               "Host":"test1.com",
               "Port":443,
               "Scheme":"https",
               "Enable":true
            },
            "Enable":false
         }
      ],
      "Cache":{
         "Enable":false,
         "Duration":600,
         "SizeLimit":67108864,
         "MaximumBodySize":104857600,
         "UseCaseSensitivePaths":false
      },
      "Logging":{
         "Enable":true
      }
   }
}
```

**Balancer**
* Enable: true or false, enable capability
* Host: RawCMS Host
* Port: RawCMS Port
* Scheme: RawCMS Scheme
* Path: Regular expression for filter balancer capability
* Policy: Balancer policy (RoundRobin, RequestCount)
* Nodes: Destination Node descriprion
  * Host: Destination Host
  * Port: Destination Port
  * Scheme: Destination Scheme
  * Enable: Enable or disable Node

**Proxy**
* Enable: true or false, enable capability
* Host: RawCMS Host
* Port: RawCMS Port
* Scheme: RawCMS Scheme
* Path: Regular expression for filter proxy capability
* Nodes: Destination Node descriprion
  * Host: Destination Host
  * Port: Destination Port
  * Scheme: Destination Scheme
  * Enable: Enable or disable Node

**Cache**
Implement cache in memory using [Microsoft.AspNetCore.ResponseCaching](https://docs.microsoft.com/it-it/aspnet/core/performance/caching/middleware?view=aspnetcore-3.0) package.

**Logging**
* Enable: true or false, enable capability

# Log Collecting

This module enables the log collecting feature. Logs are isolated by application. A "default" application is created automatically.
Each application has a Public ID. This ID is the public key used for sending log through the http request.

*Note:* For a better performance, log must be added in bulk mode, one API call containing multiple rows.


```
POST /api/LogIngress/<APP-ID>

BODY
[
	{"Date":"2020-04-07T08:43:43.8494406+02:00","Message":"My message xyz","Severity":2},
	{"Date":"2020-04-07T08:43:44.9958044+02:00","Message":"My message xyz 2","Severity":3},
	{"Date":"2020-04-07T08:43:44.9958044+02:00","Message":"My message xyz 4","Severity":4}
]

```

*Serverity values:*

```
	ALL = 0,
	TRACE = 1,
	DEBUG = 2,
	INFO = 3,
	WARN = 4,
	ERROR = 5,
	FATAL = 6
```


Items are processed in background and may need up to a minute to be visible.

## Read logs by API
Api logs are avaiable by regular fulltext api, like this:

```
GET /api/FullText/doc/search/log_<APPLICATION_PUBLIC_ID>?searchQuery=level:>=1 AND message:Prova&start=0&size=20
```

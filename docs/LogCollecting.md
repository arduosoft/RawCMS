# Log Collecting

This module enables the log collecting feature. The application isolates logs. A "default" application created automatically.
Each application has a Public ID. This ID is the public key used for sending log through an HTTP request.

*Note:* for performance log must be added in bulk mode, one call with multiple rows.


```
POST /api/LogIngress/<APP-ID>

BODY
[
	{"Date":"2020-04-07T08:43:43.8494406+02:00","Message":"My message xyz","Severity":2},
	{"Date":"2020-04-07T08:43:44.9958044+02:00","Message":"My message xyz 2","Severity":3},
	{"Date":"2020-04-07T08:43:44.9958044+02:00","Message":"My message xyz 4","Severity":4}
]

```

*Severity values:*

```
	ALL = 0,
	TRACE = 1,
	DEBUG = 2,
	INFO = 3,
	WARN = 4,
	ERROR = 5,
	FATAL = 6
```


Items are processed in the background and may need up to a minute to be visible.

## Read logs by API
API logs are available by regular full-text API, like this:

```
GET /api/FullText/doc/search/log_<APPLICATION_PUBLIC_ID>?searchQuery=level:>=1 AND message:Prova&start=0&size=20
```

## Manage

Through system tables you can set schema definition for collection. 

In this examples Items collection is described by schema definition. As for all entities, POST, PUT, PATCH are supported.


### List all schemas (POST)
Request
```json
http://localhost:50093/system/admin/_schema

BODY
{
	"CollectionName": "Items",
	"AllowNonMappedFields": false,
	"FieldSettings":
	[
		{
			"Name" :"MyField",
	        "Required": true,
	        "Type": "text",
                "BaseType": "String",
	         "Options":
	         {
	         	"maxlength":200,
	         	"regexp":"allowed(.*)"
	         }
         },
         {
			"Name" :"MyNumberField",
	        "Required": false,
	        "Type": "number",
                 "BaseType": "Int",
	         "Options":
	         {
	         	"max":200,
	         	"min":"allowed(.*)"
	         }
         },
	]
}

```

Response
```json
{
    "errors": [],
    "warnings": [],
    "infos": [],
    "status": 0,
    "data": true
}
```




#### List all schemas (GET)
REQUEST 
```json
http://localhost:50093/system/admin/_schema
```

RESPONSE
```json
{
    "errors": [],
    "warnings": [],
    "infos": [],
    "status": 0,
    "data": {
        "items": [
            {
                "_id": "5b2c135fe207012bd07ff6e2",
                "CollectionName": "Items",
                "AllowNonMappedFields": false,
                "FieldSettings": [
                    {
                        "Name": "MyField",
                        "Required": true,
                        "Type": "text",
                        "BaseType": "String",
                        "Options": {
                            "maxlength": 200,
                            "regexp": "allowed(.*)"
                        }
                    },
                    {
                        "Name": "MyNumberField",
                        "Required": false,
                        "Type": "number",
                         "BaseType": "Int",
                        "Options": {
                            "max": 200,
                            "min": "allowed(.*)"
                        }
                    }
                ],
                "_createdon": "2018-06-21T23:06:39.7785905+02:00",
                "_modifiedon": "2018-06-21T23:06:39.783181+02:00"
            }
        ],
        "totalCount": 1,
        "pageNumber": 1,
        "pageSize": 20
    }
}
```

# Relation

# Settings

on configuration schema you can define fields as follows:

reference 1 to many. into child schema collection you can link parent Collection

```json
 {
            "Name" : "SingleReference",
            "Required" : false,
            "Type" : "relation",
            "BaseType" : "String",
            "Options" : {
                "Collection" : "Items",
                "Multiple" : false
            }
        },


```

reference many to many. on Parent schema collection you set the related items

```json
{
  "Name": "MultipleReference",
  "Required": false,
  "Type": "relation",
  "BaseType": "String",
  "Options": {
    "Collection": "Items",
    "Multiple": true
  }
}
```

#api
On list api add the list of items to expand with the expando query string parameter

`http://localhost:28436/api/CRUD/MasterItems/?expando=SingleReference`

result will be placed into metadata section

```json
{
  "errors": [],
  "warnings": [],
  "infos": [],
  "status": "OK",
  "data": {
    "items": [
      {
        "_id": "5dee8bc30b0734567c4c9c63",
        "MyField": "value",
        "MyNumberField": 20.0,
        "_createdon": "2019-12-09T19:00:35.2082894+01:00",
        "_modifiedon": "2019-12-09T19:00:35.2082977+01:00",
        "Prova": 23.0,
        "SingleReference": "5c717d4921919d4c88b34227",
        "_metadata": {
          "rel": {
            "SingleReference": {
              "_id": "5c717d4921919d4c88b34227",
              "MyField": "value",
              "MyNumberField": 3,
              "_createdon": "2019-02-23T18:05:13.4449161+01:00",
              "_modifiedon": "2019-02-23T18:05:13.4449262+01:00"
            }
          }
        }
      }
    ],
    "totalCount": 1,
    "pageNumber": 1,
    "pageSize": 20
  }
}
```

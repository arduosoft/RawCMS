# GraphQL

GraphQL plugin uses [graphql-dotnet](https://github.com/graphql-dotnet/graphql-dotnet) library for implementation.

This plugin is an example of RAWCMS configurable plugin

## Configuration
Like all configurable plugins, graphQL has a json stored on _configuration collection

```json
{
  "_id": "5c7155ece3b8be0f28787bf8",
  "plugin_name": "RawCMS.Plugins.GraphQL.GraphQLPlugin",
  "data": {
    "Path": "/api/graphql",
    "GraphiQLPath": "/graphql",
    "BuildUserContext": null,
    "EnableMetrics": false
}
```

with configuration you can change the path of GraphiQL exposed by RAWCMS.



## Configure collection

For expose collection with GraphQL you should add configuration on schema collection.

```json
[
  {
    "_id": "5c208138be03fb245cd6fde1",
    "CollectionName": "Currency",
    "AllowNonMappedFields": false,
    "FieldSettings": [
      {
        "Name": "CodCurrency",
        "Required": false,
        "Type": "String",
        "BaseType": "String",
        "Options": {
          "max": 5,
          "min": "allowed(.*)"
        }
      },
      {
        "Name": "Description",
        "Required": false,
        "Type": "String",
        "BaseType": "String",
        "Options": {
          "max": 350,
          "min": "allowed(.*)"
        }
      }
    ],
    "_createdon": "2018-12-24T07:48:23.9998791+01:00",
    "_modifiedon": "2018-12-24T07:48:24.0015599+01:00"
  },
  {
    "_id": "5c84be6853a5a042d016e5ab",
    "CollectionName": "Country",
    "AllowNonMappedFields": false,
    "FieldSettings": [
      {
        "Name": "CodCountry",
        "Required": false,
        "Type": "String",
        "BaseType": "String",
        "Options": {
          "max": 5,
          "min": "allowed(.*)"
        }
      },
      {
        "Name": "Currency",
        "Required": false,
        "Type": "Currency",
        "BaseType": "Object"
      },
      {
        "Name": "Description",
        "Required": false,
        "Type": "String",
        "BaseType": "String",
        "Options": {
          "max": 350,
          "min": "allowed(.*)"
        }
      }
    ],
    "_createdon": "2018-12-24T07:48:23.9998791+01:00",
    "_modifiedon": "2018-12-24T07:48:24.0015599+01:00"
  }
]
```

Supported BaseType are:

* Boolean
* Date
* Float
* Int
* ID
* String
* Object

to specify SubObject on base collection you can use _Object_ base type and write related collection name on
Type field

## Special Fields

All collections that define on schema automatically expose this field:
* Paging field
    * pageSize (default 1000)
    * pageNumber (1-base)
* _id (MongoDB key)
* rawQuery

rawQuery is special field for writing your custom MongoDB queries on collection

## Example

Base url of graphQL is

POST `http://{host}/api/graphql`

while GraphiQL

`http://{host}/graphihql`

Suppose you have defined a previous schema, this is an example of graphql query.

### 1. simple query

**Body**
```json
{
    'query':'{ 
        currency(pageSize:1, pageNumber: 1, codCurrency: "E"){
            description,
            codCurrency
        }
    }'
}
```

**Result**
```json
{
    "data": {
        "currency": [
            {
                "description": "Euro",
                "codCurrency": "EUR"
            }
        ]
    }
}
```

### 1. rawQuery query

**Body**
```json
{
	"query":"{ 
		currency(pageSize:1, pageNumber: 1, rawQuery: \"{'CodCurrency':{'$regex':'/*U/*','$options':'si'}}\"){
			description,
			codCurrency
			}
		}"
}
```

**Result**
```json
{
    "data": {
        "currency": [
            {
                "description": "Euro",
                "codCurrency": "EUR"
            }
        ]
    }
}
```

### 1. query subtype

You can query subtype using this path (collectionName)_(fieldName)

**Body**
```json
{
	"query":"{
	country(currency_CodCurrency:\"EUR\"){
		codCountry,
		description,
		currency{
			codCurrency,
			description
		}
	}
}"
}
```

**Result**
```json
{
    "data": {
        "country": [
            {
                "codCountry": "IT",
                "description": "Italy",
                "currency": {
                    "codCurrency": "EUR",
                    "description": "Euro"
                }
            },
            {
                "codCountry": "SM",
                "description": "San Marino",
                "currency": {
                    "codCurrency": "EUR",
                    "description": "Euro"
                }
            }
        ]
    }
}
```
 

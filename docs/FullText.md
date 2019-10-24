# Full Text Plugin

This  plugin enables full text capability.

By default a controller with basic full text feautures is added after installation (index creation, document CRUD on indexes, full text search).

This module can be used to store document, log  collecting or indexing data. See example below for more info.

## Document crud
 See postman for api reference. Implemented APIs:
 - create index
 - add or update document
 - delete
 - search fulltext

## Log collecting
Add a document on an index called logs. 

## Indexing data
Indexing data is out of the box. just add to the schema configuration what follows:

```json
{
	...

    "FullTextPlugin" : {
        "IncludedField" : [ 
            "Field1", 
            "Field2"
        ],
        "CollectionName" : "Items"
    }

}

```


## Configuration

```
{
    "_id" : ObjectId("5db08f4a0337645f8853f848"),
    "plugin_name" : "RawCMS.Plugins.FullText.FullTextPlugin",
    "data" : {
        "Url" : "http://localhost:9300",
        "Engine" : 0
    }
}

```




CRUD Controller offers capability to save structured or non structured data based on mondodb collections.

### Operations

| Method | Operation  |  URL  |
| ------------- |  ------------- |  ------------- |
| GET  | retrieve data  | /api/CRUD/**entityname**/ |
| GET  | retrieve data a single element  | /api/CRUD/**entityname**/**{id}** |
| POST | Insert new item (no update) | /api/CRUD/**entityname**/ |
| PUT | replace an element. Upsert mode.  | /api/CRUD/**entityname**/**{id}** |
| PATCH | patch an element, only changed field are updated  | /api/CRUD/**entityname**/**{id}** |
| DELETE | remove element  | /api/CRUD/**entityname**/**{id}** |


### POST,PUT,PATCH Request

`json
{
	"field":"value",
        "field2":value,
}
`
### DELETE 
No payload needed (id is from URL)

### GET (single element)
no payload required

### GET (search and list elements)
?rawQuery=*optional_json_query*&pageNumber=1&pageSize=20

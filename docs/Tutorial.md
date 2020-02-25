# Tutorial


## How to create an user

Open raw-cms-app under project root directory inside VSCode, open an inline terminal and run npm run serve to start the FE app.
Login with the default credentials, (admin: "alice", password: "alice"), then open the left menù and click on Users.
Press on plus-button and in the opened editor create an user, for example:
```
{
    "UserName": "joe",
    "Email": "test@test.it",
    "NewPassword": "joe",
    "Roles": ["Admin"]
}
```
After this, Save.
(https://www.youtube.com/watch?v=FuLP8WdUbew)

##How to edit an entity

Open the left menù and click on Entities, press on the plus-button and give a name to the collection.
For add a field click on **Add new field**, choose a name, a type and characterisitcs. 
When all the field have been created, save and click on Collection on left menù.
Choose the corresponding collection, click on the plus-button and populate the collection. 
(https://www.youtube.com/watch?v=omCS6M-WD80)

## GraphQl test

On postman, click on GraphQL Query and now you can send queries.
Check the documentation of GraphQL (https://rawcms.readthedocs.io/en/latest/GraphQL/).
(https://www.youtube.com/watch?v=tiBim8w1_MU)

## Swagger Edit

Click on **Auth-Get Token**, on Body change username ("alice") and password ("alice"), then send.
Copy the value of **"access_token**, then click on CRUD-GET.
Change the path of the url (example: http://localhost:28436/api/CRUD/Test), on Authorization change the type in **Bearer Token** and paste on Token the value copied first, now send.
(https://www.youtube.com/watch?v=vXEMtzfSk0U)

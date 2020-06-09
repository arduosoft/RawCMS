# Tutorial
After have been followed the [installation procedure](Deploy) (docker, docker-compose, Heroku or Kubernetes) RawCms is ready to use.

## How to create an user

Open raw-cms-app under project root directory inside VSCode, open an inline terminal and run npm run serve to start the FE app.
Login with the default credentials (admin: "bob", password: "XYZ"), then open the left menu and click on Users.
Press on plus-button and in the opened editor create an user, for example:
```
{
    "UserName": "joe",
    "Email": "test@test.it",
    "NewPassword": "joe", 
    "Roles": ["Admin"]
}
```
NewPassword field is write-only and set the new password.

[![Picture of a comptuer screen showing a graph](http://img.youtube.com/vi/FuLP8WdUbew/0.jpg)](http://www.youtube.com/watch?v=FuLP8WdUbew)

## How to edit an entity

Open the left menu and click on Entities, press on the plus-button and give a name to the collection.
For add a field click on **Add new field**, choose a name, a type and characterisitcs. 
When all the field have been created, save and click on Collection on left menu.
Choose the corresponding collection, click on the plus-button and populate the collection. 

[![Blank form fields displayed on a computer screen](http://img.youtube.com/vi/omCS6M-WD80/0.jpg)](http://www.youtube.com/watch?v=omCS6M-WD80)

## GraphQl test

On postman, click on GraphQL Query and now you can send queries.
Check the documentation of GraphQL (https://rawcms.readthedocs.io/en/latest/GraphQL/).

[![Postman, a program that tests APIs](http://img.youtube.com/vi/tiBim8w1_MU/0.jpg)](http://www.youtube.com/watch?v=tiBim8w1_MU)

## Swagger Edit

Click on **Auth-Get Token**, on Body change username and password according with your settings, then send request.
Copy the value of **"access_token**, then click on CRUD-GET.
Change the path of the url (example: http://localhost:28436/api/CRUD/Test), on Authorization change the type in **Bearer Token** and paste on Token the value copied first, now send.

[![Postman, a program that tests APIs](http://img.youtube.com/vi/vXEMtzfSk0U/0.jpg)](http://www.youtube.com/watch?v=vXEMtzfSk0U)

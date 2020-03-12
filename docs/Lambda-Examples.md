## Lambdas

Lambdas allow for user's to write custom logic on their entities. In RawCMS there is an interface to write Javascript code that will be executed.

For example, if you were creating an entity that represents a book\ you could call an endpoint to get data about the title. In this example, our book entity has an ISBN13. I have defined a pre-save action that will make a call to the OpenLibrary API. The ISBN is passed to the endpoint and we process the result as needed. In this example, we are retrieving the preview_url and assigning it to the PreviewUrl of the entity.

```js
var client = new RAWCMSRestClient();
var request = new RAWCMSRestClientRequest();
var bibkey = "ISBN:" + item.ISBN13;
request.Url = "https://openlibrary.org/api/books?format=JSON&bibkeys=" + bibkey;
request.Method = "GET";

// Process our response
var response = client.Execute(request);
var data = JSON.parse(response.Data);

// Set result property
item.PreviewUrl = data[bibkey].preview_url;
```

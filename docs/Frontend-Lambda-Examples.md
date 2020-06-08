#Frontend Lambdas

## Lambdas

Lambdas allow for user's to write custom logic on their entities. In RawCMS there is an interface to write Javascript code that will be executed.

## Basic Examples

### If you wanted a collection of books

If you were creating an entity that represents a book you could call an endpoint to get data about the title. In this example, our book entity has an ISBN13. I have defined a lambda that will make a call to the OpenLibrary API. The ISBN is passed to the endpoint and we process the result as needed. In this example, we are retrieving the preview_url and assigning it to the PreviewUrl of the entity.

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

## If you needed to find the GPS coordinates of a new shop

Another example is creating Shops in RawCMS. If you needed to get the latitude and longitude of a shop you could use a lambda instead of manually entering the data. In this case, there are multiple fields that can be used to find the physical location of the shop. In the lambda, we will do null checks to confirm if a field has a value. If it does we will pass it to the API to find the location.

```js
var client = new RAWCMSRestClient();
var request = new RAWCMSRestClientRequest();
var filter = "";

if (item.ZipCode) {
  filter += item.ZipCode + " ";
}
if (item.City) {
  filter += item.City + " ";
}
if (item.Location) {
  filter += item.Location;
}

request.Url =
  "https://api.opencagedata.com/geocode/v1/json?key=b39e8ea6daff4f6d8d13d47223b90456&q=" +
  filter +
  "&pretty=1&no_annotations=1";
request.Method = "GET";

var response = client.Execute(request);
var data = JSON.parse(response.Data);
// Set result property
item.Latitude = data.results[0].geometry.lat;
item.Longitude = data.results[0].geometry.lng;
```

### Available Clients

RAWCMSRestClient is the only client available to make requests with at this time. It has limitations it only supports GET and the format is JSON.

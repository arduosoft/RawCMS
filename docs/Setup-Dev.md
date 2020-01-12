# Software to install

- [Visual Studio](https://visualstudio.microsoft.com/it/thank-you-downloading-visual-studio/?sku=Community&rel=16)
- [Visual Studio Code](https://code.visualstudio.com/)
- Client Git([Fork](https://git-fork.com/) or [GitExtensions](http://gitextensions.github.io/))
- [NodeJS last LTS](https://nodejs.org/it/download/)
- [MongoDB Server](https://www.mongodb.com/)
- [MongoDB Compass](https://www.mongodb.com/) (optional but recommended)
- [Postman](https://www.getpostman.com/downloads/) (optional but recommended)

# Configuration

## Setup postman and visual studio

On Postman import the collection file rawCMS/docs/RawCMS.postman_collection.json.
After that, open any collection and copy the port number from the link (should be 28436).
Open rawCMS on Visual Studio, on the right in the _solution explorer_, right click on rawCMS, then property.
On the opened window, _debug_, search _URL of the app_, then delete the port number and past the Postman port.

## Setup mongoDB

Launch rawCMS on VisualStudio, open MongoDB Compass and press _connect_.
Click on rawCMS->\_configuration, then edit the plugin ending with _AuthPlugin_.
At the entry _adminApiKey_, change the type from null to string and set it to _apikeyadmin_.
Do the same for _apiKey_ and set it to _apikey_, then _Update_.
Restart the app on Visual Studio.
On Postman click on the collection _create user_, on headers at the entry _Authorization_ delete {{token}} and set it to _Apikey apikeyadmin_.
On the body, change _name_ (example: "alice"), _newPassword_ (example: "alice") and set roles to _"Admin"_, then _send_.

## start front-end application

Open raw-cms-app under project root directory inside VSCode, open an inline terminal and run npm i (this should be done only once). When finished, run npm run serve to start the FE app.

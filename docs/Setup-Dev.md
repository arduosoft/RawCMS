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
Open rawCMS on Visual Studio, on the right in the *solution explorer*, right click on rawCMS, then property.
On the opened window, *debug*, search *URL of the app*, then delete the port number and past the Postman port.

## Setup mongoDB
Launch rawCMS on VisualStudio, open MongoDB Compass and press *connect*.
Click on rawCMS->_configuration, then edit the plugin ending with *AuthPlugin*.
At the entry *adminApiKey*, change the type from null to string and set it to *apikeyadmin*.
Do the same for *apiKey* and set it to *apikey*, then *Update*.
Restart the app on Visual Studio.
On Postman click on the collection *create user*, on headers at the entry *Authorization* delete {{token}} and set it to *Apikey apikeyadmin*.
On the body, change *name* (example: "alice"), *newPassword* (example: "alice") and set roles to *"Admin"*, then *send*.

## start front-end application
Open raw-cms-app under project root directory inside VSCode, open an inline terminal and run npm i (this should be done only once). When finished, run npm run serve to start the FE app.
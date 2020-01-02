#Software to install:
-[Visual Studio](https://visualstudio.microsoft.com/it/thank-you-downloading-visual-studio/?sku=Community&rel=16)
-[Visual Studio Code](https://code.visualstudio.com/)
-Client Git([Fork](https://git-fork.com/) o [GitExtensions](http://gitextensions.github.io/))
-[NodeJS last LTS](https://nodejs.org/it/download/)
-[MongoDB Server](https://www.mongodb.com/) 
-[MongoDB Compass](https://www.mongodb.com/) (optional)
-[Postman](https://www.getpostman.com/downloads/) (optional)

#Configuration
##Visual Studio's port setting the same as Postman
On Postman import the collections: open the folder of rawCMS->docs->RawCMS.postman_collection.json.
After that, open any collection and copy the port number from the link.
Open rawCMS on Visual Studio, on the right in the *solution explorer, right click on rawCMS, then property.
On the opened window, *debug*, search *URL of the app*, then delete the port number and past the Postman port.

##Settings for MongoDB
Launch rawCMS on VisualStudio, open MongoDB and press *connect*.
Click on rawCMS->_configuration, then edit the plugin *_autPlugin*.
At the entry *adminApiKey*, change the type from null to string and set it to *apikeyadmin*.
Do the same for *apiKey* and set it to *apikey*, then *Update*.
Restart the app on Visual Studio.
On Postman click on the collection *create user*, on heders at the entry *Authorization* delete {{token}} and set it to *Apikey apikeyadmin*.
On the body, change *name* (example: "alice"), *newPassword* (example: "alice") and set roles to *"Admin"*, then *send*.

##Start Visual Studio Code application
Open *rawCMS-app*, on the terminal write *npm i*, when all data has been installed write *npm run serve*.
# Required Software/Tools

- [Visual Studio](https://visualstudio.microsoft.com/it/thank-you-downloading-visual-studio/?sku=Community&rel=16)
- [Visual Studio Code](https://code.visualstudio.com/)
- Client Git([Fork](https://git-fork.com/) or [GitExtensions](http://gitextensions.github.io/))
- [NodeJS last LTS](https://nodejs.org/it/download/)
- [MongoDB Server](https://www.mongodb.com/)
- [Docker](https://docs.docker.com/get-docker/) (optional but recommended)
- [MongoDB Compass](https://www.mongodb.com/) (optional but recommended)
- [Postman](https://www.getpostman.com/downloads/) (optional but recommended)

# Configuration

## Initial Local Setup For contributors

Open the terminal, move to the docker folder of rawCms project, then run the command **docker-compose up**.
Open the project on Visual Studio and start it. This will activate a mongodb installation with a preconfigured database. 
Please double-check the port number on the appsettings.json file and the one exposed by docker compose. These must be the same.

**Non docker users** -  For users that do not have docker, you can install mongodb locally and change the port number accordingly.

[![Docker RawCMS setup](http://img.youtube.com/vi/vFgC9N6bb3Q/0.jpg)](http://www.youtube.com/watch?v=vFgC9N6bb3Q)

## Setup postman

 - On Postman import the collection file rawCMS/docs/RawCMS.postman_collection.json.
 - Open any collection and copy the port number from the link (should be 28436).
 - Open rawCMS on Visual Studio, on the right in the _solution explorer_, right click on rawCMS, then property.
 - On the opened window, _debug_, search _URL of the app_, then delete the port number and paste the Postman port.

## Setup mongoDB

- Launch rawCMS on VisualStudio, open MongoDB Compass and press _connect_.
- Click on rawCMS->\_configuration, then edit the plugin ending with _AuthPlugin_.
- At the entry _adminApiKey_, change the type from null to string and set it to _apikeyadmin_.
Do the same for _apiKey_ and set it to _apikey_, then _Update_.
- Restart the app on Visual Studio.
- On Postman click on the collection _create user_, on headers at the entry _Authorization_ delete {{token}} and set it to _Apikey apikeyadmin_.
- On the body, change _name_ (example: "alice"), _newPassword_ (example: "alice") and set roles to _"Admin"_, then _send_.

## Start front-end application

- Open raw-cms-app under project root directory inside VSCode.
- Open an inline terminal and run `npm i` (this should be done only once). 
- Run `npm run server` to start the FE app.

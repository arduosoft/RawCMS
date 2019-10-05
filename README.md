<img align="left"  src="https://github.com/arduosoft/RawCMS/blob/develop/asset/logo_horizzontal.png?raw=true">

RawCMS is a headless CMS written in ASP.NET Core, built for developers that embrace API-first technology. RawCMS uses MongoDB as its data storage and is ready to be hosted on Docker containers.

[![CodeFactor](https://www.codefactor.io/repository/github/arduosoft/rawcms/badge?style=flat-square)](https://www.codefactor.io/repository/github/arduosoft/rawcms/)
[![Build status](https://ci.appveyor.com/api/projects/status/65b7mnf0bop393u7/branch/develop?svg=true)](https://ci.appveyor.com/project/zeppaman/rawcms)
[![Tweet](https://img.shields.io/twitter/url/http/shields.io.svg?style=social)](https://twitter.com/intent/tweet?text=Discover%20RawCMS%20a%20free%20opensource%20Headless%20CMS%20Based%20on%20AspnetCore%20and%20MongoDB%204&url=https://github.com/arduosoft/RawCMS&hashtags=CMS,Headless,AspnetCore,developer,opensource) [![Join the chat at https://gitter.im/arduosoft/RawCMS-Headless-CMS-Aspnet](https://badges.gitter.im/arduosoft/RawCMS-Headless-CMS-Aspnet.svg)](https://gitter.im/arduosoft/RawCMS-Headless-CMS-Aspnet?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)



## The mission of RawCMS

1. **Modular** Each module is shipped in a NuGet package that can be added to the system to gain new features
2. **Headless** RawCMS doesn't include any presentation logic. All presentation is delegated to the caller. This aims to create a scalable and reusable system
3. **Packaged** RawCMS must be shipped into a single deployable package or added into an existent ASP.NET core application as NuGet package.
4. **Extensible** RawCMS must be customized by the user through extension only. So, no changes to the code will be done. Just adding new packages.
5. **Buildless** RawCMS must give the possibility to manage an installation without the need to manage a codebase or a code repository. It will be possible to add lambdas code at runtime.
6. **Caller Friendly** Produce data in many formats to help integration (Odata, GraphQL)


**__If you are interested in contributing. just open an issue to start a conversion. Help wanted.__**

## Run
You have 3 options to start using RawCMS:
- Run a docker **instance** using `docker pull arduosoft/rawcms-preview` or see [dockerhu page](https://hub.docker.com/r/arduosoft/rawcms-preview) to get more options like docker-compose file.
- Download the **zip and deploy** a regular ASP.NET core application from [github releases](https://github.com/arduosoft/RawCMS/releases)
- **fork** the repository and customize (then deploy as you want)

## Technical documentation
You can find all we have on [Github wiki](https://github.com/arduosoft/RawCMS/wiki). There is user documentation for users, developers, and contributors.

## Features
- Store and filter any data using MongoDB expression
- Docker container ready
- Data validation
- Possibility to add business logic on the backend
- Authentication against external Oauth2 server
- Provide Oauth2 tokens and store users into local DB
- Possibility to create a custom endpoint
- Upsert and incremental update
- GraphQL data query


## LIMITATIONS
- CRUD controller must manage exceptions and errors
- No UI to manage entities
- No data data relation
## ROADMAP

### PHASE 1 - POC

- [x] Dynamic entity management
- [x] Expose with regular web api services and swagger
- [x] Lambda
- [x] Schema definition and validation


### PHASE 2 - Be ready for production
- [x] Expose with graphQL
- [x] Authentication and permission
- [x] Lambda Http
- [x] Plugin system
- [x] Client to automate operations

### PHASE 3 - Ready to send rocket on Mars
- [ ] Expose with Odata
- [ ] Test and client sample
- [ ] design UI for schema management
- [ ] design UI for data management


## License

This software is published under the [GNU General Public License v3](https://github.com/arduosoft/RawCMS/blob/develop/LICENSE).




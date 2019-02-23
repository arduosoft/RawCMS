<img align="left"  src="https://github.com/arduosoft/RawCMS/blob/develop/asset/logo_horizzontal.png?raw=true">

RawCMS is an headless CMS written in asp.net core build for devopers that embrace API first tecnology. RawCMS uses MongoDB as data storage and is ready to be hosted on Docker containers.

[![CodeFactor](https://www.codefactor.io/repository/github/arduosoft/rawcms/badge?style=flat-square)](https://www.codefactor.io/repository/github/arduosoft/rawcms/)
[![Build status](https://ci.appveyor.com/api/projects/status/65b7mnf0bop393u7/branch/develop?svg=true)](https://ci.appveyor.com/project/z3le/rawcms/branch/develop)



## The mission of RawCMS

RawCMS is a modular application build on asp.net core+mongo db stack. It is developed after clashing against the limits or most common opensource headless CMS. The aim of this proejct is to create a new cms, community supported, that may ovecome such limitations.

Actually RawCMS is in progress, but the target is to be:

1. **Modular** Each module is shippend into a nuget package that can be added to the system to gain new features
2. **Headless** RawCMS doesn't include any presentation logic. All is delegate to the caller. This aims to create a scalable and reusable system
3. **Packaged** RawCMS must be shipped into a single deploable package or added into an existent asp.net core applicatio as nuget package.
4. **Extensible** RawCMS must be customized by the user through extension only. So, no changes to the code will be done. Just adding new packages.
5. **Buildless** RawCMS must give the possibility to manage an installation without need to manage a codebase or a code repository. Will be possible to add lambas code at runtime.
6. **Caller Friendly** Produce data in many format to help integration (Odata,GraphQL)


**__If you are interested in contriubuting just open an issue to start a converstion. Helps wanted.__**

# Tecnical documentation
You can find all we have on [Github wiki](https://github.com/arduosoft/RawCMS/wiki). There is user documentation for user, developer and contributors.



# ROADMAP

## PHASE 1 - POC

- [x] Dynamic entity managment
- [x] Expose with regular web api services and swagger
- [x] Lambda
- [x] Schema definition and validation


## PHASE 2 - Be ready for production
- [x] Expose with graphQL
- [x] Authentication and permission
- [x] Lambda Http
- [x] Plugin system
- [x] Client to automate operations

## PHASE 3 - Ready to send rocket on Mars
- [ ] Expose with Odata
- [ ] Test and client sample
- [ ] design UI for schema managment
- [ ] design UI for data managment


# Features
- store and filter any data using mongodb expression
- docker container ready
- data validation
- possibility to add business logic on the backend
- authentication against external Oauth2 server
- proviede Oauth2 tokens and store users into local db
- possibility to create custom endpoint
- Upsert and incremental update
- GraphQL data query


# LIMITATIONS
- CRUD controller must manage exceptions and errors
- Update is in "replace mode". No upsert or incremental save are allowed
- No UI to manange entities
- No data data relation

# RawCMS
RawCMS is an headless CMS written in asp.net core build for devopers that embrace API first tecnology.

[![CodeFactor](https://www.codefactor.io/repository/github/arduosoft/rawcms/badge?style=flat-square)](https://www.codefactor.io/repository/github/arduosoft/rawcms/)

## Features

RawCMS is a modular application build on asp.net core+mongo db stack. It is based on "Lambdas". Inside RawCMS Lamba is implemented by command pattern. All parts of RawCMS are fully extensible.

1. Modular. Each module is shippend into a nuget package that can be added to the system to gain new features
2. Headless. RawCMS doesn't include any presentation logic. All is delegate to the caller. This aims to create a scalable and reusable system
3. Packaged. RawCMS must be shipped into a single deploable package or added into an existent asp.net core applicatio as nuget package.
4. Extensible. RawCMS must be customized by the user through extension only. So, no changes to the code will be done. Just adding new packages.
5. Buildless. RawCMS must give the possibility to manage an installation without need to manage a codebase or a code repository. Will be possible to add lambas code at runtime.


# ROADMAP

## PHASE 1 - POC

- [x] Dynamic entity managment
- [x] Expose with regular web api services and swagger
- [ ] Expose with graphQL
- [ ] Expose with Odata
- [ ] Test and client sample

## PHASE 2 - Be ready for production
- [x] Lambda
- [x] Schema definition and validation
- [x] Authentication and permission
- [ ] Data filtering per user
- [x] Lambda Http
- [x] Plugin system

## PHASE 3 - Ready to send rocket on Mars
- [ ] design UI for schema managment
- [ ] design UI for data managment


# Project Structure

## RawCMS
It' s the front application. It expose services and documentation. This project contains all "presentation" logic:
- Model about payloads (web api, crud)


# LIMITS
- CRUD controller must manage exceptions and errors
- filter doesnt work. They will work only on main field (equal)
- Update is in "replace mode". No upsert or incremental save are allowed
- No validation on data yet implemented
- No UI to manange entities
- Insert on new collection produces collection creations 
- No persmission controls or authorization system
- No data filtering basing on user

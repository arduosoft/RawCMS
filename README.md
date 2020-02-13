<p align="center">
  <img  src="https://github.com/arduosoft/RawCMS/blob/develop/asset/logo_horizzontal.png?raw=true">
 </p>

RawCMS is a headless CMS written in ASP.NET Core, built for developers that embrace API-first technology. RawCMS uses MongoDB as its data storage and is ready to be hosted on Docker containers.

[![CodeFactor](https://www.codefactor.io/repository/github/arduosoft/rawcms/badge?style=flat-square)](https://www.codefactor.io/repository/github/arduosoft/rawcms/)
[![Build status](https://ci.appveyor.com/api/projects/status/65b7mnf0bop393u7/branch/develop?svg=true)](https://ci.appveyor.com/project/zeppaman/rawcms)
[![Tweet](https://img.shields.io/twitter/url/http/shields.io.svg?style=social)](https://twitter.com/intent/tweet?text=Discover%20RawCMS%20a%20free%20opensource%20Headless%20CMS%20Based%20on%20AspnetCore%20and%20MongoDB%204&url=https://github.com/arduosoft/RawCMS&hashtags=CMS,Headless,AspnetCore,developer,opensource) [![Join the chat at https://gitter.im/arduosoft/RawCMS-Headless-CMS-Aspnet](https://badges.gitter.im/arduosoft/RawCMS-Headless-CMS-Aspnet.svg)](https://gitter.im/arduosoft/RawCMS-Headless-CMS-Aspnet?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
<a href="https://forms.gle/dddbHWzcxypN9rpx9"><img src="https://github.com/arduosoft/RawCMS/blob/master/asset/wantsyou.jpg?raw=true" width=300 align="right" /></a>

- 📖 [Documentation](https://rawcms.readthedocs.io/)
- 📚 [API Documentation](https://raw.githubusercontent.com/arduosoft/RawCMS/master/docs/RawCMS.postman_collection.json)
- 🐞 [Issue Tracker](https://github.com/arduosoft/RawCMS/issues) - Report bugs or suggest new features
- 👪 [Community Chat](https://gitter.im/arduosoft/RawCMS-Headless-CMS-Aspnet) - Gitter

## Contribution

As every open source project, we are looking for contributors. Any kind of help is welcome. *Just a star on the project is a lot.* If you want to contribute as a developer, you can join the project by [filling out this form](https://forms.gle/dddbHWzcxypN9rpx9) or by opening an issue. Any other kind of contribution, from docs to tests, is also welcome.

## Try it

You can test RawCMS using the public [demo](http://rawcms-demo.herokuapp.com/) using the default credentials (username:bob, password:XYZ). **App run in a free tier, so it can take a little bit to come up.**

## Deploy it

You can deploy it using many options. RawCMS is dockerized from the development stage so modern hosting is fully supported.

1. *Docker Compose* using the provided docker-compose file
2. *Kubernetes* using the provided docker images
3. *Heroku* using the provided images
4. *Virtual machines* using the packages provided and deployed manually to the server

## The mission of RawCMS

1. **Modular**: Each module is shipped in a NuGet package that can be added to the system to gain new features
2. **Headless**: RawCMS doesn't include any presentation logic. All presentation is delegated to the caller. This aims to create a scalable and reusable system
3. **Packaged**: RawCMS must be shipped into a single deployable package or added into an existent ASP.NET Core application as a NuGet package.
4. **Extensible**: RawCMS must be customized by the user through extension only. So no changes to the code will be done, just adding new packages.
5. **Buildless**: RawCMS must give the possibility to manage an installation without the need to manage a codebase or a code repository. It will be possible to add lambdas code at runtime.
6. **Caller Friendly**: Produce data in many formats to help integration (Odata, GraphQL)

****If you are interested in contributing just open an issue to start a conversation. Help wanted.****

## Run

You have 3 options to start using RawCMS:

- Run a docker **instance** using `docker pull arduosoft/rawcms-preview` or see [dockerhub page](https://hub.docker.com/r/arduosoft/rawcms-preview) to get more options like docker-compose file.
- Download the **zip and deploy** a regular ASP.NET core application from [GitHub releases](https://github.com/arduosoft/RawCMS/releases)
- **Fork** the repository and customize (then deploy as you want)


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
- Api Gateway
- Relation between entities


## License

This software is published under the [GNU General Public License v3](https://github.com/arduosoft/RawCMS/blob/develop/LICENSE).

##

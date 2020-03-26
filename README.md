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
- 🍕 [Demo](http://rawcms-demo.herokuapp.com/)  login with bob\XYZ. It is on a free tier, so be patient (you may need to click login button twice).

## Contribution

As every open source project, we are looking for contributors. Any kind of help is welcome. *Just a star on the project is a lot.* If you want to contribute as a developer, you can join the project by [filling out this form](https://forms.gle/dddbHWzcxypN9rpx9) or by opening an issue. Any other kind of contribution, from docs to tests, is also welcome.

**The easy contribution you can give us is share your impression about the project. [Please invest 1 minute of your time to fill a quick survery and tell your opinion](https://forms.gle/wvu1HF9P52ZdXujv6)**


## Key Benefits and Advantages
### ⚒ Data Modelling without coding
No matter if you're dealing with web apps, mobile apps or client application. Just forget about all data management effort. All you will need with RawCMS is to define your data structure and relationships, using a simple and intuitive UI. Simply say stop to the boring repetitive code.

### 🎛 Agnostic and Universal Framework for your Data
Due to the API approach, RawCMS is very flexible and adapts perfectly to all use cases. Using GraphQL or REST standard RawCMS can be used by everybody without any headhache. This give you a solid and modern foundation for your project. 

### 🚀 Ready to use
Built on top of the well-known .net core framework, can be deployed on every platforms in a click. No more matter about OS licenses of portability, just run it.
Benefit from all existing Symfony Components and Bundles provided by the community or create your own 
Bundles to extend your Projects with reusable components. 

### 💎 Your backend consolidated in one Platform
No more point-to-point integration, multiple tools, or expenses. You can consolidate Log Collecting, Api Gateway, Translation, Session Storage and CMS in one, simple, free user.

### ✨️ Modern and Intuitive Architecture
We love good software design and we aim to help others on building wonderful applications! Using RawCMS you will keep you architecture clean and reduce the effort.

## Architectural Prospective

With RawCMs you have a central platform that manage most critical services, making them a commodity. You have just to focus on the ui development and on implementing business login. No more stress on deployng services or selecting online tools. You have all inclusive and free out of the box.
<img src="https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/WithRawCMS.png?raw=true" width="100%" align="center" />

Use RawCms brings to a clean and easy to manage infrastructure, please compare it with the traditional one. You have a meshed connected system, with multiple services that talks togheter. That's old style approach is hard to mantain and brings to multiple costs.
<img src="https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/Without.png?raw=true" width="100%" align="center" />

This result is possible thanks to the modular and scalable RawCMS architecture. See the key points:

1. **Modular**: Each module is shipped in a NuGet package that can be added to the system to gain new features
2. **Headless**: RawCMS doesn't include any presentation logic. All presentation is delegated to the caller. This aims to create a scalable and reusable system
3. **Packaged**: RawCMS must be shipped into a single deployable package or added into an existent ASP.NET Core application as a NuGet package.
4. **Extensible**: RawCMS must be customized by the user through extension only. So no changes to the code will be done, just adding new packages.
5. **Buildless**: RawCMS must give the possibility to manage an installation without the need to manage a codebase or a code repository. It will be possible to add lambdas code at runtime.
6. **Caller Friendly**: Produce data in many formats to help integration (Odata, GraphQL)

## Preview and Demo

### Low Level Features

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


### Demo (CMS, Lambdas, User management, GraphQL)
_URL_: [http://rawcms-demo.herokuapp.com/](http://rawcms-demo.herokuapp.com/)  **App run in a free tier, so it can take a little bit to come up.**
_Username_: `bob`  
_Password_: `XYZ`

You can follow the [**tutorial**](https://rawcms.readthedocs.io/en/latest/Tutorial/) for a quick start.

## Getting Started

You can deploy it using many options. RawCMS is dockerized from the development stage so modern hosting is fully supported.

1. *Docker Compose* using the provided docker-compose file
2. *Kubernetes* using the provided docker images
3. *Heroku* using the provided images
4. *Virtual machines* using the packages provided and deployed manually to the server


## License

This software is published under the [GNU General Public License v3](https://github.com/arduosoft/RawCMS/blob/develop/LICENSE).


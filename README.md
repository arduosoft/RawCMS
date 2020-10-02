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
- 🍕 [Demo](http://rawcms-demo.herokuapp.com/)  login with **bob\XYZ**. It is on a free tier, so be patient (you may need to click login button twice).

## Contribution

We are actively seeking contribution to continue improving our open source project. Any kind of help is welcome. *Just a star on the project is a lot.* If you would like to contribute as a developer, you can join the project by [filling out this form](https://forms.gle/dddbHWzcxypN9rpx9) or by opening an issue. Any other kind of contribution, from docs to tests, is also welcome.


📣 Please fill out our [fast 5-question survey](https://forms.gle/wvu1HF9P52ZdXujv6) so that we can learn what do you think about RawCMS, how you are using it, and what improvements we should make. Thank you! 👯



## Key Benefits and Advantages
### ⚒ Data Modelling without coding
Whether you're dealing with web apps, mobile apps or client application, just forget about all data management effort. All you will need with RawCMS is to define your data structure and relationships, using a simple and intuitive UI. Simply say stop to the boring repetitive code!

### 🎛 Agnostic and Universal Framework for your Data
Due to the API approach, RawCMS is very flexible and adapts perfectly to all use cases. Using GraphQL or REST standard RawCMS can be used by everybody without any headhaches. This gives you a solid and modern foundation for your project. 

### 🚀 Ready to use
Built on top of the well-known .net core framework, RawCMS can be deployed on every platforms in a click. No longer are there concerns about OS licenses or portability, just run it.
Benefit from all existing Symfony Components and Bundles provided by the community, or create your own Bundles to extend projects with reusable components. 

### 💎 Your backend consolidated in one Platform
No more point-to-point integration, multiple tools, or expenses. You can consolidate Log Collecting, Api Gateway, Translation, Session Storage and CMS in one, simple, free platform.

### ✨️ Modern and Intuitive Architecture
We love quality software design and aim to help others on building wonderful applications! Using RawCMS you will keep you architecture clean and efficient.

## Architectural Prospective

With RawCMs you have a central platform that manages most critical services, making them a commodity. You only have to focus on the UI development and on implementation of business logins. No more stress on deploying services or selecting online tools. You simply have a well-kept, and free, Content Management System.
<img src="https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/WithRawCMS.png?raw=true" width="100%" align="center" />

Use RawCms brings to a clean and easy to manage infrastructure, compared to the traditional design. You have a meshed connected system, with multiple services that talks together. The old style approach is hard to mantain and costly.

The resulting program is possible thanks to the modular and scalable RawCMS architecture. See the key points:

1. **Modular**: Each module is shipped in a NuGet package that can be added to the system to gain new features
2. **Headless**: RawCMS doesn't include any presentation logic. All presentation is delegated to the caller. This aims to create a scalable and reusable system
3. **Packaged**: RawCMS must be shipped into a single deployable package or added into an existent ASP.NET Core application as a NuGet package.
4. **Extensible**: RawCMS must be customized by the user through extension only. So no changes to the code will be done, just adding new packages.
5. **Buildless**: RawCMS must give the possibility to manage an installation without the need to manage a codebase or a code repository. It will be possible to add lambdas code at runtime.
6. **Caller Friendly**: Produce data in many formats to help integration (Odata, GraphQL)

## Preview and Demo

### Data Modeling
Data modeling is based off a simple UI that allows for flexible field definition and relational table in a single click.
![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/entity-definition.png?raw=true)

### Data Entry
Categories may be entered manually for configuration of data entered.
![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/data-entry.png?raw=true)

### Searching
Using the full capabilities offered by MongoDB, data can be queried with speed and finesse.
![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/search.png?raw=true)

### Hook
Directly through the UI, data can be added or altered with ease.
![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/hook.png?raw=true)

### Portable settings
All the settings configured by UI are stored in json format and can be ported form one environment to another (by a copy and paste or using the cli)
![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/json-portable.png?raw=true)




### Custom endpoints
![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/custom%20endpoint.png?raw=true)


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


### Live Demo (CMS, Lambdas, User management, GraphQL)

**This app runs in Heroku free tier**

_URL_: [http://rawcms-demo.herokuapp.com/](http://rawcms-demo.herokuapp.com/)  

_Username_: `bob`  

_Password_: `XYZ`

You can follow this [**tutorial**](https://rawcms.readthedocs.io/en/latest/Tutorial/) for a quick start.

## Getting Started

RawCMS is dockerized from the development stage so modern hosting is fully supported.
Here are a few options for deployment.

1. *Docker Compose* using the provided docker-compose file
2. *Kubernetes* using the provided docker images
3. *Heroku* using the provided images

Or within the command line: 
``` bash
wget https://raw.githubusercontent.com/arduosoft/RawCMS/master/docker/docker-compose-prod.yml -o docker-compose.yml # or download it manually ;-)
docker-compose up
```


## License

This software is published under the [GNU General Public License v3](https://github.com/arduosoft/RawCMS/blob/develop/LICENSE).


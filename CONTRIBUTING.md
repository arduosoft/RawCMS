# CONTRIBUTING

## Why should I spend my time on this project?
__Investing__ time in open source is a great opportunity to grow as a professional, enjoy your spare time and to give something back to the community. Every day we freely use things released under OS licensing. It's important to balance what we receive with what we give to build a sustainable community. Moreover working on OS projects is a great opportunity to experiment, meet people, compare with others and learn new technologies. So... why would you NOT spend time on OS projects?

## How can I help?
Help can be given in any form, dependent on your time budget, skill and wishes. The main help required on this project is on dev sid. Developing new modules, backend, frontend, make test, dev ops, or simply writing some docs are the most wanted contributions.
But also activities not strictly related with dev are also welcome. Writing the product home page, help with marketing, posting into your personal blog... How you choose to help is up to you.

## How to contribute
We focus on dev related contributions. All other proposals will be discussed in person (open an issue on the project to let us know your ideas).

### Code organisation
Code is contained in the RawCMS git repository. This repository will contain all projects related to this product. This will allow us to always have a working set of components we can test or release (core, demo, cli, ui).

### Code flow
We will use GitFlow as our workflow reference for branch usage, and more:

- Nobody is allowed to directly push on shared branches (dev,releases) without approval from somebody else. 
- We will use one feature branch for each feature. 
- All feature will be merged by a PR. 

### Documentation
Documentation will be created and maintained within the code repository. This will be done by adding md files into the /docs/ directory. This may seems an old way to produce documentation comparing with a visual wiki or other tools, but it makes it possible to edit documentation while developing, removing the friction caused by using multiple tools and switching contexts since our doc files will be available within the IDE.

Docs folder will be available online via readthedoc.

### For Mantainers\Owners
All maintainers\owner of the project will follow guidelines.

1. We will follow SCRUM based approach to work, so: 
- task will be managed by a Kanban board 
- team members will be able to choose the task they prefer according to their competencies, wishes and available time
- we will manage backlog by iteration and regular stand-ups
- because we are all spare time workers on this project, we will have a weekly stand-up meeting (instead of daily)
- iterations will have a 3 week duration (2 for implementation, 1 for testing), with the purpose of keeping tasks as small as possible in order to meet deadlines

2. Every part of the deployment must be automated. No human interaction except development is allowed in our production process.

3. Every structural or infrastructure decision must be shared with others

4. Every mistake into analysis or task explanation must be clarified before stating to work.

5. Everybody will offer their knowledge as a service for others and everybody can ask for help from others.

6. Nobody will blame others for missing deadlines, mistakes in code etc., but must be helpful to make better output.

### For people who help without obligation
All kinds of help is welcome, so if you want to fork and propose some changes you are welcome. This is the best way to submit a bug fix or small improvement. Or you can also start new features. However, it is possible your change may not match the product standard or plan so it is possible for it to be rejected. In this case please contact us using the issue tracker, asking to be added as part of the project to avoid wasting time working on a good initiative in the wrong way.

### Dev environment
We will use VS2017 community as our main IDE and Windows 10 as development platform, VS code for the UI part. 

Required plugin on VS2017:
- [License manager](https://marketplace.visualstudio.com/items?itemName=StefanWenig.LicenseHeaderManager)
- [Code refactoring](https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaid)

Repository structure will be the following:
-ROOT
  - docs
  - README and root files
  - API
     - Module 1
     - Module N
  - UI
  - CLI
  
  to follow the rule \<COMPONENT\>//\<Module\>//\<Project\>

#### Api 
Code must be validated by sonarlint. 

Each commit must follow the "conventional commit" rules. Semantic code changes are parsed into the changelog which will be automatically produced during commit.

[Conventional commits](https://www.conventionalcommits.org/en/v1.0.0-beta.2/)

Releases must follow the semantic versioning rules.

[Semantic Versioning](https://semver.org/)

Features must be deployed using a TDD approach - testing will be done DURING development.




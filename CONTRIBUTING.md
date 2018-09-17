# CONTRIBUTING

## Why I should spend my time for the project?
__Invest__ time in opensource is a great opportunity to grow as professionist, enjoy in the spare time and give back something to the community. Every day we use for free something released in OS licensing, it's important to keep balance what we get with what we gitf to make community sustainable. Moreover, OS project, is a great opportunity to make experiments, meet people, compare with other and new technologies. So... why DO NOT spend time in OS projects?

## How can I help?
Help can be given in any form, basing on your time budget, skill and whishes. Main help on this project is on dev sid. Developing new modules, backend, frontend, make test, dev ops, or simply write some docs are the most wanted contribution.
But also activities not strictly related whit dev are also welcome. Write the product home page, help with marketing, posting into personal blog... How you can help is your choice.

## How to contribute
We focus on dev realated contrinution. All other proposal will be discussed by person (opening an issue on the project).

### Code regulamentation
Code is contained into RawCMS git repository. This repository will contain all projects related to this product. This will allow to have always a working set of components to be tested\released (core, demo, cli, ui). 

### Code flow
We will use GitFlow as reference for branch usages, and more:

- Nobody is allowed to directly push on shared branches (dev,releases) whithout approval from somebody else. 
- We will use 1 feature branch for each feature. 
- All feature will be merged by a PR. 

### Documentation
Documentation will be produced directly into code repository. This will be done by adding md files into /docs/ directory. This may seems an old way to produce documentation comparing with visual wiki or other tools, but is the way to make it possible to writedown documentation while developing removing friction using multiple tool and switch context ( doc file will be available within IDE).

Docs folder will be available online via readthedoc.

### For Mantainers\Owners
All mantainers\owner of the project will follow guidelines.

1. We will follow SCRUM based approach to work, so: 
- task will be mananaged by a kanban board 
- each team member will be able to choice the task he prefer accordling with its competences, whishes and time
- we will manage backlog by iteration and regular standups
- because all we are spare time workers on this project, we will have standup meeting weekly (instead of daily)
- iteration will long 3 weeks (2 for implementation, 1 for testing), with the porpose to make task as smaller as possible to meet deadlines

2. Every part of deploy have to be automated. No human iteration except development is allowe in our production process.

3. Every structural or infrastucture decision must be shared with others

4. Every mistake into analysis or task explaination must be clarified before stating to work.

5. Everybody have to put its kowdlege as service for others and everybody can ask for help to others.

6. Nobody will blame others for missing deadlines, mistake on code, but must be helpfull to make better output.

### For people who help whitout obligation
All kind of help is welcome, so if you want to fork and propose some changes you are welcome. This is the best way to submit bugfix or small improvement. You can also start new feautes, anyway, the change may do not match product standard or plan so it is possible to be rejected. In this case please contact us by issues asking to be added as part of the project to avoid wasting time doing some good initiative in the bad way.

### Dev environment
We will use VS2017 community as our main IDE and Windows 10 as development platform, VS code for UI part. 

Repository structure will be the following:
-ROOT
  - docs
  - README and root files
  - API
     - Module 1
     - Module N
  - UI
  - CLI
  
  to follow the rule <COMPONENT>//<Module>//<Project>

#### Api 
Code must be validated by sonarlint. 

Each commmit must follow the "conventional commit" rules. Semantic code are parsed changelog and automatically produced during commit.

[Conventional commits](https://www.conventionalcommits.org/en/v1.0.0-beta.2/)

Releases must follow the semantic versioni rules.

[Semantic Versioning] (https://semver.org/)

Features mus be deployed using a TDD approach, Test will be done DURING developement.




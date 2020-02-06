# Web app developer section

RawCMS is shipped with a Web App that acts as a GUI to view and modify entities schema, as well to
configure the RawCMS instance. The web app will be pluggable in the future.

RawCMS GUI is an SPA built on top of:

- [ES6](http://es6-features.org/)
- [VueJS](https://vuejs.org/)
- [Vuetify](https://vuetifyjs.com)
- [Vue-i18n](https://kazupon.github.io/vue-i18n/)
- [Vuelidate](https://vuelidate.netlify.com/)
- [Vuex](https://vuex.vuejs.org/)

All files of web app are under the `raw-cms-app` directory. In the following document, we will refer
to this directory as the root directory for the webapp, unless stated otherwise.

## Prerequisites

To tinker with the web app, you must have this tools installed on your machine:

- NodeJS + npm (tested with version v10.15.2/6.4.1)

To install needed dependencies, just run `npm i` in the project root.

## Start dev server

Just run `npm run serve`. After you edit a file, you have to manually refresh the browser page to
see the difference.

## Build for deployment

Run `npm run build`. Files will go under the `dist` directory.

## Architecture

All source files are under `src` directory.

Source file are splitted in modules, where each module contains a set of components with related
features. Each module has its own directory under `src/modules/<module name>`. Exception to this
rule are the entry points (`src/index.html` and `src/main.js`) and the root component under
`src/app`.

Components are usually splitted in 2/3 files: `<name>.js` (component logic),
`<name>.tpl.html` (component view) and optionally `<name>.css` (styles). The vast majority of
components are lazy loaded when the app needs them via an utility (more on that later).

### Notable/config files
`src/index.html` and `src/main.js` are the main entry points where the whole application is
bootstrapped.

Under `src/app` there is the root component, with the app wireframe (top bar, left menu and central
view for content).

In the `src/env` directory there is an `env.json` file with environment constants.

In the `src/utils` directory there are JS common utilities.

Concerning `src/config` directory:

- `i18n.js`: contains `vue-i18n` initialization and an helper class to lazy load internationalized
  messages for a module (see `i18nHelper.load` function in the file).
- `router.js`: contains `vue-router` initialization and routing settings. Internazionalization files
  are loaded automatically if you follow the i18n assets convention explained in the
  [module section](#Module-structure).
- `vuetify.js`: contains `vuetify` initialization.
- `vuelidate.js`: contains initialization code for `vuelidate`.
- `vuex.js`: contains init code for `Vuex`.
- `raw-cms.js`: it exports a commodity object `RawCMS` (also exposed on `window`) with a
  `loadComponentTpl` function, which can used along with native VueJS async component loading to
  obtain a full lazy-loading component experience, with view and logic files splitted on source
  code. For an usage example, see one of the views under the `core` module. Note that this object
  contains also the `eventBus` to dispatch events through all the application and can be augmented
  at will to share objects/states within the application.

### Module structure
We can use `src/modules/core` to explain a module structure:

- `assets` directory: contains all static files (e.g. images). It has a sub-directory `i18n` where
  you should put i18n files with this filename template: `i18n.<lang-code>.json`.
- `views` directory: contains the components which acts as main views for the module.
- `services` directory: contains classes/helpers/utils providing business logic to the module components
  and possibly to other modules.
- `components` directory: contains components which are logically contained in the module, but have been 
splitted from the view for maintainability or can potentially be used also elsewhere. Each subdirectory
contains a component with its own 2/3 files.

import { RawCMS } from '../config/raw-cms.js';
import { vuexStore } from '../config/vuex.js';

const _App = Vue.component('rawcms-app', (resolve, reject) => {
  RawCMS.loadComponentTpl('/app/app.tpl.html').then(tpl => {
    resolve({
      components: {
        'rawcms-top-bar': async (res, rej) => {
          const cmp = await import('/modules/core/components/top-bar/top-bar.js');
          await cmp.default(res, rej);
        },
        'rawcms-left-menu': async (res, rej) => {
          const cmp = await import('/modules/core/components/left-menu/left-menu.js');
          await cmp.default(res, rej);
        },
      },
      computed: {
        showMenus() {
          return vuexStore.state.isLoggedIn;
        },
      },
      template: tpl,
    });
  });
});

export const App = _App;
export default _App;

import { RawCMS } from '/config/raw-cms.js';

const _App = Vue.component('rawcms-app', (resolve, reject) => {
  RawCMS.loadComponentTpl('/app/app.tpl.html').then(tpl => {
    resolve({
      components: {
        'rawcms-top-bar': async (res, rej) => {
          const cmp = await import('/modules/core/top-bar/top-bar.js');
          await cmp.default(res, rej);
        },
        'rawcms-left-menu': async (res, rej) => {
          const cmp = await import('/modules/core/left-menu/left-menu.js');
          await cmp.default(res, rej);
        },
      },
      template: tpl,
    });
  });
});

export const App = _App;

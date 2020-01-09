import { tweakConsole } from './config/console.js';
import { i18n } from './config/i18n.js';
import { RawCMS } from './config/raw-cms.js';
import { router } from './config/router.js';
import { vuelidate, vuelidateValidators } from './config/vuelidate.js';
import { vuetify } from './config/vuetify.js';
import { vuexStore } from './config/vuex.js';
import { epicSpinners } from './utils/spinners.js';

tweakConsole();

Vue.config.devtools = true;
Vue.config.productionTip = false;

// Add reference to plugins for commodity
RawCMS.plugins.router = router;
RawCMS.plugins.vuetify = vuetify;
RawCMS.plugins.i18n = i18n;
RawCMS.plugins.vuelidate = vuelidate;

// Add Vuex store
RawCMS.vuexStore = vuexStore;

// Add utilities
RawCMS.utils.vuelidateValidators = vuelidateValidators;
RawCMS.utils.epicSpinners = epicSpinners;

// Add env and start app
axios({
  url: '/env/env.json',
  method: 'get',
})
  .then(e => {
    RawCMS.env = e.data;
  })
  .then(_ => {
    // FIXME: This should be properly handled!
    const moduleConfigPromises = [
      import('/modules/formly-material/config.js').then(x => x.default()),
      import('/modules/core/config.js').then(x => x.default()),
    ];
    const appCmpPromise = import('/app/app.js').then(x => x.App);

    Promise.all([appCmpPromise, ...moduleConfigPromises]).then(x => {
      const appCmp = x[0];

      const vue = new Vue({
        router: router,
        vuetify: vuetify,
        i18n: i18n,
        vuelidate: vuelidate,
        render: h => h(appCmp),
      });

      RawCMS.vue = vue;
      vue.$mount('#app');
    });
  });

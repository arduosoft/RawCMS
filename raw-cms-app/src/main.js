import { App } from './app/app.js';
import { i18n } from './config/i18n.js';
import { RawCMS } from './config/raw-cms.js';
import { router } from './config/router.js';
import { vuetify } from './config/vuetify.js';

Vue.config.productionTip = false;

// Add reference to plugins for commodity
RawCMS.plugins.router = router;
RawCMS.plugins.vuetify = vuetify;
RawCMS.plugins.i18n = i18n;

new Vue({
  router: router,
  vuetify: vuetify,
  i18n: i18n,
  render: h => h(App),
}).$mount('#app');

import { tweakConsole } from "/app/config/console.js";
import { i18n } from "/app/config/i18n.js";
import { RawCMS } from "/app/config/raw-cms.js";
import { router } from "/app/config/router.js";
import { vuelidate, vuelidateValidators } from "/app/config/vuelidate.js";
import { vuetify } from "/app/config/vuetify.js";
import { vuexStore } from "/app/config/vuex.js";
import { epicSpinners } from "/app/utils/spinners.js";

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
    url: "/api/UIMetadata",
    method: "get"
})
    .then(e => {
        RawCMS.env = e.data;
    })
    .then(_ => {
        
        const moduleConfigPromises = [
            import("/app/common/formly-material/config.js").then(x => x.default)
        ];

        RawCMS.env.metadata.forEach(module => {
            console.log(module);
            moduleConfigPromises.push(
                import(module.moduleUrl + "config.js").then(x => x.default)
            );
        });

        const appCmpPromise = import("/app/app/app.js").then(x => x.App);

        Promise.all([appCmpPromise, ...moduleConfigPromises]).then(x => {
            const appCmp = x[0];

            //App is not a module!
            for (var i = 1; i < x.length; i++) {
                let module = x[i];
                vuexStore.registerModule(module.name, module);
                if (module.init) {
                    module.init();
                }
                if (module.getRoutes) {
                    router.addRoutes(module.getRoutes());
                }
            }            

            const vue = new Vue({
                router: router,
                vuetify: vuetify,
                i18n: i18n,
                vuelidate: vuelidate,
                render: h => h(appCmp)
            });

            RawCMS.vue = vue;
            vue.$mount("#app");
        });
    });
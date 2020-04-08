import { RawCMS } from "/app/config/raw-cms.js";
import vuexStore from "/app/config/vuex.js";
const _HangfireView = async (res, rej) => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/core/views/hangfire-view/hangfire-view.tpl.html"
    );

    res({
        data: function () {
            return {
                url: `${RawCMS.env.api.baseUrl}/hangfire/`
            };
        },
        mounted() {
            vuexStore.dispatch("core/updateTopBarTitle", this.$t("core.backgroundjobs.title"));
        },
        template: tpl
    });
};

export const HangfireView = _HangfireView;
export default _HangfireView;
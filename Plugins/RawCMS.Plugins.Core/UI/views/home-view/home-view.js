import { vuexStore } from "/app/config/vuex.js";
import { DashboardDef } from "/app/modules/core/components/dashboard/dashboard.js";

const _HomeView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    "/app/modules/core/views/home-view/home-view.tpl.html"
  );
  const dashboardDef = await DashboardDef();

  res({
    components: {
      Dashboard: dashboardDef
    },
    mounted() {
      vuexStore.dispatch("core/updateTopBarTitle", this.$t("core.home.title"));
    },
    template: tpl
  });
};

export const HomeView = _HomeView;
export default _HomeView;

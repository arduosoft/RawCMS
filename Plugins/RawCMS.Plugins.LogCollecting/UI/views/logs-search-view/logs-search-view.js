import vuexStore from "/app/config/vuex.js";
import { LogsDetailsDef } from "/app/modules/logs/components/logs-search/logs-search.js";

const _LogsDetailsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    "/app/modules/logs/views/logs-search-view/logs-search-view.tpl.html"
  );
  const details = await LogsDetailsDef();

  res({
    components: {
      LogsDetails: details
    },
    mounted() {
      vuexStore.dispatch(
        "core/updateTopBarTitle",
        this.$t("logs.detail.updateTitle", { name: this.Name })
      );
    },
    computed: {
      Name: function() {
        return this.$route.params.name;
      }
    },
    template: tpl
  });
};

export const LogsDetailsView = _LogsDetailsView;
export default _LogsDetailsView;

import vuexStore from "/app/config/vuex.js";
import { LogsListDef } from "/app/modules/logs/components/logs-list/logs-list.js";

const _LogsListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    "/app/modules/logs/views/logs-list-view/logs-list-view.tpl.html"
  );
  const list = await LogsListDef();

  res({
    components: {
      LogsList: list
    },
    mounted() {
      vuexStore.dispatch("core/updateTopBarTitle", this.$t("logs.title"));
    },
    methods: {},
    template: tpl
  });
};

export const LogsListView = _LogsListView;
export default _LogsListView;

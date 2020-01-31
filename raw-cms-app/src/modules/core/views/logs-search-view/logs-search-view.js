import vuexStore from '../../../../config/vuex.js';
import { LogsDetailsDef } from '../../components/logs-search/logs-search.js';

const _LogsDetailsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/logs-search-view/logs-search-view.tpl.html'
  );
  const details = await LogsDetailsDef();

  res({
    components: {
      LogsDetails: details,
    },
    mounted() {
      vuexStore.dispatch(
        'core/updateTopBarTitle',
        this.$t('core.logs.detail.updateTitle', { name: this.Name })
      );
    },
    computed: {
      Name: function() {
        return this.$route.params.name;
      },
    },
    template: tpl,
  });
};

export const LogsDetailsView = _LogsDetailsView;
export default _LogsDetailsView;

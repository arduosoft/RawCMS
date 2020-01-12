import vuexStore from '../../../../config/vuex.js';
import { ConfigurationListDef } from '../../components/configuration-list/configuration-list.js';

const _ConfigurationListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/configuration-list-view/configuration-list-view.tpl.html'
  );
  const list = await ConfigurationListDef();

  res({
    components: {
      ConfigurationList: list,
    },
    mounted() {
      vuexStore.dispatch('core/updateTopBarTitle', this.$t('core.configuration.title'));
    },
    methods: {
      goToCreateView: function() {
        this.$router.push({ name: 'configuration-details', params: { id: 'new' } });
      },
    },
    template: tpl,
  });
};

export const ConfigurationListView = _ConfigurationListView;
export default _ConfigurationListView;

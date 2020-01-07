import { EntitiesListDef } from '../../components/entities-list/entities-list.js';
import vuexStore from '../../../../config/vuex.js';

const _EntitiesListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/entities-list-view/entities-list-view.tpl.html'
  );
  const entitiesList = await EntitiesListDef();

  res({
    components: {
      EntitiesList: entitiesList,
    },
    mounted() {
      vuexStore.dispatch('core/updateTopBarTitle', this.$t('core.entities.title'));
    },
    data: function() {
      return {};
    },
    methods: {
      goToCreateView: function() {
        this.$router.push({ name: 'entity-details', params: { id: 'new' } });
      },
    },
    template: tpl,
  });
};

export const EntitiesListView = _EntitiesListView;
export default _EntitiesListView;

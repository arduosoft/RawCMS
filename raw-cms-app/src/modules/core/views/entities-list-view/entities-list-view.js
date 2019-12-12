import vuexStore from '../../../../config/vuex.js';
import { EntitiesListDef } from '../../components/entities-list/entities-list.js';

const _EntitiesListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/entities-list-view/entities-list-view.tpl.html'
  );
  const entitiesList = await EntitiesListDef();

  res({
    components: {
      EntitiesList: entitiesList,
    },
    computed: {
      title: function() {
        return vuexStore.state.core.topBarTitle;
      },
    },
    data: function() {
      return {};
    },
    methods: {
      goToCreateView: function() {
        this.$router.push({ name: 'entity-details', params: { id: 'new' } });
      },
      capitalize: function([firstLetter, ...rest]) {
        return [firstLetter.toLocaleUpperCase(), ...rest].join('');
      },
    },
    template: tpl,
  });
};

export const EntitiesListView = _EntitiesListView;
export default _EntitiesListView;

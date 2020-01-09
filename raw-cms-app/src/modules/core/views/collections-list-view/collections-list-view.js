import vuexStore from '../../../../config/vuex.js';
import { CollectionsListDef } from '../../components/collections-list/collections-list.js';

const _CollectionsListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/collections-list-view/collections-list-view.tpl.html'
  );
  const collectionsList = await CollectionsListDef();

  res({
    components: {
      CollectionsList: collectionsList,
    },
    mounted() {
      vuexStore.dispatch('core/updateTopBarTitle', this.$t('core.collections.title'));
    },
    methods: {},
    template: tpl,
  });
};

export const CollectionsListView = _CollectionsListView;
export default _CollectionsListView;

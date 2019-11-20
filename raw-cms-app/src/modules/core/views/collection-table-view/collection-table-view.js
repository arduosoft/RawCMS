import { CollectionTableDef } from '../../components/collection-table/collection-table.js';

const _CollectionTableView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/collection-table-view/collection-table-view.tpl.html'
  );
  const collectionTableList = await CollectionTableDef();

  res({
    components: {
      CollectionTable: collectionTableList,
    },
    computed: {
      collectionName: function() {
        return this.$route.params.collName;
      },
    },
    data: function() {
      return {};
    },
    methods: {
      goToCreateView: function() {
        this.$router.push({
          name: 'collection-details',
          params: { id: 'new' },
        });
      },
    },
    template: tpl,
  });
};

export const CollectionTableView = _CollectionTableView;
export default _CollectionTableView;

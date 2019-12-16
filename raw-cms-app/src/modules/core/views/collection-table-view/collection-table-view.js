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
      title: function() {
        return this.$t('core.collections.table.title', { name: this.collectionName });
      },
    },
    data: function() {
      return {
        txtQuery: '',
        parseQuery: {},
      };
    },
    methods: {
      goToCreateView: function() {
        this.$router.push({
          name: 'collection-details',
          params: { id: 'new' },
        });
      },
      rawQuery: function() {
        if (this.txtQuery != '') {
          try {
            this.parseQuery = JSON.parse(this.txtQuery);
          } catch (e) {
            alert(e);
          }
          return this.parseQuery;
        }
        return;
      },
    },
    template: tpl,
  });
};

export const CollectionTableView = _CollectionTableView;
export default _CollectionTableView;

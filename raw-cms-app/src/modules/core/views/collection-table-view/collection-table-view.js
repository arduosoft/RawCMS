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
        monacoOptions: {
          language: 'json',
          scrollBeyondLastLine: false,
        },
      };
    },
    methods: {
      amdRequire: require,
      resizeMonaco: function() {
        const monacoEditor = this.$refs.monaco.getMonaco();
        const oldLayout = monacoEditor.getLayoutInfo();
        const newHeight =
          this.$refs.tabs.$el.getBoundingClientRect().height -
          this.$refs.tabMonacoRef.$el.getBoundingClientRect().height;
        monacoEditor.layout({ width: oldLayout.width, height: newHeight });
      },
      goToCreateView: function() {
        this.$router.push({
          name: 'collection-details',
          params: { id: 'new' },
        });
      },
      filter: function() {
        if (this.txtQuery != '') {
          try {
            this.parseQuery = JSON.parse(this.txtQuery);
          } catch (e) {}
        }
      },
    },
    template: tpl,
  });
};

export const CollectionTableView = _CollectionTableView;
export default _CollectionTableView;

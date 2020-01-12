import vuexStore from '../../../../config/vuex.js';
import { sleep } from '../../../../utils/time.utils.js';
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
    mounted() {
      vuexStore.dispatch(
        'core/updateTopBarTitle',
        this.$t('core.collections.table.title', { name: this.collectionName })
      );
    },
    computed: {
      collectionName: function() {
        return this.$route.params.collName;
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
      resizeMonaco: async function() {
        await sleep(0);

        const monacoRef = this.$refs.monaco;
        const monacoEditor = monacoRef.getMonaco();
        const expPanelContentEl = this.$refs.filterContent.$el.getElementsByClassName(
          'v-expansion-panel-content__wrap'
        )[0];
        const style = window.getComputedStyle(expPanelContentEl, null);
        const h = monacoRef.$el.offsetHeight;
        const w =
          parseFloat(style.getPropertyValue('width')) -
          parseFloat(style.getPropertyValue('padding-left')) -
          parseFloat(style.getPropertyValue('padding-right'));
        monacoEditor.layout({ width: w, height: h });
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

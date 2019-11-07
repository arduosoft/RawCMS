import { epicSpinners } from '../../../../utils/spinners.js';
import { BaseCrudService } from '../../../shared/services/base-crud-service.js';

const _rawCmsDetailEditEvents = {
  loaded: 'rawcms_detail-edit_loaded',
};

const _RawCmsDetailEditDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/shared/components/detail-edit/detail-edit.tpl.html'
  );

  return {
    components: {
      AtomSpinner: epicSpinners.AtomSpinner,
    },
    computed: {
      compCode: {
        get: function() {
          return this.code;
        },
        set: function(newValue) {
          this.code = newValue;

          try {
            this.value = JSON.parse(newValue);
          } catch (e) {}
        },
      },
      isFound: function() {
        return this.value !== null; // FIXME:
      },
    },
    created: function() {
      this.fetchData();
    },
    data: () => {
      return {
        apiService: this.apiBasePath ? new BaseCrudService({ basePath: this.apiBasePath }) : null,
        activeTabId: 1,
        code: '',
        isLoading: true,
        monacoOptions: {
          language: 'json',
          scrollBeyondLastLine: false,
        },
        value,
      };
    },
    methods: {
      amdRequire: require,
      fetchData: async function() {
        this.value = await this.apiService.getById(this.id);
        this.isLoading = false;
        this.$emit(_rawCmsDetailEditEvents.loaded, { isFound: this.isFound });
      },
      formatJson: function() {
        return JSON.stringify(this.value, null, 4);
      },
      resizeMonaco: function() {
        const monacoEditor = this.$refs.monaco.getMonaco();
        const oldLayout = monacoEditor.getLayoutInfo();
        const newHeight =
          this.$refs.tabs.$el.getBoundingClientRect().height -
          this.$refs.tab0.$el.getBoundingClientRect().height;
        monacoEditor.layout({ width: oldLayout.width, height: newHeight });
      },
      save: function() {
        // FIXME:
      },
    },
    created: function() {
      this.code = this.formatJson(this.value || {});
    },
    props: {
      apiBasePath: String,
    },
    template: tpl,
    watch: {
      $route: 'fetchData',
    },
  };
};
const _RawCmsDetailEdit = async (res, rej) => {
  const cmpDef = await _RawCmsDetailEditDef();
  res(cmpDef);
};

export const RawCmsDetailEditDef = _RawCmsDetailEditDef;
export const RawCmsDetailEdit = _RawCmsDetailEdit;
export default _RawCmsDetailEdit;

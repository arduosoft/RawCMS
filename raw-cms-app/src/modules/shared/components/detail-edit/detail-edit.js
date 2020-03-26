import { RawCMS } from '../../../../config/raw-cms.js';
import { epicSpinners } from '../../../../utils/spinners.js';
import { snackbarService } from '../../../core/services/snackbar.service.js';
import { BaseCrudService } from '../../../shared/services/base-crud-service.js';

const _rawCmsDetailEditEvents = {
  loaded: 'rawcms_detail-edit_loaded',
  default: 'rawcms_detail-edit_default',
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
      isSaveDisabled: function() {
        return this.isSaving;
      },
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
      isNew: function() {
        return this.$route.params.id === 'new';
      },
    },
    created: async function() {
      if (!this.isNew) {
        await this.fetchData();
      }

      this.isLoading = false;
      this.code = this.formatJson(this.value || {});

      RawCMS.eventBus.$on(_rawCmsDetailEditEvents.default, ev => {
        this.value = ev.value;
      });

      RawCMS.eventBus.$emit(_rawCmsDetailEditEvents.loaded, {
        isNew: this.isNew,
        value: this.value,
      });
    },
    data: function() {
      return {
        apiService: this.apiBasePath ? new BaseCrudService({ basePath: this.apiBasePath }) : null,
        activeTabId: 'tabMonaco',
        code: '',
        isLoading: true,
        isSaving: false,
        monacoOptions: {
          language: 'json',
          scrollBeyondLastLine: false,
        },
        value: {},
      };
    },
    methods: {
      amdRequire: require,
      saveSuccessMsg: function(item) {
        return this.$t('core.common.saveSuccessMsgTpl');
      },
      saveErrorMsg: function(item) {
        return this.$t('core.common.saveErrorMsgTpl');
      },
      fetchData: async function() {
        const id = this.$route.params.id;
        this.value = await this.apiService.getById(id);
      },
      formatJson: function() {
        return JSON.stringify(this.value, null, 4);
      },
      resizeMonaco: function() {
        const monacoEditor = this.$refs.monaco.getMonaco();
        const oldLayout = monacoEditor.getLayoutInfo();
        const newHeight =
          this.$refs.tabs.$el.getBoundingClientRect().height -
          this.$refs.tabMonacoRef.$el.getBoundingClientRect().height;
        monacoEditor.layout({ width: oldLayout.width, height: newHeight });
      },
      save: async function() {
        this.isSaving = true;
        const apiCall = this.isNew
          ? this.apiService.create(this.value)
          : this.apiService.update(this.value);
        const res = await apiCall;
        this.isSaving = false;

        if (!res) {
          snackbarService.showMessage({
            color: 'error',
            message: this.saveErrorMsg(this.value),
          });
          return;
        }

        snackbarService.showMessage({
          color: 'success',
          message: this.saveSuccessMsg(this.value),
        });

        this.value = res;
      },
    },
    props: {
      apiBasePath: String,
    },
    template: tpl,
    watch: {
      $route: 'fetchData',
      value: {
        handler: function(val) {
          this.code = this.formatJson(val || {});
        },
        deep: true,
      },
    },
  };
};
const _RawCmsDetailEdit = async (res, rej) => {
  const cmpDef = await _RawCmsDetailEditDef();
  res(cmpDef);
};

export const rawCmsDetailEditEvents = _rawCmsDetailEditEvents;
export const RawCmsDetailEditDef = _RawCmsDetailEditDef;
export const RawCmsDetailEdit = _RawCmsDetailEdit;
export default _RawCmsDetailEdit;

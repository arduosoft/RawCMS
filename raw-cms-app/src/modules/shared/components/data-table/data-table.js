import { RawCMS } from '../../../../config/raw-cms.js';
import { epicSpinners } from '../../../../utils/spinners.js';
import { delay } from '../../../../utils/time.utils.js';
import { snackbarService } from '../../../core/services/snackbar-service.js';
import { BaseCrudService } from '../../../shared/services/base-crud-service.js';

const _rawCmsDataTableEvents = {
  loaded: 'rawcms_data-table_loaded',
};

const _RawCmsDataTableDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/shared/components/data-table/data-table.tpl.html'
  );

  return {
    components: {
      AtomSpinner: epicSpinners.AtomSpinner,
    },
    computed: {
      headers: function() {
        return [...this.dataHeaders, { text: 'Actions', value: 'action', sortable: false }];
      },
      isEmpty: function() {
        return this.items.length <= 0;
      },
    },
    created: async function() {
      await this.fetchData();
      this.isLoading = false;
      RawCMS.eventBus.$emit(_rawCmsDataTableEvents.loaded);
    },
    data: function() {
      return {
        apiService: this.apiBasePath ? new BaseCrudService({ basePath: this.apiBasePath }) : null,
        currentItem: {},
        dataHeaders: [],
        isDeleteConfirmVisible: false,
        isLoading: true,
        isSaving: false,
        items: [],
      };
    },
    methods: {
      fetchData: async function() {
        const res = await Promise.all([this.getDataHeaders(), this.apiService.getPage()]);

        this.dataHeaders = res[0];
        this.items = res[1].map(x => {
          return { ...x, _meta_: { isDeleting: false } };
        });
      },
      goTo: function(item) {
        if (!this.detailRouteName) {
          console.warn('"detailRouteName" prop was not defined, cannot go to detail view.');
          return;
        }

        this.$router.push({ name: this.detailRouteName, params: { id: item._id } });
      },
      showDeleteConfirm: function(item) {
        this.currentItem = item;
        this.isDeleteConfirmVisible = true;
      },
      dismissDeleteConfirm: function() {
        this.isDeleteConfirmVisible = false;
      },
      deleteConfirmMsg(item) {
        return this.$t('core.common.deleteConfirmMsgTpl', { id: item._id });
      },
      deleteSuccessMsg(item) {
        return this.$t('core.common.deleteSuccessMsgTpl', { id: item._id });
      },
      deleteErrorMsg(item) {
        return this.$t('core.common.deleteErrorMsgTpl', { id: item._id });
      },
      deleteItem: async function(item) {
        this.dismissDeleteConfirm();
        item._meta_.isDeleting = true;
        // const res = await this.apiService.delete(item._id); // FIXME: Restore
        const res = await delay({ millis: 3000, value: true });
        item._meta_.isDeleting = false;

        if (!res) {
          snackbarService.showMessage({
            color: 'error',
            message: this.deleteErrorMsg(item),
          });
          return;
        }

        this.items = this.items.filter(x => x._id !== item._id);
        snackbarService.showMessage({
          color: 'success',
          message: this.deleteSuccessMsg(item),
        });
      },
      getDataHeaders: async function() {
        throw new Error('You should implement this!');
      },
      getTemplateName: function(header) {
        return `item.${header.value}`;
      },
    },
    props: {
      apiBasePath: String,
      detailRouteName: String,
    },
    template: tpl,
    watch: {
      $route: 'fetchData',
    },
  };
};
const _RawCmsDataTable = async (res, rej) => {
  const cmpDef = await _RawCmsDataTableDef();
  res(cmpDef);
};

export const rawCmsDataTableEvents = _rawCmsDataTableEvents;
export const RawCmsDataTableDef = _RawCmsDataTableDef;
export const RawCmsDataTable = _RawCmsDataTable;
export default _RawCmsDataTable;

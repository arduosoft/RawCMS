import { epicSpinners } from '../../../../utils/spinners.js';
import { BaseCrudService } from '../../../shared/services/base-crud-service.js';

// import { entitiesSchemaService } from '../../../core/services/entities-schema.service.js';
// import { snackbarService } from '../../../core/services/snackbar-service.js';

const _RawCmsListDef = async () => {
  const tpl = await RawCMS.loadComponentTpl('/modules/shared/components/list/list.tpl.html');

  return {
    components: {
      AtomSpinner: epicSpinners.AtomSpinner,
    },
    created: function() {
      this.fetchEntities();
    },
    data: function() {
      return {
        apiService: new BaseCrudService({ basePath: this.apiBasePath }),
        items: [],
      };
    },
    methods: {
      fetchEntities: async function() {
        // FIXME: Pagination
        const res = await this.apiService.getPage();
        this.items = res.map(x => {
          return { ...x, _meta_: { isDeleting: false } };
        });
        this.isLoading = false;
      },
      goTo: function(entityId) {
        // this.$router.push({ name: 'entity-details', params: { id: entityId } });
      },
      showDeleteConfirm: function(entity) {
        // this.currentEntity = entity;
        // this.isDeleteConfirmVisible = true;
      },
      dismissDeleteConfirm: function() {
        // this.isDeleteConfirmVisible = false;
      },
      deleteEntity: async function(entity) {
        // this.dismissDeleteConfirm();
        // entity._meta_.isDeleting = true;
        // const res = await entitiesSchemaService.deleteEntity(entity._id);
        // entity._meta_.isDeleting = false;
        // if (!res) {
        //   snackbarService.showMessage({
        //     color: 'error',
        //     message: this.$t('core.entities.deleteErrorMsgTpl', {
        //       entityName: entity.CollectionName,
        //     }),
        //   });
        //   return;
        // }
        // this.entities = this.entities.filter(x => x._id !== entity._id);
        // snackbarService.showMessage({
        //   color: 'success',
        //   message: this.$t('core.entities.deletedMsgTpl', {
        //     entityName: entity.CollectionName,
        //   }),
        // });
      },
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

const _RawCmsList = async (res, rej) => {
  const cmpDef = _RawCmsListDef();
  res(cmpDef);
};

export const RawCmsListDef = _RawCmsListDef;
export const RawCmsList = _RawCmsList;
export default _RawCmsList;

import { RawCmsListDef } from '../../../shared/components/list/list.js';
// import { epicSpinners } from '../../../../utils/spinners.js';
import { entitiesSchemaService } from '../../services/entities-schema.service.js';

// import { snackbarService } from '../../services/snackbar-service.js';

const _EntitiesList = async (res, rej) => {
  const cmpDef = await RawCmsListDef();
  // const tpl = await RawCMS.loadComponentTpl(
  //   '/modules/core/components/entities-list/entities-list.tpl.html'
  // );

  res({
    // components: {
    //   AtomSpinner: epicSpinners.AtomSpinner,
    // },
    // created: function() {
    //   this.fetchEntities();
    // },
    data: function() {
      return {
        apiService: entitiesSchemaService,
        // isLoading: true,
        // entities: [],
        // currentEntity: {},
        // isDeleteConfirmVisible: false,
      };
    },
    // methods: {
    //   fetchEntities: async function() {
    //     const res = await entitiesSchemaService.getPage({ page: 0 });
    //     this.entities = res.map(x => {
    //       return { ...x, _meta_: { isDeleting: false } };
    //     });
    //     this.isLoading = false;
    //   },
    //   goTo: function(entityId) {
    //     this.$router.push({ name: 'entity-details', params: { id: entityId } });
    //   },
    //   showDeleteConfirm: function(entity) {
    //     this.currentEntity = entity;
    //     this.isDeleteConfirmVisible = true;
    //   },
    //   dismissDeleteConfirm: function() {
    //     this.isDeleteConfirmVisible = false;
    //   },
    //   deleteEntity: async function(entity) {
    //     this.dismissDeleteConfirm();

    //     entity._meta_.isDeleting = true;
    //     const res = await entitiesSchemaService.delete(entity._id);

    //     entity._meta_.isDeleting = false;
    //     if (!res) {
    //       snackbarService.showMessage({
    //         color: 'error',
    //         message: this.$t('core.entities.deleteErrorMsgTpl', {
    //           entityName: entity.CollectionName,
    //         }),
    //       });
    //       return;
    //     }

    //     this.entities = this.entities.filter(x => x._id !== entity._id);
    //     snackbarService.showMessage({
    //       color: 'success',
    //       message: this.$t('core.entities.deletedMsgTpl', {
    //         entityName: entity.CollectionName,
    //       }),
    //     });
    //   },
    // },
    // template: tpl,
    // watch: {
    //   $route: 'fetchData',
    // },
    extends: cmpDef,
  });
};

export const EntitiesList = _EntitiesList;
export default _EntitiesList;

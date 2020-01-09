import { RawCmsListDef } from '../../../shared/components/list/list.js';
import { entitiesSchemaService } from '../../services/entities-schema.service.js';

const _EntitiesListWrapperDef = async () => {
  const rawCmsListDef = await RawCmsListDef();

  return {
    data: function() {
      return {
        apiService: entitiesSchemaService,
      };
    },
    extends: rawCmsListDef,
    methods: {
      deleteConfirmMsg(item) {
        return this.$t('core.entities.deleteConfirmMsgTpl', { name: item.CollectionName });
      },
      deleteSuccessMsg(item) {
        return this.$t('core.entities.deleteSuccessMsgTpl', { name: item.CollectionName });
      },
      deleteErrorMsg(item) {
        return this.$t('core.entities.deleteErrorMsgTpl', { name: item.CollectionName });
      },
    },
    props: {
      detailRouteName: {
        typ: String,
        default: 'entity-details',
      },
    },
  };
};

const _EntitiesListDef = async () => {
  const listWrapperDef = await _EntitiesListWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/entities-list/entities-list.tpl.html'
  );

  return {
    components: {
      ListWrapper: listWrapperDef,
    },
    template: tpl,
  };
};

const _EntitiesList = async (res, rej) => {
  const cmpDef = _EntitiesListDef();
  res(cmpDef);
};

export const EntitiesListDef = _EntitiesListDef;
export const EntitiesList = _EntitiesList;
export default _EntitiesList;

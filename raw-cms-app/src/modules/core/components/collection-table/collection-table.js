import { optionalChain } from '../../../../utils/object.utils.js';
import { RawCmsDataTableDef } from '../../../shared/components/data-table/data-table.js';
import { entitiesSchemaService } from '../../services/entities-schema.service.js';

const _TableWrapperDef = async () => {
  const rawCmsDataTableDef = await RawCmsDataTableDef();

  return {
    data: function() {
      return {
        entitiesSchemaService: entitiesSchemaService,
      };
    },
    extends: rawCmsDataTableDef,
    methods: {
      deleteConfirmMsg(item) {
        return this.$t('core.collections.table.deleteConfirmMsgTpl');
      },
      deleteSuccessMsg(item) {
        return this.$t('core.collections.table.deleteSuccessMsgTpl');
      },
      deleteErrorMsg(item) {
        return this.$t('core.collections.table.deleteErrorMsgTpl');
      },
      getDataHeaders: async function() {
        const res = await this.entitiesSchemaService.getPage({
          size: 1,
          rawQuery: { CollectionName: this.collectionName },
        });

        let result = res.items[0].FieldSettings.filter(
          x => optionalChain(() => x.Options.showOnTable, { fallbackValue: true }) === true
        ).map(x => {
          return { text: x.Name, value: x.Name, sortable: true };
        });

        return result;
      },
    },
    props: {
      collectionName: String,
    },
  };
};

const _CollectionTableDef = async () => {
  const tableWrapperDef = await _TableWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/collection-table/collection-table.tpl.html'
  );

  return {
    components: {
      TableWrapper: tableWrapperDef,
    },
    computed: {
      apiBasePath: function() {
        return `/api/CRUD/${this.collectionName}`;
      },
    },
    data: function() {
      return {
        detailRouteName: 'collection-details',
      };
    },
    props: {
      collectionName: String,
      externalRawQuery: Object,
    },
    template: tpl,
  };
};

const _CollectionTable = async (res, rej) => {
  const cmpDef = _CollectionTableDef();
  res(cmpDef);
};

export const CollectionTableDef = _CollectionTableDef;
export const CollectionTable = _CollectionTable;
export default _CollectionTable;

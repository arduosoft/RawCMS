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
      // FIXME: Improve this!
      getDataHeaders: async function() {
        const res = await this.entitiesSchemaService.getPage();
        return res
          .filter(x => x.CollectionName === this.collectionName)[0]
          .FieldSettings.map(x => {
            return { text: x.Name, value: x.Name };
          });
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

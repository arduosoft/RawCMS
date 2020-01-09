import { RawCmsListDef } from '../../../shared/components/list/list.js';
import { entitiesSchemaService } from '../../services/entities-schema.service.js';

const _CollectionsListWrapperDef = async () => {
  const rawCmsListDef = await RawCmsListDef();

  return {
    data: function() {
      return {
        apiService: entitiesSchemaService,
      };
    },
    extends: rawCmsListDef,
    methods: {
      goTo: function(item) {
        this.$router.push({
          name: this.detailRouteName,
          params: { collName: item.CollectionName },
        });
      },
    },
    props: {
      detailRouteName: {
        typ: String,
        default: 'collection-table',
      },
    },
  };
};

const _CollectionsListDef = async () => {
  const listWrapperDef = await _CollectionsListWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/collections-list/collections-list.tpl.html'
  );

  return {
    components: {
      ListWrapper: listWrapperDef,
    },
    template: tpl,
  };
};

const _CollectionsList = async (res, rej) => {
  const cmpDef = _CollectionsListDef();
  res(cmpDef);
};

export const CollectionsListDef = _CollectionsListDef;
export const CollectionsList = _CollectionsList;
export default _CollectionsList;

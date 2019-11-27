import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';
import { entitiesSchemaService } from '../../services/entities-schema.service.js';

const _CollectionItemDetailsWrapperDef = async () => {
  const rawCmsDetailEditDef = await RawCmsDetailEditDef();

  return {
    extends: rawCmsDetailEditDef,
  };
};

const _CollectionItemDetailsDef = async () => {
  const detailWrapperDef = await _CollectionItemDetailsWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/collection-item-details/collection-item-details.tpl.html'
  );

  return {
    components: {
      DetailWrapper: detailWrapperDef,
    },
    computed: {
      apiBasePath: function() {
        return `/api/CRUD/${this.collectionName}`;
      },
    },
    created: async function() {
      this.formFields = await this.getFormFields();
    },
    data: function() {
      return {
        entitiesSchemaService: entitiesSchemaService,
        formState: {},
        formFields: [],
      };
    },
    methods: {
      getFormFields: async function() {
        const res = await this.entitiesSchemaService.getPage({
          size: 1,
          rawQuery: { CollectionName: this.collectionName },
        });

        const result = res.items[0].FieldSettings.map(x => {
          return {
            key: x.Name,
            type: this.getFormlyType(x.Type),
            wrapper: '<div class="col-12 col-sm-6"></div>',
          };
        });

        return result;
      },
      getFormlyType: function(beType) {
        switch (beType) {
          case 'String':
          case 'string':
            return 'text';
          case 'number':
            return 'number';
          default:
            return 'text';
        }
      },
    },
    props: {
      collectionName: String,
    },
    template: tpl,
  };
};

const _CollectionItemDetails = async (res, rej) => {
  const cmpDef = _CollectionItemDetailsDef();
  res(cmpDef);
};

export const CollectionItemDetailsDef = _CollectionItemDetailsDef;
export const CollectionItemDetails = _CollectionItemDetails;
export default _CollectionItemDetails;

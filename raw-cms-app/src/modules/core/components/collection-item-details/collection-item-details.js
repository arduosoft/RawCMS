import { vuexStore } from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';
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
      fieldsMetadata: function() {
        return optionalChain(() => vuexStore.state.core.fieldsMetadata, {
          fallbackValue: [],
          replaceLastUndefined: true,
        });
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
        const schema = res.items[0];

        const result = schema.FieldSettings.map(x => {
          return this.applyFieldMetadata(
            {
              key: x.Name,
              type: x.Type,
              validators: {},
              templateOptions: { validation: {} },
              wrapper: '<div class="col-12 col-sm-6"></div>',
            },
            x
          );
        });

        return result;
      },
      applyFieldMetadata: function(formlyField, schemaField) {
        const validators = this.fieldsMetadata[schemaField.Type].validations;
        validators.forEach(x => {
          const key = `${schemaField.Type}.${x.name}`;
          var option = optionalChain(() => schemaField.Options[x.name]);

          if (option === undefined) {
            option = {};
          }

          formlyField.validators[key] = function(field, model, next) {
            var item = model;
            var errors = [];
            var options = schemaField.Options;
            var name = field.key;
            var Type = schemaField.Type;
            var value = model[field.key];
            var fnValidator = Function(
              'item',
              'errors',
              'options',
              'name',
              'Type',
              'value',
              x.function
            );
            var t = fnValidator(item, errors, options, name, Type, value);

            if (errors.length == 0) {
              return next(true);
            } else {
              return next(
                false,
                errors
                  .map(function(item) {
                    return item.Code + ' - ' + item.Title;
                  })
                  .join(' <br/> ')
              );
            }
          };

          formlyField.templateOptions.validation[key] = { optionValue: option };
        });

        return formlyField;
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

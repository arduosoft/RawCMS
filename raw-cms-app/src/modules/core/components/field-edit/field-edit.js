import { vuexStore } from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';
import { randomString } from '../../../../utils/random.utils.js';
import { validationService } from '../../services/validation.service.js';

const _fieldEditEvents = {
  closed: 'closed',
};

const _FieldEditDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/field-edit/field-edit.tpl.html'
  );

  return {
    computed: {
      availableFieldTypes: function() {
        return Object.keys(vuexStore.state.core.fieldsMetadata).map(x => {
          return { text: x, value: x };
        });
      },
      areOptionsAvailable: function() {
        return optionalChain(() => this.optionsFields.length > 0, {
          fallbackValue: false,
        });
      },
      isAllValid: function() {
        return (
          this.isValid && optionalChain(() => this.optionsFormState.$valid, { fallbackValue: true })
        );
      },
      nameRules: function() {
        return [
          val => (val !== undefined && val !== '' ? true : this.$t('core.validation.required')),
        ];
      },
    },
    created: function() {
      this.setField(this.field);
    },
    data: function() {
      return {
        isValid: true,
        currentField: {},
        optionsFormState: {},
        optionsFields: [],
      };
    },
    methods: {
      close: function(isOk) {
        this.$emit(_fieldEditEvents.closed, { isOk: isOk, field: this.currentField });
      },
      onFieldTypeChanged: function(evt) {
        this.updateFieldOptions(evt);
      },
      setField: function(field) {
        this.$set(this, 'currentField', field || {});
        this.$set(this.currentField, 'Options', this.currentField.Options || {});

        this.updateFieldOptions(optionalChain(() => this.currentField.Type));
      },
      updateFieldOptions: function(fieldTypeName) {
        const formId = randomString(16);
        vuexStore.dispatch('core/updateRelationMetadata', { formId: formId });

        if (fieldTypeName === undefined) {
          this.optionsFields = [];
          return;
        }

        const fieldDef = vuexStore.state.core.fieldsMetadata[fieldTypeName];
        if (fieldDef === undefined) {
          this.optionsFields = [];
          return;
        }

        const optionParams = optionalChain(() => fieldDef.type.optionParameter);
        if (optionParams === undefined) {
          this.optionsFields = [];
          return;
        }

        const result = optionParams.map(x => {
          return validationService.applyFieldMetadataToFormlyInput(
            {
              key: x.name,
              type: x.type,
              validators: {},
              templateOptions: { validation: {} },
            },
            {
              fieldType: x.type,
              formId: formId,
            }
          );
        });

        this.optionsFields = result;
      },
    },
    props: {
      field: {
        type: Object,
        default: {},
      },
    },
    template: tpl,
    watch: {
      field: function(newVal, oldVal) {
        this.setField(newVal);
      },
    },
  };
};

const _FieldEdit = async (res, rej) => {
  const cmpDef = _FieldEditDef();
  res(cmpDef);
};

export const fieldEditEvents = _fieldEditEvents;
export const FieldEditDef = _FieldEditDef;
export const FieldEdit = _FieldEdit;
export default _FieldEdit;

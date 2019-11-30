import { vuexStore } from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';

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
    },
    created: function() {
      this.updateFieldOptions(optionalChain(() => this.field.Type));
    },
    data: function() {
      return {
        currentField: this.field || {},
        optionsFormState: {},
        optionsModel: {},
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
      updateFieldOptions: function(fieldTypeName) {
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

        optionParams.forEach(x => console.log(x));
        const result = optionParams.map(x => {
          return {
            key: x.name,
            type: x.type,
            validators: {},
            templateOptions: { validation: {} },
            wrapper: '<div class="col-12 col-sm-6"></div>',
          };
        });

        console.log(result);
        // this.optionsFields = fieldDef.optionParameter;
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
        this.currentField = newVal;
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

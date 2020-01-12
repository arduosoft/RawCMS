import { optionalChain } from '../../../../utils/object.utils.js';
import { toFirstUpperCase } from '../../../../utils/string.utils.js';

const _BaseFieldProps = {
  props: {
    form: {
      type: Object,
      required: true,
    },
    field: {
      type: Object,
      required: true,
    },
    model: {
      type: Object,
      required: true,
    },
    to: {
      type: Object,
      required: true,
    },
  },
};

const _BaseField = {
  computed: {
    hasError: function() {
      if (this.form[this.field.key].$dirty == false || this.form[this.field.key].$active == true) {
        return false;
      }

      const errors = this.form.$errors[this.field.key];
      let hasErrors = false;
      Object.keys(errors).forEach(err => {
        if (errors[err] !== false) {
          hasErrors = true;
        }
      });

      this.$set(this.form[this.field.key], '$hasError', hasErrors);
      return hasErrors;
    },
    errorMsgs: function() {
      if (!this.hasError) {
        return [];
      }

      const errors = this.form.$errors[this.field.key];
      const errorMsgs = [];
      for (const err of Object.keys(errors)) {
        if (errors[err] === false) {
          continue;
        }

        const errKey = err.replace('$async_', '');
        const validator = this.field.validators[errKey];
        const tplOptions = this.field.templateOptions;

        const errMsg =
          typeof errors[errKey] === 'string'
            ? errors[errKey]
            : optionalChain(() => validator.message, {
                fallbackValue: this.$t(
                  `formly.validation.${errKey}`,
                  optionalChain(() => tplOptions.validation[errKey])
                ),
              });
        errorMsgs.push(() => errMsg);
      }

      return errorMsgs;
    },
    modelValue: function() {
      return this.model[this.field.key];
    },
    label: function() {
      return toFirstUpperCase(
        optionalChain(() => this.to.label, { fallbackValue: this.field.key })
      );
    },
    value: {
      get: function() {
        const currentValue = this.model[this.field.key];
        if (
          this.form[this.field.key].$dirty === false &&
          (currentValue === undefined || currentValue === '')
        ) {
          this.setValue(undefined);
        }

        return this.preProcessValueForGet(this.modelValue);
      },
      set: function(value) {
        this.setValue(value);
      },
    },
  },
  created: function() {
    const state = {
      $dirty: false,
      $active: false,
      $hasError: false,
    };

    this.$set(this.form, this.field.key, state);
  },
  data: function() {
    return {
      basicListeners: {
        blur: this.onBlur,
        focus: this.onFocus,
        click: this.onClick,
        change: this.onChange,
        input: this.onInput,
        keyup: this.onKeyup,
        keydown: this.onKeydown,
      },
    };
  },
  methods: {
    booleanValue(value) {
      return value === 'true' || value === 'false' ? value === 'true' : value;
    },
    onFocus: function(e) {
      this.$set(this.form[this.field.key], '$active', true);
      this.runFunction('onFocus', e);
    },
    onBlur: function(e) {
      this.$set(this.form[this.field.key], '$dirty', true);
      this.$set(this.form[this.field.key], '$active', false);
      this.runFunction('onBlur', e);
    },
    onClick: function(e) {
      this.runFunction('onClick', e);
    },
    onChange: function(e) {
      this.$set(this.form[this.field.key], '$dirty', true);
      this.runFunction('onChange', e);
    },
    onInput: function(e) {
      this.setValue(e);
      this.runFunction('onInput', e);
    },
    onKeyup: function(e) {
      this.runFunction('onKeyup', e);
    },
    onKeydown: function(e) {
      this.runFunction('onKeydown', e);
    },
    preProcessValueForGet: function(val) {
      return val;
    },
    preProcessValueForSet: function(val) {
      return val;
    },
    runFunction: function(action, e) {
      if (typeof this.to[action] === 'function') this.to[action].call(this, e);
    },
    setValue: function(val, { applyDirectly } = { applyDirectly: false }) {
      const newValue = applyDirectly ? val : this.preProcessValueForSet(val);
      this.$set(this.model, this.field.key, newValue);
    },
  },
  mixins: [_BaseFieldProps],
};

export const BaseFieldProps = _BaseFieldProps;
export const BaseField = _BaseField;
export default _BaseField;

import { BaseField } from '../base-field/base-field.js';

const _DateFieldDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/date-field/date-field.tpl.html'
  );

  return {
    data: function() {
      return {
        isMenuVisible: false,
      };
    },
    computed: {
      dateFormatted: function() {
        return this.toTextValue(this.modelValue);
      },
    },
    created: function() {
      this.fixModelValue();
    },
    methods: {
      clearValue: function() {
        this.setValue(undefined);
      },
      closeMenu: function() {
        this.isMenuVisible = false;
      },
      fixModelValue: function() {
        if (this.modelValue === undefined || this.modelValue instanceof Date) {
          return;
        }

        const fixedValue =
          this.modelValue === undefined || this.modelValue === ''
            ? undefined
            : new Date(this.modelValue);
        this.setValue(fixedValue, { applyDirectly: true });
      },
      toTextValue: function(date) {
        if (!(date instanceof Date)) {
          return undefined;
        }

        return date.toLocaleDateString(undefined, { timeZone: 'UTC' });
      },
      preProcessValueForGet: function(date) {
        if (!(date instanceof Date)) {
          return undefined;
        }

        return `${date.getUTCFullYear()}-${date.getUTCMonth() + 1}-${date.getUTCDate()}`;
      },
      preProcessValueForSet: function(dateStr) {
        if (dateStr === undefined || dateStr === '') {
          return undefined;
        }

        const [year, month, day] = dateStr.split('-');
        return new Date(Date.UTC(year, month - 1, day));
      },
    },
    mixins: [BaseField],
    template: tpl,
    updated: function() {
      this.fixModelValue();
    },
  };
};

const _DateField = async (res, rej) => {
  const cmpDef = await _DateFieldDef();
  res(cmpDef);
};

export const DateFieldDef = _DateFieldDef;
export const DateField = _DateField;
export default _DateField;

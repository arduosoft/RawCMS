import { BaseField } from '../base-field/base-field.js';

const _DateFieldDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/date-field/date-field.tpl.html'
  );

  return {
    data: vm => ({
      isMenuVisible: false,
    }),
    computed: {
      dateFormatted: function() {
        return this.formatDate(this.date);
      },
      datepickerValue: {
        get() {
          return this.value;
        },
        set(val) {
          this.value = val;
        },
      },
    },
    methods: {
      closeMenu: function() {
        this.isMenuVisible = false;
      },
      formatDate: date => {
        if (!date) return null;

        const [year, month, day] = date.split('-');
        return `${month}/${day}/${year}`;
      },
      parseDate: value => {},
      setValue: function(val) {
        console.log(val);
        BaseField.methods.setValue.call(this, val);

        // if (val === undefined || val === '') {
        //   this.$set(this.model, this.field.key, undefined);
        // } else {
        //   this.$set(this.model, this.field.key, new Number(val));
        // }
      },
    },
    mixins: [BaseField],
    props: {
      step: {
        default: 'any',
      },
    },
    template: tpl,
  };
};

const _DateField = async (res, rej) => {
  const cmpDef = await _DateFieldDef();
  res(cmpDef);
};

export const DateFieldDef = _DateFieldDef;
export const DateField = _DateField;
export default _DateField;

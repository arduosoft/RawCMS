import { BaseField } from '../base-field/base-field.js';

const _NumberFieldDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/number-field/number-field.tpl.html'
  );

  return {
    methods: {
      setValue: function(val) {
        if (val === undefined || val === '') {
          this.model[this.field.key] = undefined;
        } else {
          this.model[this.field.key] = new Number(val);
        }
      },
    },
    mixins: [BaseField],
    template: tpl,
  };
};

const _NumberField = async (res, rej) => {
  const cmpDef = await _NumberFieldDef();
  res(cmpDef);
};

export const NumberFieldDef = _NumberFieldDef;
export const NumberField = _NumberField;
export default _NumberField;

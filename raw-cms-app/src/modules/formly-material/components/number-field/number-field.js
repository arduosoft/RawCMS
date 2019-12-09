import { BaseField } from '../base-field/base-field.js';

const _NumberFieldDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/number-field/number-field.tpl.html'
  );

  return {
    methods: {
      preProcessValueForSet: function(val) {
        return val === undefined || val === '' ? undefined : new Number(val);
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

const _NumberField = async (res, rej) => {
  const cmpDef = await _NumberFieldDef();
  res(cmpDef);
};

export const NumberFieldDef = _NumberFieldDef;
export const NumberField = _NumberField;
export default _NumberField;

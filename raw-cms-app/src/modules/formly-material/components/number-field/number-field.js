import { BaseField } from '../base-field/base-field.js';

const _NumberField = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/number-field/number-field.tpl.html'
  );

  res({
    methods: {
      onInput: function(e) {
        this.model[this.field.key] = new Number(e);
        this.runFunction('onInput', e);
      },
    },
    mixins: [BaseField],
    template: tpl,
  });
};

export const NumberField = _NumberField;
export default _NumberField;

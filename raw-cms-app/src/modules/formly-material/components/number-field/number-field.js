import { BaseField } from '../base-field/base-field.js';

const _NumberField = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/number-field/number-field.tpl.html'
  );

  res({
    mixins: [BaseField],
    template: tpl,
  });
};

export const NumberField = _NumberField;
export default _NumberField;

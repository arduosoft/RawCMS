import { NumberField } from '../number-field/number-field.js';

const _IntField = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/number-field/number-field.tpl.html'
  );

  res({
    extends: [NumberField],
    methods: {},
    template: tpl,
  });
};

export const IntField = _IntField;
export default _IntField;

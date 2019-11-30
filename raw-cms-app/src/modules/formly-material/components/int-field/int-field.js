import { NumberFieldDef } from '../number-field/number-field.js';

const _IntFieldDef = async () => {
  const baseDef = await NumberFieldDef();

  return {
    extends: baseDef,
  };
};

const _IntField = async (res, rej) => {
  const cmpDef = await _IntFieldDef();
  res(cmpDef);
};

export const IntFieldDef = _IntFieldDef;
export const IntField = _IntField;
export default _IntField;

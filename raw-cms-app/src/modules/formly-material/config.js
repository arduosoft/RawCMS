import { NumberField } from './components/number-field/number-field.js';
import { TextField } from './components/text-field/text-field.js';

const _configFormlyMaterialModule = function() {
  Vue.$formly.addType('text', TextField);
  Vue.$formly.addType('number', NumberField);
};

export const configFormlyMaterialModule = _configFormlyMaterialModule;
export default _configFormlyMaterialModule;

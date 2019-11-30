import { IntField } from './components/int-field/int-field.js';
import { NumberField } from './components/number-field/number-field.js';
import { TextField } from './components/text-field/text-field.js';

const _configFormlyMaterialModule = function() {
  // Strings
  Vue.$formly.addType('regexp', TextField);
  Vue.$formly.addType('text', TextField);
  // Numbers
  Vue.$formly.addType('number', NumberField);
  Vue.$formly.addType('int', IntField);
};

export const configFormlyMaterialModule = _configFormlyMaterialModule;
export default _configFormlyMaterialModule;

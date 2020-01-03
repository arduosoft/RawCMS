import { BoolField } from './components/bool-field/bool-field.js';
import { DateField } from './components/date-field/date-field.js';
import { EntitiesListField } from './components/entities-list-field/entities-list-field.js';
import { IntField } from './components/int-field/int-field.js';
import { ListField } from './components/list-field/list-field.js';
import { NumberField } from './components/number-field/number-field.js';
import { RelationField } from './components/relation-field/relation-field.js';
import { TextField } from './components/text-field/text-field.js';

const _configFormlyMaterialModule = function() {
  // Bool
  Vue.$formly.addType('bool', BoolField);
  // Strings
  Vue.$formly.addType('regexp', TextField);
  Vue.$formly.addType('text', TextField);
  // Numbers
  Vue.$formly.addType('number', NumberField);
  Vue.$formly.addType('int', IntField);
  // Date/Time
  Vue.$formly.addType('date', DateField);
  // List
  Vue.$formly.addType('list', ListField);
  Vue.$formly.addType('entities-list', EntitiesListField);
  // Relations
  Vue.$formly.addType('relation', RelationField);
};

export const configFormlyMaterialModule = _configFormlyMaterialModule;
export default _configFormlyMaterialModule;

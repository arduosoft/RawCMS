import { RawCMS } from '../../../config/raw-cms.js';
import { vuexStore } from '../../../config/vuex.js';
import { optionalChain } from '../../../utils/object.utils.js';

class ValidationService {
  _validations = {
    text: {
      maxlength: maxLength => {
        return {
          expression: val => val.length > maxLength,
          message: RawCMS.vue.$t('core.validation.text.maxlength', { maxLength: maxLength }),
        };
      },
    },
  };

  getValidationFn(key, obj) {
    const validation = optionalChain(() =>
      key.split('.').reduce((acc, curr) => acc[curr], this._validations)
    );
    return validation(obj);
  }

  applyFieldMetadataToFormlyInput(
    formlyField,
    { fieldType, formId = undefined, fieldOptions = {} }
  ) {
    formlyField._meta_ = {};
    formlyField._meta_.formId = formId;
    formlyField._meta_.options = fieldOptions;

    const fieldsMetadata = optionalChain(() => vuexStore.state.core.fieldsMetadata, {
      fallbackValue: {},
      replaceLastUndefined: true,
    });

    const validators = fieldsMetadata[fieldType].validations;
    validators.forEach(x => {
      const key = `${fieldType}.${x.name}`;
      const option = optionalChain(() => fieldOptions[x.name], { fallbackValue: {} });

      formlyField.validators[key] = function(field, model, next) {
        const item = model;
        const errors = [];
        const options = fieldOptions;
        const name = field.key;
        const Type = fieldType;
        const value = model[field.key];
        const fnValidator = Function('{ item, errors, options, name, Type, value }', x.function);
        const context = { item, errors, options, name, Type, value };
        fnValidator(context);

        if (errors.length == 0) {
          return next(true);
        } else {
          return next(false, errors.map(item => item.Title).join('\n'));
        }
      };

      formlyField.templateOptions.validation[key] = { optionValue: option };
    });

    return formlyField;
  }
}

export const validationService = new ValidationService();
export default validationService;

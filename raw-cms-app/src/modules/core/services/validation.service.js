import { RawCMS } from '../../../config/raw-cms.js';
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
}

export const validationService = new ValidationService();
export default validationService;

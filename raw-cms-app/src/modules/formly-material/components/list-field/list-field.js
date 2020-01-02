import { optionalChain } from '../../../../utils/object.utils.js';
import { BaseField } from '../base-field/base-field.js';

const _ListField = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/list-field/list-field.tpl.html'
  );

  res({
    computed: {
      items: function() {
        const strVal = optionalChain(() => this.field._meta_.options.values);

        if (strVal === undefined) {
          return [];
        }

        return strVal.split('|').map(x => x.trim());
      },
    },
    mixins: [BaseField],
    template: tpl,
  });
};

export const ListField = _ListField;
export default _ListField;

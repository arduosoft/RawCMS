import { optionalChain } from '../../../../utils/object.utils.js';
import { BaseField } from '../base-field/base-field.js';

const _BaseListFieldDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/base-list-field/base-list-field.tpl.html'
  );

  return {
    data: function() {
      return {
        items: [],
      };
    },
    methods: {
      itemText: function(item) {
        return optionalChain(() => item.toString());
      },
      itemValue: function(item) {
        return item;
      },
    },
    mixins: [BaseField],
    template: tpl,
  };
};

const _BaseListField = async (res, rej) => {
  const def = await _BaseListFieldDef();
  return res(def);
};

export const BaseListFieldDef = _BaseListFieldDef;
export const BaseListField = _BaseListField;
export default _BaseListField;

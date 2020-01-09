import { nameOf, optionalChain } from '../../../../utils/object.utils.js';
import { BaseListFieldDef } from '../base-list-field/base-list-field.js';

const _ListField = async (res, rej) => {
  const baseDef = await BaseListFieldDef();

  res({
    mounted: function() {
      const strVal = optionalChain(() => this.field._meta_.options.values);

      if (strVal === undefined) {
        this.$set(this, nameOf(() => this.items), []);
        return;
      }

      this.$set(this, nameOf(() => this.items), strVal.split('|').map(x => x.trim()));
    },
    extends: baseDef,
  });
};

export const ListField = _ListField;
export default _ListField;

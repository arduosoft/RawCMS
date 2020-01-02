import { nameOf } from '../../../../utils/object.utils.js';
import { entitiesSchemaService } from '../../../core/services/entities-schema.service.js';
import { BaseListFieldDef } from '../base-list-field/base-list-field.js';

const _EntitiesListField = async (res, rej) => {
  const baseDef = await BaseListFieldDef();

  res({
    extends: baseDef,
    mounted: async function() {
      const entities = await entitiesSchemaService.getAll();
      this.$set(this, nameOf(() => this.items), entities.map(x => x.CollectionName));
    },
  });
};

export const EntitiesListField = _EntitiesListField;
export default _EntitiesListField;

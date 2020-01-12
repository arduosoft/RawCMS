import { vuexStore } from '../../../../config/vuex.js';
import { nameOf, optionalChain } from '../../../../utils/object.utils.js';
import { entitiesSchemaService } from '../../../core/services/entities-schema.service.js';
import { BaseListFieldDef } from '../base-list-field/base-list-field.js';

const _FieldsListField = async (res, rej) => {
  const baseDef = await BaseListFieldDef();

  res({
    computed: {
      cmpCollName: function() {
        const local = optionalChain(() => this.field._meta_.options.Collection);
        if (local !== undefined) {
          return local;
        }

        if (
          this.field._meta_.formId === undefined ||
          vuexStore.state.core.relationMetadata.formId !== this.field._meta_.formId
        ) {
          return undefined;
        }

        return optionalChain(() => vuexStore.state.core.relationMetadata.collectionName);
      },
    },
    extends: baseDef,
    methods: {
      updateAvailableFields: async function(collName) {
        if (!collName) {
          this.$set(this, nameOf(() => this.items), []);
          return;
        }

        const entity = await entitiesSchemaService.getByName(collName);
        const fields = optionalChain(() => entity.FieldSettings, { fallbackValue: [] }).map(
          x => x.Name
        );
        this.$set(this, nameOf(() => this.items), fields);
      },
    },
    mounted: async function() {
      await this.updateAvailableFields(this.cmpCollName);
    },
    watch: {
      cmpCollName: async function(collName) {
        await this.updateAvailableFields(collName);
      },
    },
  });
};

export const FieldsListField = _FieldsListField;
export default _FieldsListField;

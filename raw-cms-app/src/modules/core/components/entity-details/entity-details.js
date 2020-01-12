import { addOrReplace } from '../../../../utils/immutable.utils.js';
import { deepClone, nameOf, optionalChain } from '../../../../utils/object.utils.js';
import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';
import { entitiesSchemaService } from '../../services/entities-schema.service.js';
import { FieldEditDef } from '../field-edit/field-edit.js';

const _EntityDetailsWrapperDef = async () => {
  const rawCmsDetailEditDef = await RawCmsDetailEditDef();

  return {
    data: function() {
      return {
        activeTabId: 'tabFormly',
        apiService: entitiesSchemaService,
      };
    },
    extends: rawCmsDetailEditDef,
  };
};

const _EntityDetailsDef = async () => {
  const detailWrapperDef = await _EntityDetailsWrapperDef();
  const fieldEditDef = await FieldEditDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/entity-details/entity-details.tpl.html'
  );

  return {
    components: {
      DetailWrapper: detailWrapperDef,
      FieldEdit: fieldEditDef,
    },
    data: function() {
      return {
        currentFieldCopy: null,
        isFieldDialogVisible: false,
        nameRules: [v => !!v || this.$t('core.entities.detail.requiredNameMsg')],
      };
    },
    methods: {
      dismissFieldDialog: function() {
        this.isFieldDialogVisible = false;
      },
      onFieldEdited: function(entity, evt) {
        this.dismissFieldDialog();

        if (!evt.isOk) {
          return;
        }

        const newFieldSettings = addOrReplace({
          array: optionalChain(() => entity.FieldSettings, { fallbackValue: [] }),
          element: evt.field,
          findFn: a => a.Name === evt.field.Name,
        });
        this.$set(entity, nameOf(() => entity.FieldSettings), newFieldSettings);
      },
      removeField: function(entity, field) {
        entity.FieldSettings = entity.FieldSettings.filter(x => x.Name !== field.Name);
      },
      showFieldDialog: function(field = {}) {
        this.currentFieldCopy = deepClone(field);
        this.isFieldDialogVisible = true;
      },
    },
    props: detailWrapperDef.extends.props,
    template: tpl,
  };
};

const _EntityDetails = async (res, rej) => {
  const cmpDef = _EntityDetailsDef();
  res(cmpDef);
};

export const EntityDetailsDef = _EntityDetailsDef;
export const EntityDetails = _EntityDetails;
export default _EntityDetails;

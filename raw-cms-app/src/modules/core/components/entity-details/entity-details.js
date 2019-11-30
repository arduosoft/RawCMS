import { deepClone } from '../../../../utils/object.utils.js';
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
      };
    },
    methods: {
      addNewField: function() {
        console.log('TODO');
      },
      dismissFieldDialog: function() {
        this.isFieldDialogVisible = false;
      },
      onFieldEdited: function(entity, evt) {
        this.dismissFieldDialog();

        if (!evt.isOk) {
          return;
        }

        const actualFieldIndex = entity.FieldSettings.findIndex(x => x.Name === evt.field.Name);

        if (actualFieldIndex >= 0) {
          entity.FieldSettings[actualFieldIndex] = evt.field;
        } else {
          entity.FieldSettings.push(evt.field);
        }
      },
      removeField: function(field) {
        // FIXME: entity.FieldSettings = entity.FieldSettings.filter(x => x.Name !== field.Name);
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

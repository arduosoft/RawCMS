import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';

const _EditDialogDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/edit-dialog/edit-dialog.tpl.html'
  );
  const cp = await RawCmsDetailEditDef();

  return {
    components: {
      RawCmsDetailEdit: cp,
    },
    template: tpl,
  };
};

const _EditDialog = async (res, rej) => {
  const cmpDef = await _EditDialogDef();
  res(cmpDef);
};

export const EditDialogDef = _EditDialogDef;
export const EditDialog = _EditDialog;
export default _EditDialog;

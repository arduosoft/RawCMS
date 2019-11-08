const _EditDialogDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/edit-dialog/edit-dialog.tpl.html'
  );
  return {
    data: () => {
      return {
        data: {},
      };
    },
    methods: {
      amdRequire: require,
    },
    template: tpl,
  };
};
const _EditDialog = async (res, rej) => {
  const cmpDef = _EditDialogDef();
  res(cmpDef);
};

export const EditDialogDef = _EditDialogDef;
export const EditDialog = _EditDialog;
export default _EditDialog;

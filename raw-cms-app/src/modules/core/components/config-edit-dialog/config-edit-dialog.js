import { EditDialogDef } from '../edit-dialog/edit-dialog.js';

const _ConfigEditDialog = async (res, rej) => {
  const cmpDef = await EditDialogDef();

  res({
    props: {
      activeEntity: Object,
    },
    data: function() {
      return {
        data: { code: '' },
      };
    },
    methods: {
      amdRequire: require,
    },
    mixins: [cmpDef],
  });
};

export const ConfigEditDialog = _ConfigEditDialog;
export default _ConfigEditDialog;

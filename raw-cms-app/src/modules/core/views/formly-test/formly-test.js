const _FormlyTestiew = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/formly-test/formly-test.tpl.html');

  res({
    data: function() {
      return {
        model: {
          name: '',
        },
        form: {},
        fields: [
          {
            key: 'name',
            type: 'text',
          },
          {
            key: 'name2',
            type: 'text',
            required: true,
          },
          {
            key: 'name3',
            type: 'text',
            validators: {
              length3: (field, model, next) => next(model[field.key].length > 3),
            },
          },
        ],
      };
    },
    template: tpl,
  });
};

export const FormlyTestiew = _FormlyTestiew;
export default _FormlyTestiew;

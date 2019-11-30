const _FormlyTestiew = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/sandbox/formly-test/formly-test.tpl.html'
  );

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
            wrapper: '<div class="col-12 col-sm-6"></div>',
          },
          {
            key: 'name req',
            type: 'text',
            required: true,
            wrapper: '<div class="col-12 col-sm-6"></div>',
          },
          {
            key: 'name length >3',
            type: 'text',
            validators: {
              length3: {
                expression: (field, model, next) => next(model[field.key].length > 3),
                message: 'This is a test for message override',
              },
            },
            wrapper: '<div class="col-12"></div>',
          },
          {
            key: 'num',
            type: 'number',
            wrapper: '<div class="col-12"></div>',
          },
          {
            key: 'myint',
            type: 'int',
            wrapper: '<div class="col-12"></div>',
          },
        ],
      };
    },
    template: tpl,
  });
};

export const FormlyTestiew = _FormlyTestiew;
export default _FormlyTestiew;

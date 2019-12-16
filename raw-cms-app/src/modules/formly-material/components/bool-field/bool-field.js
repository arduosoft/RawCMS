import { BaseField } from '../base-field/base-field.js';

const _BoolField = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/bool-field/bool-field.tpl.html'
  );

  res({
    data: function() {
      return {
        listeners: {
          blur: this.onBlur,
          focus: this.onFocus,
          // click: this.onClick, NOTE: This is handled by Vuetify
          change: this.onChange,
          input: this.onInput,
          keyup: this.onKeyup,
          keydown: this.onKeydown,
        },
      };
    },
    mixins: [BaseField],
    template: tpl,
  });
};

export const BoolField = _BoolField;
export default _BoolField;

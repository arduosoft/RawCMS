import { epicSpinners } from '../../../../utils/spinners.js';

const _CodeEditor = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/lambda-editor/lambda-editor.tpl.html'
  );

  res({
    components: {
      AtomSpinner: epicSpinners.AtomSpinner,
    },
    created: function() {
      this.isLoading = false;
    },
    data: () => {
      return {
        isLoading: true,
        data: { code: 'const noop = () => {}' },
        valid: true,
        nameRules: [
          v => !!v || 'Name is required',
          v => (v && v.length <= 10) || 'Name must be less than 10 characters',
        ],
      };
    },
    methods: {
      amdRequire: require,
      save: function() {
        console.log('saving');
      },
    },
    template: tpl,
    watch: {},
  });
};

export const CodeEditor = _CodeEditor;
export default _CodeEditor;

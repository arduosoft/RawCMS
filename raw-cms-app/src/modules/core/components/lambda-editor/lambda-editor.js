import { epicSpinners } from '../../../../utils/spinners.js';
import { entitiesService } from '../../services/entities.service.js';

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
      this.id = this.$route.params.id;
      this.fetchData();
    },
    data: () => {
      return {
        id: 'new',
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
      save: async function() {
        console.log('saving');
        if (this.id == 'new') {
          await entitiesService.post('_js', this.data);
        } else {
          await entitiesService.put('_js', this.id, this.data);
        }
        this.$router.push({ name: 'lambda-list' });
      },
      fetchData: async function() {
        if (this.id != 'new') {
          const res = await entitiesService.get('_js', this.id);
          this.data = res;
        }
      },
    },
    template: tpl,
    watch: {
      $route: 'fetchData',
    },
  });
};

export const CodeEditor = _CodeEditor;
export default _CodeEditor;

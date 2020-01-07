import vuexStore from '../../../../config/vuex.js';
import { LambdasListDef } from '../../components/lambdas-list/lambdas-list.js';

const _LambdasListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/lambdas-list-view/lambdas-list-view.tpl.html'
  );
  const list = await LambdasListDef();

  res({
    components: {
      LambdasList: list,
    },
    mounted() {
      vuexStore.dispatch('core/updateTopBarTitle', this.$t('core.lambdas.title'));
    },
    methods: {
      goToCreateView: function() {
        this.$router.push({ name: 'lambda-details', params: { id: 'new' } });
      },
    },
    template: tpl,
  });
};

export const LambdasListView = _LambdasListView;
export default _LambdasListView;

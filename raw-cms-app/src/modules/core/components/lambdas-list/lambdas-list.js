import { RawCmsListDef } from '../../../shared/components/list/list.js';
import { lambdasService } from '../../services/lambdas.service.js';

const _LambdasListWrapperDef = async () => {
  const rawCmsListDef = await RawCmsListDef();

  return {
    data: function() {
      return {
        apiService: lambdasService,
      };
    },
    extends: rawCmsListDef,
    methods: {
      deleteConfirmMsg(item) {
        return this.$t('core.lambdas.deleteConfirmMsgTpl', { name: item.Name });
      },
      deleteSuccessMsg(item) {
        return this.$t('core.lambdas.deleteSuccessMsgTpl', { name: item.Name });
      },
      deleteErrorMsg(item) {
        return this.$t('core.lambdas.deleteErrorMsgTpl', { name: item.Name });
      },
    },
    props: {
      detailRouteName: {
        typ: String,
        default: 'lambda-details',
      },
    },
  };
};

const _LambdasListDef = async () => {
  const listWrapperDef = await _LambdasListWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/lambdas-list/lambdas-list.tpl.html'
  );

  return {
    components: {
      ListWrapper: listWrapperDef,
    },
    template: tpl,
  };
};

const _LambdasList = async (res, rej) => {
  const cmpDef = _LambdasListDef();
  res(cmpDef);
};

export const LambdasListDef = _LambdasListDef;
export const LambdasList = _LambdasList;
export default _LambdasList;

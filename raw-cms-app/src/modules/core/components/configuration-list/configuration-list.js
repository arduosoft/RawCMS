import { RawCmsListDef } from '../../../shared/components/list/list.js';
import { configurationService } from '../../services/configuration.service.js';

const _ConfigurationListWrapperDef = async () => {
  const rawCmsListDef = await RawCmsListDef();

  return {
    data: function() {
      return {
        apiService: configurationService,
      };
    },
    extends: rawCmsListDef,
    methods: {
      deleteConfirmMsg(item) {
        return this.$t('core.configuration.deleteConfirmMsgTpl', { name: item.plugin_name });
      },
      deleteSuccessMsg(item) {
        return this.$t('core.configuration.deleteSuccessMsgTpl', { name: item.plugin_name });
      },
      deleteErrorMsg(item) {
        return this.$t('core.configuration.deleteErrorMsgTpl', { name: item.plugin_name });
      },
    },
    props: {
      detailRouteName: {
        typ: String,
        default: 'configuration-details',
      },
    },
  };
};

const _ConfigurationListDef = async () => {
  const listWrapperDef = await _ConfigurationListWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/configuration-list/configuration-list.tpl.html'
  );

  return {
    components: {
      ListWrapper: listWrapperDef,
    },
    template: tpl,
  };
};

const _ConfigurationList = async (res, rej) => {
  const cmpDef = _ConfigurationListDef();
  res(cmpDef);
};

export const ConfigurationListDef = _ConfigurationListDef;
export const ConfigurationList = _ConfigurationList;
export default _ConfigurationList;

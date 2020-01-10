import { RawCmsListDef } from '../../../shared/components/list/list.js';
import { logsService } from '../../services/application.service.js';

const _LogsListWrapperDef = async () => {
  const rawCmsListDef = await RawCmsListDef();

  return {
    data: function() {
      return {
        apiService: logsService,
      };
    },
    extends: rawCmsListDef,
    methods: {
      goTo: function(item) {
        this.$router.push({
          name: this.detailRouteName,
          params: { name: item.Name },
        });
      },
    },
    props: {
      detailRouteName: {
        typ: String,
        default: 'logs-search',
      },
    },
  };
};

const _LogsListDef = async () => {
  const listWrapperDef = await _LogsListWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/logs-list/logs-list.tpl.html'
  );

  return {
    components: {
      ListWrapper: listWrapperDef,
    },
    template: tpl,
  };
};

const _LogsList = async (res, rej) => {
  const cmpDef = _LogsListDef();
  res(cmpDef);
};

export const LogsListDef = _LogsListDef;
export const LogsList = _LogsList;
export default _LogsList;

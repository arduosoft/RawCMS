import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';
import { logsService } from '../../services/application.service.js';

const _LogsDetailsWrapperDef = async () => {
  const rawCmsDetailEditDef = await RawCmsDetailEditDef();

  return {
    data: function() {
      return {
        search: '',
        apiService: logsService,
      };
    },
    extends: rawCmsDetailEditDef,
  };
};

const _LogsDetailsDef = async () => {
  const detailWrapperDef = await _LogsDetailsWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/logs-search/logs-search.tpl.html'
  );

  return {
    components: {
      DetailWrapper: detailWrapperDef,
    },
    methods: {
      search: function() {
        alert();
        return;
      },
    },
    mounted: async function() {
      const res = await logsService.getAppByName(this.CmpName);
      console.log(res.LogName);
      return res.LogName;
    },
    computed: {
      CmpName: function() {
        return this.$route.params.name;
      },
    },
    props: detailWrapperDef.extends.props,
    props: { name: String },
    template: tpl,
  };
};

const _LogsDetails = async (res, rej) => {
  const cmpDef = _LogsDetailsDef();
  res(cmpDef);
};

export const LogsDetailsDef = _LogsDetailsDef;
export const LogsDetails = _LogsDetails;
export default _LogsDetails;

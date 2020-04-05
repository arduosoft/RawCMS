import { RawCmsDataTableDef } from "/app/common/shared/components/data-table/data-table.js";
import { applicationsService } from "/app/modules/core/services/application.service.js";
import { fullTextService } from "/app/modules/fulltext/services/full-text.service.js";
const _LogsTableWrapperDef = async () => {
  const rawCmsDataTableDef = await RawCmsDataTableDef();

  return {
    data: function() {
      return {
        apiService: applicationsService,
        headTable: []
      };
    },
    extends: rawCmsDataTableDef,
    methods: {
      deleteConfirmMsg(item) {
        return this.$t("core.collections.table.deleteConfirmMsgTpl");
      },
      deleteSuccessMsg(item) {
        return this.$t("core.collections.table.deleteSuccessMsgTpl");
      },
      deleteErrorMsg(item) {
        return this.$t("core.collections.table.deleteErrorMsgTpl");
      },
      showDeleteConfirm: function(item) {
        this.currentItem = item;
        this.isDeleteConfirmVisible = false;
      },
      getDataHeaders: async function() {
        this.headTable = [
          { text: "Level", value: "level", sortable: false },
          { text: "Message", value: "message", sortable: false }
        ];
        return this.headTable;
      },
      fetchData: async function() {
        const res = await fullTextService.getTextByLevel(
          "1337215778276f64eb92f3359b1164e220c122440b156cc174e6cf4baa9e8ebc",
          "INFO"
        );
        this.items = res.map(x => {
          return { ...x, _meta_: { isDeleting: false } };
        });
        this.totalItemsCount = this.totalCount;
        this.isLoading = false;
      }
    }
  };
};

const _LogsDetailsDef = async () => {
  const tableWrapperDef = await _LogsTableWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    "/app/modules/logs/components/logs-search/logs-search.tpl.html"
  );

  return {
    components: {
      TableWrapper: tableWrapperDef
    },
    data: function() {
      return {
        level: 1,
        levels: [
          { text: this.$t("core.logs.detail.level0") },
          { text: this.$t("core.logs.detail.level1") },
          { text: this.$t("core.logs.detail.level2") }
        ],
        //search: '',
        list: ""
      };
    },
    methods: {
      search: function() {
        alert();
        return;
      },
      sLevel: async function(level) {
        const res = await fullTextService.getTextByLevel(
          this.logHashName,
          level
        );
        console.log(res);
        return res;
      }
    },
    mounted: async function() {
      const res = await applicationsService.getAppByName(this.CmpName);
      console.log(res.LogNameHash);
      return (this.logHashName = res.LogNameHash);
    },
    computed: {
      CmpName: function() {
        return this.$route.params.name;
      }
    },
    //props: detailWrapperDef.extends.props,
    props: { name: String },
    template: tpl
  };
};

const _LogsDetails = async (res, rej) => {
  const cmpDef = _LogsDetailsDef();
  res(cmpDef);
};

export const LogsDetailsDef = _LogsDetailsDef;
export const LogsDetails = _LogsDetails;
export default _LogsDetails;

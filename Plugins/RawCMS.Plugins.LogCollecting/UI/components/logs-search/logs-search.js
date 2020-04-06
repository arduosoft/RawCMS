import { RawCmsDataTableDef } from "/app/common/shared/components/data-table/data-table.js";
import { applicationsService } from "/app/modules/core/services/application.service.js";
import { fullTextService } from "/app/modules/fulltext/services/full-text.service.js";
import { evtLogSearch } from "/app/modules/logs/events.js";
import { RawCMS } from "/app/config/raw-cms.js";

const _LogsTableWrapperDef = async () => {
  const rawCmsDataTableDef = await RawCmsDataTableDef();

  return {
    data: function() {
      return {
          apiService: applicationsService,
          fullTextService: fullTextService,
          headTable: [],
          logLevel:"ALL"
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
        search: async function (level, text, indexname) {

         var app=applicationsService.getAppByName("default");
            let res = await fullTextService.search(
                {
                    size: 1,
                    query: {}
                },
                indexname
            );
                
            this.items = res.map(x => {
                console.log(x);
          return { ...x, _meta_: { isDeleting: false } };
        });
        this.totalItemsCount = this.totalCount;
        this.isLoading = false;
        },


     
      }, mounted: function () {
        console.log("inner mounted");
          RawCMS.eventBus.$on(evtLogSearch, (level,text,indexname) => {
              console.log("inner search $level,$text,$indexname");
              this.search(level, text, indexname);
          });
      },
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
        level: 'ALL',
        levels: [
          { text: this.$t("core.logs.detail.level0") },
          { text: this.$t("core.logs.detail.level1") },
          { text: this.$t("core.logs.detail.level2") }
        ],
        text: '',
        indexname:''
      };
    },
    methods: {
        search: async function() {

            RawCMS.eventBus.$emit(evtLogSearch, this.level, this.text, this.indexname);
            console.log("search");
            
      }
    },
    mounted: async function() {
      const res = await applicationsService.getAppByName(this.CmpName);
      
        return (this.indexname = "log_"+res.PublicId);
    },
    computed: {
      //CmpName: function() {
      //  return this.$route.params.name;
      //}
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

import { optionalChain } from '../../../../utils/object.utils.js';
import { debounce } from '../../../../utils/time.utils.js';
import { BaseField } from '../base-field/base-field.js';

const _BaseListFieldDef = async () => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/base-list-field/base-list-field.tpl.html'
  );

  return {
    computed: {
      isSearchable: function() {
        return false;
      },
      isRemoteSearch: function() {
        return false;
      },
      showCounter: function() {
        return this.multi && Array.isArray(this.value) && this.value.length > 0;
      },
      multi: function() {
        return false;
      },
    },
    data: function() {
      return {
        isLoading: false,
        items: [],
        search: undefined,
        actualTextSearch: undefined,
        lastSearch: undefined,
      };
    },
    methods: {
      itemText: function(item) {
        return optionalChain(() => item.toString());
      },
      itemValue: function(item) {
        return item;
      },
      setValue: function(val, { applyDirectly } = { applyDirectly: false }) {
        BaseField.methods.setValue.apply(this, [val, { applyDirectly }]);
        this.actualTextSearch = undefined;
      },
      remoteSearchCaller: debounce(async function() {
        if (this.search === null || this.search === undefined) {
          return;
        }

        if (this.search === this.actualTextSearch || this.search === this.lastSearch) {
          return;
        }

        if (!this.isRemoteSearch) {
          return;
        }

        if (this.isLoading) {
          return;
        }

        this.isLoading = true;
        this.items = await this.remoteSearch(this.search);
        this.lastSearch = this.search;
        this.isLoading = false;
      }, 250),
      remoteSearch: async function(search) {
        throw new Error('You have to implement `remoteSearch` method if `isRemoteSearch` is true!');
      },
    },
    mixins: [BaseField],
    template: tpl,
    watch: {
      search: function(newVal, oldVal) {
        if (this.actualTextSearch === undefined && (newVal !== null && newVal !== undefined)) {
          this.actualTextSearch = newVal;
        }

        this.remoteSearchCaller();
      },
    },
  };
};

const _BaseListField = async (res, rej) => {
  const def = await _BaseListFieldDef();
  return res(def);
};

export const BaseListFieldDef = _BaseListFieldDef;
export const BaseListField = _BaseListField;
export default _BaseListField;

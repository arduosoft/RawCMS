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
      };
    },
    methods: {
      itemText: function(item) {
        return optionalChain(() => item.toString());
      },
      itemValue: function(item) {
        return item;
      },
      remoteSearch: async function(search) {
        throw new Error('You have to implement `remoteSearch` method if `isRemoteSearch` is true!');
      },
    },
    mixins: [BaseField],
    template: tpl,
    watch: {
      search: debounce(
        async _val => {
          if (!context.isRemoteSearch) {
            return;
          }

          if (context.isLoading) {
            return;
          }

          context.isLoading = true;
          context.items = await context.remoteSearch(context.search);
          context.isLoading = false;
        },
        500,
        { context: this }
      ),
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

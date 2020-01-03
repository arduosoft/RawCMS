import { nameOf, optionalChain } from '../../../../utils/object.utils.js';
import { BaseCrudService } from '../../../shared/services/base-crud-service.js';
import { BaseFieldProps } from '../base-field/base-field.js';
import { BaseListFieldDef } from '../base-list-field/base-list-field.js';

const _ListFieldWrapperDef = async () => {
  const listFieldDef = await BaseListFieldDef();

  return {
    computed: {
      isSearchable: function() {
        return true;
      },
      isRemoteSearch: function() {
        return true;
      },
      multi: function() {
        return optionalChain(() => this.field._meta_.options.Multiple, { fallbackValue: false });
      },
      showProp: function() {
        return optionalChain(() => this.field._meta_.options.Property, { fallbackValue: '_id' });
      },
    },
    methods: {
      itemText: function(item) {
        const val = item[this.showProp];
        return val !== undefined ? val.toString() : `NA (id: ${item._id})`;
      },
      itemValue: function(item) {
        return item._id;
      },
      remoteSearch: async function(search) {
        const rawQuery = { [this.showProp]: { $regex: `.*${search}.*` } };
        const res = (await this.apiService.getPage({ rawQuery })).items;
        return res;
      },
    },
    mounted: async function() {
      const collectionName = optionalChain(() => this.field._meta_.options.Collection);

      if (collectionName === undefined) {
        this.$set(this, nameOf(() => this.items), []);
        return;
      }

      this.apiService = new BaseCrudService({ basePath: `/api/CRUD/${collectionName}` });
      const values = await this.apiService.getAll();

      this.$set(this, nameOf(() => this.items), values);
    },
    extends: listFieldDef,
  };
};

const _RelationField = async (res, rej) => {
  const wrapperDef = await _ListFieldWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/formly-material/components/relation-field/relation-field.tpl.html'
  );

  res({
    components: {
      ListWrapper: wrapperDef,
    },

    mixins: [BaseFieldProps],
    template: tpl,
  });
};

export const RelationField = _RelationField;
export default _RelationField;

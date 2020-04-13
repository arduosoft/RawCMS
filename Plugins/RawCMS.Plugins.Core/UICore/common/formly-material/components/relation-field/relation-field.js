import { nameOf, optionalChain } from "/app/utils/object.utils.js";
import metadataService from "/app/modules/core/services/metadata.service.js";
import { getFieldSearchPayload } from "/app/modules/core/utils/metadata.utils.js";
import { BaseCrudService } from "/app/common/shared/services/base-crud-service.js";
import { BaseFieldProps } from "/app/common/formly-material/components/base-field/base-field.js";
import { BaseListFieldDef } from "/app/common/formly-material/components/base-list-field/base-list-field.js";

const _ListFieldWrapperDef = async () => {
    const listFieldDef = await BaseListFieldDef();

    return {
        computed: {
            isSearchable: function () {
                return true;
            },
            isRemoteSearch: function () {
                return true;
            },
            multi: function () {
                return optionalChain(() => this.field._meta_.options.Multiple, {
                    fallbackValue: false
                });
            },
            showProp: function () {
                return optionalChain(() => this.field._meta_.options.Property, {
                    fallbackValue: "_id"
                });
            },
            collectionName: function () {
                return optionalChain(() => this.field._meta_.options.Collection);
            },
            fieldMetadata: async function () {
                const searchKey = `${this.collectionName}-${this.showProp}`;
                if (this.lastSearchKey !== searchKey) {
                    this.lastFoundMeta = await metadataService.getFieldMetadata(
                        this.collectionName,
                        this.showProp
                    );
                    this.lastSearchKey = searchKey;
                }

                return this.lastFoundMeta;
            }
        },
        data: function () {
            return {
                lastSearchKey: undefined,
                lastFoundMeta: undefined
            };
        },
        methods: {
            itemText: function (item) {
                const val = item[this.showProp];
                return val !== undefined ? val.toString() : `NA (id: ${item._id})`;
            },
            itemValue: function (item) {
                return item._id;
            },
            remoteSearch: async function (search) {
                const fieldType = (await this.fieldMetadata).type.typeName;
                const searchPayload = getFieldSearchPayload(search, fieldType);
                const rawQuery = { [this.showProp]: searchPayload };
                const res = (await this.apiService.getPage({ rawQuery })).items;
                return res;
            }
        },
        mounted: async function () {
            if (this.collectionName === undefined) {
                this.$set(this, nameOf(() => this.items), []);
                return;
            }

            this.apiService = new BaseCrudService({
                basePath: `/api/CRUD/${this.collectionName}`
            });
            const values = await this.apiService.getAll();

            this.$set(this, nameOf(() => this.items), values);
        },
        extends: listFieldDef
    };
};

const _RelationField = async (res, rej) => {
    const wrapperDef = await _ListFieldWrapperDef();
    const tpl = await RawCMS.loadComponentTpl(
        "/app/common/formly-material/components/relation-field/relation-field.tpl.html"
    );

    res({
        components: {
            ListWrapper: wrapperDef
        },

        mixins: [BaseFieldProps],
        template: tpl
    });
};

export const RelationField = _RelationField;
export default _RelationField;
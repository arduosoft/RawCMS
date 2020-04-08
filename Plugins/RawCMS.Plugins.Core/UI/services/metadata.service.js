import { vuexStore } from "/app/config/vuex.js";
import { optionalChain } from "/app/utils/object.utils.js";
import { apiClient } from "/app/modules/core/api/api-client.js";
import { entitiesSchemaService } from "/app/modules/core/services/entities-schema.service.js";
import { snackbarService } from "/app/modules/core/services/snackbar.service.js";

class MetadataService {
    async getFieldsMetadata() {
        try {
            //TODO: remove exluded types 'fields-list', 'entities-list
            const res = await apiClient.get(`/system/metadata/fieldinfo`);
            return res.data.reduce((map, obj) => {
                map[obj.type.typeName] = obj;
                return map;
            }, {});
        } catch (e) {
            snackbarService.showMessage({
                color: "error",
                message: e
            });
        }
    }

    async getFieldMetadata(collectionName, fieldName) {
        const res = await entitiesSchemaService.getByName(collectionName);
        const fieldType = optionalChain(
            () => res.FieldSettings.find(x => x.Name === fieldName).Type
        );
        const meta = optionalChain(() => vuexStore.state.core.fieldsMetadata, {
            fallbackValue: {}
        });
        const fieldMeta = meta[fieldType];
        if (fieldMeta === undefined) {
            throw new Error(
                `Unable to find field with type ${fieldType} in fields metadata.`
            );
        }

        return fieldMeta;
    }
}

export const metadataService = new MetadataService();
export default metadataService;
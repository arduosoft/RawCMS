import { apiClient } from '../api/api-client.js';
import { snackbarService } from '../services/snackbar.service.js';

class MetadataService {
  async getFieldsMetadata() {
    try {
      const res = await apiClient.get(`/system/metadata/fieldinfo`);
      return res.data.reduce((map, obj) => {
        map[obj.type.typeName] = obj;
        return map;
      }, {});
    } catch (e) {
      snackbarService.showMessage({
        color: 'error',
        message: e,
      });
    }
  }
}

export const metadataService = new MetadataService();
export default metadataService;

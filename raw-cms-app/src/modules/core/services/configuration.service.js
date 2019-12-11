import { BaseCrudService } from '../../shared/services/base-crud-service.js';

class ConfigurationService extends BaseCrudService {
  constructor() {
    super({ basePath: '/system/admin/_configuration' });
  }
}

export const configurationService = new ConfigurationService();
export default configurationService;

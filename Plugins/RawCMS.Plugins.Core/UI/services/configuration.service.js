import { BaseCrudService } from "/app/common/shared/services/base-crud-service.js";

class ConfigurationService extends BaseCrudService {
  constructor() {
    super({ basePath: "/system/admin/_configuration" });
  }
}

export const configurationService = new ConfigurationService();
export default configurationService;

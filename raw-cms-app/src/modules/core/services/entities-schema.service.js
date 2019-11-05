import { BaseCrudService } from '../../shared/services/base-crud-service.js';

class EntitiesSchemaService extends BaseCrudService {
  constructor() {
    super({ basePath: '/system/admin/_schema' });
  }
}

export const entitiesSchemaService = new EntitiesSchemaService();
export default entitiesSchemaService;

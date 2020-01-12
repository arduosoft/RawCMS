import { BaseCrudService } from '../../shared/services/base-crud-service.js';

class EntitiesSchemaService extends BaseCrudService {
  constructor() {
    super({ basePath: '/system/admin/_schema' });
  }

  async getByName(collectionName) {
    const res = await this.getPage({
      size: 1,
      rawQuery: { CollectionName: collectionName },
    });
    return res.items[0];
  }
}

export const entitiesSchemaService = new EntitiesSchemaService();
export default entitiesSchemaService;

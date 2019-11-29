import { BaseCrudService } from '../../shared/services/base-crud-service.js';

class LambdasService extends BaseCrudService {
  constructor() {
    super({ basePath: '/system/admin/_js' });
  }
}

export const lambdasService = new LambdasService();
export default lambdasService;

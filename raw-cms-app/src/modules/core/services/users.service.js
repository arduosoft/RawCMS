import { BaseCrudService } from '../../shared/services/base-crud-service.js';

class UserService extends BaseCrudService {
  constructor() {
    super({ basePath: '/system/admin/_users' });
  }
}

export const userService = new UserService();
export default userService;

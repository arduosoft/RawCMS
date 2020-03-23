import { BaseCrudService } from "/app/common/shared/services/base-crud-service.js";

class LogsService extends BaseCrudService {
  constructor() {
    super({ basePath: "/api/CRUD/Application" });
  }

  async getAppByName(name) {
    const res = await this.getPage({
      size: 1,
      rawQuery: { Name: name }
    });
    console.log(res);
    return res.items[0];
  }
}

export const logsService = new LogsService();
export default logsService;

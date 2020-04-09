import { BaseCrudService } from "/app/common/shared/services/base-crud-service.js";

class ApplicationService extends BaseCrudService {
    constructor() {
        super({ basePath: "/api/CRUD/application" });
    }

    async getAppByName(name) {
        const res = await this.getPage({
            size: 1,
            rawQuery: { Name: name }
        });
        
        return res.items[0];
    }
}

export const applicationsService = new ApplicationService();
export default applicationsService;
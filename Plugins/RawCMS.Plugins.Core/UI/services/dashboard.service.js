import { BaseCrudService } from '/app/common/shared/services/base-crud-service.js';

class DashboardService extends BaseCrudService {
    constructor() {
        super({ basePath: "/system/admin/_schema" });
    }

    async getDashboardInfo() {
        
        this._basePath = '/system/admin/_schema';
        let recordCount = await this.count();
        let collections = await this.getAll();
        let quota = {};
        for await (const item of collections) {
            let collName = item.CollectionName;
            this._basePath = '/api/CRUD/' + collName;
            let collCount = await this.count();
            quota[collName] = collCount;
        }
        
        return {
            recordQuotas: quota,
            entitiesNum: recordCount
        };
    }
}

export const dashboardService = new DashboardService();
export default dashboardService;
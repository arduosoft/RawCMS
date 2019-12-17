import { BaseApiService } from '../../shared/services/base-api-service.js';

class DashboardService extends BaseApiService {
  constructor() {
    super();
  }

  async getDashboardInfo() {
    // FIXME: For now we use mock data
    return {
      recordQuotas: {
        TEST: 30,
        Items1: 20,
        Items2: 40,
      },
      entitiesNum: 7,
      lastWeekCallsNum: 500,
    };
  }
}

export const dashboardService = new DashboardService();
export default dashboardService;

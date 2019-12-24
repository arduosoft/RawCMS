import { sleep } from '../../../utils/time.utils.js';
import { BaseApiService } from '../../shared/services/base-api-service.js';

class DashboardService extends BaseApiService {
  constructor() {
    super();
  }

  async getDashboardInfo() {
    // FIXME: For now we use mock data

    await sleep(5000);

    const quota = {
      TEST: Math.floor(Math.random() * 100),
      Items1: Math.floor(Math.random() * 100),
      Items2: Math.floor(Math.random() * 100),
      Items3: Math.floor(Math.random() * 100),
      Items4: Math.floor(Math.random() * 100),
      Items5: Math.floor(Math.random() * 100),
      Items6: Math.floor(Math.random() * 100),
    };
    return {
      recordQuotas: quota,
      entitiesNum: Object.keys(quota).length,
      lastWeekCallsNum: Math.floor(Math.random() * 500),
    };
  }
}

export const dashboardService = new DashboardService();
export default dashboardService;

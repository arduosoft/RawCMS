import { apiClient } from "/app/modules/core/api/api-client.js";
import { snackbarService } from "/app/modules/core/services/snackbar.service.js";

class LogsService {
    async getStatistics() {
        try {
            const res = await apiClient.get(`/api/logstatics`);
            return res.data;
        } catch (e) {
            snackbarService.showMessage({
                color: "error",
                message: e
            });
        }
    }
}

export const logsService = new LogsService();
export default logsService;
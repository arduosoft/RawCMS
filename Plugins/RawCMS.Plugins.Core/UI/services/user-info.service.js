import { apiClient } from "/app/modules/core/api/api-client.js";
import { snackbarService } from "/app/modules/core/services/snackbar.service.js";

class UserInfoService {
  async getUserInfo() {
    try {
      const res = await apiClient.get(`/connect/userinfo`);
      return res.data;
    } catch (e) {
      snackbarService.showMessage({
        color: "error",
        message: e
      });
    }
  }
}

export const userInfoService = new UserInfoService();
export default userInfoService;

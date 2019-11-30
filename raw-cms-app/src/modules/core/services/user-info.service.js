import { apiClient } from '../api/api-client.js';
import { snackbarService } from '../services/snackbar.service.js';

class UserInfoService {
  async getUserInfo() {
    try {
      const res = await apiClient.get(`/connect/userinfo`);
      return res.data;
    } catch (e) {
      snackbarService.showMessage({
        color: 'error',
        message: e,
      });
    }
  }
}

export const userInfoService = new UserInfoService();
export default userInfoService;

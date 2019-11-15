import { apiClient } from '../api/api-client.js';
import { loginService } from '../services/login.service.js';

class UserInfoService {
  async getUserInfo() {
    if (!loginService.isLoggedIn) {
      return null;
    }

    const res = await apiClient.get(`/connect/userinfo`);
    return res.data;
  }
}

export const userInfoService = new UserInfoService();
export default userInfoService;

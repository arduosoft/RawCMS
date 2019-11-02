import { apiClient } from '../api/api-client.js';

class UserService {
  _apiClient;

  constructor() {
    this._apiClient = apiClient;
  }

  async getUsers(page = 0) {
    // FIXME: Handle pagination (maybe in api client?)
    const res = await this._apiClient.get('/system/admin/_users');
    return res.data.data.items;
  }

  async deleteUser(userId) {
    if (!entityId) {
      console.error(`Unable to delete user from schema: given id is invalid (${userId})`);
      return;
    }

    const res = await this._apiClient.delete(`/system/admin/_users/${userId}`);
    return res.data.data === true;
  }
}

export const userService = new UserService();
export default userService;

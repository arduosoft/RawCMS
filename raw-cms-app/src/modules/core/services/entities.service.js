import { apiClient } from '../api/api-client.js';

class EntitiesService {
  _apiClient;
  baseUrl = '/system/admin';

  constructor() {
    this._apiClient = apiClient;
  }

  async get(entity, id) {
    // FIXME: Handle pagination (maybe in api client?)
    const res = await this._apiClient.get(`${this.baseUrl}/${entity}/${id}`);
    return res.data.data;
  }

  async list(entity, page = 0) {
    // FIXME: Handle pagination (maybe in api client?)
    const res = await this._apiClient.get(`${this.baseUrl}/${entity}`);
    return res.data.data.items;
  }

  async delete(entity, entityId) {
    if (!entityId) {
      console.error(`Unable to delete entity from schema: given id is invalid (${entityId})`);
      return;
    }

    const res = await this._apiClient.delete(`${this.baseUrl}/${entity}/${entityId}`);
    return res.data.data === true;
  }

  async post(entity, data) {
    const res = await this._apiClient.post(`${this.baseUrl}/${entity}`, data);
    return res.data.data === true;
  }

  async put(entity, entityId, data) {
    const res = await this._apiClient.put(`${this.baseUrl}/${entity}/${entityId}`, data);
    return res.data.data === true;
  }
}

export const entitiesService = new EntitiesService();
export default entitiesService;

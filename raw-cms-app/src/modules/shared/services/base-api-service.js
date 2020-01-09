import { apiClient } from '../../core/api/api-client.js';

export class BaseApiService {
  _apiClient;

  constructor() {
    this._apiClient = apiClient;
  }

  _checkGenericError(axiosRes) {
    if (axiosRes.status !== 200) {
      return false;
    }

    return true;
  }
}

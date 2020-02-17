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

    if (axiosRes.data && axiosRes.data.status && axiosRes.data.status != 'OK') {
      return false;
    }

    if (axiosRes.data && axiosRes.data.errors && axiosRes.data.errors.lenght > 0) {
      return false;
    }

    return true;
  }
}

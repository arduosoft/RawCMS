import { RawCMS } from '../../../config/raw-cms.js';
import { loginService } from '../services/login.service.js';

const _apiClient = axios.create({
  baseURL: `${RawCMS.env.api.baseUrl}`,
});

_apiClient.interceptors.request.use(request => {
  if (loginService.isLoggedIn) {
    request.headers.common['Authorization'] = `bearer ${loginService.auth.access_token}`;
  }
  return request;
});

export const apiClient = _apiClient;
export default apiClient;

import { RawCMS } from '../../../config/raw-cms.js';

const _apiClient = axios.create({
  baseURL: `${RawCMS.env.api.baseUrl}`,
});

_apiClient.interceptors.request.use(request => {
  // if (loginService.isLoggedIn) {
  //   request.headers['Authorization'] = `Bearer ${loginService.auth.access_token}`;
  // }
  return request;
});

export const apiClient = _apiClient;
export default apiClient;

import { RawCMS } from '../../../config/raw-cms.js';
import { loginService } from '../services/login.service.js';

const _apiClient = axios.create({
  baseURL: `${RawCMS.env.api.baseUrl}`,
});

_apiClient.interceptors.request.use(request => {
  if (loginService.isLoggedIn) {
    request.headers.common['Authorization'] = `Bearer ${loginService.auth.access_token}`;
  }
  return request;
});

_apiClient.interceptors.response.use(
  function(response) {
    console.log(response);
    if (response.status === 401) {
      loginService.logout();
    }

    return response;
  },
  function(error) {
    console.log(response);
    return Promise.reject(error);
  }
);

export const apiClient = _apiClient;
export default apiClient;

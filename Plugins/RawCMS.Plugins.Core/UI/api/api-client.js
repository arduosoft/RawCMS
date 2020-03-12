import { RawCMS } from "/app/config/raw-cms.js";
import { optionalChain } from "/app/utils/object.utils.js";
import { loginService } from "/app/modules/core/services/login.service.js";

const _apiClient = axios.create({
  baseURL: `${RawCMS.env.api.baseUrl}`
});

_apiClient.interceptors.request.use(request => {
  if (loginService.isLoggedIn) {
    request.headers.common[
      "Authorization"
    ] = `Bearer ${loginService.auth.access_token}`;
  }
  return request;
});

_apiClient.interceptors.response.use(
  response => response,
  error => {
    const isUnauthorized =
      optionalChain(() => error.response.status, { fallbackValue: 0 }) === 401;
    if (isUnauthorized) {
      loginService.logout();
    } else {
      Promise.reject(error);
    }
  }
);

export const apiClient = _apiClient;
export default apiClient;

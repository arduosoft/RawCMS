import { apiClient } from '../api/api-client.js';
import { snackbarService } from '../services/snackbar.service.js';

class ReloadService {
  async reloadBackend() {
    try {
      await apiClient
        .get(`/system/Config/reload`)
        .then(() => {
          snackbarService.showMessage({
            color: 'green',
            message: 'Reload successfully called',
          });
        })
        .catch(error => {
          console.log(error);
          snackbarService.showMessage({
            color: 'error',
            message: 'Error on reloading the backend',
          });
        });
    } catch (e) {
      snackbarService.showMessage({
        color: 'error',
        message: e,
      });
    }
  }
}

export const reloadService = new ReloadService();
export default reloadService;

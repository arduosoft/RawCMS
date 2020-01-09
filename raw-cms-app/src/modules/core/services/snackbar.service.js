import { evtSnackbarMessage } from '../../../app/events.js';
import { RawCMS } from '../../../config/raw-cms.js';

class SnackbarService {
  _eventBus;

  constructor() {
    this._eventBus = RawCMS.eventBus;
  }

  showMessage(snackbarConfig) {
    this._eventBus.$emit(evtSnackbarMessage, snackbarConfig);
  }
}

export const snackbarService = new SnackbarService();
export default snackbarService;

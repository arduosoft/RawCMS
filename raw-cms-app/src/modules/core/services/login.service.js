import { router } from '../../../config/router.js';
import { evtLogin, evtLogout } from '../events.js';
import { RawCMS } from '/config/raw-cms.js';
import { vuexStore } from '/config/vuex.js';

class LoginService {
  _auth;

  get auth() {
    if (!this._auth && localStorage.getItem('auth') !== null) {
      this._auth = JSON.parse(localStorage.getItem('auth'));
    }

    return this._auth;
  }

  get isLoggedIn() {
    if (vuexStore.state.core.isLoggedIn === undefined) {
      this._refreshLoginState();
    }

    return vuexStore.state.core.isLoggedIn;
  }

  async login(username, password) {
    var params = new URLSearchParams();
    params.append('grant_type', RawCMS.env.login.grant_type);
    params.append('scope', RawCMS.env.login.scope);
    params.append('client_id', RawCMS.env.login.client_id);
    params.append('client_secret', RawCMS.env.login.client_secret);
    params.append('password', password);
    params.append('username', username);

    return axios.post(`${RawCMS.env.api.baseUrl}/connect/token`, params).then(x => {
      this._auth = x.data;
      localStorage.setItem('auth', JSON.stringify(x.data));
      this._refreshLoginState();
      RawCMS.eventBus.$emit(evtLogin);
    });
  }

  async logout() {
    localStorage.removeItem('auth');
    this._refreshLoginState();
    RawCMS.eventBus.$emit(evtLogout);
    router.push({ name: 'login', params: { return: document.location.href } });
  }

  _refreshLoginState() {
    vuexStore.dispatch('core/isLoggedIn', localStorage.getItem('auth') !== null);
  }
}

export const loginService = new LoginService();
export default loginService;

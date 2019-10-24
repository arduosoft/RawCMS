import { evtLogin } from '../events.js';
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
    if (vuexStore.state.isLoggedIn === undefined) {
      this._refreshLoginState();
    }

    return vuexStore.state.isLoggedIn;
  }

  async login(username, password) {
    return axios
      .post(`${RawCMS.env.api.baseUrl}/api/web-app-login`, {
        username,
        password,
      })
      .then(x => {
        this._auth = x.data;
        localStorage.setItem('auth', JSON.stringify(x.data));
        vuexStore.commit('isLoggedIn', true);
        RawCMS.eventBus.$emit(evtLogin);
      });
  }

  _refreshLoginState() {
    vuexStore.commit('isLoggedIn', localStorage.getItem('auth') !== null);
  }
}

export const loginService = new LoginService();
export default loginService;

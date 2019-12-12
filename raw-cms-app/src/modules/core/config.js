import { vuexStore } from '../../config/vuex.js';
import { userInfoService } from './services/user-info.service.js';

const _configCoreModule = function() {
  vuexStore.registerModule('core', {
    namespaced: true,
    state: {
      isLoggedIn: undefined,
      userInfo: undefined,
      topBarTitle: undefined,
    },
    mutations: {
      isLoggedIn(state, value) {
        state.isLoggedIn = value;
      },
      setUserInfo(state, value) {
        state.userInfo = value;
      },
      setTopBarTitle(state, value) {
        state.topBarTitle = value;
      },
    },
    actions: {
      async isLoggedIn({ commit }, value) {
        commit('isLoggedIn', value);
        const userInfo = await userInfoService.getUserInfo();
        commit('setUserInfo', userInfo);
      },
      async topBarTitle({ commit }, value) {
        commit('setTopBarTitle', value);
      },
    },
  });
};

export const configCoreModule = _configCoreModule;
export default _configCoreModule;

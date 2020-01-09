import { vuexStore } from '../../config/vuex.js';
import { metadataService } from './services/metadata.service.js';
import { userInfoService } from './services/user-info.service.js';

const _configCoreModule = function() {
  vuexStore.registerModule('core', {
    namespaced: true,
    state: {
      isLoggedIn: undefined,
      userInfo: undefined,
      fieldsMetadata: undefined,
      relationMetadata: undefined,
      topBarTitle: undefined,
    },
    mutations: {
      isLoggedIn(state, value) {
        state.isLoggedIn = value;
      },
      setUserInfo(state, value) {
        state.userInfo = value;
      },
      setFieldsMetadata(state, value) {
        state.fieldsMetadata = value;
      },
      setRelationMetadata(state, value) {
        state.relationMetadata = value;
      },
      setTopBarTitle(state, value) {
        state.topBarTitle = value;
      },
    },
    actions: {
      async isLoggedIn({ commit, dispatch }, value) {
        commit('isLoggedIn', value);
        if (!value) {
          return;
        }

        const userInfo = await userInfoService.getUserInfo();
        commit('setUserInfo', userInfo);

        dispatch('updateFieldsMetadata');
      },
      async updateFieldsMetadata({ commit }) {
        const metadata = await metadataService.getFieldsMetadata();
        commit('setFieldsMetadata', metadata);
      },
      async updateTopBarTitle({ commit }, value) {
        commit('setTopBarTitle', value);
      },
      updateRelationMetadata({ commit, state }, value) {
        const newState = { ...state.relationMetadata, ...value };
        commit('setRelationMetadata', newState);
      },
      clearRelationMetadata({ commit }) {
        commit('setRelationMetadata', {});
      },
      async updateTopBarTitle({ commit }, value) {
        commit('setTopBarTitle', value);
      },
    },
  });
};

export const configCoreModule = _configCoreModule;
export default _configCoreModule;

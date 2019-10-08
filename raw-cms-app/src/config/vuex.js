const _vuexStore = new Vuex.Store({
  state: {
    isLoggedIn: undefined,
  },
  mutations: {
    isLoggedIn(state, value) {
      state.isLoggedIn = value;
    },
  },
});

export const vuexStore = _vuexStore;
export default vuexStore;

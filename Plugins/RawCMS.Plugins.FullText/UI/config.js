import { vuexStore } from "/app/config/vuex.js";

const _configCoreModule = {
    namespaced: true,
    name: "fulltext",

    getters: {
        menuItems() {
            return [];
        }
    },

    getRoutes() {
        return [];
    },
    state: {},
    mutations: {},
    actions: {}
};

export const configCoreModule = _configCoreModule;
export default _configCoreModule;
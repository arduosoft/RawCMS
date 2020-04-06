import { vuexStore } from "/app/config/vuex.js";

const _configCoreModule = {
  namespaced: true,
  name: "graphql",

  getters: {
    menuItems() {
      return [{ icon: "mdi-graphql", text: "GraphQL", route: "graphql" }];
    }
  },

  getRoutes() {
    return [
      {
        path: "/graphql",
        name: "graphql",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/graphql/views/graphql-view/graphql-view.js"
          );
          await cmp.default(res, rej);
            },
         meta: {
                i18nLoad: ["modules/graphql"]
            }
      }
    ];
  },
  state: {},
  mutations: {},
  actions: {}
};

export const configCoreModule = _configCoreModule;
export default _configCoreModule;

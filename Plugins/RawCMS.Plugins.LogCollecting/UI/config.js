import { vuexStore } from "/app/config/vuex.js";

const _configCoreModule = {
  namespaced: true,
  name: "logs",

  getters: {
    menuItems() {
      return [
        { icon: "mdi-file-multiple-outline", text: "Logs", route: "logs" }
      ];
    }
  },

  getRoutes() {
    return [
      {
        path: "/logs",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/logs/views/logs-view/logs-view.js"
          );
          await cmp.default(res, rej);
          await cmp.default(res, rej);
        },

        children: [
          {
            path: "/",
            name: "logs",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/logs/views/logs-list-view/logs-list-view.js"
              );
              await cmp.default(res, rej);
            }
          },
          {
            path: ":name",
            name: "logs-search",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/logs/views/logs-search-view/logs-search-view.js"
              );
              await cmp.default(res, rej);
            }
          }
        ]
      }
    ];
  },
  state: {},
  mutations: {},
  actions: {}
};

export const configCoreModule = _configCoreModule;
export default _configCoreModule;

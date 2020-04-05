import { vuexStore } from "/app/config/vuex.js";
import { metadataService } from "/app/modules/core/services/metadata.service.js";
import { userInfoService } from "/app/modules/core/services/user-info.service.js";
import { LoginView } from "/app/modules/core/views/login-view/login-view.js";

const _configCoreModule = {
  namespaced: true,
  name: "core",

  getters: {
    menuItems() {
      return [
        { icon: "mdi-home", text: "Home", route: "home" },
        { icon: "mdi-account", text: "Users", route: "users" },
        { icon: "mdi-cube", text: "Entities", route: "entities" },
        { icon: "mdi-book-open", text: "Collections", route: "collections" },
        { icon: "mdi-circle", text: "Lambdas", route: "lambdas" },
        { icon: "mdi-settings", text: "Configuration", route: "plugins" },
        { icon: "mdi-cogs", text: "Background Jobs", route: "hangfire" },

        { icon: "mdi-file-document-outline", text: "Swagger", extLink: RawCMS.env.api.baseUrl + '/swagger' }
      ];
    }
  },
  getRoutes() {
    return [
      {
        path: "/login",
        name: "login",
        component: async (res, rej) => await LoginView(res, rej),
        meta: {
          requiresAuth: false
        }
      },
      {
        path: "/",
        name: "home",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/home-view/home-view.js"
          );
          await cmp.default(res, rej);
        }
      },
      {
        path: "/entities",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/entities-view/entities-view.js"
          );
          await cmp.default(res, rej);
        },
        children: [
          {
            path: "/",
            name: "entities",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/entities-list-view/entities-list-view.js"
              );
              await cmp.default(res, rej);
            }
          },
          {
            path: ":id",
            name: "entity-details",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/entity-details-view/entity-details-view.js"
              );
              await cmp.default(res, rej);
            }
          }
        ],
        meta: {
            i18nLoad: ["modules/core", "common/formly-material"]
        }
      },
      {
        path: "/collections",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/collections-view/collections-view.js"
          );
          await cmp.default(res, rej);
        },
        children: [
          {
            path: "/",
            name: "collections",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/collections-list-view/collections-list-view.js"
              );
              await cmp.default(res, rej);
            }
          },
          {
            path: ":collName",
            name: "collection-table",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/collection-table-view/collection-table-view.js"
              );
              await cmp.default(res, rej);
            }
          },
          {
            path: ":collName/:id",
            name: "collection-details",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/collection-item-details-view/collection-item-details-view.js"
              );
              await cmp.default(res, rej);
            }
          }
        ],
        meta: {
            i18nLoad: ["modules/core", "common/formly-material"]
        }
      },
      {
        path: "/users",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/users-view/users-view.js"
          );
          await cmp.default(res, rej);
        },
        children: [
          {
            path: "/",
            name: "users",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/users-list-view/users-list-view.js"
              );
              await cmp.default(res, rej);
            }
          },
          {
            path: ":id",
            name: "user-details",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/user-details-view/user-details-view.js"
              );
              await cmp.default(res, rej);
            }
          }
        ]
      },
      {
        path: "/lambdas",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/lambdas-view/lambdas-view.js"
          );
          await cmp.default(res, rej);
        },
        children: [
          {
            path: "/",
            name: "lambdas",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/lambdas-list-view/lambdas-list-view.js"
              );
              await cmp.default(res, rej);
            }
          },
          {
            path: ":id",
            name: "lambda-details",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/lambda-details-view/lambda-details-view.js"
              );
              await cmp.default(res, rej);
            }
          }
        ]
      },
      {
        path: "/configuration",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/configuration-view/configuration-view.js"
          );
          await cmp.default(res, rej);
        },
        children: [
          {
            path: "/",
            name: "plugins",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/configuration-list-view/configuration-list-view.js"
              );
              await cmp.default(res, rej);
            }
          },
          {
            path: ":id",
            name: "configuration-details",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/configuration-details-view/configuration-details-view.js"
              );
              await cmp.default(res, rej);
            }
          }
        ]
      },
      {
        path: "/hangfire",
        name: "hangfire",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/hangfire-view/hangfire-view.js"
          );
          await cmp.default(res, rej);
        }
      },
      {
        path: "/sandbox",
        component: {
          template: `<router-view></router-view>`
        },
        children: [
          {
            path: "/",
            name: "sandbox",
            component: {
              template: `
            <v-container>
              <ul>
                <li><router-link :to="{ name: 'sandbox-formly' }">Formly</router-link></li>
              </ul>
            </v-container>
            `
            }
          },
          {
            path: "formly",
            name: "sandbox-formly",
            component: async (res, rej) => {
              const cmp = await import(
                "/app/modules/core/views/sandbox/formly-test/formly-test.js"
              );
              await cmp.default(res, rej);
            },
            meta: {
                i18nLoad: ["modules/core", "common/formly-material"]
            }
          }
        ]
      },
      {
        path: "/about",
        name: "about",
        component: async (res, rej) => {
          const cmp = await import(
            "/app/modules/core/views/about-view/about-view.js"
          );
          await cmp.default(res, rej);
        }
      }
    ];
  },
  state: {
    isLoggedIn: undefined,
    userInfo: undefined,
    fieldsMetadata: undefined,
    relationMetadata: undefined,
    topBarTitle: undefined
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
    }
  },
  actions: {
    async isLoggedIn({ commit, dispatch }, value) {
      commit("isLoggedIn", value);
      if (!value) {
        return;
      }

      const userInfo = await userInfoService.getUserInfo();
      commit("setUserInfo", userInfo);

      dispatch("updateFieldsMetadata");
    },
    async updateFieldsMetadata({ commit }) {
      const metadata = await metadataService.getFieldsMetadata();
      commit("setFieldsMetadata", metadata);
    },
    async updateTopBarTitle({ commit }, value) {
      commit("setTopBarTitle", value);
    },
    updateRelationMetadata({ commit, state }, value) {
      const newState = { ...state.relationMetadata, ...value };
      commit("setRelationMetadata", newState);
    },
    clearRelationMetadata({ commit }) {
      commit("setRelationMetadata", {});
    }
  }
};

export const configCoreModule = _configCoreModule;
export default _configCoreModule;

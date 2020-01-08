import { i18nHelper } from '../config/i18n.js';
import { loginService } from '../modules/core/services/login.service.js';
import { LoginView } from '../modules/core/views/login-view/login-view.js';
import { optionalChain } from '../utils/object.utils.js';

const _router = new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/login',
      name: 'login',
      component: async (res, rej) => await LoginView(res, rej),
      meta: {
        requiresAuth: false,
      },
    },
    {
      path: '/',
      name: 'home',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/home-view/home-view.js');
        await cmp.default(res, rej);
      },
    },
    {
      path: '/entities',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/entities-view/entities-view.js');
        await cmp.default(res, rej);
      },
      children: [
        {
          path: '/',
          name: 'entities',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/entities-list-view/entities-list-view.js'
            );
            await cmp.default(res, rej);
          },
        },
        {
          path: ':id',
          name: 'entity-details',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/entity-details-view/entity-details-view.js'
            );
            await cmp.default(res, rej);
          },
        },
      ],
      meta: {
        i18nLoad: ['core', 'formly-material'],
      },
    },
    {
      path: '/collections',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/collections-view/collections-view.js');
        await cmp.default(res, rej);
      },
      children: [
        {
          path: '/',
          name: 'collections',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/collections-list-view/collections-list-view.js'
            );
            await cmp.default(res, rej);
          },
        },
        {
          path: ':collName',
          name: 'collection-table',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/collection-table-view/collection-table-view.js'
            );
            await cmp.default(res, rej);
          },
        },
        {
          path: ':collName/:id',
          name: 'collection-details',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/collection-item-details-view/collection-item-details-view.js'
            );
            await cmp.default(res, rej);
          },
        },
      ],
      meta: {
        i18nLoad: ['core', 'formly-material'],
      },
    },
    {
      path: '/users',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/users-view/users-view.js');
        await cmp.default(res, rej);
      },
      children: [
        {
          path: '/',
          name: 'users',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/views/users-list-view/users-list-view.js');
            await cmp.default(res, rej);
          },
        },
        {
          path: ':id',
          name: 'user-details',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/views/user-details-view/user-details-view.js');
            await cmp.default(res, rej);
          },
        },
      ],
    },
    {
      path: '/lambdas',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/lambdas-view/lambdas-view.js');
        await cmp.default(res, rej);
      },
      children: [
        {
          path: '/',
          name: 'lambdas',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/views/lambdas-list-view/lambdas-list-view.js');
            await cmp.default(res, rej);
          },
        },
        {
          path: ':id',
          name: 'lambda-details',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/lambda-details-view/lambda-details-view.js'
            );
            await cmp.default(res, rej);
          },
        },
      ],
    },
    {
      path: '/configuration',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/configuration-view/configuration-view.js');
        await cmp.default(res, rej);
      },
      children: [
        {
          path: '/',
          name: 'plugins',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/configuration-list-view/configuration-list-view.js'
            );
            await cmp.default(res, rej);
          },
        },
        {
          path: ':id',
          name: 'configuration-details',
          component: async (res, rej) => {
            const cmp = await import(
              '/modules/core/views/configuration-details-view/configuration-details-view.js'
            );
            await cmp.default(res, rej);
          },
        },
      ],
    },
    {
      path: '/graphql',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/graphql-view/graphql-view.js');
        await cmp.default(res, rej);
      },
    },
    {
      path: '/sandbox',
      component: {
        template: `<router-view></router-view>`,
      },
      children: [
        {
          path: '/',
          name: 'sandbox',
          component: {
            template: `
            <v-container>
              <ul>
                <li><router-link :to="{ name: 'sandbox-formly' }">Formly</router-link></li>
              </ul>
            </v-container>
            `,
          },
        },
        {
          path: 'formly',
          name: 'sandbox-formly',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/views/sandbox/formly-test/formly-test.js');
            await cmp.default(res, rej);
          },
          meta: {
            i18nLoad: ['core', 'formly-material'],
          },
        },
      ],
    },
    {
      path: '/about',
      name: 'about',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/about-view/about-view.js');
        await cmp.default(res, rej);
      },
    },
  ],
});

// Automatically load i18n messages
_router.beforeEach(async (to, from, next) => {
  if (!optionalChain(() => to.matched)) {
    next();
    return;
  }

  let i18nModulesToLoad = to.matched
    .map(r =>
      optionalChain(() => r.meta.i18nLoad, { fallbackValue: ['core'], replaceLastUndefined: true })
    )
    .reduce((acc, val) => [...acc, ...val], []);
  i18nModulesToLoad = [...new Set(i18nModulesToLoad)];

  for (const mod of i18nModulesToLoad) {
    await i18nHelper.load('en', `/modules/${mod}/assets/i18n/i18n.en.json`);
  }

  next();
});

// Check if user is authenticated
_router.beforeEach((to, from, next) => {
  if (to.matched.some(r => !optionalChain(() => r.meta.requiresAuth, { fallbackValue: true }))) {
    next();
    return;
  }

  if (loginService.isLoggedIn) {
    next();
    return;
  }

  next({
    path: '/login',
    params: { nextUrl: to.fullPath },
  });
});

export const router = _router;

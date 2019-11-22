import { i18nHelper } from '../config/i18n.js';
import { loginService } from '../modules/core/services/login.service.js';
import { Login } from '../modules/core/views/login/login.js';
import { optionalChain } from '../utils/object.utils.js';

const _router = new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/login',
      name: 'login',
      component: async (res, rej) => await Login(res, rej),
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
    },
    {
      path: '/users',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/users/users.js');
        await cmp.default(res, rej);
      },
      children: [
        {
          path: '/',
          name: 'users',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/components/users-list/users-list.js');
            await cmp.default(res, rej);
          },
        },
        {
          path: ':id',
          name: 'user-details',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/components/user-details/user-details.js');
            await cmp.default(res, rej);
          },
        },
      ],
    },
    {
      path: '/lambda',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/lambdas/lambdas.js');
        await cmp.default(res, rej);
      },
      children: [
        {
          path: '/',
          name: 'lambda-list',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/components/lambda-list/lambda-list.js');
            await cmp.default(res, rej);
          },
        },
        {
          path: '/lambda/editor/:id',
          name: 'lambda-editor',
          component: async (res, rej) => {
            const cmp = await import('/modules/core/components/lambda-editor/lambda-editor.js');
            await cmp.default(res, rej);
          },
        },
      ],
    },
  ],
});

// Automatically load i18n messages
_router.beforeEach(async (to, from, next) => {
  await i18nHelper.load('en', '/modules/core/assets/i18n/i18n.en.json');
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

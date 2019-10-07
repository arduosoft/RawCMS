import { i18nHelper } from '../config/i18n.js';

const _router = new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/home.js');
        await cmp.default(res, rej);
      },
    },
    {
      path: '/entities',
      name: 'entities',
      component: async (res, rej) => {
        const cmp = await import('/modules/core/views/entities.js');
        await cmp.default(res, rej);
      },
    },
  ],
});

// Automatically load i18n messages
_router.beforeEach(async (to, from, next) => {
  await i18nHelper.load('en', '/modules/core/assets/i18n/i18n.en.json');
  next();
});
export const router = _router;

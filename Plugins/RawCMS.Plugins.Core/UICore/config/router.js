import { i18nHelper } from "/app/config/i18n.js";
import { loginService } from "/app/modules/core/services/login.service.js";
import { LoginView } from "/app/modules/core/views/login-view/login-view.js";
import { optionalChain } from "/app/utils/object.utils.js";

const _router = new VueRouter({
  mode: "history",
  base: "app"
});

// Automatically load i18n messages
_router.beforeEach(async (to, from, next) => {
  if (!optionalChain(() => to.matched)) {
    next();
    return;
  }

  let i18nModulesToLoad = to.matched
    .map(r =>
      optionalChain(() => r.meta.i18nLoad, {
        fallbackValue: ["modules/core"],
        replaceLastUndefined: true
      })
    )
    .reduce((acc, val) => [...acc, ...val], []);
  i18nModulesToLoad = [...new Set(i18nModulesToLoad)];

    for (const mod of i18nModulesToLoad) {
        try {
            await i18nHelper.load("en", `/app/${mod}/assets/i18n/i18n.en.json`);
        } catch (e) {
            console.log(e);
        }
  }

  next();
});

// Check if user is authenticated
_router.beforeEach((to, from, next) => {
  if (
    to.matched.some(
      r => !optionalChain(() => r.meta.requiresAuth, { fallbackValue: true })
    )
  ) {
    next();
    return;
  }

  if (loginService.isLoggedIn) {
    next();
    return;
  }

  next({
    path: "/login",
    params: { nextUrl: to.fullPath }
  });
});

if (typeof ga != "undefined") {
  // ga('set', 'page', _router.currentRoute.path);
  // ga('send', 'pageview');

  _router.afterEach((to, from) => {
    ga("set", "page", to.path);
    ga("send", "pageview");
  });
}

export const router = _router;

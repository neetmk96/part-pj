import Vue from 'vue'
import Router from 'vue-router'
import { normalizeURL, decode } from 'ufo'
import { interopDefault } from './utils'
import scrollBehavior from './router.scrollBehavior.js'

const _297308f6 = () => interopDefault(import('..\\pages\\create.vue' /* webpackChunkName: "pages/create" */))
const _5be5405a = () => interopDefault(import('..\\pages\\detail.vue' /* webpackChunkName: "pages/detail" */))
const _7b8a2c5b = () => interopDefault(import('..\\pages\\test.vue' /* webpackChunkName: "pages/test" */))
const _eff3365c = () => interopDefault(import('..\\pages\\update.vue' /* webpackChunkName: "pages/update" */))
const _0e911a59 = () => interopDefault(import('..\\pages\\index.vue' /* webpackChunkName: "pages/index" */))

const emptyFn = () => {}

Vue.use(Router)

export const routerOptions = {
  mode: 'history',
  base: '/',
  linkActiveClass: 'nuxt-link-active',
  linkExactActiveClass: 'nuxt-link-exact-active',
  scrollBehavior,

  routes: [{
    path: "/create",
    component: _297308f6,
    name: "create"
  }, {
    path: "/detail",
    component: _5be5405a,
    name: "detail"
  }, {
    path: "/test",
    component: _7b8a2c5b,
    name: "test"
  }, {
    path: "/update",
    component: _eff3365c,
    name: "update"
  }, {
    path: "/",
    component: _0e911a59,
    name: "index"
  }],

  fallback: false
}

export function createRouter (ssrContext, config) {
  const base = (config._app && config._app.basePath) || routerOptions.base
  const router = new Router({ ...routerOptions, base  })

  // TODO: remove in Nuxt 3
  const originalPush = router.push
  router.push = function push (location, onComplete = emptyFn, onAbort) {
    return originalPush.call(this, location, onComplete, onAbort)
  }

  const resolve = router.resolve.bind(router)
  router.resolve = (to, current, append) => {
    if (typeof to === 'string') {
      to = normalizeURL(to)
    }
    return resolve(to, current, append)
  }

  return router
}

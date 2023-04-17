export { default as CCon2 } from '../..\\components\\c-con-2.vue'
export { default as CCon } from '../..\\components\\c-con.vue'
export { default as ConA } from '../..\\components\\con-a.vue'
export { default as ConB } from '../..\\components\\con-b.vue'
export { default as ConC } from '../..\\components\\con-c.vue'
export { default as ConD } from '../..\\components\\con-d.vue'
export { default as NuxtLogo } from '../..\\components\\NuxtLogo.vue'
export { default as TreePart } from '../..\\components\\tree-part.vue'

// nuxt/nuxt.js#8607
function wrapFunctional(options) {
  if (!options || !options.functional) {
    return options
  }

  const propKeys = Array.isArray(options.props) ? options.props : Object.keys(options.props || {})

  return {
    render(h) {
      const attrs = {}
      const props = {}

      for (const key in this.$attrs) {
        if (propKeys.includes(key)) {
          props[key] = this.$attrs[key]
        } else {
          attrs[key] = this.$attrs[key]
        }
      }

      return h(options, {
        on: this.$listeners,
        attrs,
        props,
        scopedSlots: this.$scopedSlots,
      }, this.$slots.default)
    }
  }
}

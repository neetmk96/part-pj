// components/ComponentA.vue
<template>
  <div>
    <p>Component C</p>
    <input v-model="inputValue" @input="setInputValue" />
  </div>
</template>

<script>

import { mapState, mapMutations } from 'vuex'

export default {
  data() {
    return {
     
    }
  },
  computed: {
    ...mapState({
      inputValue: state => state.inputValue
    }),
    inputValue: {
    get() {
      return this.$store.state.inputValue
    },
    set(value) {
      this.$store.commit('updateInputValue', value)
    }
  }
  },
  methods: {
    ...mapMutations([
      'updateInputValue'
    ]),
    setInputValue(event) {
      this.updateInputValue(event.target.value)
    }
  }
}
</script>

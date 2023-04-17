// components/ComponentB.vue
<template>
  <div>
    <p>Component D :  {{ inputValue }} </p>
   
  </div>
</template>

<script>

import { mapState } from 'vuex'

export default {
  computed: {
    ...mapState({
      inputValue: state => state.inputValue
    })
  }
}
</script>

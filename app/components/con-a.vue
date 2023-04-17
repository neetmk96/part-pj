<template>
  <div>
    <input v-model="data" @input="emitData">
  
  </div>
</template>

<script>
export default {
  data(){
   return {
    data:''
   }
  },
  methods: {
    emitData() {
      console.log(this.data)
      this.$emit('send-data', this.data)
    }
  }
}
</script>
  
<template>
  <div>
    <h2>Component Con</h2>
    <input type="text" v-model="data" @input="sendDataToParent" />
  </div>
</template>

<script>
export default {
  data() {
    return {
      data: '',
    };
  },
  methods: {
    sendDataToParent() {
      this.$emit('sendData', this.data);
    },
  },
};
</script>

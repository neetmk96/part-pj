<!-- ChildComponent.vue -->
<template>
    <div>
      <h3>Component con</h3>
      <p>Dữ liệu từ cha : {{ message }}</p>
    </div>
  </template>
  
  <script>
  
  export default {
    name: 'ChildComponent',
    props: {
      message: {
        type: String,
        required: true
      }
    }
  }
  </script>
<template>
    <div>
      <p>Conponent B nhận dữ liệu từ A : {{ receivedData }}</p>
    </div>
  </template>
  
  <script>
  export default {
    props: ['receivedData']
  }
  </script>
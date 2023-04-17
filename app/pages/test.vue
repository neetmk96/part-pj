<template>
 <DefaultLayout>
  <div>
    <h1>1. Component Cha : nhận data từ con -> cha</h1>
    <p>Dữ liệu từ Component Con  (qua emit): {{ dataNhan }}</p>
    <component-con @sendData="getDataFromChild"></component-con>
     
    <hr>
    <h1>2. Truyền data từ cha -> con</h1>
    <input v-model="messCha">
    <Componentconver2 :message="messCha"  ></Componentconver2>
    <hr>
    <h1>3. truyền data giữa con A -> B</h1>
    <conA  @send-data="handleData"></conA>
    <conB :received-data="receivedData"></conB>
    <hr>
    <h1>4. Sử dụng VueX để quản lý state</h1>
    <conC></conC>
    <conD></conD>
  </div>
 </DefaultLayout>

</template>

<script>

import DefaultLayout from '~/layouts/DefaultLayout.vue';
import ComponentCon from '@/Components/c-con.vue';
import Componentconver2 from '@/Components/c-con-2.vue';
import conA from '@/Components/con-a.vue';
import conB from '@/Components/con-b.vue';
import conC from '@/Components/con-c.vue';
import conD from '@/Components/con-d.vue';
export default {
  components: {
    DefaultLayout,
    ComponentCon,
    Componentconver2,
    conA,
    conB,
    conC,
    conD
  },
  data() {
    return {
      dataNhan: '',
      messCha:'',
      receivedData: ''
    };
  },
  methods: {
    getDataFromChild(d) {
      this.dataNhan = d;
    },
    handleData(data) {
      this.receivedData = data
    }
  },
};
</script>

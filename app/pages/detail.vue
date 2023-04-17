<template>
  <default-layout>
    <div>
      <a-form-model :model="form" :label-col="labelCol" :wrapper-col="wrapperCol">
        <a-form-model-item label="Mã bộ phận">
          <a-input v-model="form.code" disabled/>
        </a-form-model-item>
        <a-form-model-item label="Tên bộ phận">
          <a-input v-model="form.name" disabled />
        </a-form-model-item>
        <a-form-model-item label="Mô tả">
          <a-input v-model="form.description" disabled />
        </a-form-model-item>

        <a-form-model-item label="Bộ phận cha">
          <a-tree-select v-model="form.parentCode" style="width: 100%"
            :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }" :tree-data="treeData"
            placeholder="" :tree-default-expand-all=false allow-clear disabled> 
          </a-tree-select>
        </a-form-model-item>

        <a-form-model-item :wrapper-col="{ span: 14, offset: 4 }">
          <a-button type="danger" style="margin-left: 10px;">
            <router-link to="/"> Quay lại</router-link>

          </a-button>
        </a-form-model-item>
      </a-form-model>
    </div>

    <div>
  </div>
  </default-layout>
</template>
<script>
import DefaultLayout from '~/layouts/DefaultLayout.vue';
import axios from 'axios';
export default {

  components: {
    'default-layout': DefaultLayout
  },
  data() {
    return {
      labelCol: { span: 4 },
      wrapperCol: { span: 14 },
      visible :false,
      responMessage:'',
      treeData: [],
      form: {
        id:'',
        code: '',
        name: '',
        description: '',
        parentCode: null,
        type: [],
        resource: '',
        desc: '',
      },
    };
  },
  mounted() {
    this.fetchTreePartsById();
    this.getInfoPart();
  },
  methods: {
    handleOk(e) {
      console.log(e);
      this.visible = false;
    },
    // api
    fetchTreePartsById() {
      let url = 'https://localhost:7003/api/PartsInfo/GetTreePart?id=' + `${this.$route.query.id}`;
      axios.get(url)
        .then(response => {
          this.treeData = response?.data?.data
        })
        .catch(error => {
          console.error(error);
        });
    },
    getInfoPart() {
      let url = 'https://localhost:7003/api/PartsInfo/GetPartById?id=' + `${this.$route.query.id}`;
      axios.get(url)
        .then(response => {
          console.log(response)
          if (response.data?.count > 0) this.form = response.data?.data
          else alert('Không tìm thấy id đơn vị')

        })
        .catch(error => {
          console.error(error);
        });
    }

  },
};
</script>

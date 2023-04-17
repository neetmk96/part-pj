<template>
  <default-layout>
    <div>
      <a-form-model ref="ruleForm" :model="form" :rules="rules" :label-col="labelCol" :wrapper-col="wrapperCol">
        <a-form-model-item label="Mã bộ phận" prop="code" has-feedback>
          <a-input v-model="form.code" type="text" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item has-feedback label="Tên bộ phận" prop="name">
          <a-input v-model="form.name" />
        </a-form-model-item>
        <a-form-model-item label="Mô tả" prop="description">
          <a-input v-model="form.description" />
        </a-form-model-item>
        <a-form-model-item label="Số tiền" prop="money">
          <a-input type="number" v-model="form.money" />
        </a-form-model-item>
        <a-form-model-item label="Bộ phận cha">
          <a-tree-select v-model="form.parentCode" style="width: 100%"
            :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }" :tree-data="treeData"
            placeholder="Chọn bộ phận cha nếu có" :tree-default-expand-all=false allow-clear>
          </a-tree-select>
        </a-form-model-item>

        <a-form-model-item :wrapper-col="{ span: 14, offset: 4 }">

          <a-button type="danger" style="margin-right: 10px;">
            <router-link to="/"> Hủy</router-link>
          </a-button>
          <a-button type="primary" @click="submitForm('ruleForm')">
            Xác nhận
          </a-button>
        </a-form-model-item>
      </a-form-model>
    </div>


  </default-layout>
</template>
<script>
import DefaultLayout from '~/layouts/DefaultLayout.vue';
import axios from 'axios';
import Swal from 'sweetalert2';
export default {

  components: {
    'default-layout': DefaultLayout
  },
  data() {
    return {
      responMessage: '',
      labelCol: { span: 4 },
      wrapperCol: { span: 14 },
      treeData: [],
      form: {
        code: '',
        name: '',
        description: '',
        parentCode: null,
        money:0
      },
      rules: {
        code: [
          { required: true, message: 'Nhập vào mã đơn vị', trigger: 'change' },
          { pattern: /^([a-zA-Z0-9-]){1,20}$/, message: 'Mã đơn vị chỉ bao gồm số chữ và dấu - , có độ dài từ 1 - 20 ký tự', trigger: 'change' }
        ],
        name: [
          { required: true, message: 'Nhập vào tên đơn vị', trigger: 'change' },
          { pattern: /^([a-zA-ZÀ-ỹ0-9- ]){1,100}$/, message: 'Tên đơn vị chỉ bao gồm số chữ và dấu - , có độ dài từ 1 - 100 ký tự', trigger: 'change' }
        ],
        description: [
          { pattern: /^([a-zA-ZÀ-ỹ0-9- ]){1,100}$/, message: 'Mô tả chỉ bao gồm số chữ và dấu - , có độ dài từ 1 - 100 ký tự', trigger: 'change' }
        ],
        money:[
        { pattern: /^(:?(?=[1])(10{0,6})|(?=[^0])(\d{1,6})|0)$/, message: 'Số tiền nằm trong khoảng 0 - 1.000.000', trigger: 'change' }
        ]
      }
    };
  },
  mounted() {
    this.fetchTreeParts();
  },
  beforeUpdate() {
   
  },
  updated() {
   
  }
  ,
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          this.createNewPart();
        } else {
          console.log('error submit!!');
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
    showAlert(message,icon) {
      Swal.fire({
        text:message,
        icon:icon
      });
    },
    // api
    async fetchTreeParts() {
      axios.get('https://localhost:7003/api/PartsInfo/GetTreePart')
        .then(response => {
          console.log(response?.data?.data)
          this.treeData = response?.data?.data
        })
        .catch(error => {
          console.error(error);
        });
    },
    createNewPart() {
      axios.post('https://localhost:7003/api/PartsInfo/CreateNewPart', this.form)
        .then(response => {
          console.log(response)
          this.responMessage = response.data.message
          this.showAlert(response.data.message, response.data.isSuccessful ?'success':'error')
        })
        .catch(error => {
          console.log(error)
          this.responMessage = error.message
          this.showAlert(error.message, 'error')
        });
    }
  },
};
</script>

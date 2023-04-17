<template>
  <default-layout>
    <a-row type="flex" justify="end" style="margin-bottom: 10px;">
      <div>
        <a-button>
          <router-link to="/create">
            <a-icon type="plus-circle" theme="filled" /> Thêm mới
          </router-link>
        </a-button>
      </div>

    </a-row>
    <a-table :columns="columns" :row-key="record => record.id" :data-source="parts" :pagination="pagination" @change="handleTableChange">
      <template #customIndex="{ text, record, index }">
        {{ index + 1 }}
      </template>
      <span slot="code" slot-scope="text, record">
        <nuxt-link :to="{ path: '/detail?id=' + record.id }">{{ record.code }}</nuxt-link>
      </span>
      <span slot="action" slot-scope="text, record">
        <nuxt-link :to="{ path: '/update?id=' + record.id }"><a-icon type="edit" /></nuxt-link>

        <a-divider type="vertical" />

        <a-icon type="delete" @click="showDeleteConfirm(record.id)" style="color: red;" />


      </span>
    </a-table>
  </default-layout>
</template>
<script>

import DefaultLayout from '~/layouts/DefaultLayout.vue';
import axios from 'axios'
import { defineComponent } from 'vue';
import Swal from 'sweetalert2';



export default defineComponent({
  name: 'IndexPage',
  components: {
    'default-layout': DefaultLayout,
  },
  data() {
    return {
      parts: [],
      columns: [
        {
          title: "#",
          dataIndex: "customIndex",
          width: "50px",
          customRender: (text, record, index) => index + 1,
        }, {
          title: 'Mã bộ phận',
          dataIndex: 'code',
          key: 'code',
          responsive: ['md'],
          scopedSlots: { customRender: 'code' }
        }, {
          title: 'Tên bộ phận',
          dataIndex: 'name',
          key: 'name'
        },
        {
          title: 'Mô tả',
          dataIndex: 'description',
          key: 'description',
        }
        , {
          title: 'Ngày tạo',
          dataIndex: 'createAt',
          key: 'createAt',
          responsive: ['md'],
        },
        {
          title: 'Bộ phận cha',
          dataIndex: 'parentName',
          key: 'parentName'

        },
        {
          title: 'Action',
          key: 'action',
          scopedSlots: { customRender: 'action' },
        }],
     pagination: {
      current: 1, // current page number
      pageSize: 5, // number of items per page
      total: 0, // total number of items
      showSizeChanger: true, // show option to change pageSize
      showTotal: (total, range) => `${range[0]}-${range[1]} of ${total} items`, // display total items
    }
    }
  },
  beforeMount(){


  }
  ,
  mounted() {

    this.fetchDataPart();
  }
  ,
  methods: {
    async fetchDataPart() {
      const { current, pageSize } = this.pagination;
      axios.get(`https://localhost:7003/api/PartsInfo/GetAllPart?page=${current}&pageSize=${pageSize}`)
        .then(response => {
          console.log(response)
          this.pagination.total = response?.data?.count
          this.parts = response?.data?.data
        })
        .catch(error => {
          console.error(error);
        });
    },
    handleTableChange(pagination){
      this.pagination = pagination;
      this.fetchDataPart();
    },
    //
    showDeleteConfirm(idPart) {
      this.$confirm({
        title: 'Bạn chắc chắn muốn xóa đơn vị này?',
        content: 'Vui lòng kiểm tra kỹ lại thông tin',
        okText: 'Yes',
        okType: 'danger',
        cancelText: 'No',
        onOk: () => {
          axios.put('https://localhost:7003/api/PartsInfo/DeletePart?id=' + idPart)
            .then(response => {
              console.log(response);
              this.showAlert(response.data.message, response.data.isSuccessful ? 'success' : 'error')
              this.fetchDataPart();

            })
            .catch(error => {
              console.error(error);
            });
        },
        onCancel() {
        },
      });
    },
    showAlert(message, icon) {
      Swal.fire({
        text: message,
        icon: icon
      });
    },
  }
});


</script>

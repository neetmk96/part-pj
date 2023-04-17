<template>
    <a-form-model-item label="Bộ phận cha">
        <a-tree-select v-model="form.parentCode" style="width: 100%"
            :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }" :tree-data="treeData"
            placeholder="Chọn bộ phận cha nếu có" :tree-default-expand-all=false allow-clear>
        </a-tree-select>
    </a-form-model-item>
</template>
  
<script>
export default {
    name: 'MyComponent',
    props: {
        title: {
            type: String,
            required: true
        },
        content: {
            type: String,
            required: true
        }
    }
}
</script>
  
<style scoped>
.my-component {
    border: 1px solid #ccc;
    padding: 10px;
}
</style>
  
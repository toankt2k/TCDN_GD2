<template>
  <div class="m-dropdown-button" :class="this.typeClass">
    <div class="content">{{this.text}}</div>
    <div class="icon"></div>
  </div>
</template>

<script>

export default {
    name:"MDropdownButton",
    components:{},
    /**
    * Mô tả : khởi tạo các props nhận từ component cha
    * @param
    * @return
    * Created by: Nguyễn Đức Toán - MF1095
    * Created date: 11:49 05/04/2022
    */
    props:{
        //typeClass xác định xem button này thuộc loại nào: primary / default
        typeClass:{type:String,default:''},
        //text đặt nội dung cho button
        text:{type:String,default:''}
    }
}
</script>

<style scoped>
@import url('@/css/components/dropdown-button.css');
</style>
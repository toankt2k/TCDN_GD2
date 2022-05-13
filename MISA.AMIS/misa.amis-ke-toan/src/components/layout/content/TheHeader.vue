<template>
  <div class="m-header">
    <div class="m-left">
      <div class="toggle-header"></div>
      <div class="branch-select">
        <div class="branch-name">Công ty cổ phần Thiên Nhiên</div>
        <div class="toggle-select"></div>
      </div>
      <!-- <div class="tool-tip">
        <div class="current-db" title="DC-01">
          <div class="current-db-status"></div>
          <div class="text">DC-01</div>
        </div>
      </div> -->
    </div>
    <div class="m-right">
      <!-- <div class="download-process">
        <div class="icon"></div>
      </div>
      <div class="quick-search">
        <MInput 
          :placeholder="'Nhập từ khóa tìm kiếm'"
          :classIcon="'search'"
        />
      </div>
      <div class="menu-header"><div class="icon"></div></div>
      <div class="setting"><div class="icon"></div></div>
      <div class="chat"><div class="icon"></div></div> -->
      <!-- <div class="question">
        <div class="icon"></div>
      </div> -->
      <div class="alert"><div class="icon"></div></div>
      <div class="personal"><div class="branch-select">
        <img class="person-image" />
        <div class="person-name">Nguyễn Đức Toán </div>
        <div class="person-setting"></div>
      </div></div>
    </div>
  </div>
</template>

<script>
// import MInput from '@/components/input/Input.vue'

export default {
  name: "TheHeader",
  components:{
    // MInput,
  }
};
</script>


<style scoped>
@import url("@/css/components/header.css");
</style>
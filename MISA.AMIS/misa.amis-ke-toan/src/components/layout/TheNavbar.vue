<template>
  <div class="m-navbar">
    <div class="logo-container">
      <div class="toggle"></div>
      <a href="/employee">
        <div class="logo-app"></div>
      </a>
    </div>
    <div class="menu-container">
      <div class="navbar-item" v-for="(item, index) in data" :key="index">
        <router-link :to="item.link" exact class="item-content">
          <div class="logo-link" :class="item.classLogoLink"></div>
          <div class="text-link">{{ item.text}}</div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  /**
   * Mô tả : Khởi tạo các thuộc tính cho component trong navbar
   * @param
   * @return
   * Created by: Nguyễn Đức Toán - MF1095
   * Created date: 10:40 04/04/2022
   */
  props: {},
  /**
   * Mô tả : Khởi tạo các dataF cho component trong navbar
   * @param
   * @return
   * Created by: Nguyễn Đức Toán - MF1095
   * Created date: 10:40 04/04/2022
   */
  data() {
    return {
      /**
     * Mô tả : data cho các item trong menu
     *          this.menuDât lấy từ biến global khai báo trong main.js
     *       ** data có cấu trúc ví dụ : {
                                    classLogoLink: "buy",
                                    text: "Mua hàng",
                                },
     *       ** classLogoLink: thêm class html cho logo link
     *       ** text: xác định nội dung text của text link
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 10:40 04/04/2022
     */
      data: this.dataStorage.menuData,
    };
  },
  mounted() {
    // console.log(this.$router.options.routes);
  },
};
</script>

<style scoped>
@import url("@/css/components/navbar.css");
</style>
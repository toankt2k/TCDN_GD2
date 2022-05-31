<template>
  <div class="m-container">
    <div class="header">
      <div class="header-content">
        <div class="title">Nhân viên</div>
        <div class="header-button">
          <MButton
            :buttonType="'primary'"
            :text="'Thêm mới nhân viên'"
            @click="addEmployeeButton"
          />
        </div>
      </div>
      <!-- <BreadCrump /> -->
    </div>

    <div class="table">
      <MTable ref="table" @addToast="addToast"/>
    </div>
    <div class="m-toast-box">
      <ToastMessage
        v-for="(val, ind) in listToastMessage"
        :key="ind"
        :type="val.type"
        :message="val.message"
      />
    </div>
  </div>
</template>

<script>
// import MDropdownButton from "@/components/button/DropdownButton.vue";
// import BreadCrump from "@/components/base/BreadCrump.vue";
import MTable from "@/components/table/TheTable.vue";
import ToastMessage from "@/components/base/BaseToastMessage.vue";
import MButton from "@/components/base/button/BaseButton.vue";
export default {
  name: "TheEmployee",
  components: {
    // MDropdownButton,
    // BreadCrump,
    MTable,
    ToastMessage,
    MButton,
  },
  data() {
    return {
      /**
       * Mô tả : danh sách các toastmessage đang được hiện
       * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
       */
      listToastMessage: [],
    };
  },
  methods: {
    /**
     * Mô tả : nhấn nút thêm nhân viên, mở form thêm nhân viên
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    addEmployeeButton() {
      this.$refs.table.openForm(true);
    },
    /**
     * Mô tả : thêm 1 thông báo vào toastmess sau 4s thì xóa
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    addToast(toast) {
      try {
        this.listToastMessage.push(toast);
        setTimeout(() => {
          this.listToastMessage.shift();
        }, 4500);
      } catch (error) {
        console.log(error);
      }
    },
    keyDownListenner(e) {
      if (e.keyCode == 49 && e.ctrlKey == true) {
        e.preventDefault();
      }
    },
    keyUpListenner() {
      // console.log(e);
    },
    /**
     * Mô tả : phím tắt thêm nhân viên
     * Created by: Nguyễn Đức Toán - MF1095 (23/04/2022)
     */
    openAddForm(e) {
      let me = this;
      try {
        if (e.keyCode == 49 && e.ctrlKey == true) {
          e.preventDefault();
          me.addEmployeeButton(true);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  /**
  * Mô tả : thêm sự kiện cho các phím tắt
  * Created by: Nguyễn Đức Toán - MF1095 (25/04/2022)
  */
  mounted() {
    // sự kiện khi kết thúc nhấn phím
    window.addEventListener("keyup", this.openAddForm);
    //sự kiệ khi nút đcược nhấn
    window.addEventListener("keydown", this.keyDownListenner);
  },
  /**
  * Mô tả : xóa sự kiện khi component unmount
  * Created by: Nguyễn Đức Toán - MF1095 (25/04/2022)
  */
  unmounted() {
    // sự kiện khi kết thúc nhấn phím
    window.removeEventListener("keyup", this.openAddForm);
    //sự kiệ khi nút đcược nhấn
    window.removeEventListener("keydown", this.keyDownListenner);
  },
};
</script>

<style scoped>
@import url("@/css/pages/employee.css");
.m-toast-box {
  position: fixed;
  top: 24px;
  left: 40%;
  z-index: 10;
}
</style>
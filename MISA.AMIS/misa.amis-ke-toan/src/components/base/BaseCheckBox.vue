<template>
  <div class="m-checkbox">
    <label :for="`check${rowId}`" :id="id">
      <input
        ref="row"
        type="checkbox"
        :id="`check${rowId}`"
        :checked="modelValue"
        @change="getCheck($event)"
      />
      <!-- <div class="text">là khách</div> -->
      <div class="checkmark"></div>
    </label>
  </div>
</template>

<script>
export default {
  name: "MCheckBox",
  components: {},
  props: {
    /**
     * Mô tả : rowId - xác định id cho từng checkbox nhận được từ component cha
     * Created by: Nguyễn Đức Toán - MF1095 (07/04/2022)
     */
    rowId: {
      type: String,
      default: "",
    },
    /**
     * Mô tả : modelValue - giá trị true/falss xác định xem checkbox này có được mặc định check hay không
     * Created by: Nguyễn Đức Toán - MF1095 (07/04/2022)
     */
    modelValue: {
      type: Boolean,
      default: false,
    },
    /**
     * Mô tả : id là giá trị của đối tượng sử dụng check box
     * ******* nếu không truyền thì checkbox không có đối tượng nào sử dụng
     * Created by: Nguyễn Đức Toán - MF1095 (07/04/2022)
     */ 
    id: {
      type: String,
      default: "",
    },
  },
  /**
   * Mô tả : Khai báo và khởi tạo data ửu dụng bên trong component
   * @param
   * @return
   * Created by: Nguyễn Đức Toán - MF1095
   * Created date: 11:38 07/04/2022
   */
  data() {
    return {
      /**
       * Mô tả : Set checkbox được check hay không true/false
       * Created by: Nguyễn Đức Toán - MF1095 (07/04/2022)
       */
      check: this.defaultCheck,
    };
  },
  methods: {
    /**
     * Mô tả : Gọi để set check hoặc không check cho check box
     * @param {*} isCheck true/false
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 11:46 07/04/2022
     */
    setCheck(isCheck) {
      try {
          this.check = isCheck;
          this.$emit("update:modelValue", isCheck);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : dùng để gửi dữ liệu khi checkbox được check hoặc hủy check
     * ******* dữ liệu được emit lên v-model của component cha, 
     * ******* nếu checkbox được check thì gủi lên id của đối tượng ngược lại thì không
     * Created by: Nguyễn Đức Toán - MF1095 (07/04/2022)
     */
    getCheck(event) {
      this.check = event.target.checked;
      this.$emit("update:modelValue", this.check);
    },
  },
  watch: {
      /**
      * Mô tả : bắt sự kiện data check thay đổi: set check lại cho checkbox và emit giá trị lên component cha
      * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
      */
    //   check(){
    //     try {
    //         this.$emit("update:modelValue", this.check);
    //         this.setCheck(this.check)
    //     } catch (error) {
    //         console.log(error);
    //     }
    //   }
  },
};
</script>

<style scoped>
@import url("@/css/components/checkbox.css");
</style>
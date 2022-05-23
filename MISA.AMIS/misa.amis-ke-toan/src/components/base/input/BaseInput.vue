<template>
  <div class="m-input">
    <div class="input-area">
      <input
        ref="input"
        :placeholder="placeholder"
        :class="{ icon: classIcon }"
        :value="modelValue"
        @input="getValue($event)"
        :style="{'text-align': align}"
        :maxlength="maxlength"
        @mousemove="stopDraggable"
        :readonly="readonly"
      />
      <div class="input-icon" :class="classIcon" v-if="classIcon"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MInput",
  component: {},
  /**
   * Mô tả : khởi tạo các thuộc tinnsh dùng trong component input
   * @param
   * @return
   * Created by: Nguyễn Đức Toán - MF1095
   * Created date: 10:18 05/04/2022
   */
  props: {
    /**
     * placeholder của thẻ input được truyền từ component cha
     */
    placeholder: { type: String, default: "" },
    /**
     * class icon nhận từ component cha để xác định input có icon và icon là loại nào
     */
    classIcon: { type: String, default: "" },
    /**
     * Mô tả : modelValue binding 2 chiều cho thẻ input
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    modelValue: {
      type: String,
      default: "",
    },
    //trường dữ liệu chỉ  đc đọc
    readonly: {
      type: Boolean,
      default: false,
    },
    /**
    * Mô tả : kiểu nhập mặc định cho input
    * Created by: Nguyễn Đức Toán - MF1095 (11/04/2022)
    */
    type:{
      type:String,
      default:'text'
    },
    /**
    * Mô tả : căn lề cho chữ trong input
    * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
    */
    align:{
      type:String,
      default:'left'
    },
    /**
    * Mô tả : giới hạn số ký tự cho ô input input
    * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
    */
    maxlength:{
      type:Number,
      default:255
    }
  },
  /**
   * Mô tả : khởi tạo các data dùng trong component input
   * @param
   * @return
   * Created by: Nguyễn Đức Toán - MF1095
   * Created date: 10:19 05/04/2022
   */
  data() {
    return {
      /**
       * biến xác định xem input có được focus hay không
       */
      isFocus: false,
      /**
       * biến xác định xem thẻ input có được nhập đúng dữ liệu hay không
       */
      isError: false,
    };
  },
  /**
   * Mô tả : khởi tạo, khai báo các method dùng trong component input
   * @param
   * @return
   * Created by: Nguyễn Đức Toán - MF1095
   * Created date: 10:23 05/04/2022
   */
  methods: {
    /**
     * Mô tả : set focus cho input this.focus = true thì set focus và ngược lại
     * @param {*} focus true/false xác định input có được focus không
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 10:26 05/04/2022
     */
    setFocus(focus) {
      try {
        this.focus = focus;
        if (this.focus) {
          this.$refs.input.focus();
          this.focus = false;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
    * Mô tả : xóa focus error khi input
    * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
    */
    removeError(){
      try {
        this.$refs.input.style = '';
        this.$refs.input.title = '';
      } catch (error) {
        console.log(error);
      }
    },
    /**
    * Mô tả : emit dữ liệu nhập vào lên form
    * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
    */
    getValue(event) {
      try {
        this.$emit("update:modelValue", event.target.value);
        this.removeError();
      } catch (error) {
        console.log(error);
      }
    },
    /**
    * Mô tả : setfocus cho input lỗi
    * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
    */
    setErrorFocus(title){
      try {
        this.$refs.input.style.border = '1px solid #FF4747';
        this.$refs.input.title = title;
        this.setFocus(true);
      } catch (error) {
        console.log(error);
      }
    },
    /**
    * Mô tả : setfocus cho input lỗi
    * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
    */
    stopDraggable(e){
      try {
        e.stopPropagation();
      } catch (error) {
        console.log(error);
      }
    }
  },
};
</script>

<style>
@import url("@/css/components/input.css");

</style>
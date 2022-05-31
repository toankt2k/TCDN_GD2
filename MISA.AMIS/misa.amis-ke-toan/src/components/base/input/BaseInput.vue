<template>
  <div class="m-input">
    <div class="input-area">
      <input
        ref="input"
        :placeholder="placeholder"
        :class="[align, { icon: classIcon }]"
        @input="getValue($event)"
        :maxlength="maxlength"
        :readonly="readonly"
        :disabled="disabled"
      />
      <div class="input-icon" :class="classIcon" v-if="classIcon"></div>
    </div>
  </div>
</template>

<script>
import format from "@/js/lib/formatContent.js";
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
      type: [String, Number],
      default: "",
    },
    //trường dữ liệu chỉ  đc đọc
    readonly: {
      type: Boolean,
      default: false,
    },
    //trường dữ liệu chỉ  đc đọc
    disabled: {
      type: Boolean,
      default: false,
    },
    /**
     * Mô tả : kiểu nhập mặc định cho input
     * Created by: Nguyễn Đức Toán - MF1095 (11/04/2022)
     */
    type: {
      type: String,
      default: "text",
    },
    /**
     * Mô tả : căn lề cho chữ trong input
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    align: {
      type: String,
      default: "left",
    },
    /**
     * Mô tả : giới hạn số ký tự cho ô input input
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    maxlength: {
      type: Number,
      default: 255,
    },
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
      // xác định giá trị cũ
      oldVal: "",
      inputed: false,
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
        this.$nextTick(() => {
          if (focus) {
            this.$refs.input.focus();
          }
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : xóa focus error khi input
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    removeError() {
      try {
        this.$refs.input.style = "";
        this.$refs.input.title = "";
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
        this.inputed = true;
        switch (this.type) {
          case "number":
            this.handleNumber(event);
            break;
          case "currency":
            this.handleCurrency1(event);
            break;
          default:
            this.$emit("update:modelValue", event.target.value);
            break;
        }
        this.removeError();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : setfocus cho input lỗi
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    setErrorFocus(title) {
      try {
        this.$refs.input.style.border = "1px solid #FF4747";
        this.$refs.input.title = title;
        this.setFocus(true);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : format sang dạng số
     * Created by: Nguyễn Đức Toán - MF1095 (28/05/2022)
     */
    formatNumber(value) {
      try {
        if (!value) return 0;
        let result = Intl.NumberFormat("vi-VN").format(value);
        return result;
      } catch (error) {
        console.log(error);
        return 0;
      }
    },
    onFocus(event) {
      try {
        if (this.type != "number" && this.type != "currency") {
          event.target.value = this.modelValue;
        }
      } catch (error) {
        console.log(error);
      }
    },
    //Xử lý khi type input là số
    handleNumber(event) {
      let value = event.target.value;
      // this.removeClass("error");
      //Xóa bỏ định dạng số
      value = value.toString().replaceAll(".", "");
      //Kiểm tra ký tự nhập vào đúng định dạng chưa
      var ok = format.interger(value);
      if (ok) {
        try {
          //Chuyển từ string sang dạng số nguyên
          var numberFormat = value == "" ? "" : parseInt(value);
          //Nếu số bằng null thì hiển thị chuỗi rỗng không thi hiển thị số
          event.target.value = numberFormat
            ? this.formatNumber(numberFormat)
            : "";

          //Gán biến giá trị hiện tại
          this.oldVal = event.target.value;

          this.$emit("update:modelValue", numberFormat ? numberFormat : null);
        } catch (e) {
          this.$emit("update:modelValue", null);
        }
      } else {
        //Nếu validate lỗi lấy giá trị cũ
        event.target.value = this.oldVal;
      }
    },
    //Xử lý dạng tiền tệ
    handleCurrency(event) {
      let value = event.target.value;
      let temp = `${value}`;
      //Xóa bỏ định dạng số
      value = value.toString().replaceAll(".", "");
      //Kiểm tra ký tự nhập vào đúng định dạng chưa
      var ok = format.numeric(value);
      if (ok) {
        // value = parseFloat(value)
        //Tách chuỗi chưa dấu ,
        var number = value.split(",");
        //Chuyển sang dạng số phần nguyên
        var numberInteger =
          number[0] !== "" ? this.formatNumber(parseInt(number[0])) : 0;

        //Không có phần dư
        if (number.length == 1) {
          temp = `${numberInteger}`;
        }
        //Chỉ nhập dấu ,
        if (number.length > 1 && number[0] === "") {
          temp = `${numberInteger},`;
        }
        //Nhập dấu , không nhập phần dư
        if (number.length > 1 && number[1] !== "") {
          temp = `${numberInteger},${number[1]}`;
        }
        // if (!temp.contains(",")) {
        //   temp = temp + "," + 0;
        // }
        event.target.value = temp;
        this.oldVal = temp;
        //Chuyển sang biến kiểu float
        let numberFormat = temp.toString().replaceAll(".", "");
        numberFormat = numberFormat.toString().replaceAll(",", ".");
        numberFormat = parseFloat(numberFormat);
        this.$emit("update:modelValue", numberFormat);
      } else {
        event.target.value = this.oldVal;
      }
    },
    //Xử lý dạng tiền tệ
    handleCurrency1(event) {
      let value = event.target.value;
      let temp = `${value}`;
      //Xóa bỏ định dạng số
      value = value.toString().replaceAll(".", "");
      //Kiểm tra ký tự nhập vào đúng định dạng chưa
      var ok = format.numeric(value);
      if (ok) {
        // đổi dấu , thành .
        // value = value.toString().replaceAll(",", ".");
        var number = value.split(",");
        //Chuyển sang dạng số phần nguyên
        var numberInteger =
          number[0] !== "" ? this.formatNumber(parseInt(number[0])) : 0;
        temp = numberInteger;
        if (number.length > 1) {
          temp = `${numberInteger},${number[1]}`;
        }
        event.target.value = temp;
        this.oldVal = temp;
        //Chuyển sang biến kiểu float
        let numberFormat = temp.toString().replaceAll(".", "");
        numberFormat = numberFormat.toString().replaceAll(",", ".");
        numberFormat = parseFloat(numberFormat);
        this.$emit("update:modelValue", numberFormat);
      } else {
        event.target.value = this.oldVal;
      }
    },
  },
  watch: {
    modelValue(newVal) {
      if (this.type == "currency" || this.type == "number") {
        if (!this.inputed && this.type == "currency")
          this.$refs.input.value = format.currencyFormatDE(newVal);
        if (!this.inputed && this.type == "number")
          this.$refs.input.value = format.currencyVNFormat(newVal);
      } else {
        this.$refs.input.value = newVal;
      }
    },
  },
  mounted() {
    if (this.type == "currency") {
      if (!this.inputed)
        this.$refs.input.value = format.currencyFormatDE(this.modelValue);
    } else {
      this.$refs.input.value = this.modelValue;
    }
  },
};
</script>

<style scoped>
@import url("@/css/components/input.css");
.right {
  text-align: right;
}
.left {
  text-align: left;
}
.center {
  text-align: center;
}
</style>
<template>
  <div class="m-button" :class="buttonType">
    <button class="button" :class="{ 'is-dropdown': isDropdown }" ref="btnLeft">
      <div class="text" :style="{ color: textColor }">{{ text }}</div>
    </button>
    <!-- <div class="icon"></div> -->
    <div class="btn-dropdown" v-if="isDropdown" >
      <button class="dropdown-text" ref="btnRight"><div class="ic-arrow-down"></div></button>
    </div>
  </div>
</template>

<script>
export default {
  name: "MButton",
  props: {
    /**
     * Mô tả : loại button: default,primary...
     * Created by: Nguyễn Đức Toán - MF1095 (11/04/2022)
     */
    buttonType: {
      type: String,
      default: "default",
    },
    /**
     * Mô tả : chữ hiển thị trong button
     * Created by: Nguyễn Đức Toán - MF1095 (11/04/2022)
     */
    text: {
      type: String,
      default: "",
    },
    //xác định button có phải btn dropdown
    isDropdown: {
      type: Boolean,
      default: false,
    },
    //xác định button có phải btn utility
    isUtility: {
      type: Boolean,
      default: false,
    },
    //xác định màu chữ của button
    textColor: {
      type: String,
      default: "",
    },
  },
  mounted() {
    try {
        console.log(this.isUtility,this.isDropdown);
      if (this.isDropdown && this.isUtility) {
        this.$refs.btnRight.style.borderTopRightRadius = 32 + "px";
        this.$refs.btnRight.style.borderBottomRightRadius = 32 + "px";
        this.$refs.btnLeft.style.borderTopLeftRadius = 32 + "px";
        this.$refs.btnLeft.style.borderBottomLeftRadius = 32 + "px";
      }
    } catch (error) {
      console.log(error);
    }
  },
};
</script>

<style scoped>
@import url("@/css/components/button.css");
.m-button {
  display: flex;
}
.m-button .btn-dropdown {
  height: 100%;
  position: relative;
}
.m-button .btn-dropdown::after {
  content: "";
  width: 20px;
  height: 20px;
  position: absolute;
  top: 8px;
  left: 0px;
  border: 2px solid transparent;
  border-left: 1px solid #fff;
  box-sizing: border-box;
}
.m-button .btn-dropdown > button {
  height: 100%;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
  padding: 8px 10px;
  /* margin-left: 2px; */
  border-left: none !important;
}
.is-dropdown {
  border-top-right-radius: 0px !important;
  border-bottom-right-radius: 0px !important;
  border-right: none !important;
}
</style>
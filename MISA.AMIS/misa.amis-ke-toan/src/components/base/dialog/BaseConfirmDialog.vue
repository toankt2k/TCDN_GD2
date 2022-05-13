<template>
  <div class="m-dialog-confirm" :class="type">
    <div class="m-popup">
      <div class="popup-body">
        <!-- <div class="popup-header">
            <span>Thông báo</span>
            <div  class="popup-exit-button"></div>
          </div> -->
        <div class="popup-content">
          <div class="popup-content-img"></div>
          <div class="popup-content-text">
            {{ text }}
          </div>
        </div>
      </div>
      <div class="line"></div>
      <div class="popup-footer">
        <div class="popup-footer-button" :class="align">
          <MButton
            v-for="(val, ind) in listButton"
            :key="ind"
            :buttonType="val.type"
            :text="val.name"
            :style="{
              float: val.align,
              'margin-left': `${ind != 0 ? 8 : 0}px`,
            }"
            @click="confirm(val)"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MButton from "@/components/base/button/BaseButton.vue";
export default {
  name: "ConfirmDialog",
  components: {
    MButton,
  },
  /**
   * Mô tả : khai báo và định nghĩa các prop của component
   * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
   */
  props: {
    /**
      * Mô tả : danh sách các button và chức năng của nó
      * vd:button:[{
            id:1,
            name:"Không",
            type:"default",
            align:"left"
          },]
      * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
      */
    listButton: {
      type: Array,
      default: () => [],
    },
    //kiểu confirm:warning, dange,success
    type: {
      type: String,
      default: "",
    },
    //dòng thông báo cần hiển thị
    text: {
      type: String,
      default: "",
    },
    //xác định confirm cái gì
    keyConfirm: {
      type: String,
      default: "",
    },
    //xác định button có được align không mặc định là align
    align: {
      type: String,
      default: "",
    },
  },
  /**
   * Mô tả : khởi tạo và định nghĩa hàm cho component
   * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
   */
  methods: {
    confirm(val) {
      try {
        this.$emit("confirm", this.keyConfirm, val.id);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
 /* @keyframes slidein-confirm {
    from {
        margin-bottom: 100%;
    }
    to {
        margin-top: 0%;

    }
} */

/* .m-dialog-confirm .m-popup{
  animation: slidein-confirm 0.5s;
}  */
/* dialog warning, confirm, cancel */
.m-dialog-confirm.warning {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: #00000030;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 12;
}
.m-dialog-confirm.warning .m-popup {
  background-color: #fff;
  width: 444px;
  min-width: 444px;
  border-radius: 3px;
  padding: 32px;
}

.m-dialog-confirm.warning .m-popup .line {
  background-color: #bbb;
  width: 100%;
  height: 1px;
  margin: 0px 0px 20px 0px;
}

.m-dialog-confirm.warning .m-popup .popup-content {
  display: flex;
  flex-direction: row;
}

.m-dialog-confirm.warning .m-popup .popup-content .popup-content-img {
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
  background: url("@/assets/img/Sprites.64af8f61.svg") no-repeat;
  background-position: -592px -456px;
}

.m-dialog-confirm.warning .m-popup .popup-content .popup-content-text {
  padding: 12px 0 0 16px;
  margin-bottom: 32px;
}
.m-dialog-confirm.warning .m-popup .popup-footer .left {
  float: left;
}
.m-dialog-confirm.warning .m-popup .popup-footer .right {
  display: flex;
  align-items: center;
  float: right;
}
.m-dialog-confirm.info {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: #00000030;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 12;
}
.m-dialog-confirm.info .m-popup {
  background-color: #fff;
  width: 444px;
  min-width: 444px;
  border-radius: 3px;
  padding: 32px;
}

.m-dialog-confirm.info .m-popup .line {
  background-color: #bbb;
  width: 100%;
  height: 1px;
  margin: 0px 0px 20px 0px;
}

.m-dialog-confirm.info .m-popup .popup-content {
  display: flex;
  flex-direction: row;
}

.m-dialog-confirm.info .m-popup .popup-content .popup-content-img {
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
  background: url("@/assets/img/Sprites.64af8f61.svg") no-repeat;
  background-position: -666px -456px;
}

.m-dialog-confirm.info .m-popup .popup-content .popup-content-text {
  padding: 12px 0 0 16px;
  margin-bottom: 32px;
}
.m-dialog-confirm.info .m-popup .popup-footer .left {
  float: left;
}
.m-dialog-confirm.info .m-popup .popup-footer .right {
  display: flex;
  align-items: center;
  float: right;
}
.m-dialog-confirm.error {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: #00000030;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 12;
}
.m-dialog-confirm.error .m-popup {
  background-color: #fff;
  width: 444px;
  min-width: 444px;
  border-radius: 3px;
  padding: 32px;
}

.m-dialog-confirm.error .m-popup .line {
  background-color: #bbb;
  width: 100%;
  height: 1px;
  margin: 0px 0px 20px 0px;
}

.m-dialog-confirm.error .m-popup .popup-content {
  display: flex;
  flex-direction: row;
}

.m-dialog-confirm.error .m-popup .popup-content .popup-content-img {
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
  background: url("@/assets/img/Sprites.64af8f61.svg") no-repeat;
  background-position: -746px -456px;
}

.m-dialog-confirm.error .m-popup .popup-content .popup-content-text {
  padding: 12px 0 0 16px;
  margin-bottom: 32px;
}
.m-dialog-confirm.error .m-popup .popup-footer .left {
  float: left;
}
.m-dialog-confirm.error .m-popup .popup-footer .right {
  display: flex;
  align-items: center;
  float: right;
}
.popup-footer-button.center {
  display: flex;
  justify-content: center;
}
.popup-footer-button.left {
  display: flex;
  justify-content: start;
}
.popup-footer-button.right {
  display: flex;
  justify-content: end;
}
</style>
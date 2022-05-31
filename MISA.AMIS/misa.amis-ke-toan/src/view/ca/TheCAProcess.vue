<template>
  <div id="ca-process">
    <div class="content-area">
      <div class="process-area-utility">
        <div class="process">
          <div class="title">Nghiệp vụ tiền mặt</div>
          <div class="process-button">
            <div class="btn-recive" @click="funcImprovConfirm">
              <div class="title">Thu tiền</div>
            </div>
            <a class="btn-payment" href="/ThePaymentDetail">
              <div class="title" style="color: #111">Chi tiền</div>
            </a>
            <div class="btn-audit" @click="funcImprovConfirm">
              <div class="title">Kiểm kê quỹ</div>
            </div>
          </div>
        </div>
        <div class="utility">
          <div
            class="utility-btn m-col-3"
            v-for="item in utilityBtn"
            :key="item.id"
            @click="redict(item)"
          >
            <div class="icon" :class="item.classIcon"></div>
            <div class="text">{{ item.text }}</div>
          </div>
        </div>
      </div>
      <div class="report-area">
        <div class="report-title">Báo cáo</div>
        <div class="report-list">
          <div class="report-item">
            <div
              class="icon"
              style="font-size: 24px; padding-right: 12px; color: #646464"
            >
              •
            </div>
            <div class="text" style="cursor: pointer">
              Bảng kê số dư tiền theo ngày
            </div>
          </div>

          <div class="report-item">
            <div
              class="icon"
              style="font-size: 24px; padding-right: 12px; color: #646464"
            >
              •
            </div>
            <div class="text" style="cursor: pointer">
              Bảng kê số dư tiền theo ngày
            </div>
          </div>

          <div class="report-item">
            <div
              class="icon"
              style="font-size: 24px; padding-right: 12px; color: #646464"
            >
              •
            </div>
            <div class="text" style="cursor: pointer">
              Bảng kê số dư tiền theo ngày
            </div>
          </div>
        </div>
        <div class="report-footer" style="cursor: pointer">Tất cả báo cáo</div>
      </div>
    </div>
    <!-- dialog confirm -->
    <ConfirmDialog
      :text="confirmDialogData.text"
      :type="confirmDialogData.type"
      :listButton="confirmDialogData.button"
      :keyConfirm="confirmDialogData.key"
      :align="confirmDialogData.align"
      @confirm="confirm"
      v-if="isConfirm"
    />
  </div>
</template>

<script>
import ConfirmDialog from "@/components/base/dialog/BaseConfirmDialog.vue";
export default {
  components: {
    ConfirmDialog,
  },
  props: {},
  data() {
    return {
      /**
       * Mô tả : danh sách button cho utility
       * Created by: Nguyễn Đức Toán - MF1095 (04/05/2022)
       */
      utilityBtn: this.processResource.utilityBtn,
      /**
       * Mô tả : data cho confirm dialog
       * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
       */
      confirmDialogData: {
        name: "",
        type: "",
        key: "",
        button: [],
        text: "",
        align: "",
      },
      //ẩn hiện confirm dialog
      isConfirm: false,
    };
  },
  methods: {
    /**
     * Mô tả : xác nhận xem dialog confirm thực hiện việc gì
     * @param key keyword xác định confirm cái gì
     * @param btnKey keyword xác định nhấn nút nào
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    confirm(key, btnKey) {
      try {
        switch (key) {
          case "funcImprov":
            switch (btnKey) {
              case this.confirmResource.funcImprov.button[0].id:
                this.exitConfirm();
                break;

              default:
                break;
            }
            break;

          default:
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở confirm tính nawg đang phát triển
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    funcImprovConfirm() {
      try {
        this.confirmDialogData = {
          name: "funcImprov",
          type: "info",
          key: "funcImprov",
          button: this.confirmResource.funcImprov.button,
          text: this.confirmResource.funcImprov.getText(),
          align: "center",
        };
        this.isConfirm = true;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở confirm tính nawg đang phát triển
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    redict(item) {
      try {
        if (item.funcImprovConfirm) {
          this.confirmDialogData = {
            name: "funcImprov",
            type: "info",
            key: "funcImprov",
            button: this.confirmResource.funcImprov.button,
            text: this.confirmResource.funcImprov.getText(),
            align: "center",
          };
          this.isConfirm = true;
          return;
        }
        this.$router.push(item.link);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : đóng confirm dialog
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    exitConfirm() {
      try {
        this.isConfirm = false;
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
#ca-process {
  padding: 0px 0px 0px 20px;
  height: 100%;
}
.content-area {
  background-color: transparent;
  width: 100%;
  display: flex;
  margin-top: 20px;
  justify-content: center;
  overflow: auto;
}
.content-area .process-area-utility {
  width: 690px;
  height: 440px;
  min-width: 690px;
  min-height: 440px;
}
.content-area .process-area-utility .process {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-bottom: 50px;
  background-color: #fff;
  border: 1px solid rgb(229, 229, 229);
}
.content-area .process-area-utility .process > .title {
  font-family: Notosans-bold;
  font-weight: 700;
  text-align: center;
  min-height: 40px;
  line-height: 40px;
  height: 40px;
  font-size: 14px;
  width: 100%;
  border-bottom: 1px solid rgb(229, 229, 229);
}
.content-area .process-area-utility .process .process-button {
  background: url("@/assets/qt_TienMat.1f64b87b.svg") no-repeat -116px -53px;
  width: 464px;
  height: 220px;
  margin-top: 50px;
  position: relative;
}
.content-area .process-area-utility .process .process-button .btn-recive {
  padding: 60px 20px 10px 20px;
  position: absolute;
  top: -10px;
  left: -25px;
}
.content-area .process-area-utility .process .process-button .btn-recive:hover {
  box-shadow: rgb(0 0 0 / 25%) 0px 3px 6px;
  cursor: pointer;
}
.content-area .process-area-utility .process .process-button .btn-payment {
  padding: 60px 20px 10px 20px;
  position: absolute;
  top: 165px;
  left: -25px;
}
.content-area
  .process-area-utility
  .process
  .process-button
  .btn-payment:hover {
  box-shadow: rgb(0 0 0 / 25%) 0px 3px 6px;
  cursor: pointer;
}
.content-area .process-area-utility .process .process-button .btn-audit {
  padding: 60px 20px 10px 20px;
  position: absolute;
  top: 80px;
  left: 225px;
}
.content-area .process-area-utility .process .process-button .btn-audit:hover {
  box-shadow: rgb(0 0 0 / 25%) 0px 3px 6px;
  cursor: pointer;
}
.content-area .process-area-utility .utility {
  background-color: #fff;
  width: 100%;
  height: 70px;
  min-width: 100%;
  min-height: 70px;
  margin-top: 10px;
  display: flex;
  align-items: center;
  border: 1px solid rgb(229, 229, 229);
}
.content-area .process-area-utility .utility .utility-btn {
  height: 68px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.content-area .process-area-utility .utility .utility-btn:hover {
  box-shadow: rgb(0 0 0 / 25%) 0px 3px 6px;
  cursor: pointer;
}
.content-area .report-area {
  background-color: #fff;
  height: 440px;
  min-width: 100px;
  min-height: 440px;
  margin-left: 10px;
  width: 300px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  border: 1px solid rgb(229, 229, 229);
}
.content-area .report-area .report-title {
  font-family: Notosans-bold;
  font-weight: 700;
  text-align: center;
  min-height: 40px;
  line-height: 40px;
  height: 40px;
  font-size: 14px;
  width: 100%;
  border-bottom: 1px solid rgb(229, 229, 229);
}
.content-area .report-area .report-footer {
  font-weight: 400;
  text-align: center;
  min-height: 40px;
  line-height: 40px;
  height: 40px;
  font-size: 14px;
  width: 100%;
  color: #0075c0;
  border-top: 1px solid rgb(229, 229, 229);
}
.content-area .report-area .report-list {
  padding: 0px 16px;
  height: 100%;
}
.content-area .report-area .report-list .report-item {
  padding: 16px 0px;
  display: flex;
  align-items: center;
  border-bottom: 1px solid rgb(229, 229, 229);
}
.content-area .report-area .report-list .report-item .text:hover {
  color: #0075c0;
}

@media only screen and (max-width: 1024px) {
  .content-area .process-area-utility {
    overflow: hidden;
  }
}
</style>
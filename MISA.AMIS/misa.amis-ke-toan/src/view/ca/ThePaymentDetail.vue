<template>
  <div class="m-popup">
    <div class="payment-popup">
      <div class="popup-title">
        <div class="left">
          <div class="popup-icon ic-recent-log"></div>
          <div class="popup-title-text">Phiếu chi rrrrr123</div>
          <div class="type-payment">
            <MCombobox />
          </div>
        </div>
        <div class="right">
          <div class="guideline">
            <div class="ic-guideline"></div>
            <a class="link-guideline">Hướng dẫn</a>
          </div>
          <div class="setting">
            <div class="ic-setting"></div>
          </div>
          <div class="help">
            <div class="ic-help-dialog"></div>
          </div>
          <div class="exit">
            <div class="ic-exit-dialog"></div>
          </div>
        </div>
      </div>
      <!-- content -->
      <div class="popup-content">
        <div class="form m-col-12">
          <div class="form-input m-col-10">
            <div class="input-left m-col-8">
              <div class="m-row m-col-12">
                <div class="left m-col-6">
                  <div class="lable"><label for="">Đối tượng</label></div>
                  <MCombobox
                    :displayProps="tesst"
                    :apiData="'http://localhost:5093/api/v1/Departments'"
                    :isObject="true"
                    :idProp="'DepartmentId'"
                    :nameProp="'DepartmentName'"
                    v-model="test"
                  />
                </div>
                <div class="right m-col-6">
                  <div class="lable"><label for="">Người nhận</label></div>
                  <MInput />
                </div>
              </div>
              <div class="m-row m-col-12">
                <div class="right m-col-12">
                  <div class="lable"><label for="">Địa chỉ</label></div>
                  <MInput />
                </div>
              </div>
              <div class="m-row m-col-12">
                <div class="right m-col-12">
                  <div class="lable"><label for="">Lý do chi</label></div>
                  <MInput :placeholder="'Chi tiền cho'" />
                </div>
              </div>
              <div class="m-row m-col-12">
                <div class="left m-col-6">
                  <div class="lable"><label for="">Nhân viên</label></div>
                  <MInput />
                </div>
                <div class="right m-col-6">
                  <div class="lable"><label for="">Kèm theo</label></div>
                  <div style="display: flex; align-items: center">
                    <div class="m-col-7">
                      <MInput :placeholder="'Số lượng'" :align="'right'" />
                    </div>
                    <div class="description" style="margin-left: 4px">
                      chứng từ gốc
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="input-right m-col-4">
              <div class="m-row m-col-9">
                <div class="right m-col-12">
                  <div class="lable"><label for="">Ngày hạch toán</label></div>
                  <!-- eslint-disable -->
                  <DatePicker
                    v-model:value="dateOfbirth"
                    :format="'DD/MM/YYYY'"
                    :placeholder="'DD/MM/YYYY'"
                    :lang="'vi'"
                    :clearable="false"
                    :disabled-date="afterToday"
                    title-format="DD/MM/YYYY"
                  />
                  <!-- eslint-enable -->
                </div>
              </div>
              <div class="m-row m-col-9">
                <div class="right m-col-12">
                  <div class="lable"><label for="">Ngày phiếu chi</label></div>
                  <!-- eslint-disable -->
                  <DatePicker
                    v-model:value="dateOfbirth"
                    :format="'DD/MM/YYYY'"
                    :placeholder="'DD/MM/YYYY'"
                    :lang="'vi'"
                    :clearable="false"
                    :disabled-date="afterToday"
                    title-format="DD/MM/YYYY"
                  />
                  <!-- eslint-enable -->
                </div>
              </div>
              <div class="m-row m-col-9">
                <div class="right m-col-12">
                  <div class="lable">
                    <label for=""
                      >Số phiếu chi (<span class="required">*</span>)</label
                    >
                  </div>
                  <MInput />
                </div>
              </div>
            </div>
          </div>
          <div class="m-right summary-info m-col-2">
            <div class="total-payment">
              <div class="header">Tổng tiền</div>
              <div class="total">100,0</div>
            </div>
          </div>
        </div>
        <div class="table-area-1">
          <div class="title">
            <a class="link">Hạch toán</a>
            <div class="type-currency">
              <div style="padding: 0px 10px 0px 20px">Loại tiền</div>
              <div class="m-col-6"><MCombobox /></div>
            </div>
          </div>
        </div>
        <div class="table-area">
          <div class="table">
            <MTableEditable :deleteFunc="true"/>
          </div>
        </div>
        <div class="file-attach">
          <div class="button-area">
            <div class="add-row">
              <button>Thêm dòng</button>
            </div>
            <div class="delete-all">
              <button>Xóa hết dòng</button>
            </div>
          </div>
          <div class="attach">
            <div class="head">
              <div class="ic-attach"></div>
              <div class="text" style="font-weight: 600; padding-right: 8px">
                Đính kèm
              </div>
              <div class="text">Dung lượng tối đa 5Mb</div>
            </div>
            <div class="drag-target">
              <div class="text">Kéo/thả tệp vào đây hoặc bấm vào đây</div>
            </div>
          </div>
        </div>
      </div>
      <!-- footer -->
      <div class="popup-footer">
        <div class="m-left">
          <div class="cancel-btn">
            <MButton
              :buttonType="'default'"
              :text="'Hủy'"
              :textColor="'#fff'"
            />
          </div>
        </div>
        <div class="m-right">
          <div class="save-btn">
            <MButton
              :buttonType="'default'"
              :text="'Cất'"
              :textColor="'#fff'"
            />
          </div>
          <div class="save-and-print-btn">
            <MButton
              :buttonType="'primary'"
              :text="'Cất và In'"
              :isDropdown="true"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MCombobox from "@/components/base/BaseComboBox.vue";
import MButton from "@/components/base/button/BaseButton.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import DatePicker from "vue-datepicker-next";
import MTableEditable from "@/components/base/table/BaseTableEditable.vue";
import "vue-datepicker-next/index.css";
export default {
  name: "ThePaymentDialog",
  components: {
    MCombobox,
    MButton,
    MInput,
    DatePicker,
    MTableEditable,
  },
  data() {
    return {
      test: "1",
      tesst: [
        {
          id: "DepartmentName",
          name: "Phòng ban",
        },
        {
          id: "DepartmentCode",
          name: "Mã phòng ban",
        },
        {
          id: "CreateBy",
          name: "Người tạo",
        },
      ],
      comboboxData: {
        name: "GenderName",
        id: "Gender",
        data: [
          {
            Gender: "0",
            GenderName: "Nam",
          },
          {
            Gender: "1",
            GenderName: "Nữ",
          },
        ],
      },
    };
  },
  methods: {
    /**
     * Mô tả : disable ngày lớn hơn hiện tại trả về true nếu ngày lớn hơn hiện tại
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    afterToday(date) {
      return date > new Date();
    },
  },
};
</script>

<style scoped>
.m-popup {
  position: fixed;
  width: 100vw;
  height: 100vh;
  background-color: #eceef1;
  z-index: 10;
  top: 0;
  left: 0;
}
.m-popup .payment-popup {
  width: 100vw;
  height: 100vh;
}
/*title*/
.m-popup .popup-title {
  height: 72px;
  min-height: 72px;
  width: 100%;
  padding: 16px 16px 20px 16px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.m-popup .popup-title .left {
  display: flex;
  align-items: center;
}
.m-popup .popup-title .right {
  display: flex;
  align-items: center;
}

.m-popup .popup-title .popup-title-text {
  font-weight: 700;
  font-size: 24px;
  top: -7px;
  left: 20px;
  padding: 0 0 0 12px;
  color: #111;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.m-popup .popup-title .type-payment {
  margin-left: 24px;
}
.m-popup .popup-title .setting {
  width: 40px;
  height: 36px;
  padding: 6px 8px;
}
.m-popup .popup-title .setting:hover {
  background-color: #ccc;
}
.m-popup .popup-title .help {
  width: 40px;
  height: 36px;
  padding: 6px 8px;
}
.m-popup .popup-title .help:hover {
  background-color: #ccc;
}
.m-popup .popup-title .exit {
  width: 40px;
  height: 36px;
  padding: 6px 8px;
}
.m-popup .popup-title .exit:hover {
  background-color: #ccc;
}
.m-popup .popup-title .guideline {
  display: flex;
  align-items: center;
  margin: 0px 12px;
}
.m-popup .popup-title .guideline .link-guideline {
  white-space: nowrap;
  padding-left: 5px;
  color: #0075c0;
  font-size: 13px;
}
.m-popup .popup-title .guideline .link-guideline:hover {
  text-decoration: underline;
  cursor: pointer;
}
/*content*/
.m-popup .popup-content {
  width: 100%;
  height: calc(100% - 122px);
  overflow: auto;
}
.m-popup .popup-content .form {
  background-color: transparent;
  width: 100%;
  padding: 8px 24px 20px 24px;
  display: flex;
  position: sticky;
  left: 0px;
  z-index: 10;
}
.m-popup .popup-content .form .total-payment {
  color: #111;
  float: right;
}
.m-popup .popup-content .form .total-payment .header{
  float: right;
}
.m-popup .popup-content .form .total-payment .total{
  font-size: 36px;
  font-weight: 700;
}
.m-popup .popup-content .form .m-row {
  margin-bottom: 12px;
}
.m-popup .popup-content .form .form-input {
  display: flex;
}
.m-popup .popup-content .form .input-left {
  padding-right: 16px;
  border-right: 1px solid #ccc;
}
.m-popup .popup-content .form .input-right {
  padding-right: 16px;
  padding-left: 16px;
}
.m-popup .popup-content .form .input-left .left {
  padding-right: 12px;
}
.m-popup .popup-content .table-area {
  background-color: #fff;
  width: 100%;
}
.m-popup .popup-content .table-area .hidden-right {
  background-color: #fff !important;
}
.m-popup .popup-content .table-area-1 {
  background-color: #fff;
  width: 100%;
  padding: 8px 24px 0px 24px;
  position: sticky;
  left: 0;
}
.m-popup .popup-content .table-area-1 .title {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding-bottom: 16px;
}
.m-popup .popup-content .table-area-1 .title .type-currency {
  display: flex;
  align-items: center;
  justify-content: end;
}
.m-popup .popup-content .table-area-1 .title .link {
  white-space: nowrap;
  color: #0075c0;
  font-size: 13px;
  text-decoration: underline;
  cursor: pointer;
  font-weight: 700;
  font-family: Notosans-bold;
}
.m-popup .popup-content .table-area-1 .title .link:hover {
  color: #2ca01c;
}
.m-popup .popup-content .file-attach {
  position: sticky;
  left: 0px;
  background-color: #fff;
  padding: 12px 24px 10px 24px;
}
.m-popup .popup-content .file-attach .button-area {
  display: flex;
  align-items: center;
}
.m-popup .popup-content .file-attach .button-area .add-row {
  height: 24px;
  margin-right: 12px;
}
.m-popup .popup-content .file-attach .button-area button {
  background-color: transparent;
  border: 1px solid #ccc;
  height: 100%;
  border-radius: 2px;
  color: #111;
  font-size: 12px;
  font-weight: 600;
  padding: 2px 16px;
  cursor: pointer;
}
.m-popup .popup-content .file-attach .button-area button:hover {
  background-color: #eceef1;
}
.m-popup .popup-content .file-attach .button-area button:active {
  background-color: #bbb;
}
.m-popup .popup-content .file-attach .button-area .delete-all {
  height: 24px;
}
.m-popup .popup-content .file-attach .attach .head {
  display: flex;
  align-items: center;
  padding-bottom: 8px;
   margin-top: 8px;
}
.m-popup .popup-content .file-attach .attach .ic-attach {
  margin-right: 8px;
 
}
.m-popup .popup-content .file-attach .attach .drag-target {
  min-height: 50px;
  border: 1px solid #bbb;
  display: flex;
  align-items: center;
  justify-content: center;
  max-width: 333px;
  border-radius: 2px;
}
/**footer */
.m-popup .popup-footer {
  background-color: #38393d;
  height: 50px;
  width: 100%;
  min-height: 50px;
  padding: 0 16px;
}
.m-popup .popup-footer {
  background-color: #38393d;
  height: 50px;
  width: 100%;
  min-height: 50px;
  padding: 0 16px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.m-popup .popup-footer .save-btn {
  margin: 0px 12px;
}
.m-popup .popup-footer .m-left {
  display: flex;
  align-items: center;
}
.m-popup .popup-footer .m-right {
  display: flex;
  align-items: center;
}
</style>
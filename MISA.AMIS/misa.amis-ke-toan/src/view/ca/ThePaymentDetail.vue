<template >
  <div class="m-popup"  >
    <div class="payment-popup">
      <div class="popup-title">
        <div class="left">
          <div class="popup-icon ic-recent-log"></div>
          <div class="popup-title-text">
            Phiếu chi {{ payment.PaymentCode }}
          </div>
          <div class="type-payment">
            <MCombobox
              :placeholder="'Loại chứng từ'"
              :defaultList="this.dataStorage.paymentDetail.paymentTypeCombobox"
              :idProp="'id'"
              :nameProp="'name'"
              v-model="paymentType"
              :disabled="disabled"
            />
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
            <div class="ic-exit-dialog" @click="$router.back()"></div>
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
                    :displayProps="objectProp"
                    :apiData="this.dataStorage.api.vendor.filter"
                    :isObject="true"
                    :idProp="'VendorId'"
                    :nameProp="'VendorCode'"
                    v-model="payment.VendorId"
                    @getSelected="getSelectedVendor"
                    :disabled="disabled"
                    ref="VendorId"
                    :func="add"
                  />
                </div>
                <div class="right m-col-6">
                  <div class="lable"><label for="">Tên đối tượng</label></div>
                  <MInput
                    v-model="vendorName"
                    :disabled="disabled"
                    ref="VendorName"
                  />
                </div>
              </div>
              <div class="m-row m-col-12">
                <div class="left m-col-6">
                  <div class="lable"><label for="">Địa chỉ</label></div>
                  <MInput
                    v-model="payment.Address"
                    :disabled="disabled"
                    ref="Address"
                  />
                </div>

                <div class="right m-col-6">
                  <div class="lable"><label for="">Người nhận</label></div>
                  <MInput
                    v-model="payment.ReceiverName"
                    :disabled="disabled"
                    ref="ReceiverName"
                  />
                </div>
              </div>
              <div class="m-row m-col-12">
                <div class="m-col-12">
                  <div class="lable"><label for="">Lý do chi</label></div>
                  <MInput
                    :placeholder="'Chi tiền cho'"
                    v-model="descriptionPayment"
                    :disabled="disabled"
                    ref="DescriptionPayment"
                  />
                </div>
              </div>
              <div class="m-row m-col-12">
                <div class="left m-col-6">
                  <div class="lable"><label for="">Nhân viên</label></div>
                  <MCombobox
                    :displayProps="employeeProp"
                    :apiData="this.dataStorage.api.employee.filter"
                    :isObject="true"
                    :idProp="'EmployeeId'"
                    :nameProp="'EmployeeName'"
                    v-model="payment.EmployeeId"
                    :disabled="disabled"
                    ref="EmployeeId"
                  />
                </div>
                <div class="right m-col-6">
                  <div class="lable"><label for="">Kèm theo</label></div>
                  <div style="display: flex; align-items: center">
                    <div class="m-col-7">
                      <MInput
                        :placeholder="'Số lượng'"
                        :align="'right'"
                        v-model="payment.AttachDocumentAmount"
                        :disabled="disabled"
                        :type="'number'"
                        ref="AttachDocumentAmount"
                      />
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
                    v-model:value="accountingDate"
                    :format="'DD/MM/YYYY'"
                    :placeholder="'DD/MM/YYYY'"
                    :lang="'vi'"
                    :clearable="false"
                    title-format="DD/MM/YYYY"
                    :disabled="disabled"
                    ref="AccountingDate"
                  />
                  <!-- eslint-enable -->
                </div>
              </div>
              <div class="m-row m-col-9">
                <div class="right m-col-12">
                  <div class="lable"><label for="">Ngày phiếu chi</label></div>
                  <!-- eslint-disable -->
                  <DatePicker
                    v-model:value="paymentDate"
                    :format="'DD/MM/YYYY'"
                    :placeholder="'DD/MM/YYYY'"
                    :lang="'vi'"
                    :clearable="false"
                    title-format="DD/MM/YYYY"
                    :disabled="disabled"
                    ref="PaymentDate"
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
                  <MInput
                    ref="PaymentCode"
                    v-model="payment.PaymentCode"
                    :disabled="disabled"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="m-right summary-info m-col-2">
            <div class="total-payment">
              <div class="header">Tổng tiền</div>
              <div class="total">
                {{ formatContent(payment.TotalAmount, "currency") }}
              </div>
            </div>
          </div>
        </div>
        <div class="table-area-1">
          <div class="title">
            <a class="link">Hạch toán</a>
            <div class="type-currency">
              <div style="padding: 0px 10px 0px 20px">Loại tiền</div>
              <div class="m-col-1">
                <MCombobox
                  :placeholder="'Loại tiền'"
                  :defaultList="this.dataStorage.paymentDetail.currencyCombobox"
                  :idProp="'id'"
                  :nameProp="'code'"
                  :isObject="true"
                  :displayProps="this.dataStorage.paymentDetail.currencyProp"
                  :quickAdd="false"
                  v-model="payment.CurrencyId"
                  @getSelected="getExchange"
                  :disabled="disabled"
                  ref="CurrencyId"
                />
              </div>
              <div
                style="padding: 0px 10px 0px 20px"
                v-if="payment.CurrencyId != 1 && payment.CurrencyId"
              >
                Tỷ giá
              </div>
              <div
                class="m-col-1"
                v-if="payment.CurrencyId != 1 && payment.CurrencyId"
              >
                <MInput
                  :placeholder="'Tỷ giá'"
                  v-model="exchange"
                  :disabled="true"
                  :type="'currency'"
                  :align="'right'"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="table-area">
          <div class="table">
            <MTableEditable
              ref="PaymentDetailTable"
              :deleteFunc="true"
              :columns="columnOfPaymentDetail"
              :checkBox="false"
              :counter="true"
              :defaultData="paymentDetail"
              @getData="getPaymentDetail"
              :disabled="disabled"
            />
          </div>
        </div>
        <div class="file-attach">
          <div class="button-area">
            <div class="add-row">
              <button @click="addRow">Thêm dòng</button>
            </div>
            <div class="delete-all">
              <button @click="removeRow">Xóa hết dòng</button>
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
              ref="cancelBtn"
              @click="$router.back()"
            />
          </div>
        </div>
        <div class="m-right">
          <div class="save-btn">
            <MButton
              :buttonType="'default'"
              :text="'Cất'"
              :textColor="'#fff'"
              @click="savePayment"
              ref="saveBtn"
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
    <VendorDetail
      ref="vendorDetail"
      v-if="vendorOpen"
      @exitForm="exitVendor"
      @addToast="addToast"
      @getVendor="getAddVendor"
    />
    <ConfirmDialog
      :text="confirmDialogData.text"
      :type="confirmDialogData.type"
      :listButton="confirmDialogData.button"
      :align="confirmDialogData.align"
      @confirm="confirm"
      :keyConfirm="confirmDialogData.key"
      v-if="isConfirm"
    />
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
import MCombobox from "@/components/base/BaseComboBox.vue";
import MButton from "@/components/base/button/BaseButton.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import DatePicker from "vue-datepicker-next";
import "vue-datepicker-next/index.css";
import MTableEditable from "@/components/base/table/BaseTableEditable.vue";
import ConfirmDialog from "@/components/base/dialog/BaseConfirmDialog.vue";
import ToastMessage from "@/components/base/BaseToastMessage.vue";
import VendorDetail from "@/view/vendor/VendorDetail.vue";
import format from "@/js/lib/formatContent.js";
import axios from "axios";
export default {
  name: "ThePaymentDialog",
  components: {
    MCombobox,
    MButton,
    MInput,
    DatePicker,
    MTableEditable,
    ConfirmDialog,
    ToastMessage,
    VendorDetail,
  },
  computed: {
    accountingDate: {
      get() {
        try {
          if (!this.payment.AccountingDate) return new Date();
          return new Date(this.payment.AccountingDate);
        } catch (error) {
          return new Date();
        }
      },
      set(val) {
        try {
          if (
            this.payment.AccountingDate.toDateString() ===
            this.payment.PaymentDate.toDateString()
          ) {
            this.payment.PaymentDate = new Date(val);
          }
          this.payment.AccountingDate = new Date(val);
        } catch (error) {
          return new Date();
        }
      },
    },
    paymentDate: {
      get() {
        try {
          if (!this.payment.PaymentDate) return new Date();
          return new Date(this.payment.PaymentDate);
        } catch (error) {
          return new Date();
        }
      },
      set(val) {
        try {
          this.payment.PaymentDate = new Date(val);
        } catch (error) {
          return new Date();
        }
      },
    },
    paymentDetail: {
      get() {
        try {
          let rs = JSON.parse(
            this.payment.PaymentDetail ? this.payment.PaymentDetail : "[{}]"
          );
          return rs;
        } catch (error) {
          console.log(error);
          return [
            {
              DescriptionPaymentDetail: "",
              DebitAccountId: "",
              CreditAccountId: "",
              CashAmount: 0,
              VendorId: "",
              VendorName: "",
            },
          ];
        }
      },
      set(val) {
        try {
          this.payment.PaymentDetail = JSON.stringify(val);
        } catch (error) {
          console.log(error);
          this.payment.PaymentDetail = JSON.stringify([
            {
              DescriptionPaymentDetail: "",
              DebitAccountId: "",
              CreditAccountId: "",
              CashAmount: 0,
              VendorId: "",
              VendorName: "",
            },
          ]);
        }
      },
    },
    descriptionPayment: {
      set(val) {
        let table = JSON.parse(this.payment.PaymentDetail);
        if (table) {
          table.forEach((item) => {
            if (
              item.DescriptionPaymentDetail == this.payment.DescriptionPayment
            ) {
              item.DescriptionPaymentDetail = val;
            }
          });
        }
        this.payment.PaymentDetail = JSON.stringify(table);
        this.payment.DescriptionPayment = val;
      },
      get() {
        return this.payment.DescriptionPayment;
      },
    },
    vendorName: {
      get() {
        return this.payment.VendorName;
      },
      set(val) {
        if (
          `Chi tiền cho ${this.payment.VendorName}` ==
            this.descriptionPayment ||
          this.descriptionPayment == "Chi tiền cho "
        ) {
          this.descriptionPayment = "Chi tiền cho " + val;
        }
        this.payment.VendorName = val;
      },
    },
  },
  data() {
    return {
      //data cho toastmessage
      listToastMessage: [],
      //tỷ lệ đổi tiền
      exchange: 1,
      //kiểu chi
      paymentType: 7,
      vendorOpen: false,
      //vendor
      objectProp: this.dataStorage.vendorDetail.vendorProp,
      //prop cho combox nhân viên
      employeeProp: [
        {
          id: "EmployeeCode",
          name: "Mã nhân viên",
        },
        {
          id: "EmployeeName",
          name: "Tên nhân viên",
        },
        {
          id: "DepartmentName",
          name: "Đơn vị",
        },
        {
          id: "PhoneNumber",
          name: "Đt di động",
        },
      ],
      confirmDialogData: {},
      columnOfPaymentDetail: [
        {
          id: "DescriptionPaymentDetail",
          name: "Diễn giải",
          displayName: "Diễn giải",
          descriptionName: "Diễn giải",
          width: "150px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
          component: false,
        },
        {
          id: "DebitAccountId",
          name: "Tài khoản nợ",
          displayName: "Tài khoản nợ",
          descriptionName: "Tài khoản nợ",
          width: "200",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
          component: true,
          componentData: {
            name: "MCombobox",
            objectName: "DebitAccount",
            prop: {
              displayProps: this.dataStorage.paymentDetail.accountDebitProp,
              defaultList: this.dataStorage.paymentDetail.accountDebit,
              isObject: true,
              quickAdd: false,
              nameObject: "DebitAccountCode",
              idProp: "DebitAccountId",
              nameProp: "DebitAccountCode",
              ref: "DebitAccountId",
            },
          },
        },
        {
          id: "CreditAccountId",
          name: "Tài khoản có",
          displayName: "Tài khoản có",
          descriptionName: "Tài khoản có",
          width: "200px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
          component: true,
          componentData: {
            name: "MCombobox",
            objectName: "CreditAccount",
            prop: {
              displayProps: this.dataStorage.paymentDetail.accountCreditProp,
              defaultList: this.dataStorage.paymentDetail.accountCreadit,
              isObject: true,
              quickAdd: false,
              nameObject: "CreditAccountCode",
              idProp: "CreditAccountId",
              nameProp: "CreditAccountCode",
              ref: "CreditAccountId",
            },
          },
        },
        {
          id: "CashAmount",
          name: "Số tiền",
          displayName: "Số tiền",
          descriptionName: "Số tiền",
          width: "100px",
          minWidth: "120px",
          classList: [],
          align: "right",
          edit: true,
          count: true,
          type: "currency",
        },
        {
          id: "VendorId",
          name: "Đối tượng",
          displayName: "Đối tượng",
          descriptionName: "Đối tượng",
          width: "auto",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
          component: true,
          componentData: {
            name: "MCombobox",
            objectName: "Vendor",
            prop: {
              displayProps: this.dataStorage.vendorDetail.vendorProp,
              apiData: this.dataStorage.api.vendor.filter,
              isObject: true,
              quickAdd: false,
              idProp: "VendorId",
              nameObject: "VendorName",
              nameProp: "VendorCode",
              ref: "VendorId",
            },
          },
        },
        {
          id: "VendorName",
          name: "Tên Đối tượng",
          displayName: "Tên Đối tượng",
          descriptionName: "Tên Đối tượng",
          width: "auto",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: false,
          count: false,
          type: "text",
          comboboxName: true,
          refReferent: "VendorId",
        },
      ],
      //không cho phép sửa
      disabled: false,
      payment: {},
      //show confirm dialog
      isConfirm: false,
      //danh sách cac input lỗi
      listErrorInput: [],
      oldVendorId: "",
      //tab index
      tabIndex: [
        "VendorId",
        "VendorName",
        "Address",
        "ReceiverName",
        "DescriptionPayment",
        "EmployeeId",
        "AttachDocumentAmount",
        "AccountingDate",
        "PaymentDate",
        "PaymentCode",
        "CurrencyId",
        "saveBtn",
        "cancelBtn",
      ],
      //vị trí hiện tại của tab index
      curIndex: 0,
      //form đang được xem
      isView: false,
    };
  },
  methods: {
    /**
     * Mô tả : lưu payment
     * Created by: Nguyễn Đức Toán - MF1095 (23/05/2022)
     */
    savePayment() {
      if (this.disabled) return;
      if (!this.validate()) return;
      this.addPayment();
    },
    /**
     * Mô tả : gọi api insert payment
     * Created by: Nguyễn Đức Toán - MF1095 (24/05/2022)
     */
    addPayment() {
      try {
        let me = this;
        let data = JSON.parse(JSON.stringify(me.payment));
        if (data.PaymentDetail)
          data.PaymentDetail = JSON.parse(data.PaymentDetail);
        axios({
          method: "POST",
          url: this.dataStorage.api.payment.add,
          data: data,
        })
          .then(() => {
            let toast = this.resource.toastMessage.addSuccess;
            this.addToast(toast);
            this.isView = true;
            this.disabled = true;
          })
          .catch((res) => {
            if (res.response.status == 400) {
              let confirm = {
                name: "dataError",
                button: this.resource.confirmDialogData.errorResponse,
                // align: "",
                type: "warning",
                text: `${res.response.data.data["PaymentCode"]}`,
                key: "dataError",
              };
              this.openConfirm(confirm);
            }
          });
      } catch (error) {
        console.log(error);
      }
    },
    //thay đổi tỷ giá tiền tệ
    getExchange(data) {
      this.exchange = data.exchange;
      this.payment.TotalAmount = this.totalAmount * data.exchange;
    },
    /**
     * Mô tả : lấy số phiếu chi mới nhất
     * Created by: Nguyễn Đức Toán - MF1095 (24/05/2022)
     */
    async getNewPaymentCode() {
      try {
        let me = this;
        await axios({
          method: "GET",
          url: this.dataStorage.api.payment.getNewCode,
          async: true,
        })
          .then((res) => {
            me.payment.PaymentCode = res.data;
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : validate dữ lieuj trên form
     * Created by: Nguyễn Đức Toán - MF1095 (24/05/2022)
     */
    validate() {
      try {
        let isOk = true;
        this.listErrorInput = [];
        if (
          new Date(this.payment.PaymentDate.toDateString()) >
          new Date(this.payment.AccountingDate.toDateString())
        ) {
          this.listErrorInput.push({
            id: "AccountingDate",
            title: "Ngày hạch toán phải lớn hơn hoặc bằng ngày chứng từ.",
          });
          isOk = false;
        }
        if (!this.payment.PaymentCode) {
          this.listErrorInput.push({
            id: "PaymentCode",
            title: "Số phiếu chi không được trống.",
          });
          isOk = false;
        }
        if (!isOk) {
          let input = this.listErrorInput[0];
          let confirm = {
            name: "emptyInput",
            button: this.resource.confirmDialogData.errorInputConfirm,
            align: "center",
            type: "error",
            text: input.title,
            key: "emptyInput",
          };
          this.openConfirm(confirm);
        }
        return isOk;
      } catch (error) {
        console.log(error);
      }
    },
    openConfirm(confirm) {
      this.confirmDialogData = confirm;
      this.isConfirm = true;
    },
    /**
     * Mô tả : forcus vào ô đầu tiên lỗi
     * Created by: Nguyễn Đức Toán - MF1095 (24/05/2022)
     */
    focusErrorInput() {
      try {
        if (this.listErrorInput.length > 0) {
          let input = this.listErrorInput[0];
          let target = this.$refs[input.id];
          if (target) target.setErrorFocus(input.title);
        }
      } catch (error) {
        console.log(error);
      }
    },
    confirm(key, val) {
      let me = this;
      switch (key) {
        case "emptyError": //input nhập trống
          switch (val) {
            case 1: //đóng
              this.isConfirm = false;
              this.focusErrorInput();
              break;
            default:
              break;
          }
          break;
        case "emptyInput": //input nhập trống
          switch (val) {
            case 1: //đóng
              this.isConfirm = false;
              break;
            default:
              break;
          }
          break;
        case "removeAll": //input nhập trống
          switch (val) {
            case 1: //đóng
              this.isConfirm = false;
              break;
            case 2: //đóng
              this.payment.PaymentDetail = JSON.stringify([
                {
                  DescriptionPaymentDetail: this.payment.DescriptionPayment,
                  DebitAccountId: "",
                  CreditAccountId: "",
                  CashAmount: 0,
                  VendorId: this.payment.VendorId,
                  VendorName: this.payment.VendorName,
                },
              ]);
              this.isConfirm = false;
              break;
            default:
              break;
          }
          break;
        case "dataError": //input nhập trống
          switch (val) {
            case 1: //không
              this.isConfirm = false;
              this.focusErrorInput();
              break;
            case 2: //có
              this.isConfirm = false;

              this.getNewPaymentCode().then(() => {
                me.savePayment();
              });
              break;
            default:
              break;
          }
          break;

        default:
          break;
      }
    },
    /**
     * Mô tả : disabled ngày lớn hơn hiện tại trả về true nếu ngày lớn hơn hiện tại
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    getAddVendor(data) {
      this.payment.VendorId = data.VendorId;
      this.$refs["VendorId"].pushNewObject(data);
      // let target = this.$refs["PaymentDetailTable"];
      // if (target) target.addNewObject(data, "VendorId");
      this.getSelectedVendor(data);
    },
    getSelectedVendor(data) {
      try {
        if (this.isView) return;
        if (data.VendorType == 1) this.payment.ReceiverName = data.ReceiverName;
        if (data.VendorType == 0)
          this.payment.ReceiverName = data.ContactLegalRep;
        this.payment.Address = data.Address;
        this.vendorName = data.VendorName;
        let table = JSON.parse(this.payment.PaymentDetail);
        if (table) {
          table.forEach((item) => {
            if (item.VendorId == this.oldVendorId || !item.VendorId) {
              item.VendorId = data.VendorId;
              item.VendorName = data.VendorName;
            }
          });
        }
        this.payment.PaymentDetail = JSON.stringify(table);
        this.oldVendorId = this.payment.VendorId;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy dữ liêu jtuwf bảng payment detail
     * Created by: Nguyễn Đức Toán - MF1095 (23/05/2022)
     */
    getPaymentDetail(data, count) {
      try {
        this.payment.PaymentDetail = JSON.stringify(data);
        this.totalAmount = count["CashAmount"];
        this.payment.TotalAmount = count["CashAmount"] * this.exchange;
      } catch (error) {
        console.log(error);
      }
    },
    exitVendor() {
      this.vendorOpen = false;
    },
    add() {
      this.vendorOpen = true;
      this.$nextTick(() => {
        this.$refs["vendorDetail"].isAdd = true;
        this.$refs["vendorDetail"].getNewVendorCode();
      });
    },
    /**
     * Mô tả : thêm dòng cho bảng payment detail
     * Created by: Nguyễn Đức Toán - MF1095 (24/05/2022)
     */
    addRow() {
      try {
        if (this.isView) return;
        if (this.payment.PaymentDetail) {
          let temp = JSON.parse(this.payment.PaymentDetail);
          if (temp.length >= 1) {
            let newRow = {};
            Object.assign(newRow, temp[temp.length - 1]);
            newRow.CashAmount = 0;
            temp.push(newRow);
          } else {
            temp.push({
              DescriptionPaymentDetail: this.payment.DescriptionPayment,
              DebitAccountId: "",
              CreditAccountId: "",
              CashAmount: 0,
              VendorId: this.payment.VendorId,
              VendorName: this.payment.VendorName,
            });
          }
          this.payment.PaymentDetail = JSON.stringify(temp);
        } else {
          let temp = JSON.parse(JSON.stringify([]));
          temp.push({
            DescriptionPaymentDetail: this.payment.DescriptionPayment,
            DebitAccountId: "",
            CreditAccountId: "",
            CashAmount: 0,
            VendorId: this.payment.VendorId,
            VendorName: this.payment.VendorName,
          });
          this.payment.PaymentDetail = JSON.stringify(temp);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : xóa hàng cho bảng paymentdetail
     * Created by: Nguyễn Đức Toán - MF1095 (24/05/2022)
     */
    removeRow() {
      try {
        if (this.isView) return;
        let confirm = {
          name: "removeAll",
          button: this.resource.confirmDialogData.delete,
          // align: "center",
          type: "warning",
          text: "Bạn có thực sự muốn xóa tất cả các dòng đã nhập không?",
          key: "removeAll",
        };
        this.openConfirm(confirm);
      } catch (error) {
        console.log(error);
      }
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
    /**
     * Mô tả : lấy dữ liệu payment và bind lên form
     * Created by: Nguyễn Đức Toán - MF1095 (26/05/2022)
     */
    getPayment(id) {
      try {
        if (!id) return;
        axios({
          url: `${this.dataStorage.api.payment.getById}/${id}`,
          method: "GET",
        })
          .then((res) => {
            this.payment = res.data;
            if (!this.payment.CurrenceId) {
              this.payment.CurrencyId = 1;
            }
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : format đinh dạng dữ liệu
     * Created by: Nguyễn Đức Toán - MF1095 (27/05/2022)
     */
    formatContent(val, key) {
      switch (key) {
        case "currency":
          return format.currencyFormatDE(val);
        default:
          break;
      }
    },

    /**
     * Mô tả : lấy dữ liệu payment và bind lên form
     * Created by: Nguyễn Đức Toán - MF1095 (26/05/2022)
     */
    getDetailOfPayment(id) {
      try {
        if (!id) return;
        axios({
          url: `${this.dataStorage.api.paymentDetail.getByPayment}/${id}`,
          method: "GET",
        })
          .then((res) => {
            this.paymentDetail = res.data;
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    //tab in dẽ
    tab(e) {
      try {
        if (e.keyCode != 9) return;
        if (this.curIndex + 1 >= this.tabIndex.length) {
          this.curIndex = -1;
        }
        this.curIndex += 1;
        let ref = this.tabIndex[this.curIndex];
        if (!(ref == "AccountingDate" || ref == "PaymentDate")) {
          e.preventDefault();
          let target = this.$refs[ref];
          if (target) {
            target.setFocus(true);
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    keyup(e) {
      try {
        if (e.keyCode == 112 && e.ctrlKey) {
          e.preventDefault();
          if (!this.isConfirm) {
            this.openAddVendor();
          }
        }
        if (e.keyCode == 27) {
          e.preventDefault();
          if (!this.isConfirm) {
            this.$router.back()
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    keydown(e) {
      try {
        if (e.keyCode == 112 && e.ctrlKey) {
          e.preventDefault();
        }
        if (e.keyCode == 27) {
          e.preventDefault();
        }
      } catch (error) {
        console.log(error);
      }
      
    },
  },
  mounted() {
    try {
      let id = this.$route.params.id;
      if (id) {
        this.isView = true;
        this.disabled = true;
        this.getPayment(id);
        this.getDetailOfPayment(id);
        return;
      }
      if (!this.payment.CurrencyId) {
        this.payment.CurrencyId = 1;
      }
      if (!this.payment.AccountingDate)
        this.payment.AccountingDate = new Date();
      if (!this.payment.PaymentDate) this.payment.PaymentDate = new Date();
      this.payment.DescriptionPayment = "Chi tiền cho ";
      this.payment.PaymentDetail = JSON.stringify([
        {
          DescriptionPaymentDetail: this.payment.DescriptionPayment,
          DebitAccountId: "1",
          CreditAccountId: "1",
          CashAmount: 0,
          VendorId: this.payment.VendorId,
          VendorName: this.payment.VendorName,
        },
      ]);
      this.getNewPaymentCode();
      let firstInput = this.$refs[this.tabIndex[0]];
      if (firstInput) {
        firstInput.setFocus(true);
      }
      window.addEventListener("keydown", this.keydown);
      window.addEventListener("keyup", this.keyup);
    } catch (error) {
      console.log(error);
    }
  },
  beforeUnmount() {
    try {
      window.removeEventListener("keydown", this.keydown);
      window.removeEventListener("keyup", this.keyup);
    } catch (error) {
      console.log(error);
    }
  },
  watch: {
    
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
.m-popup .popup-content .form .total-payment .header {
  float: right;
}
.m-popup .popup-content .form .total-payment .total {
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
  padding-right: 6px;
}
.m-popup .popup-content .form .input-left .right {
  padding-left: 6px;
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
  z-index: 5;
}
.m-popup .popup-content .table-area-1 .title {
  display: flex;
  align-items: center;
  padding-bottom: 16px;
}
.m-popup .popup-content .table-area-1 .title .type-currency {
  display: flex;
  align-items: center;
  justify-content: end;
  flex: 1;
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
.m-toast-box {
  position: fixed;
  top: 24px;
  left: 40%;
  z-index: 20;
}
</style>

<template>
  <div class="m-dialog" id="mDialog">
    <div class="dialog-info">
      <div class="dialog" id="dialog" ref="dialogDragable">
        <div class="tool">
          <div class="help-button"></div>
          <div
            class="exit-button"
            title="Đóng (ESC)"
            @click="exitForm"
          ></div>
        </div>
        <div class="title-dialog">
          <div class="title">Thông tin nhà cung cấp</div>
          <div class="selection-type">
            <RadioButton
              :button="vendorTypeRadio.button"
              :id="vendorTypeRadio.id"
              :name="vendorTypeRadio.name"
            />
          </div>
          <div class="selection-type">
            <MCheckBox :rowId="'customer'" />
            <div class="text">Là khách hàng</div>
          </div>
        </div>
        <div class="content-dialog">
          <div class="form">
            <div class="row">
              <div class="left m-col-6">
                <div class="row">
                  <div class="m-col-5">
                    <label for="" title="Mã nhân viên">Mã số thuế</label>
                    <div class="field-input">
                      <MInput ref="TaxCode" v-model="vendor.TaxCode" />
                    </div>
                  </div>
                  <div class="m-col-7" style="padding-left: 6px">
                    <label for="" title="Họ và tên nhân viên"
                      >Mã nhà cung cấp <span class="required">*</span></label
                    >
                    <div class="field-input">
                      <MInput ref="VendorCode" v-model="vendor.VendorCode" />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for=""
                      >Tên nhà cung cấp <span class="required">*</span></label
                    >
                    <div class="field-input">
                      <MInput ref="VendorName" v-model="vendor.VendorName" />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="">Địa chỉ</label>
                    <div class="field-input">
                      <MTextArea
                        :placeholder="'VD: Số 82 Duy Tân, Dịch Vọng, Cầu Giấy, Hà Nội'"
                      />
                    </div>
                  </div>
                </div>
              </div>
              <div class="right m-col-6">
                <div class="row">
                  <div class="m-col-5">
                    <label for="">Điện thoại</label>
                    <div class="field-input">
                      <MInput ref="PhoneNumber" v-model="vendor.PhoneNumber" />
                    </div>
                  </div>
                  <div class="m-col-7" style="padding-left: 16px">
                    <label for="">Website</label>
                    <div class="field-input">
                      <MInput ref="Website" v-model="vendor.Website" />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="">Nhóm nhà cung cấp</label>
                    <div class="field-input">
                      <MCombobox
                        :displayProps="tesst"
                        :apiData="'http://localhost:5093/api/v1/Departments'"
                        :isObject="true"
                        :idProp="'DepartmentId'"
                        :nameProp="'DepartmentName'"
                        v-model="test"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="" title="Nơi cấp chứng minh nhân dân"
                      >Nơi cấp</label
                    >
                    <div class="field-input">
                      <MInput ref="IdentityPlace" />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="tab-form">
                <div class="tab">
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'contact' }"
                    @click="selectTab('contact')"
                  >
                    <div class="text">Liên hệ</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'roles' }"
                    @click="selectTab('roles')"
                  >
                    <div class="text">Điều khoản thanh toán</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'banks' }"
                    @click="selectTab('banks')"
                  >
                    <div class="text">Tài khoản ngân hàng</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'address' }"
                    @click="selectTab('address')"
                  >
                    <div class="text">Địa chỉ khác</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'note' }"
                    @click="selectTab('note')"
                  >
                    <div class="text">Ghi chú</div>
                  </div>
                </div>
                <div class="form-in-tab">
                  <!-- contact tab -->
                  <div class="tab-content" v-show="tabSelected == 'contact'">
                    <div class="left m-col-6">
                      <label for="">Người liên hệ</label>
                      <div class="row">
                        <div class="m-col-12 m-d-flex">
                          <div class="m-col-5" style="padding-right: 8px">
                            <div class="field-input">
                              <MInput
                                v-model="vendor.PhoneNumber"
                                :placeholder="'Xưng hô'"
                              />
                            </div>
                          </div>
                          <div class="m-col-7">
                            <div class="field-input">
                              <MInput
                                v-model="vendor.PhoneNumber"
                                :placeholder="'Họ và tên'"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.PhoneNumber"
                              :placeholder="'Email'"
                            />
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-6">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.PhoneNumber"
                              :placeholder="'Số điện thoại'"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="right m-col-6">
                      <label for="" title="Đại diện theo pháp luật"
                        >Đại diện theo PL</label
                      >
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput :placeholder="'Đại diện theo pháp luật'" />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- roles tab -->
                  <div
                    class="tab-content"
                    style="display: block"
                    v-show="tabSelected == 'roles'"
                  >
                    <div class="row m-col-12">
                      <div class="m-col-3">
                        <label for="" title="Số điện thoại di động"
                          >Điều khoản thanh toán</label
                        >
                        <div class="field-input">
                          <MInput ref="PhoneNumber" />
                        </div>
                      </div>
                      <div class="m-col-3" style="padding-left: 6px">
                        <label for="" title="Số điện thoại cố định"
                          >Số ngày được nợ</label
                        >
                        <div class="field-input">
                          <MInput ref="LandlineNumber" />
                        </div>
                      </div>
                      <div class="m-col-3" style="padding-left: 6px">
                        <label for="" title="Địa chỉ email">Số nợ tối đa</label>
                        <div class="field-input">
                          <MInput ref="Email" />
                        </div>
                      </div>
                    </div>
                    <div class="row m-col-12">
                      <div class="m-col-3">
                        <label for="" title="Số điện thoại di động"
                          >Tài khoản công nợ phải trả</label
                        >
                        <div class="field-input">
                          <MInput ref="PhoneNumber" />
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- bank account -->
                  <div
                    class="tab-content"
                    style="display: block;height:220px"
                    v-show="tabSelected == 'banks'"

                  >
                    <div class="row m-col-12" style="height:calc(100% - 34px);overflow:auto">
                      <MTableEditable
                        :deleteFunc="true"
                        :colunms="colunmOfBankTab"
                        :checkBox="false"
                        :defaultData="bankTable"
                      />
                    </div>
                    <div class="row" style="padding-bottom:0px;position:sticky;bottom:1px;background:#fff;width:100%;z-index:20">
                      <div class="button-area">
                        <div class="add-row" @click="addRow">
                          <button>Thêm dòng</button>
                        </div>
                        <div class="delete-all" @click="deleteAllRow">
                          <button>Xóa hết dòng</button>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- addres tab -->
                  <div class="tab-content" v-show="tabSelected == 'address'">
                    <div class="left m-col-6">
                      <label for="">Vị trí địa lý</label>
                      <div class="row">
                        <div class="m-col-12 m-d-flex">
                          <div class="m-col-5" style="padding-right: 8px">
                            <div class="field-input">
                              <MInput
                                v-model="vendor.PhoneNumber"
                                :placeholder="'Xưng hô'"
                              />
                            </div>
                          </div>
                          <div class="m-col-7">
                            <div class="field-input">
                              <MInput
                                v-model="vendor.PhoneNumber"
                                :placeholder="'Họ và tên'"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.PhoneNumber"
                              :placeholder="'Email'"
                            />
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-6">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.PhoneNumber"
                              :placeholder="'Số điện thoại'"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="right m-col-6">
                      <label for="" title="Địa chỉ giao hàng"
                        >Địa chỉ giao hàng</label
                      >
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput :placeholder="'Đại diện theo pháp luật'" />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- addres tab -->
                  <div class="tab-content" v-show="tabSelected == 'note'">
                    <div class="m-col-12">
                        <MTextArea
                        :rows="11"
                        />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="bottom-line"></div>
          <div class="footer">
            <div class="footer-button">
              <div class="left">
                <div class="cancel-button">
                  <MButton @click="exitForm" :text="'Hủy'" ref="cancelButton" />
                </div>
              </div>
              <div class="right">
                <div class="save-button" style="margin: 0px 9.75px">
                  <MButton :text="'Cất'" />
                </div>
                <div class="add-save-button">
                  <MButton :buttonType="'primary'" :text="'Cất và Thêm'" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <ConfirmDialog
      :text="confirmDialogData.text"
      :type="confirmDialogData.type"
      :listButton="confirmDialogData.button"
      :align="confirmDialogData.align"
      @confirm="confirm"
      :keyConfirm="confirmDialogData.key"
      v-if="isConfirm"
    />
  </div>
</template>

<script>
//component
import MCheckBox from "@/components/base/BaseCheckBox.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import RadioButton from "@/components/base/BaseRadioButton.vue";
import MTextArea from "@/components/base/input/BaseTextArea.vue";
import MCombobox from "@/components/base/BaseComboBox.vue";
import MButton from "@/components/base/button/BaseButton.vue";
import MTableEditable from "@/components/base/table/BaseTableEditable.vue";

import ConfirmDialog from "@/components/base/dialog/BaseConfirmDialog.vue";
import "vue-datepicker-next/index.css";

export default {
  name: "VendorDetail",
  components: {
    MCheckBox,
    MInput,
    RadioButton,
    MTextArea,
    MCombobox,
    MButton,
    ConfirmDialog,
    MTableEditable,
  },
  data() {
    return {
      bankTable: [{ EmployeeCode: "45" }],
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
      //thông tin các cột cho tab nhập tài khoản ngân hàng
      colunmOfBankTab: [
        {
          id: "BankAccount",
          name: "Số tài khoản",
          displayName: "Số tài khoản",
          descriptionName: "Số tài khoản",
          width: "150px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
        {
          id: "BankName",
          name: "Tên ngân hàng",
          displayName: "Tên ngân hàng",
          descriptionName: "Tên ngân hàng",
          width: "auto",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
        {
          id: "BankBranch",
          name: "Chi nhánh",
          displayName: "Chi nhánh",
          descriptionName: "Chi nhánh",
          width: "100px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
        {
          id: "District",
          name: "Tỉnh/TP của ngân hàng",
          displayName: "Tỉnh/TP của ngân hàng",
          descriptionName: "Tỉnh/TP của ngân hàng",
          width: "100px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
      ],
      //data cho radiobutton loại nhà cũng cấp
      vendorTypeRadio: this.dataStorage.vendorDetail.radioButtonTypeVendor,
      //đối tượng nhà cung cấp
      vendor: {},
      //xác định tab được chọn
      tabSelected: "contact",
    };
  },
  methods: {
    /**
     * Mô tả : chọn chuyển tab form
     * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
     */
    selectTab(tabName) {
      try {
        this.tabSelected = tabName;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : thêm 1 dòng dữ liệu mới cho table
     * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
     */
    addRow() {
      try {
          this.bankTable.push({BankName:"abc"})
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : thêm 1 dòng dữ liệu mới cho table
     * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
     */
    deleteAllRow() {
      try {
          console.log(2);
          this.bankTable.splice(0,this.bankTable.length);
          this.bankTable.push({});
      } catch (error) {
        console.log(error);
      }
    },
    /**
    * Mô tả : đóng form chi tiết
    * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
    */
   exitForm(){
       try {
           this.$emit("exitForm",true);
       } catch (error) {
           console.log(error);
       }
   }
  },
};
</script>

<style >
@import url("@/css/components/dialog.css");
.content-dialog .form {
  overflow: auto !important;
}
.content-dialog .form .tab-form {
  width: calc(100% - 1px);
}
.content-dialog .form .tab-form .tab {
  display: flex;
  align-items: flex-end;
  height: 32px;
}
.content-dialog .form .tab-form .tab .tab-item {
  display: flex;
  align-items: center;
  border: 1px solid #babec5;
  border-bottom: none;
  margin-right: 4px;
  padding: 3px 8px;
  cursor: pointer;
  border-top-left-radius: 2px;
  border-top-right-radius: 2px;
}
.content-dialog .form .tab-form .tab .tab-item.selected {
  padding: 5px 8px;
  background-color: #cce4f9;
}
.content-dialog .form .tab-form .tab .tab-item:hover .text {
  color: #2ca01c;
}
.content-dialog .form .tab-form .tab .tab-item .text {
  text-align: center;
}
.content-dialog .form .tab-form .form-in-tab {
  min-height: 220px;
  border: 1px solid #babec5;
  width: 100%;
}
.content-dialog .form .tab-form .form-in-tab .tab-content {
  width: 100%;
  display: flex;
  padding: 10px;
}
.content-dialog .form {
  overflow: auto !important;
}
.tab-content .button-area {
  display: flex;
  align-items: center;
}
.tab-content .button-area .add-row {
  height: 24px;
  margin-right: 12px;
}
.tab-content .button-area button {
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
.tab-content .button-area button:hover {
  background-color: #eceef1;
}
.tab-content .button-area button:active {
  background-color: #bbb;
}
.tab-content .button-area .delete-all {
  height: 24px;
}
.m-d-flex {
  display: flex;
}

.hidden-left {
  display: none !important;
}
.hidden-right {
  display: none !important;
}
</style>
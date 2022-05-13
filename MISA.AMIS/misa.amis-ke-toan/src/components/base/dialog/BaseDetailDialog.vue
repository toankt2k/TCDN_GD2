<template>
  <div class="m-dialog" id="mDialog">
    <div class="dialog-info">
      <div class="dialog" id="dialog" ref="dialogDragable">
        <div class="tool">
          <div
            class="help-button"
            @mouseover="openTooltip($event, 'Giúp (F1)')"
            @mouseleave="closeTooltip"
          ></div>
          <div
            class="exit-button"
            title="Đóng (ESC)"
            @click="showExitConfirm"
          ></div>
        </div>
        <div class="title-dialog">
          <div class="title">Thông tin nhân viên</div>
          <div class="selection-type">
            <MCheckBox :rowId="'customer'" />
            <div class="text">Là khách hàng</div>
          </div>

          <div class="selection-type">
            <MCheckBox :rowId="'branch'" />
            <div class="text">Là nhà cung cấp</div>
          </div>
        </div>
        <div class="content-dialog">
          <div class="form">
            <div class="row">
              <div class="left m-col-6">
                <div class="row">
                  <div class="m-col-5">
                    <label for="" title="Mã nhân viên"
                      >Mã <span class="required">*</span></label
                    >
                    <div class="field-input">
                      <MInput
                        ref="EmployeeCode"
                        v-model="employee.EmployeeCode"
                      />
                    </div>
                  </div>
                  <div class="m-col-7" style="padding-left: 6px">
                    <label for="" title="Họ và tên nhân viên"
                      >Tên <span class="required">*</span></label
                    >
                    <div class="field-input">
                      <MInput
                        ref="EmployeeName"
                        v-model="employee.EmployeeName"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="">Đơn vị <span class="required">*</span></label>
                    <div class="field-input">
                      <ComboBox
                        ref="DepartmentId"
                        :defaultList="comboboxData.departmentCombobox.data"
                        :idProp="comboboxData.departmentCombobox.id"
                        :nameProp="comboboxData.departmentCombobox.name"
                        :apiData="comboboxData.departmentCombobox.api"
                        v-model="employee.DepartmentId"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="" title="Vị trí làm việc">Vị trí</label>
                    <div class="field-input">
                      <MInput
                        ref="PositionName"
                        v-model="employee.PositionName"
                      />
                    </div>
                  </div>
                </div>
              </div>
              <div class="right m-col-6">
                <div class="row">
                  <div class="m-col-5">
                    <label for="">Ngày sinh</label>
                    <div class="field-input">
                      <!-- <MInput v-model="employee.DateOfBirth" :type="'date'" /> -->
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
                  <div class="m-col-7" style="padding-left: 16px">
                    <label for="">Giới tính</label>
                    <div class="field-input">
                      <RadioButton ref="Gender" v-model="employee.Gender" />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-7">
                    <label for="" title="Số chứng minh nhân dân">Số CMND</label>
                    <div class="field-input">
                      <MInput
                        ref="IdentityNumber"
                        v-model="employee.IdentityNumber"
                      />
                    </div>
                  </div>
                  <div class="m-col-5" style="padding-left: 6px">
                    <label for="" title="Ngày cấp chứng minh nhân dân"
                      >Ngày cấp</label
                    >
                    <div class="field-input">
                      <!-- <MInput :type="'date'" v-model="employee.IdentityDate" /> -->
                      <!-- eslint-disable -->
                      <DatePicker
                        v-model:value="identifyDate"
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
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="" title="Nơi cấp chứng minh nhân dân"
                      >Nơi cấp</label
                    >
                    <div class="field-input">
                      <MInput
                        ref="IdentityPlace"
                        v-model="employee.IdentityPlace"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="m-col-12">
                <label for="">Địa chỉ</label>
                <div class="field-input">
                  <MInput ref="Address" v-model="employee.Address" />
                </div>
              </div>
            </div>
            <div class="row m-col-12">
              <div class="m-col-3">
                <label for="" title="Số điện thoại di động">ĐT di động</label>
                <div class="field-input">
                  <MInput ref="PhoneNumber" v-model="employee.PhoneNumber" />
                </div>
              </div>
              <div class="m-col-3" style="padding-left: 6px">
                <label for="" title="Số điện thoại cố định">ĐT cố định</label>
                <div class="field-input">
                  <MInput
                    ref="LandlineNumber"
                    v-model="employee.LandlineNumber"
                  />
                </div>
              </div>
              <div class="m-col-3" style="padding-left: 6px">
                <label for="" title="Địa chỉ email">Email</label>
                <div class="field-input">
                  <MInput ref="Email" v-model="employee.Email" />
                </div>
              </div>
            </div>
            <div class="row m-col-12">
              <div class="m-col-3">
                <label for="">Tài khoản ngân hàng</label>
                <div class="field-input">
                  <MInput v-model="employee.BankAccount" />
                </div>
              </div>
              <div class="m-col-3" style="padding-left: 6px">
                <label for="">Tên ngân hàng</label>
                <div class="field-input">
                  <MInput ref="TaxCode" v-model="employee.BankName" />
                </div>
              </div>
              <div class="m-col-3" style="padding-left: 6px">
                <label for="">Chi nhánh</label>
                <div class="field-input">
                  <MInput ref="WorkStatus" v-model="employee.BankBranch" />
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
                  <MButton :text="'Cất'" @click="saveEmployee" />
                </div>
                <div class="add-save-button">
                  <MButton
                    :buttonType="'primary'"
                    :text="'Cất và Thêm'"
                    @click="addEmployee"
                    @keydown="lastTabIndex"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <ToolTip
      :top="`${toolTip.top}px`"
      :left="`${toolTip.left}px`"
      :valueTooltip="toolTip.text"
      v-show="toolTip.isShowToolTip"
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
  </div>
</template>

<script>
//component
import MCheckBox from "@/components/base/BaseCheckBox.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import RadioButton from "@/components/base/BaseRadioButton.vue";
import ComboBox from "@/components/base/BaseComboBox.vue";
import MButton from "@/components/base/button/BaseButton.vue";
import ToolTip from "@/components/base/BaseToolTip.vue";
import DatePicker from "vue-datepicker-next";
import ConfirmDialog from "@/components/base/dialog/BaseConfirmDialog.vue";
import "vue-datepicker-next/index.css";
//lib
import axios from "axios";
import format from "@/js/lib/formatContent.js";
import validator from "@/js/lib/validateField.js";
import Helpers from "@/js/lib/helpers.js";
// import $ from 'jquery';
// import 'jquery-ui/ui/widgets/dialog.js';

export default {
  name: "DetailDialog",
  components: {
    MCheckBox,
    MInput,
    RadioButton,
    ComboBox,
    MButton,
    ToolTip,
    DatePicker,
    ConfirmDialog,
  },
  props: {
    //xác định xem là form thêm hay sửa
    isAdd: {
      type: Boolean,
      default: true,
    },
    //id nhân viên cần sửa
    id: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      /**
       * Mô tả : đối tượng nhân viên
       * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
       */
      employee: {},
      //kiểm tra thay đổi của form
      oldEmployee: {},
      /**
       * Mô tả : combobox data lưu trữ các thuộc tính của các combobox dùng trong component
       * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
       */
      comboboxData: {
        /**
         * Mô tả : data các thuộc tính của combobox giới tính, lấy từ dataStorage
         * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
         */
        genderCombobox: this.dataStorage.genderCombobox,
        /**
         * Mô tả : data các thuộc tính của combobox Phòng ban, lấy từ dataStorage
         * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
         */
        departmentCombobox: this.dataStorage.departmentCombobox,
      },
      //thông tin tooltip
      toolTip: {
        top: 0,
        left: 0,
        isShowToolTip: false,
        text: "",
      },
      //show dialog confirm
      isConfirm: false,
      //thông tin dialog confirm
      confirmDialogData: {},
      //danh sách các trường input bị lỗi
      listInputError: [],
      //trạng thái có đang gọi api không
      isBusy: false,
      //top
      top: 0,
      left: 0,
    };
  },
  /**
   * Mô tả : khai báo và khởi tạo các hàm dùng trong component
   * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
   */
  methods: {
    /**
     * Mô tả : thêm mới nhân viên và đóng form
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    async saveEmployee() {
      //nếu đang gọi api thì không được thực hiện hàm này
      if (this.isBusy) return;
      let me = this;
      //validate dữ liệu
      this.listInputError = this.validate(me.employee);
      if (this.listInputError.length > 0) {
        let data = {
          name: "emptyError",
          button: this.resource.confirmDialogData.errorInputConfirm,
          align: "center",
          type: "error",
          text: `${this.listInputError[this.listInputError.length - 1].name}`,
          key: "emptyError",
        };
        this.openConfirm(data);
        return false;
      }
      try {
        me.isBusy = true;
        me.employee.DateOfBirth = format.setTimeZone(me.employee.DateOfBirth);
        me.employee.IdentityDate = format.setTimeZone(me.employee.IdentityDate);
        if (me.isAdd) {
          await axios({
            method: "POST",
            url: this.dataStorage.api.addEmployee,
            data: me.employee,
          })
            .then((res) => {
              console.log(res);
              if (res.status == 201) {
                //thêm toast vào danh sách toast
                me.addToast(this.resource.toastMessage.addSuccess);
                me.isBusy = false;
                this.exitForm();
              }
            })
            .catch((res) => {
              if (res.response.status == 400) {
                me.showErrorResponse(res);
              } else {
                console.error(res);
              }
              me.isBusy = false;
              // this.exitForm();
            });
        } else {
          await axios({
            method: "PUT",
            url: `${this.dataStorage.api.updateEmployee}/${me.employee.EmployeeId}`,
            data: me.employee,
          })
            .then((res) => {
              console.log(res);
              if (res.status == 200) {
                //thêm toast vào danh sách toast
                me.addToast(this.resource.toastMessage.editSuccess);
                me.isBusy = false;
                this.exitForm();
              }
            })
            .catch((res) => {
              //lỗi 400 trả về khi validate kiểu dữ liệu
              if (res.response.status == 400) {
                me.showErrorResponse(res);
              } else {
                console.error(res.response);
              }
              me.isBusy = false;
            });
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : trả về ngày lớn hơn ngày hiện tại
     * @param date tự dộng truyền dùng riêng date-picker-next
     * Created by: Nguyễn Đức Toán - MF1095 (21/04/2022)
     */
    afterToday(date) {
      return date > new Date();
    },
    /**
     * Mô tả : thêm mới 1 nhân viên lưu vào db và reset forrm thêm mới
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    async addEmployee() {
      if (this.isBusy) return;
      let me = this;
      this.listInputError = this.validate(me.employee);
      if (this.listInputError.length > 0) {
        let data = {
          name: "emptyError",
          button: [
            {
              id: 1,
              name: "Đóng",
              type: "primary",
              align: "center",
            },
          ],
          align: "center",
          type: "error",
          text: `${this.listInputError[this.listInputError.length - 1].name}`,
          key: "emptyError",
        };
        this.openConfirm(data);
        return false;
      }
      try {
        me.isBusy = true;
        me.employee.DateOfBirth = format.setTimeZone(me.employee.DateOfBirth);
        me.employee.IdentityDate = format.setTimeZone(me.employee.IdentityDate);
        if (me.isAdd) {
          await axios({
            method: "POST",
            url: this.dataStorage.api.addEmployee,
            data: me.employee,
          })
            .then((res) => {
              console.log(res);
              if (res.status == 201) {
                //thêm toast vào danh sách toast
                me.addToast(this.resource.toastMessage.addSuccess);
                //reset form
                me.employee = {};
                me.getNewEmployeeCode();
                me.isBusy = false;
              }
            })
            .catch((res) => {
              //lỗi 400 trả về khi validate kiểu dữ liệu
              if (res.response.status == 400) {
                me.showErrorResponse(res);
              } else {
                console.error(res);
              }
              me.isBusy = false;
            });
        } else {
          await axios({
            method: "PUT",
            url: `${this.dataStorage.api.updateEmployee}/${me.employee.EmployeeId}`,
            data: me.employee,
          })
            .then((res) => {
              console.log(res);
              if (res.status == 200) {
                //thêm toast vào danh sách toast
                me.addToast(this.resource.toastMessage.editSuccess);
                //reset form
                me.employee = {};
                me.getNewEmployeeCode();
                me.isBusy = false;
              }
            })
            .catch((res) => {
              //lỗi 400 trả về khi validate kiểu dữ liệu
              if (res.response.status == 400) {
                me.showErrorResponse(res);
              } else {
                console.error(res.response);
              }
              me.isBusy = false;
            });
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.$emit("setAddForm", true);
      }
    },
    /**
     * Mô tả : hiện confirm dialog khi có response trả về
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    showErrorResponse(res) {
      let key = Object.keys(res.response.data.data)[0];
      // /* eslint-disable */
      // debugger;
      this.$refs[key].setErrorFocus(res.response.data.data[key]);
      this.listInputError.push({
        key: key,
        name: res.response.data.data[key],
      });
      let data = {
        name: "badRequest",
        button: this.resource.confirmDialogData.errorResponse,
        align: "right",
        type: "warning",
        text: res.response.data.data[key],
        key: "badRequest",
      };
      this.openConfirm(data);
    },
    /**
     * Mô tả : validate employee trc khi gửi lên server
     * @param {*} employee đối tượng validate
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 15:02 19/04/2022
     */
    validate(employee) {
      try {
        let listError = [];
        //validate định dạng Email
        if (employee.Email) {
          if (!validator.email(employee.Email)) {
            this.$refs.Email.setErrorFocus(
              this.resource.confirmDialogData.errorInputMsg.email.name
            );
            listError.push(this.resource.confirmDialogData.errorInputMsg.email);
          }
        }
        //validate định dạng Email
        if (employee.PhoneNumber) {
          if (!validator.phone(employee.PhoneNumber)) {
            this.$refs.PhoneNumber.setErrorFocus(
              this.resource.confirmDialogData.errorInputMsg.phoneNumber.name
            );
            listError.push(
              this.resource.confirmDialogData.errorInputMsg.phoneNumber
            );
          }
        }
        //validate mã phòng ban trống
        if (!employee.DepartmentId) {
          this.$refs.DepartmentId.setErrorFocus(
            this.resource.confirmDialogData.errorInputMsg.departmentId.name
          );
          listError.push(
            this.resource.confirmDialogData.errorInputMsg.departmentId
          );
        }
        //validate tên trống
        if (!employee.EmployeeName) {
          this.$refs.EmployeeName.setErrorFocus(
            this.resource.confirmDialogData.errorInputMsg.employeeName.name
          );
          listError.push(
            this.resource.confirmDialogData.errorInputMsg.employeeName
          );
        }
        //validate mã trống
        if (!employee.EmployeeCode) {
          this.$refs.EmployeeCode.setErrorFocus(
            this.resource.confirmDialogData.errorInputMsg.employeeCode.name
          );
          listError.push(
            this.resource.confirmDialogData.errorInputMsg.employeeCode
          );
        }
        return listError;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : load lại table
     * Created by: Nguyễn Đức Toán - MF1095 (16/04/2022)
     */
    loadTable() {
      try {
        this.$emit("loadTable");
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : khi người dùng nhấn nút X hoặc nút hủy thì confirm hiện lên
     * Created by: Nguyễn Đức Toán - MF1095 (19/04/2022)
     */
    showExitConfirm() {
      try {
        //departmentId tự động gán
        this.oldEmployee.DepartmentId = this.employee.DepartmentId;
        let temp = this.checkEmployeeChange();
        if (temp) {
          let data = {
            name: "exit",
            button: this.resource.confirmDialogData.exitConfirm,
            type: "info",
            text: `Dữ liệu đã bị thay đổi, Bạn có muốn cất không?`,
            key: "exit",
          };
          this.openConfirm(data);
        } else {
          // chọn không thì thoát form
          this.exitForm();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : kiểm tra employee có thay đổi khi thoát form
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    checkEmployeeChange() {
      // chuyển qua chuỗi json để so sánh
      let newEmployee = JSON.stringify(this.employee);
      let oldEmployee = JSON.stringify(this.oldEmployee);
      return !(newEmployee === oldEmployee);
    },
    /**
     * Mô tả : nhận xác nhân từ confirm
     * Created by: Nguyễn Đức Toán - MF1095 (19/04/2022)
     */
    confirm(key, confirm) {
      try {
        let me = this;
        // kiểm tra xem chọn button gì
        switch (key) {
          case "exit": //thoat
            switch (confirm) {
              case this.resource.confirmDialogData.exitConfirm[1].id:
                //chọn có thì thêm hoặc lưu
                this.saveEmployee();
                // this.exitConfirm();
                break;
              case this.resource.confirmDialogData.exitConfirm[2].id:
                // chọn không thì thoát form
                this.exitForm();
                break;
              case this.resource.confirmDialogData.exitConfirm[0].id:
                // chọn hủy thì thoat confirm
                this.exitConfirm();
                break;
              default:
                this.exitConfirm();
                break;
            }
            break;
          case "emptyError": //chọn xóa
            switch (confirm) {
              case this.resource.confirmDialogData.errorInputConfirm[0].id:
                this.exitConfirm();
                this.listInputError.forEach((element) => {
                  me.$refs[element.key].setErrorFocus(element.name);
                });
                me.listInputError = [];
                break;
              default:
                this.exitConfirm();
                break;
            }
            break;
          case "badRequest": //chọn xóa
            switch (confirm) {
              case this.resource.confirmDialogData.errorResponse[0].id:
                this.exitConfirm();
                this.listInputError.forEach((element) => {
                  me.$refs[element.key].setErrorFocus(element.name);
                });
                me.listInputError = [];
                break;
              default:
                this.exitConfirm();
                break;
            }
            break;
          default:
            break;
        }
        // this.exitConfirm();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : khi người dùng xác nhân đóng thì form sẽ đc đóng
     * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
     */
    exitForm() {
      try {
        //load lại table
        this.loadTable();
        this.$emit("exit", false);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả : lấy mã nhân viên mới nhất trong database
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    getNewEmployeeCode() {
      let me = this;
      try {
        axios({
          method: "GET",
          url: this.dataStorage.api.getNewEmployeeCode,
        })
          .then((res) => {
            console.log(res);
            if (res.status == 200) {
              //gán newEmployeeCode cho employee.EmployeeCode
              me.employee.EmployeeCode = res.data;
              // gán Gender mặc định là 1(nam)
              me.employee.Gender = "1";
              //gán newEmployeeCode cho employee.EmployeeCode
              me.oldEmployee = {};
              me.oldEmployee.EmployeeCode = res.data;
              // gán Gender mặc định là 1(nam)
              me.oldEmployee.Gender = "1";
              //set focus cho input employeecode
              me.$refs.EmployeeCode.setFocus(true);
            }
          })
          .catch((res) => {
            console.error(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : emit gọi hàm thêm toastmessage
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    addToast(toast) {
      try {
        //add toastmsg
        this.$emit("addToast", toast);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : hiện tooltip
     * Created by: Nguyễn Đức Toán - MF1095 (19/04/2022)
     */
    openTooltip(e, text) {
      try {
        this.toolTip = {
          top: e.clientY + 10,
          left: e.clientX + 10,
          isShowToolTip: true,
          text: text,
        };
      } catch (error) {
        console.log(error);
      }
    },
    //đóng tooltip
    closeTooltip() {
      this.toolTip = {
        top: 0,
        left: 0,
        isShowToolTip: false,
        text: "",
      };
    },
    /**
     * Mô tả : mở dialog confirm
     * @param data thông tin về dialog vd:let data = {
            button: [
              {
                id: 1,
                name: "Hủy",
                type: "default",
                align: "left",
              },
            ],
            type: "info",
            text: `thông báo hiển thị`,
            key: "exit",
          };
     * Created by: Nguyễn Đức Toán - MF1095 (19/04/2022)
     */
    openConfirm(data) {
      try {
        this.confirmDialogData = data;
        this.isConfirm = true;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : exit confirm
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    exitConfirm() {
      try {
        this.isConfirm = false;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy employee từ id đc truyền vào
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 21:10 21/04/2022
     */
    getEmployee() {
      let me = this;
      axios({
        method: "GET",
        url: `${this.dataStorage.api.getById}/${me.id}`,
      })
        .then((res) => {
          //nếu không có lỗi
          if (res.status == 200) {
            //format lại ngày sinh sang Date()
            res.data.DateOfBirth = new Date(
              !res.data.DateOfBirth ? "" : res.data.DateOfBirth
            );
            //format lại ngày cấp CMND sang Date()
            res.data.IdentityDate = new Date(
              !res.data.IdentityDate ? "" : res.data.IdentityDate
            );
            //format lại ngày tham gia sang Date()
            res.data.JoinDate = new Date(
              !res.data.JoinDate ? "" : res.data.JoinDate
            );
            //format lại lương có dấu chấm sau hàng nghìn
            res.data.Salary = format.salaryFormat(res.data.Salary);
            // gán data cho employee
            me.employee = res.data;
            //set giá trị cho oldEmployee
            Object.assign(me.oldEmployee, res.data);
          }
        })
        .catch((res) => {
          console.error(res);
        });
    },
    /**
     * Mô tả : sự kiện khi phím được nhẫn xuống
     * Created by: Nguyễn Đức Toán - MF1095 (25/04/2022)
     */
    keyDownListenner(e) {
      try {
        //nhấn ESC bỏ qua sự kiện mặc địn của trình duyệt
        if (e.keyCode == 27) {
          e.preventDefault();
        }
        //Ctrl + S bỏ qua sự kiên mặc định của trình duyệt
        if (e.keyCode == 83 && e.ctrlKey == true) {
          e.preventDefault();
        }
      } catch (error) {
        console.log(error);
      }
    },
    keyUpListenner(e) {
      try {
        //nếu nhấn ESC thì exit form
        if (e.keyCode == 27) {
          this.showExitConfirm();
        }
        //Ctrl + S
        if (e.keyCode == 83 && e.ctrlKey == true) {
          console.log("ok");
          this.saveEmployee();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : sự kiện khi tab vào index cuối cung thì focus trở lại index đầu
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    lastTabIndex(e) {
      try {
        if (e.keyCode === 9) {
          e.preventDefault();
          this.$refs.EmployeeCode.setFocus(true);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  created() {
    try {
      if (!this.isAdd || this.id != "") {
        this.getEmployee();
      }
    } catch (error) {
      console.log(error);
    }
  },
  computed: {
    //format đinh dạng ngày tháng cho dateofbirth
    dateOfbirth: {
      set(newVal) {
        try {
          this.employee.DateOfBirth = newVal.toDateString();
        } catch (error) {
          console.log(error);
        }
      },
      get() {
        try {
          if (!this.employee.DateOfBirth) return;
          return new Date(this.employee.DateOfBirth);
        } catch (error) {
          console.log(error);
          return "";
        }
      },
    },
    //format đinh dạng ngày tháng cho identifydate
    identifyDate: {
      set(newVal) {
        try {
          this.employee.IdentityDate = newVal.toDateString();
        } catch (error) {
          console.log(error);
        }
      },
      get() {
        try {
          if (!this.employee.IdentityDate) return;
          return new Date(this.employee.IdentityDate);
        } catch (error) {
          console.log(error);
          return "";
        }
      },
    },
  },
  /**
   * Mô tả : khi dialog hiện lên thì set focus cho employeeCode, set phím tắt
   * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
   */
  mounted() {
    try {
      //set focus cho input employeecode
      this.$refs.EmployeeCode.setFocus(true);
      if (this.isAdd) {
        this.getNewEmployeeCode();
      }
      // sự kiện khi kết thúc nhấn phím
      window.addEventListener("keyup", this.keyUpListenner);
      //sự kiệ khi nút đcược nhấn
      window.addEventListener("keydown", this.keyDownListenner);
      //sự kiện khi nut cuối cùng đc focus
      // let me = this;
      // this.$refs.cancelButton.$el.addEventListener('focus',function(){
      //   me.$refs.EmployeeCode.setFocus(true);
      // },true)

      Helpers.draggable("#dialog");
    } catch (error) {
      console.log(error);
    }
  },
  /**
   * Mô tả : xóa sự kiện khi component unmount
   * Created by: Nguyễn Đức Toán - MF1095 (25/04/2022)
   */
  unmounted() {
    // sự kiện khi kết thúc nhấn phím
    window.removeEventListener("keyup", this.keyUpListenner);
    //sự kiệ khi nút đcược nhấn
    window.removeEventListener("keydown", this.keyDownListenner);
  },
};
</script>

<style scoped>
@import url("@/css/components/dialog.css");
#dialog:hover {
  cursor: grab;
}
#dialog:active {
  cursor: grabbing;
}
</style>
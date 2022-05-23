<template>
  <div class="m-table">
    <div class="table-content">
      <table>
        <thead>
          <tr>
            <th class="hidden-left"></th>
            <th class="table-checkbox" style="width: 40px" v-if="checkBox">
              <MCheckBox />
            </th>
            <th
              v-for="(col, index) in displayColumns"
              :key="'col' + index"
              :style="{
                minWidth: col.minWidth,
                width: `${col.width.replace(/\D/g, '') + 'px'}`,
                textAlign: col.align,
              }"
              :title="col.descriptionName"
              :class="col.classList"
            >
              {{ col.displayName }}
            </th>
            <th
              class="table-function"
              v-if="deleteFunc"
              style="min-width: 15px; width: 15px"
            ></th>
            <th
              class="hidden-right"
              style="background-color: #fff !important"
            ></th>
          </tr>
        </thead>

        <!-- <tbody v-show="!listEmployee.length > 0 && isLoading">
          <tr
            v-for="index in listEmployee.length > 0 ? listEmployee.length : 12"
            :key="index"
          >
            <td class="hidden-left"></td>
            <td class="table-checkbox">
              <div class="loading-1" style="width: 18px"></div>
            </td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td><div class="loading-1"></div></td>
            <td class="table-function" style="display: table-cell">
              <div
                class="table-function-button"
                style="display: block; padding: 9px 0px"
              >
                <div class="loading-1"></div>
              </div>
            </td>
            <td class="hidden-right"></td>
          </tr>
        </tbody>
        <tbody v-show="!listEmployee.length > 0 && !isLoading">
          <div class="no-content">
            <img src="@/assets/no-employee.svg" />
            <div class="no-content-text">Không có dữ liệu</div>
          </div>
        </tbody>
        <tbody ref="tbody" id="mainTable" v-show="listEmployee.length > 0">
          <tr
            :class="{ checked: val.checked }"
            v-for="(val, index) in listEmployee"
            :key="index"
            @dblclick="openForm(false, val)"
          >
            <td class="hidden-left"></td>
            <td class="table-checkbox">
              <MCheckBox v-model="val.checked" :rowId="`r${index}`" />
            </td>
            <td :title="val.EmployeeCode">
              <div class="overflow-content">
                {{ val.EmployeeCode }}
              </div>
            </td>
            <td :title="val.EmployeeName">
              <div class="overflow-content">
                {{ val.EmployeeName }}
              </div>
            </td>
            <td :title="val.GenderName">
              <div class="overflow-content">{{ val.GenderName }}</div>
            </td>
            <td :title="dateFormat(val.DateOfBirth)" class="m-text-center">
              <div class="overflow-content">
                {{ dateFormat(val.DateOfBirth) }}
              </div>
            </td>
            <td :title="val.IdentityNumber">
              <div class="overflow-content">
                {{ val.IdentityNumber }}
              </div>
            </td>
            <td :title="val.PositionName">
              <div class="overflow-content">
                {{ val.PositionName }}
              </div>
            </td>
            <td :title="val.DepartmentName">
              <div class="overflow-content">{{ val.DepartmentName }}</div>
            </td>
            <td :title="val.BankAccount">
              <div class="overflow-content">{{ val.BankAccount }}</div>
            </td>
            <td :title="val.BankName">
              <div class="overflow-content">{{ val.BankName }}</div>
            </td>
            <td :title="val.BankBranch">
              <div class="overflow-content">
                {{ val.BankBranch }}
              </div>
            </td>
            <td class="table-function">
              <div class="table-function-button">
                <div class="button-edit" @click="openForm(false, val)">Sửa</div>
                <div class="arrow-down-button">
                  <div
                    class="icon"
                    @click="tableOption($event, val, index)"
                  ></div>
                </div>
              </div>
            </td>
            <td class="hidden-right"></td>
          </tr>
        </tbody> -->

        <tbody ref="tbody" id="mainTable">
          <tr v-for="(item, ind) in tableData" :key="'row' + ind">
            <td
              class="hidden-left"
              style="background-color: #fff !important"
            ></td>
            <td class="table-checkbox" v-if="checkBox">
              <MCheckBox />
            </td>
            <td
              v-for="(col, index) in displayColumns"
              :key="'rcol' + index"
              @click="setEdit($event, `ip${ind + col.id}`, ind,col)"
              :title="item[col.id]"
            >
              <div v-show="editRow == ind && col.edit">
                <MInput
                  v-if="!col.component"
                  :ref="`ip${ind + col.id}`"
                  v-model="item[col.id]"
                  :align="col.align"
                  :type="col.type"
                  @input="getDataTable"
                />
                <component
                  v-if="col.component"
                  :is="col.componentData.name"
                  v-bind="col.componentData.prop"
                  v-model="item[col.id]"
                ></component>
              </div>
              <div
                v-show="editRow != ind || !col.edit"
                :style="{ textAlign: col.align }"
              >
                {{ dataFormat(item[col.id], col.type) }}
              </div>
            </td>
            <td class="table-function" v-if="deleteFunc">
              <div
                class="table-function-button ic-delete"
                @click="deleteRow(ind)"
              ></div>
            </td>
            <td
              class="hidden-right"
              style="background-color: #fff !important"
            ></td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <td class="hidden-left"></td>
            <td class="table-checkbox" v-if="checkBox"></td>
            <td
              v-for="(col, index) in displayColumns"
              class="m-text-right count"
              :key="'rcol' + index"
            >
              {{ col.count ? dataFormat(0, "number") : "" }}
            </td>
            <td class="table-function" v-if="deleteFunc">
              <div class="table-function-button"></div>
            </td>
            <td class="hidden-right"></td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</template>

<script>
//import component
import MCheckBox from "@/components/base/BaseCheckBox.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import MCombobox from "@/components/base/BaseComboBox.vue";
//import lib
import format from "@/js/lib/formatContent.js";

export default {
  name: "BaseTable",
  components: {
    MCheckBox,
    MInput,
    MCombobox,
  },
  props: {
    defaultData: {
      type: Array,
      default: () => [],
    },
    //các côt jđược hiển thị trên bảng
    columns: {
      type: Array,
      default: () => [],
    },
    //có chwucs năng xóa hay không
    deleteFunc: {
      type: Boolean,
      default: false,
    },
    //có check box hay không
    checkBox: {
      type: Boolean,
      default: true,
    },
  },
  data() {
    return {
      //xác định hàng nào trong bảng đang được sửa
      editRow: -1,
      //danh sách các cột được hiển thị
      displayColumns: this.columns,
      tableData: this.defaultData,
      test: false,
      //show detailDialog
      infoDialog: false,
      //xác định là dialog them hay sửa
      isAdd: true,
      //X,Y là tọa độ top,left của dropdown chức năng
      X: 0,
      Y: 0,
      maxHeight: 0,
      isShow: false,
      functionDropdown: [],
      /**
       * Mô tả : listEmployee làdanh sách nhân viên hiện lên bảng
       * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
       */
      listEmployee: [],
      //Mô tả : mã id nhân viên đang được chọn
      selectedId: "",
      //mã nhân viên được chọn
      selectedCode: "",
      confirmDialogData: {},
      //nếu check all đc check thì sẽ check tất cả các check trong bảng
      checkAll: false,
      isConfirm: false,
      //sự kiện delay khi nhập searchtext
      delayTimer: null,
      //hiện loading table
      isLoading: false,
      //cho phep thực hiện xóa nhiều
      multiFlag: false,
    };
  },
  /**
   * Mô tả : khởi tạo và định nghĩa các hàm dùng trong component này
   * Created by: Nguyễn Đức Toán - MF1095 (07/04/2022)
   */
  methods: {
    getDataTable() {
      try {
        this.$emit("getData", this.tableData);
      } catch (error) {
        console.log(error);
      }
    },

    setEdit(e, refOfTagetInput, indexOfColunm, col) {
      try {
        e.preventDefault();
        this.editRow = indexOfColunm;
        if(col.component)return;
        this.$nextTick(() => {
          this.$refs[refOfTagetInput][0].setFocus(true);
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : sau khi nhập searchtext thì delay1s
     * Created by: Nguyễn Đức Toán - MF1095 (21/04/2022)
     */
    delaySearch() {
      let me = this;
      clearTimeout(this.delayTimer);
      this.delayTimer = setTimeout(function () {
        me.pagingOption.currentPage = 1;
        me.loadTable();
      }, 500);
    },

    /**
     * Mô tả : bắt emit từ confirm dialog
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    confirmDelete(data, confirm) {
      // kiểm tra xem chọn button gì
      switch (data) {
        case "delete": //xóa
          switch (confirm) {
            case this.resource.confirmDialogData.delete[1].id:
              this.deleteEmployee();
              break;
            case this.resource.confirmDialogData.delete[0].id:
              this.exitConfirm();
              break;
            default:
              this.exitConfirm();
              break;
          }
          break;
        case "multiDelete": //chọn xóa
          switch (confirm) {
            case this.resource.confirmDialogData.deleteMultiple[1].id:
              this.deleteMultiEmployee();
              break;
            case this.resource.confirmDialogData.deleteMultiple[0].id:
              this.exitConfirm();
              break;
            default:
              this.exitConfirm();
              break;
          }
          break;
        default:
          break;
      }
      this.selectedId = "";
      this.selectedCode = "";
      this.exitConfirm();
    },

    /**
     * Mô tả : đóng form thông tin nhân viên
     * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
     */
    exitForm() {
      try {
        this.infoDialog = false;
        this.selectedId = "";
        this.selectedCode = "";
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : bắt emit từ dropdown
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    selectDropdown(key) {
      console.log(key);
      switch (key) {
        case "multiDelete":
          this.confirmDialogData = {
            name: "xóa",
            button: this.resource.confirmDialogData.deleteMultiple,
            type: "warning",
            text: this.resource.confirmDialogData.getMultiDeleteConfirm(),
            key: "multiDelete",
          }; //global property
          this.isConfirm = true;
          break;
        case "delete": //chọn xóa 1 nhân
          this.confirmDialogData = {
            name: "xóa",
            button: this.resource.confirmDialogData.delete,
            type: "warning",
            text: this.resource.confirmDialogData.getDeleteConfirm(
              this.selectedCode
            ),
            key: "delete",
          };
          this.isConfirm = true;
          break;
        case "duplicate":
          this.openForm(true, { EmployeeId: this.selectedId });
          break;
        default:
          break;
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
     * Mô tả : mở form thông tin nhân viên
     * @param {*} type true/false xác định form thêm(true) hay sửa(false);
     * @param {*} val đối tượng employee đã chọn;
     * Created by: Nguyễn Đức Toán - MF1095 (16/04/2022)
     */
    openForm(type, val) {
      this.isAdd = type;
      if (val) {
        this.selectedId = val.EmployeeId;
      } else {
        this.selectedId = "";
      }
      this.infoDialog = true;
    },

    /**
     * Mô tả : emit gọi hàm thêm toastmessage
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    addToast(toast) {
      try {
        this.$emit("addToast", toast);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : bắt sự kiện click chuột vào nút sửa để hiện ra dropdown lựa chọn trong table
     * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
     */
    tableOption(e, val, index) {
      try {
        let tagetElement = e.target.getBoundingClientRect();
        //dừng sự kiện click khác
        e.stopPropagation();
        this.functionDropdown = this.resource.dropdownData.tableFunction;
        //ngăn sự kiện click khác
        // e.preventDefault();
        //hiện dropdown
        this.isShow = true;
        //lấy độ cao màn hình
        this.maxHeight = e.view.innerHeight;
        this.X = tagetElement.left;
        this.Y = tagetElement.top + 10;
        //set selected row
        this.$el.querySelectorAll(`tbody#mainTable tr`).forEach((element) => {
          element.classList.remove("selected");
        });
        this.$el
          .querySelector(`tbody#mainTable tr:nth-child(${index + 1})`)
          .classList.add("selected");
        //id nhân viên nđược chọn
        this.selectedId = val.EmployeeId;
        //mã nhân viên được chọn
        this.selectedCode = val.EmployeeCode;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : get dữ liệu khi cất
     * Created by: Nguyễn Đức Toán - MF1095 (23/04/2022)
     */
    getData() {
      try {
        return this.tableData;
      } catch (error) {
        console.log(error);
      }
    },
    deleteRow(ind) {
      try {
        this.tableData.splice(ind, 1);
        this.getDataTable();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : bắt sự kiện click chuột vào nút sửa để hiện ra dropdown lựa chọn thực hiện hàng loạt
     * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
     */
    multiFunction(e) {
      if (!this.multiFlag) return;
      try {
        //dừng sự kiện click khác
        e.stopPropagation();
        //ngăn sự kiện click khác
        // kiểm tra sô dòng được chọn
        let count = 0;
        this.listEmployee.forEach((element) => {
          if (element.checked == true) {
            count++;
          }
        });
        //lất element đc click
        let elementTarget = this.$refs.multiAction.$el.getBoundingClientRect();
        // e.preventDefault();
        //nếu sso dòng được chọn lớn hơn 0
        if (count > 0) {
          this.functionDropdown = this.resource.dropdownData.multipleFunction;
        } else {
          this.functionDropdown = [];
        }
        //lấy độ cao màn hình
        this.maxHeight = e.view.innerHeight;
        this.X = elementTarget.left + 130;
        this.Y = elementTarget.top + elementTarget.height + 4;
        //hiện dropdown
        this.isShow = true;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : bắt sự kiện phím tắt nhập vào
     * *******1. ctrl + S submit form thông tin
     * @param
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 12:08 02/04/2022
     */
    handleKeyUp() {
      // console.log(e);
    },
    handleKeyDown() {
      // console.log(e);
    },
    /**
     * Mô tả : click ra ngoài dropdown list
     * Created by: Nguyễn Đức Toán - MF1095 (10/04/2022)
     */
    clickOutSide() {
      this.isShow = false;
    },
    /**
     * Mô tả : fomat data sang date
     * @param data dữ liệu cần format
     * @param type kiểu dữ liệu format
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    dataFormat(data, type) {
      try {
        switch (type) {
          case "text": //kiểu dữ liệu dạng text
            return format.textFormat(data);
          case "number": //kiểu dữ liệu dạng số
            return format.numberVNFormat(data);
          case "currency": //kiểu dữ liệu dạng số
            return format.currencyVNFormat(data);
          case "date": //kiểu dữ liệu dạng ngày
            return format.dateFormat(data);
          default:
            return "";
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : gán lai jdanh sách cho bảng
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    setData(data) {
      try {
        if (data) {
          this.tableData = data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : gán lai jdanh sách cho bảng
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    setColumns(columns) {
      try {
        if (columns) {
          this.displayColumns = columns;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    defaultData(newVal, old) {
      try {
        if (newVal != old) this.tableData = newVal;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả : kiểm tra nếu chọn nhiều hơn 1 em ployee thì cho phép dùng chức năng xóa nhiều
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    listEmployee: {
      deep: true,
      handler(newVal) {
        let count = 0;
        newVal.forEach((val) => {
          if (val.checked) {
            count++;
          }
        });
        let target = this.$refs.multiAction;
        if (target) {
          if (count < 1) {
            target.$el.classList.add("disable-click");
            //k cho phép click
            this.multiFlag = false;
          } else {
            target.$el.classList.remove("disable-click");
            //cho phep click
            this.multiFlag = true;
          }
        }
      },
    },
    /**
     * Mô tả : set chek all cho check box
     * @param
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 13:07 16/04/2022
     */
    checkAll(newVal) {
      try {
        //nếu check all được check thì check tất cả check box và ngược lại
        this.listEmployee.forEach((ele) => {
          ele.checked = newVal;
        });
      } catch (error) {
        console.log(error);
      }
    },
  },
  /**
   * Mô tả : sau khi tạo compnent thì lấy dữu liệu nhân viên từ api
   * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
   */
  created() {},
};
</script>

<style scoped>
@import url("@/css/components/table.css");
.table-function {
  right: 24px !important;
}
.table-checkbox {
  left: 24px !important;
}
.m-table {
  margin: 0 !important;
  width: 100% !important;
  overflow: unset;
}
table {
  padding-bottom: 10px;
}
tfoot .count {
  font-weight: 600;
}
.hidden-left {
  width: 24px;
  min-width: 24px;
}
.hidden-right {
  width: 24px;
  min-width: 24px;
}
.table-function {
  right: 0px !important;
}
@-webkit-keyframes moving-gradient {
  0% {
    background-position: -250px 0;
  }
  100% {
    background-position: 250px 0;
  }
}
.loading-1 {
  height: 18px;
  background: linear-gradient(to right, #eee 20%, #ddd 50%, #eee 80%);
  background-size: 500px 100px;
  animation-name: moving-gradient;
  animation-duration: 0.5s;
  animation-iteration-count: infinite;
  animation-timing-function: linear;
  animation-fill-mode: forwards;
}
.no-content {
  width: 100%;
  /* height: 132px; */
  position: absolute;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}
.no-content .no-content-text {
  padding: 10px;
}
.no-content > img {
  height: 100px;
  padding: 10px;
}
.disable-click {
  background-color: #fff;
  /* pointer-events: none; */
  cursor: not-allowed !important;
}
.disable-click:hover {
  background-color: #fff;
}
.disable-click .container:hover {
  pointer-events: none;
  background-color: #fff !important;
}
.v-enter-active,
.v-leave-active {
  transition: opacity 0.5s ease-in-out;
}
@keyframes identifier {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
.v-enter-from,
.v-leave-to {
  opacity: 0;
}
</style>
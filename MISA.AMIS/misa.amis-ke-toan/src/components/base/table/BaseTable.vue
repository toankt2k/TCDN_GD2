<template>
  <div class="m-table">
    <div class="table-content">
      <table>
        <thead>
          <tr>
            <th
              class="hidden-left"
              style="min-width: 16px; width: 16px; background-color: #fff"
            ></th>
            <th class="table-checkbox" style="min-width: 40px; width: 40px">
              <MCheckBox v-model="checkAll" :rowId="'All'" />
            </th>
            <th
              v-for="(item, index) in displayColumns"
              :key="`th${index}`"
              :style="{
                width: `${item.width.replace(/\D/g, '')}px`,
                minWidth: `${
                  item.width.replace(/\D/g, '') != 0
                    ? item.width.replace(/\D/g, '')
                    : item.minWidth.replace(/\D/g, '')
                }px`,
                textAlign: item.align,
              }"
              :class="item.classList"
              :title="item.descriptionName"
            >
              {{ item.displayName }}
            </th>
            <th class="table-function" style="min-width: 100px">Chức năng</th>
            <th
              class="hidden-right"
              style="right: 20px; width: 20px; background-color: #fff"
            ></th>
            <td
              class="hidden-right"
              style="background-color: #f4f5f8; width: 20px"
            ></td>
          </tr>
        </thead>
        <tbody v-show="!isLoading" id="mainTable">
          <tr
            :class="{ checked: val.checked }"
            v-for="(val, index) in tableData"
            :key="index"
            @dblclick="openForm(false, val)"
          >
            <td
              class="hidden-left"
              style="background-color: #fff !important"
            ></td>
            <td class="table-checkbox">
              <MCheckBox v-model="val.checked" :rowId="`r${index}`" />
            </td>
            <td
              v-for="(item, ind) in displayColumns"
              :key="`td${ind}`"
              :title="val[item.id]"
              :style="{
                width: `${item.width.replace(/\D/g, '')}px`,
                minWidth: `${
                  item.width.replace(/\D/g, '') != 0
                    ? item.width.replace(/\D/g, '')
                    : item.minWidth.replace(/\D/g, '')
                }px`,
                textAlign: item.align,
              }"
            >
              <div
                :style="{
                  width: `${item.width.replace(/\D/g, '')}px`,
                  minWidth: `${
                    item.width.replace(/\D/g, '') != 0
                      ? item.width.replace(/\D/g, '')
                      : item.minWidth.replace(/\D/g, '')
                  }px`,
                  overflowWrap: 'break-word',
                }"
              >
                {{ dataFormat(val[item.id], item.type) }}
              </div>
            </td>
            <td class="table-function">
              <div class="table-function-button">
                <div class="button-edit" @click="openForm(val, tableFunc.key)">
                  {{ tableFunc.name }}
                </div>
                <div class="arrow-down-button">
                  <div
                    class="icon"
                    @click="tableOption($event, val, index)"
                  ></div>
                </div>
              </div>
            </td>
            <td
              class="hidden-right"
              style="right: 20px; background-color: #fff !important"
            ></td>
            <td
              class="hidden-right"
              style="background-color: #f4f5f8 !important"
            ></td>
          </tr>
          <tr class="footer" v-if="isCount">
            <td
              class="hidden-left"
              style="background-color: #fff !important"
            ></td>
            <td class="table-checkbox"></td>
            <td
              v-for="(item, ind) in displayColumns"
              :key="`td${ind}`"
              :style="{
                width: `${item.width.replace(/\D/g, '')}px`,
                minWidth: `${
                  item.width.replace(/\D/g, '') != 0
                    ? item.width.replace(/\D/g, '')
                    : item.minWidth.replace(/\D/g, '')
                }px`,
                textAlign: item.align,
              }"
            >
              <div
                :style="{
                  width: `${item.width.replace(/\D/g, '')}px`,
                  minWidth: `${
                    item.width.replace(/\D/g, '') != 0
                      ? item.width.replace(/\D/g, '')
                      : item.minWidth.replace(/\D/g, '')
                  }px`,
                  overflowWrap: 'break-word',
                }"
              >
                {{ item.count?getCount(item.id):"" }}
              </div>
            </td>
            <td class="table-function">
              <div class="table-function-button"></div>
            </td>
            <td
              class="hidden-right"
              style="right: 20px; background-color: #fff !important"
            ></td>
            <td
              class="hidden-right"
              style="background-color: #f4f5f8 !important"
            ></td>
          </tr>
        </tbody>
        <tbody v-show="isLoading">
          <tr v-for="(val, index) in tableData" :key="index">
            <td
              class="hidden-left"
              style="background-color: #fff !important"
            ></td>
            <td class="table-checkbox">
              <div class="loading-1" style="width: 18px"></div>
            </td>
            <td
              v-for="(item, ind) in displayColumns"
              :key="`td${ind}`"
              :title="val[item.id]"
              :style="{
                textAlign: item.align,
              }"
            >
              <div class="loading-1"></div>
            </td>
            <td class="table-function" style="display: table-cell">
              <div
                class="table-function-button"
                style="display: block; padding: 9px 0px"
              >
                <div class="loading-1"></div>
              </div>
            </td>
            <td
              class="hidden-right"
              style="right: 20px; background-color: #fff !important"
            ></td>
            <td
              class="hidden-right"
              style="background-color: #f4f5f8 !important"
            ></td>
          </tr>
        </tbody>
      </table>
      <!-- <div v-show="!isLoading && tableData.length <= 0">
        <div class="no-content">
          <img src="@/assets/no-employee.svg" />
          <div class="no-content-text">Không có dữ liệu</div>
        </div>
      </div> -->
    </div>
  </div>
</template>

<script>
//import component
import MCheckBox from "@/components/base/BaseCheckBox.vue";

//import lib
import axios from "axios";
import format from "@/js/lib/formatContent.js";

export default {
  name: "MTable",
  components: {
    MCheckBox,
  },
  props: {
    isCount:{
      type:Boolean,
      default:false,
    },
    //các cột hiển thị
    columns: {
      type: Array,
      default: () => [],
    },
    //các chức năng trong bảng
    tableFunction: {
      type: Object,
      default: () => {
        return {};
      },
    },
    //dữ liêu mặc đinh trong bảng
    defaultData: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      //các cột hiển thị trong table
      displayColumns: this.columns,
      //các chức nawg có trogn table
      tableFunc: this.tableFunction,
      /**
       * Mô tả : tableData làdanh sách nhân viên hiện lên bảng
       * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
       */
      tableData: this.defaultData,
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

      //Mô tả : mã id nhân viên đang được chọn
      selectedId: "",
      //mã nhân viên được chọn
      selectedCode: "",
      confirmDialogData: {},
      //nếu check all đc check thì sẽ check tất cả các check trong bảng
      checkAll: false,
      test: false,
      isConfirm: false,
      //thông tin để phân trang và filter
      pagingOption: {
        //thông tin bảng:paging,số bản ghi/1 trang
        currentPage: this.dataStorage.paging.defaultCurrentPage, //global property
        //tổng số trang
        totalPage: 1,
        //filter theo mã nhân viên
        totalRecord: 0,
        pageSize: this.dataStorage.paging.defaultPageSize, //global property
        filterText: "",
      },
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
     * Mô tả : lấy dữ liệu table từ api
     * Created by: Nguyễn Đức Toán - MF1095 (15/04/2022)
     */
    loadTable() {
      this.isLoading = true;
      this.checkAll = false;
      let me = this;
      me.tableData = [];
      try {
        axios({
          method: "GET",
          url: `${this.dataStorage.api.filter}?currentPage=${me.pagingOption.currentPage}&pageSize=${me.pagingOption.pageSize}&filterText=${me.pagingOption.filterText}`,
        })
          .then((res) => {
            if (res.status == 200) {
              me.tableData = res.data.list;
              me.pagingOption.totalRecord = res.data.totalRecord;
              me.pagingOption.totalPage = res.data.totalPage;
              me.isLoading = false;
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
        //lấy độ cao màn hình
        this.maxHeight = e.view.innerHeight;
        this.X = tagetElement.left - 50;
        this.Y = tagetElement.top;
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
        let event = {
          left: this.X,
          top: this.Y,
          height: tagetElement.height,
        };
        this.tableEvent("tableOption", val, event);
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

    openForm(val, key) {
      try {
        this.tableEvent(key, val);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : emit even của table ra comp cha
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    tableEvent(key, val, event) {
      try {
        this.$emit("tableEvent", key, val, event);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : click ra ngoài dropdown list
     * Created by: Nguyễn Đức Toán - MF1095 (10/04/2022)
     */
    clickOutSide() {
      this.isShow = false;
    },
    /**
     * Mô tả : fomat datestring sang date
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    dateFormat(date) {
      return format.dateFormat(date);
    },
    // fomat string sang number
    salaryFormat(salary) {
      return format.salaryFormat(salary);
    },
    //tính tổng cho các hàng đc tính
    getCount(id) {
      try {
        let count = 0;
        this.tableData.forEach((item) => {
          count += item[id];
        });
        return format.currencyFormatDE(count);
      } catch (error) {
        console.log(error);
        return 0;
      }
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
            return format.currencyFormatDE(data);
          case "date": //kiểu dữ liệu dạng ngày
            return format.dateFormat(data);
          default:
            return "";
        }
      } catch (error) {
        console.log(error);
      }
    },
  },

  watch: {
    /**
     * Mô tả : kiểm tra nếu chọn nhiều hơn 1 em ployee thì cho phép dùng chức năng xóa nhiều
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    tableData: {
      deep: true,
      handler(newVal) {
        try {
          if (newVal) {
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
          }
        } catch (error) {
          console.log(error);
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
        this.tableData.forEach((ele) => {
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
  position: fixed;
  transform: translate(-100px, 0);
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  z-index: 20;
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

tr.footer td{
  background: #e8eaef !important;
  border: unset;
  position: sticky;
  bottom: 56px;
}
tr.footer td{
  font-weight: 700;
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
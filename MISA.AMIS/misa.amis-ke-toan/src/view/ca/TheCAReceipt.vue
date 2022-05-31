<template>
  <div id="ca-receipt">
    <div class="page-content">
      <div class="title">
        <div class="title-text m-left">Thu chi tiền mặt</div>
        <div class="button-area m-right">
          <div class="guideline">
            <div class="ic-guideline"></div>
            <a class="link-guideline">Hướng dẫn</a>
          </div>
          <div style="margin-right: 8px">
            <DropdownButton
              ref="ultility"
              :typeClass="'default'"
              :text="'Tiện ích'"
              @click="funcImprovConfirm"
            />
          </div>
          <div class="button">
            <MButton
              :buttonType="'primary'"
              :text="'Thêm chi tiền'"
              :isDropdown="true"
              :isUtility="true"
              @click="openAddPayment"
            />
          </div>
        </div>
      </div>
      <div class="counter">
        <div class="view-total m-col-4">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đầu năm đến hiện tại</div>
        </div>
        <div class="view-total m-col-4"  style="background-color: #74cb2f">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đầu năm đến hiện tại</div>
        </div>
        <div class="view-total m-col-4" style="background-color: #b8bcc3">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đầu năm đến hiện tại</div>
        </div>
      </div>
      <div class="sub-tab">
        <div class="tab-item selected">
          <div class="text">Tất cả</div>
        </div>
        <div class="tab-item">
          <div class="text">Thu tiền</div>
        </div>
        <div class="tab-item">
          <div class="text">Chi tiền</div>
        </div>
      </div>
      <div class="table-tool">
        <div class="m-left m-d-flex m-align-item-center">
          <div class="check-all" style="padding: 0 10px">
            <div class="ic-check-all"></div>
          </div>
          <DropdownButton
            ref="multiAction"
            :typeClass="'default'"
            :text="'Thực hiện hàng loạt'"
            @click="multiFunction"
            :disabled="!isMulti"
            @mouseover="overMultiFunction"
          />
          <DropdownButton
              ref="filter"
              :typeClass="'default'"
              :text="'Lọc'"
              @click="funcImprovConfirm"
              style="margin-left:12px"
            />
        </div>
        <div class="m-right">
          <div class="tool-search">
            <MInput
              :classIcon="'search'"
              :placeholder="'Tìm theo mã, tên nhân viên'"
              v-model="searchText"
              @input="delaySearch"
            />
          </div>
          <div class="ultility-button">
            <div class="refresh" @click="reloadData"></div>
            <div class="export" @click="exportData"></div>
            <div class="setting" @click="setting(true)"></div>
          </div>
        </div>
      </div>
      <div class="table-area">
        <MTable
          ref="PaymentTable"
          :columns="tableOption"
          :defaultData="tableData"
          @tableEvent="tableEvent"
          :tableFunction="tableFunction"
          :isCount="true"
        />
      </div>
      <div class="list no-content" v-if="noConTent">
        <div class="sub">
          <img src="@/assets/no-employee.svg" />
          <div class="no-content-text">Không có dữ liệu</div>
        </div>
      </div>
      <div class="paging-area" v-if="!noConTent">
        <div class="table-footer">
          <div class="footer">
            <div class="count-row">
              <div class="count-text">
                Tổng số: <strong>{{ paging.totalRecord }}</strong> bản ghi
              </div>
            </div>
            <MPaging
              :totalRecord="paging.totalRecord"
              :totalPage="paging.totalPage"
              @selectPage="selectFilter"
              v-model="paging.currentPage"
            />
          </div>
        </div>
      </div>
    </div>
    <!-- dropdown chức năng -->
    <DropdownList
      ref="funcionDropdown"
      :maxheight="maxHeight"
      :listItem="functionDropdown"
      v-show="isShow"
      @outSide="clickOutSide"
      @selected="selectDropdown"
    />
    <MSetting
      v-if="showSetting"
      @exitSetting="setting"
      :data="tableOption"
      @submit="saveTableOption"
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
import MButton from "@/components/base/button/BaseButton.vue";
import DropdownButton from "@/components/base/button/BaseDropdownButton.vue";
import MTable from "@/components/base/table/BaseTable.vue";
import MPaging from "@/components/base/BasePaging.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import DropdownList from "@/components/base/dialog/BaseDropdownList.vue";
import MSetting from "@/components/base/dialog/BaseCustomSetting.vue";
import ConfirmDialog from "@/components/base/dialog/BaseConfirmDialog.vue";
import ToastMessage from "@/components/base/BaseToastMessage.vue";
import axios from "axios";
const FileDownload = require("js-file-download");
export default {
  name: "VendorList",
  components: {
    MButton,
    MSetting,
    DropdownButton,
    MTable,
    MPaging,
    MInput,
    DropdownList,
    ConfirmDialog,
    ToastMessage,
  },
  data() {
    return {
      //thông tin các  cột có trong bảng
      option: {},
      test: 10,
      //các côt jhieenr thị trong bảng
      tableOption: [],
      //dât cho bảng
      tableData: [],
      //các chức năng cho dropdown
      functionDropdown: [],
      isShow: false,
      //toastmessage
      listToastMessage: [],
      //data cho confirm dialog
      confirmDialogData: {},
      isConfirm: false,
      //độ cao tối đa của màn hình
      maxHeight: window.screen.availHeight,
      //hiện setting table
      showSetting: false,
      //hiện form Payment
      paymentOpen: false,
      //Payment được chọn
      paymentTarget: {},
      //phân trang
      paging: {
        pageSize: this.dataStorage.paging.defaultPageSize,
        currentPage: this.dataStorage.paging.defaultCurrentPage,
        totalPage: 1,
        totalRecord: 0,
      },
      //tìm kiếm
      searchText: "",
      //debound tìm kiếm
      delayTimer: null,
      //bảng trống dữ liệu hay k
      noConTent: false,
      //cho phép dùng chức năng nhóm
      isMulti: false,
      tableFunction: { key: "view", name: "Xem" },
      // danh sách các id đối tượng đưuọc chọn
      listId: [],
    };
  },
  methods: {
    /**
     * Mô tả : lấy danh sách nhân dữ liệu về table
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    getTableData() {
      try {
        let me = this;
        me.$refs.PaymentTable.isLoading = true;

        axios
          .get(
            `${this.dataStorage.api.payment.filter}?currentPage=${me.paging.currentPage}&pageSize=${me.paging.pageSize}&filterText=${me.searchText}`
          )
          .then((res) => {
            if (!res.data.List) {
              me.noConTent = true;
            } else {
              me.noConTent = false;
            }
            this.tableData = res.data.List;
            me.$refs.PaymentTable.setData(res.data.List ? res.data.List : []);
            me.paging.totalPage = res.data.TotalPage;
            me.paging.totalRecord = res.data.TotalRecord;
            me.$nextTick(() => {
              me.$refs.PaymentTable.isLoading = false;
            });
          })
          .catch((res) => {
            console.log(res);
            me.$refs.PaymentTable.isLoading = false;
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy danh sách nhân dữ liệu về table
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    getTableOption() {
      try {
        let me = this;
        axios
          .get(`${this.dataStorage.api.tableOption.getByCode}?code=Payment`)
          .then((res) => {
            me.option = res.data;
            me.tableOption = JSON.parse(res.data.ListColumns);
            me.$refs.PaymentTable.setColumns(
              JSON.parse(res.data.ListColumns).filter((item) => {
                return item.checked == true;
              })
            );
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : bắt sự kiện click chuột vào nút sửa để hiện ra dropdown lựa chọn thực hiện hàng loạt
     * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
     */
    multiFunction(e) {
      // if (!this.multiFlag) return;
      try {
        //dừng sự kiện click khác
        e.stopPropagation();
        let elementTarget =
          this.$refs["multiAction"].$el.getBoundingClientRect();
        if (this.isMulti) {
          this.functionDropdown = this.resource.dropdownData.multipleFunction;
        } else {
          this.functionDropdown = [];
          return;
        }
        elementTarget.left + 100;
        elementTarget.top;
        this.openFuncDropdown(elementTarget);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở dropdown function
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    openFuncDropdown(elementTarget) {
      try {
        //hiện dropdown
        this.isShow = true;
        //lấy độ cao màn hình
        this.$refs["funcionDropdown"].setX(elementTarget.left);
        this.$refs["funcionDropdown"].setY(
          elementTarget.top + elementTarget.height
        );
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : bắt sự kiện chọn dropdown
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    selectDropdown(key) {
      try {
        let data;
        switch (key) {
          // case "duplicate":
          //   // this.openEditPayment(this.paymentTarget);
          //   break;
          case "view":
            this.openViewPayment(this.paymentTarget);
            break;
          case "multiDelete":
            data = {
              name: "xóa",
              button: this.resource.confirmDialogData.deleteMultiple,
              type: "warning",
              text: this.resource.confirmDialogData.getMultiDeleteConfirm(),
              key: "multiDelete",
            }; //global property
            this.openConfirm(data);
            break;
          case "delete": //chọn xóa 1 nhân
            data = {
              name: "xóa",
              button: this.resource.confirmDialogData.delete,
              type: "warning",
              text: this.resource.confirmDialogData.getDeleteConfirm(
                `chứng từ <${this.paymentTarget.PaymentCode}>`
              ),
              key: "delete",
            }; //global property
            this.openConfirm(data);
            break;
          case "duplicate":
            this.openForm(this.paymentTarget.PaymentId);
            break;
          default:
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở confirm dialog
     * Created by: Nguyễn Đức Toán - MF1095 (27/05/2022)
     */
    openConfirm(confirm) {
      this.confirmDialogData = confirm;
      this.isConfirm = true;
    },
    /**
     * Mô tả : nhận xác nhận của người dùng
     * Created by: Nguyễn Đức Toán - MF1095 (27/05/2022)
     */
    confirm(key, val) {
      let me = this;
      switch (key) {
        case "error": //input nhập trống
          switch (val) {
            case 1: //đóng
              this.isConfirm = false;
              break;
            default:
              break;
          }
          break;
        case "delete": //input nhập trống
          switch (val) {
            case 1: //đóng
              this.isConfirm = false;
              break;
            case 2: //đóng
              this.delete(this.paymentTarget.PaymentId);
              this.isConfirm = false;
              break;
            default:
              break;
          }
          break;
        case "multiDelete": //input nhập trống
          switch (val) {
            case 1: //đóng
              this.isConfirm = false;
              break;
            case 2: //đóng
              this.multiDelete(this.paymentTarget.PaymentId);
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
        case "funcImprov":
          switch (val) {
            case this.confirmResource.funcImprov.button[0].id:
              this.isConfirm = false;
              break;

            default:
              break;
          }
          break;
        default:
          break;
      }
      this.paymentTarget = {};
    },
    /**
     * Mô tả : đóng dropdown lits
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    clickOutSide(value) {
      try {
        this.isShow = value;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : đóng mở form setting
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    setting(show) {
      try {
        this.showSetting = show;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : xóa 1 Payment  với id truyền vào
     * Created by: Nguyễn Đức Toán - MF1095 (23/05/2022)
     */
    delete(id) {
      try {
        if (!id) return;
        // let me = this;
        axios({
          method: "DELETE",
          url: `${this.dataStorage.api.payment.delete}/${id}`,
        })
          .then(() => {
            this.reloadData();
            let toast = this.resource.toastMessage.deleteSuccess;
            this.addToast(toast);
          })
          .catch((res) => {
            console.log(res);
            let responseData = res.response.data;
            let data = {
              name: "xóa",
              button: this.resource.confirmDialogData.errorConfirm,
              type: "error",
              align: "center",
              text: responseData.data[Object.keys(responseData.data)[0]],
              key: "error",
            }; //global property
            this.openConfirm(data);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : xóa 1 Payment  với id truyền vào
     * Created by: Nguyễn Đức Toán - MF1095 (23/05/2022)
     */
    multiDelete() {
      try {
        if (this.listId.length <= 1) return;
        // let me = this;
        axios({
          method: "DELETE",
          url: this.dataStorage.api.payment.multi,
          data: this.listId,
        })
          .then(() => {
            this.reloadData();
            let toast = this.resource.toastMessage.deleteSuccess;
            this.addToast(toast);
          })
          .catch((res) => {
            let responseData = res.response.data;
            let data = {
              name: "xóa",
              button: this.resource.confirmDialogData.errorConfirm,
              type: "error",
              align: "center",
              text: responseData.data[Object.keys(responseData.data)[0]],
              key: "error",
            }; //global property
            this.openConfirm(data);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : submit form setting
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    saveTableOption(tableOption) {
      try {
        let me = this;
        let dataRequest = JSON.parse(JSON.stringify(this.option));
        dataRequest["ListColumns"] = JSON.stringify(tableOption);
        axios({
          method: "Put",
          url: this.dataStorage.api.tableOption.updateByCode,
          data: dataRequest,
          contentType: "aplication/json",
        })
          .then(() => {
            this.setting(false);
            me.getTableOption();
            me.getTableData();
          })
          .catch((res) => {
            console.log(res);
          });
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
     * Mô tả : nhaạn page size và currnet page từ comp paging
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    selectFilter(current, pageSize) {
      try {
        this.paging.pageSize = pageSize;
        this.paging.currentPage = current;
        this.getTableData();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : kiểm tra xem đã có bản ghi nào đc check
     * Created by: Nguyễn Đức Toán - MF1095 (28/05/2022)
     */
    overMultiFunction() {
      try {
        let count = 0;
        this.listId = [];
        this.tableData.forEach((element) => {
          if (element.checked == true) {
            count++;
            this.listId.push(element.PaymentId);
          }
        });
        //nếu sso dòng được chọn lớn hơn 0
        if (count > 1) {
          this.isMulti = true;
        } else {
          this.isMulti = false;
        }
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
        me.paging.currentPage = 1;
        me.getTableData();
      }, 500);
    },
    /**
     * Mô tả : load lại dữ liệu của bảng
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    reloadData() {
      try {
        this.getTableOption();
        this.getTableData();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : bắt các sự kiện đc emit từ table
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    tableEvent(key, val, event) {
      try {
        switch (key) {
          case "view":
            //mở form sửa Payment
            this.openViewPayment(this.paymentTarget);
            break;
          case "tableOption":
            this.paymentTarget = val;
            this.functionDropdown =
              this.resource.dropdownData.tablePaymentFunction;
            //mở form sửa Payment
            this.openFuncDropdown(event, event.event);
            break;
          default:
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở form sửa Payment
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    openEditPayment(val) {
      try {
        let me = this;
        this.openPayment();
        this.$nextTick(() => {
          me.$refs["paymentDetail"].isAdd = false;
          me.$refs["paymentDetail"].getPayment(val.PaymentId);
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở form xem Payment
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    openViewPayment(val) {
      try {
        // let data = {
        //   id: val,
        //   description: "pass data through params",
        // };
        this.$router.push({
          name: "ThePaymentDetail", //use name for router push
          params: { id: val.PaymentId },
          // query: {data:'ád'},
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở form xem Payment
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    openAddPayment() {
      try {
        // let data = {
        //   id: val,
        //   description: "pass data through params",
        // };
        this.$router.push("/ThePaymentDetail");
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : xuất danh sách các cột hiện trên table
     * Created by: Nguyễn Đức Toán - MF1095 (29/05/2022)
     */
    exportData() {
      try {
        let columns = [];
        this.tableOption.forEach((item) => {
          if (item.checked) {
            columns.push({
              Key: item.id,
              Name: item.displayName,
              Align: item.align,
            });
          }
        });
        axios({
          method: "POST",
          data: columns,
          url: `${this.dataStorage.api.payment.export}?currentPage=${this.paging.currentPage}&pageSize=${this.paging.pageSize}&filterText=${this.searchText}`,
          responseType: "blob",
        }).then((res) => {
          FileDownload(res.data, "danh_sach_thu_chi.xlsx");
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : đóng form Payment detail
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    exitPayment() {
      this.paymentOpen = false;
    },
  },
  created() {
    try {
      this.getTableOption();
    } catch (error) {
      console.log(error);
    }
  },
  mounted() {
    try {
      this.getTableData();
    } catch (error) {
      console.log(error);
    }
  },
  watch: {},
};
</script>

<style scoped>
#ca-receipt {
  padding: 0px 0px 0px 20px;
}

.page-content {
  overflow: auto;
  height: calc(100vh - 113px);
}
.page-content .title {
  position: sticky;
  left: 0px;
  display: flex;
  align-items: center;
  width: 100%;
  justify-content: space-between;
  padding: 24px 30px 24px 0px;
}
.page-content .table-tool {
  position: sticky;
  left: 0px;
  background-color: #fff;
  margin: 24px 20px 0px 0px;
}
.page-content .title .title-text {
  font-size: 24px;
  padding-left: 10px;
  font-weight: 600;
}
.page-content .button-area {
  display: flex;
  align-items: center;
}
.page-content .button-area {
  display: flex;
  align-items: center;
}

.page-content .counter {
  display: flex;
  align-items: center;
  padding-right: 50px;
  position: sticky;
  left: 0px;
}
.page-content .paging-area {
  position: sticky;
  left: 0px;
  bottom: 0px;
  z-index: 20;
  padding-right: 15px;
}
.page-content .counter .view-total {
  padding: 10px;
  height: 64px;
  background-color: #ff7f2c;
  margin-inline-end: 10px;
  color: #fff;
}
.page-content .sub-tab {
  display: flex;
  align-items: center;
  position: sticky;
  left: 0px;
}
.page-content .sub-tab .tab-item {
  padding: 12px 20px;
}
.page-content .sub-tab .tab-item.selected {
  color: #2ca01c;
  font-weight: 700;
}
.page-content .sub-tab .tab-item .text:hover {
  color: #2ca01c;
  cursor: pointer;
}

.page-content .button-area .guideline {
  display: flex;
  align-items: center;
  margin: 0px 12px;
}
.page-content .button-area .guideline .link-guideline {
  white-space: nowrap;
  padding-left: 5px;
  color: #0075c0;
  font-size: 13px;
}
.page-content .button-area .guideline .link-guideline:hover {
  text-decoration: underline;
  cursor: pointer;
}
.hidden-right {
  background-color: #f4f5f8 !important;
}
.hidden-left {
  background-color: #fff !important;
}
.list.no-content {
  position: sticky;
  left: 0;
  padding: 0 20px 0 0;
  height: 180px;
  width: 100%;
}
.list.no-content .sub {
  background: #fff;
  height: 100%;
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
.list.no-content .sub img {
  max-width: 200px;
}
.m-toast-box {
  position: fixed;
  top: 24px;
  left: 40%;
  z-index: 20;
}
</style>
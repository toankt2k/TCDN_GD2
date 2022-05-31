<template>
  <div id="ca-receipt">
    <div class="page-content">
      <div class="title">
        <div class="title-text m-left">Danh sách nhà cung cấp</div>
        <div class="button-area m-right">
          <div class="guideline">
            <div class="ic-guideline"></div>
            <a class="link-guideline">Hướng dẫn</a>
          </div>
          <div style="margin-right: 8px">
            <DropdownButton
              ref="multiAction"
              :typeClass="'default'"
              :text="'Tiện ích'"
              @click="funcImprovConfirm"
            />
          </div>
          <div class="button">
            <MButton
              :buttonType="'primary'"
              :text="'Thêm'"
              :isDropdown="true"
              :isUtility="true"
              @click="openAddVendor"
            />
          </div>
        </div>
      </div>
      <div class="counter">
        <div class="view-total m-col-4">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đầu năm đến hiện tại</div>
        </div>
        <div class="view-total m-col-4" style="background-color: #b8bcc3">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đầu năm đến hiện tại</div>
        </div>
        <div class="view-total m-col-4" style="background-color: #74cb2f">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đầu năm đến hiện tại</div>
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
            :disabled="!isMulti"
            @click="multiFunction"
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
          ref="VendorTable"
          :columns="tableOption"
          :defaultData="tableData"
          @tableEvent="tableEvent"
          :tableFunction="tableFunction"
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
import MButton from "@/components/base/button/BaseButton.vue";
import DropdownButton from "@/components/base/button/BaseDropdownButton.vue";
import MTable from "@/components/base/table/BaseTable.vue";
import MPaging from "@/components/base/BasePaging.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import DropdownList from "@/components/base/dialog/BaseDropdownList.vue";
import MSetting from "@/components/base/dialog/BaseCustomSetting.vue";
import VendorDetail from "@/view/vendor/VendorDetail.vue";
import ConfirmDialog from "@/components/base/dialog/BaseConfirmDialog.vue";
import ToastMessage from "@/components/base/BaseToastMessage.vue";

const FileDownload = require("js-file-download");
import axios from "axios";
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
    VendorDetail,
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
      //độ cao tối đa của màn hình
      maxHeight: window.screen.availHeight,
      //hiện setting table
      showSetting: false,
      //hiện form vendor
      vendorOpen: false,
      //vendor được chọn
      vendorTarget: {},
      //data cho confirm dialog
      confirmDialogData: {},
      isConfirm: false,
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
      tableFunction: { key: "edit", name: "Sửa" },
      //data cho toastmessage
      listToastMessage: [],
      //cho phép sử dụng chức nawg chọn nhiều
      isMulti: false,
      //danh sách id các đôi tượng đối tượng
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
        me.$refs.VendorTable.isLoading = true;
        axios
          .get(
            `${this.dataStorage.api.vendor.filter}?currentPage=${me.paging.currentPage}&pageSize=${me.paging.pageSize}&filterText=${me.searchText}`
          )
          .then((res) => {
            if (!res.data.List) {
              me.noConTent = true;
            } else {
              me.noConTent = false;
            }
            this.tableData = res.data.List;
            me.$refs.VendorTable.setData(res.data.List ? res.data.List : []);
            me.paging.totalPage = res.data.TotalPage;
            me.paging.totalRecord = res.data.TotalRecord;
            me.$nextTick(() => {
              me.$refs.VendorTable.isLoading = false;
            });
          })
          .catch((res) => {
            console.log(res);
            me.$refs.VendorTable.isLoading = false;
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
          .get(`${this.dataStorage.api.tableOption.getByCode}?code=Vendor`)
          .then((res) => {
            me.option = res.data;
            me.tableOption = JSON.parse(res.data.ListColumns);
            me.$refs.VendorTable.setColumns(
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
              this.delete(this.vendorTarget.VendorId);
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
            case 2: //ok
              this.multiDelete();
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
      this.vendorTarget = {};
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
        //ngăn sự kiện click khác
        //lất element đc click
        let elementTarget =
          this.$refs["multiAction"].$el.getBoundingClientRect();
        //nếu sso dòng được chọn lớn hơn 0
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
          case "edit":
            this.openEditVendor(this.vendorTarget);
            this.vendorTarget = {};
            break;
          case "view":
            this.openViewVendor(this.vendorTarget);
            this.vendorTarget = {};
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
              text: this.resource.confirmDialogData.getDeleteConfirm(`
                nhà cung cấp <${this.vendorTarget.VendorCode}> `),
              key: "delete",
            }; //global property
            this.openConfirm(data);
            break;
          case "duplicate":
            this.openForm(this.vendorTarget);
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
     * Mô tả : xóa 1 vendor  với id truyền vào
     * Created by: Nguyễn Đức Toán - MF1095 (23/05/2022)
     */
    delete(id) {
      try {
        if (!id) return;
        // let me = this;
        axios({
          method: "DELETE",
          url: `${this.dataStorage.api.vendor.delete}/${id}`,
        })
          .then((res) => {
            console.log(res);
            let toast = this.resource.toastMessage.deleteSuccess;
            this.addToast(toast);
            this.reloadData();
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
     * Mô tả : xóa 1 Payment  với id truyền vào
     * Created by: Nguyễn Đức Toán - MF1095 (23/05/2022)
     */
    multiDelete() {
      try {
        if (this.listId.length <= 1) return;
        // let me = this;
        axios({
          method: "DELETE",
          url: this.dataStorage.api.vendor.multi,
          data: this.listId,
        })
          .then((res) => {
            this.reloadData();
            console.log(res);
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
     * Mô tả : submit form setting
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    saveTableOption(tableOption) {
      try {
        let me = this;
        let dataRequest = JSON.parse(JSON.stringify(this.option));
        // tableOption.forEach((item)=>{
        //   if(item.checked)item.show = true;
        //   else item.show=false;
        //   item.checked = true;
        // })
        dataRequest["ListColumns"] = JSON.stringify(tableOption);
        axios({
          method: "Put",
          url: `${this.dataStorage.api.tableOption.updateByCode}`,
          data: dataRequest,
          contentType: "aplication/json",
        })
          .then((res) => {
            console.log(res);
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
            this.listId.push(element.VendorId);
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
          case "edit":
            //mở form sửa vendor
            this.openEditVendor(val);
            break;
          case "tableOption":
            this.vendorTarget = val;
            this.functionDropdown =
              this.resource.dropdownData.tableVendorFunction;
            //mở form sửa vendor
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
     * Mô tả : mở form sửa vendor
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    openEditVendor(val) {
      try {
        let me = this;
        this.openVendor();
        this.$nextTick(() => {
          me.$refs["vendorDetail"].isAdd = false;
          me.$refs["vendorDetail"].getVendor(val.VendorId);
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở form xem vendor
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    openViewVendor(val) {
      try {
        let me = this;
        this.openVendor();
        this.$nextTick(() => {
          me.$refs["vendorDetail"].isAdd = false;
          me.$refs["vendorDetail"].getVendor(val.VendorId);
          me.$refs["vendorDetail"].readonly = true;
        });
      } catch (error) {
        console.log(error);
      }
    },
    getAddVendor(data) {
      try {
        this.tableData.unshift(data);
        this.$refs.VendorTable.setData(this.tableData ? this.tableData : []);
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
     * Mô tả : mở form thêm vendor
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    openAddVendor() {
      try {
        let me = this;
        this.openVendor();
        this.$nextTick(() => {
          me.$refs["vendorDetail"].isAdd = true;
          me.$refs["vendorDetail"].getNewVendorCode();
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : mở form nhà cung cấp
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    openVendor() {
      this.vendorOpen = true;
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
          url: `${this.dataStorage.api.vendor.export}?currentPage=${this.paging.currentPage}&pageSize=${this.paging.pageSize}&filterText=${this.searchText}`,
          responseType: "blob",
        }).then((res) => {
          FileDownload(res.data, "danh_sach_nha_cung_cap.xlsx");
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : đóng form vendor detail
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    exitVendor() {
      this.vendorOpen = false;
    },
    keyup(e) {
      try {
        if (e.keyCode == 112 && e.ctrlKey) {
          e.preventDefault();
          if (!this.isConfirm) {
            this.openAddVendor();
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
      } catch (error) {
        console.log(error);
      }
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
  watch: {},
};
</script>

<style scoped>
#ca-receipt {
  padding: 0px 0px 0px 20px;
}

.page-content {
  overflow: auto;
  height: calc(100vh - 50px);
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
.page-content .counter .view-total .total {
  font-size: 24px;
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
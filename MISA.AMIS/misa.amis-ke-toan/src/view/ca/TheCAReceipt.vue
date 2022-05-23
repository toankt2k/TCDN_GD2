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
              ref="multiAction"
              :typeClass="'default'"
              :text="'Tiện ích'"
              
            />
          </div>
          <div class="button">
            <MButton
              :buttonType="'primary'"
              :text="'Thêm chi tiền'"
              :isDropdown="true"
              :isUtility="true"
            />
          </div>
        </div>
      </div>
      <div class="counter">
        <div class="view-total m-col-4">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đàu năm đến hiện tại</div>
        </div>
        <div class="view-total m-col-4">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đàu năm đến hiện tại</div>
        </div>
        <div class="view-total m-col-4">
          <div class="total">100,0</div>
          <div class="text">Tổng thu đàu năm đến hiện tại</div>
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
          />
        </div>
        <div class="m-right">
          <div class="tool-search">
            <MInput
              :classIcon="'search'"
              :placeholder="'Tìm theo mã, tên nhân viên'"
            />
          </div>
          <div class="ultility-button">
            <div class="refresh"></div>
            <div class="export"></div>
            <div class="setting"></div>
          </div>
        </div>
      </div>
      <div class="table-area">
        <MTable ref="PaymentTable" :columns="tableOption" :defaultData="tableData"/>
      </div>

      <div class="paging-area">
        <div class="table-footer">
          <div class="footer">
            <div class="count-row">
              <div class="count-text">
                Tổng số: <strong>{{ 10 }}</strong> bản ghi
              </div>
            </div>
            <MPaging :totalRecord="10" :totalPage="2" v-model="test" />
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
  </div>
</template>

<script>
import MButton from "@/components/base/button/BaseButton.vue";
import DropdownButton from "@/components/base/button/BaseDropdownButton.vue";
import MTable from "@/components/base/table/BaseTable.vue";
import MPaging from "@/components/base/BasePaging.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import DropdownList from "@/components/base/dialog/BaseDropdownList.vue";

import axios from "axios";
export default {
  components: {
    MButton,
    DropdownButton,
    MTable,
    MPaging,
    MInput,
    DropdownList,
  },
  data() {
    return {
      test: 10,
      //các côt jhieenr thị trong bảng
      tableOption: [],
      //dât cho bảng
      tableData: [],
      //các chức năng cho dropdown
      functionDropdown:[],
      isShow:false,
      //độ cao tối đa của màn hình
      maxHeight:window.screen.availHeight,
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
        axios
          .get(`http://localhost:5093/api/v1/Employees`)
          .then((res) => {
            me.$refs.PaymentTable.setData(res.data);
          })
          .catch((res) => {
            console.log(res);
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
          .get(`http://localhost:5093/api/v1/TableOptions/ByCode?code=Employee`)
          .then((res) => {
            console.log(res);
            me.tableOption = JSON.parse(res.data.ListColumns);
            me.$refs.PaymentTable.setColumns(JSON.parse(res.data.ListColumns));
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
        console.log(e);
        //ngăn sự kiện click khác
        // kiểm tra sô dòng được chọn
        let count = 3;
        this.tableData.forEach((element) => {
          if (element.checked == true) {
            count++;
          }
        });
        //lất element đc click
        let elementTarget = this.$refs['multiAction'].$el.getBoundingClientRect();
        // e.preventDefault();
        //nếu sso dòng được chọn lớn hơn 0
        if (count > 0) {
          this.functionDropdown = this.resource.dropdownData.multipleFunction;
        } else {
          this.functionDropdown = [];
        }
        //lấy độ cao màn hình
        this.maxHeight = e.view.innerHeight;
        this.$refs['funcionDropdown'].setX(elementTarget.left + 40);
        this.$refs['funcionDropdown'].setY(elementTarget.top + elementTarget.height-4);
        //hiện dropdown
        this.isShow = true;
      } catch (error) {
        console.log(error);
      }
    },
    clickOutSide(value){
      try{
        this.isShow = value;
      }
      catch(error){
        console.log(error);
      }
    }
  },
  created() {
    try {
      this.getTableOption();
      // this.getTableData()
    } catch (error) {
      console.log(error);
    }
  },
};
</script>

<style>
#ca-receipt {
  padding: 0px 0px 0px 20px;
}

.page-content {
  overflow: auto;
  height: calc(100vh - 115px);
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
/* .hidden-right{
    background-color: #f4f5f8 !important;
}
.hidden-left{
    background-color: #fff !important;
} */
</style>
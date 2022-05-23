<template>
  <div class="paging">
    <div class="select-max-row">
      <ComboBox
        :defaultList="comboboxData.maxRow.data"
        :idProp="comboboxData.maxRow.id"
        :nameProp="comboboxData.maxRow.name"
        v-model="pageSize"
        :readonly="true"
      />
    </div>
    <div class="paging-index">
      <div
        class="prev"
        @click="selectPage(selectedPage - 1)"
        :style="{ color: selectedPage == 1 ? '#9e9e9e' : '' }"
      >
        Trước
      </div>
      <!-- trường hợp số page > 5  -->
      <div class="index" v-if="totalPage > 5">
        <div
          @click="selectPage(1)"
          class="number-index"
          :class="{
            selected: selectedPage == 1,
          }"
        >
          {{ 1 }}
        </div>
        <div
          @click="selectPage(selectedPage + index - 3)"
          class="number-index"
          :class="{
            selected: selectedPage == selectedPage + index - 3,
            hidden:
              totalPage < selectedPage + index - 3 ||
              1 > selectedPage + index - 3 ||
              selectedPage + index - 3 == 1 ||
              selectedPage + index - 3 == totalPage,
          }"
          v-for="index in 5"
          :key="index"
        >
          {{
            selectedPage + index - 3 == selectedPage + 2 ||
            selectedPage + index - 3 == selectedPage - 2
              ? "..."
              : selectedPage + index - 3
          }}
        </div>
        <div
          @click="selectPage(totalPage)"
          class="number-index"
          :class="{
            selected: selectedPage == totalPage,
          }"
        >
          {{ totalPage }}
        </div>
      </div>
      <!-- trường hợp tổng số page < 5 thì hiện hết -->
      <div class="index" v-if="totalPage <= 5">
        <div
          @click="selectPage(index)"
          class="number-index"
          :class="{
            selected: selectedPage == index,
          }"
          v-for="index in totalPage"
          :key="index"
        >
          {{ index }}
        </div>
      </div>
      <div
        class="next"
        @click="selectPage(selectedPage + 1)"
        :style="{ color: selectedPage == totalPage ? '#9e9e9e' : '' }"
      >
        Sau
      </div>
    </div>
  </div>
</template>

<script>
import ComboBox from "@/components/base/BaseComboBox.vue";
export default {
  name: "MPaging",
  components: {
    ComboBox,
  },
  /**
   * Mô tả : Khai báo và khởi tạo các thuộc tính cho component
   * Created by: Nguyễn Đức Toán - MF1095 (14/04/2022)
   */
  props: {
    // totalRecord xác định số lượng bản ghi của table
    totalRecord: {
      type: Number,
      default: 0,
    },
    // totalPage xác định số lượng trang của table
    totalPage: {
      type: Number,
      default: 0,
    },
    //trao đổi dữ liệu với component cha
    modelValue: {
      type: Number,
      default: 0,
    },
  },
  /**
   * Mô tả : khai báo và khởi tạo data cho component
   * Created by: Nguyễn Đức Toán - MF1095 (14/04/2022)
   */
  data() {
    return {
      /**
       * Mô tả : combobox data lưu thông tin các combobox dùng trong component
       * Created by: Nguyễn Đức Toán - MF1095 (10/04/2022)
       */
      comboboxData: {
        //lựa chọn số lượng bản ghi tối đa 1 trang(pagesize)
        maxRow: {
          data: this.dataStorage.paging.pageSize,
          id: "id",
          name: "name",
        },
      },
      // pageCount: this.totalPage,
      selectedPage: this.modelValue,
      //số bản ghi trong 1 trang(mặc định 10)
      pageSize: this.dataStorage.paging.pageSize[0].id,
    };
  },
  /**
   * Mô tả : khai báo và khởi tạo hàm cho component
   * Created by: Nguyễn Đức Toán - MF1095 (14/04/2022)
   */
  methods: {
    /**
     * Mô tả : sự kiện chuyển trang emit trang được chọn lên component cha
     * Created by: Nguyễn Đức Toán - MF1095 (14/04/2022)
     */
    selectPage(page) {
      try {
        console.log("23");
        if (page > 0 && page <= this.totalPage) {
          //emit data
          this.$emit("selectPage", page, this.pageSize);
          //hiển thị page đc chọn
          this.selectedPage = page;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    pageSize(newVal, oldVal) {
      try {
        if (newVal != oldVal) {
          //load lại table với current page là 1
          this.selectPage(1);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : khi giá trị current page của cha bị thay đổi thì chọn lại page được chọn
     * Created by: Nguyễn Đức Toán - MF1095 (22/04/2022)
     */
    modelValue(newVal, oldVal) {
      try {
        if (newVal != oldVal) {
          this.selectedPage = newVal;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
.paging {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.paging .select-max-row {
  min-width: 200px;
}

.paging-index {
  /* width: 20px; */
  height: 20px;
  display: flex;
  align-items: center;
  width: 100%;
  margin-left: 5px;
  cursor: pointer;
}

.paging-index .index {
  height: 20px;
  margin-left: 5px;
  display: flex;
  align-items: center;
}

.paging-index .index .number-index {
  height: 20px;
  margin-left: 5px;
  display: flex;
  align-items: center;
  padding: 0 6.5px;
}

.paging-index .index .number-index.selected {
  border: 1px solid #00000021;
  color: #000;
  font-weight: 600;
}

.paging-index .index .number-index.hidden {
  display: none !important;
}
/* 
.paging-index .index .number-index.dot {
    display: none !important;
} */

.paging-index .next {
  height: 20px;
  margin-left: 5px;
}
.paging-index .prev {
  height: 20px;
  margin-left: 5px;
}
</style>
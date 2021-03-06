<template>
  <div ref="combobox" class="m-combobox">
    <div class="combobox">
      <input
        :placeholder="placeholder"
        ref="input"
        class="m-input combobox-input"
        type="text"
        :value="selectedName"
        :readonly="readonly"
        :disabled="disabled"
        @input="openItemByFocus"
        @mousemove="stopDraggable"
        @keydown="selectItemByButton"
        :style="{
          'padding-right':
            isObject && quickAdd ? '62px !important' : '30px !important',
        }"
      />
      <div class="add-btn" @click="func" v-if="isObject && quickAdd">
        <div class="ic-add"></div>
      </div>
      <span class="combobox-icon" ref="icon" @click="openItem($event)"></span>
      <div
        class="item-box"
        ref="itemBox"
        v-if="!isObject"
        :style="{
          'min-width': '100px',
          'max-height': '160px',
          'max-width': '300px',
          'z-index': '11',
        }"
      >
        <div
          :ref="`r${item[idProp]}`"
          class="item data-content"
          :class="{ selected: item.selected }"
          v-for="(item, index) in listItem"
          :key="index"
          :value="item[idProp]"
          @click="selectItem(item)"
        >
          {{ item[nameProp] }}
        </div>
        <div class="item empty" v-if="listItem.length <= 0">
          Không có dữ liệu hiển thị.
        </div>
      </div>
      <div
        class="item-box"
        :class="{ object: isObject }"
        ref="itemBox"
        style="min-width: 100%; z-index: 11"
        v-else
        v-on:clickout="close"
      >
        <div
          class="table-combobox"
          @scroll="lazyLoading($event)"
          ref="itemBoxTable"
        >
          <table>
            <thead>
              <tr>
                <th v-for="(item, index) in displayProps" :key="`it${index}`">
                  {{ item.name }}
                </th>
              </tr>
            </thead>
            <tbody>
              <tr
                :ref="`r${item[idProp]}`"
                class="item"
                :class="{ selected: item.selected }"
                v-for="(item, index) in listItem"
                :key="`item${index}`"
                @click="selectItem(item)"
              >
                <td v-for="(head, ind) in displayProps" :key="`r${ind}`">
                  {{ item[head.id] }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import "clickout-event";
export default {
  name: "ComboBox",
  components: {},
  /**
   * Mô tả : khởi tạo và khai báo các property dùng trong component
   * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
   */
  props: {
    /**
     * Mô tả : đùng để binding dữ liệu qua lại bằng v-model với component cha
     * ******* giá trị mặc định của modelValue là id của phần tử đc chọn được chọn
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    modelValue: {
      type: [String, Number],
      default: "",
    },
    func: {
      type: Function,
      default: () => {},
    },
    /**
     * Mô tả : defaultList danh sách mặc định của combobox nhận được từ component cha
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    defaultList: {
      type: Array,
      default: () => [],
    },
    /**
     * Mô tả : apiData - api lấy danh sách item của conbobox từ aip
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    apiData: {
      type: String,
      default: "",
    },
    /**
     * Mô tả : idProp - tên của thuộc tính id trong listItem, được truyền từ component cha
     * ******* vd: idProp:"GenderId"
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    idProp: {
      type: String,
      default: "",
    },
    /**
     * Mô tả : nameProp - tên của thuộc tính display name trong listItem, được truyền từ cha
     * ******* vd: nameProp:"GenderName" hoặc nameProp:"DepartmentName" là tên được hiển thị lên trên combobox
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    nameProp: {
      type: String,
      default: "",
    },
    nameObject: {
      type: String,
      default: "",
    },
    //xác định input chỉ đọc
    readonly: {
      type: Boolean,
      default: false,
    },
    //xác định input chỉ đọc
    disabled: {
      type: Boolean,
      default: false,
    },
    //xác định combobox có thể được thêm nhanh
    isObject: {
      type: Boolean,
      default: false,
    },
    //xác định combobox có thể được thêm nhanh
    quickAdd: {
      type: Boolean,
      default: true,
    },
    //danh sách các prop cần hiển thị
    displayProps: {
      type: Array,
      default: () => [],
    },
    placeholder: {
      type: String,
      default: "",
    },
  },
  /**
   * Mô tả : Khai báo và khởi tạo các data dùng cho component
   * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
   */
  data() {
    return {
      /**
       * Mô tả : listItem - danh sách các item của combobox, được gán mặc đinh giá trị từ component cha
       * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
       */
      listItem: JSON.parse(JSON.stringify(this.defaultList)),
      /**
       * Mô tả : giá trị hiển thị mặc định của combobox
       * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
       */
      selectedName: "",
      indexOfSelectedItem: 0,
      /**
       * Mô tả : vị trí bắt đầu của itembox cách top mặc đinh là 35px
       * Created by: Nguyễn Đức Toán - MF1095 (10/04/2022)
       */
      top: 0,
      //lazy loading cho combobox
      currentPage: 1,
      pageSize: 10,
      searchText: "",
      //đã load lấy dữ liệu chưa
      firstLoad: true,
    };
  },
  methods: {
    getName() {
      return this.selectedName;
    },
    /**
     * Mô tả : gọi hàm setSelectedItem để set item được chọn
     * @param {*} id - id của item được chọn
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 20:48 08/04/2022
     */
    setSelectedItem(id) {
      try {
        let me = this;
        this.listItem.forEach((element, index) => {
          if (id == element[me.idProp]) {
            element.selected = true;
            this.$emit("update:modelValue", id);
            if (this.isObject) {
              let data = element;
              this.$emit("getSelected", data);
            }
            this.selectedName = element[me.nameProp];
            this.indexOfSelectedItem = index;
          } else {
            element.selected = false;
          }
        });
      } catch (error) {
        console.log(error);
      }
    },
    close() {
      try {
        if (this.$refs.itemBox.style.display == "block") {
          this.openItem();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : set mở box item khi click button
     * ******* 1. mở box item
     * ******* 2. quay icon 180 độ
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    openItem(e) {
      try {
        if (this.disabled || this.readonly) return;
        //nếu không được truyền api thì combo box load dữ liệu khi được gọi
        if (this.apiData == "") {
          //emit giá trị true nếu sự kiện mở combobox và ngược lại
          if (this.$refs.itemBox.style.display != "block")
            this.$emit("getData");
        }
        if (this.$refs.itemBox.style.display != "block") {
          // hiện item box
          this.$refs.itemBox.style.display = "block";
          // animation quay icon 180 độ 0.3s
          this.$refs.icon.style.animation = "rotation-in 0.3s";
          // quay icon 180 độ
          this.$refs.icon.style.transform = "rotate(180deg)";
        } else {
          //đóng form
          this.$refs.itemBox.style.display = "none";
          // hiệu ứng quay ngược lại 180 độ
          this.$refs.icon.style.animation = "rotation-out 0.3s";
          // trở vwf trạng thái đầu
          this.$refs.icon.style.transform = "unset";
        }
        if (!e) return;
        //lấy độ cao,rộng max của màn hình
        let maxHeight = e.view.innerHeight;
        let maxWidth = e.view.innerWidth;
        //lấy vị trí của combobox
        let combobox = this.$refs.input.getBoundingClientRect();
        this.$refs.itemBox.style.minWidth = combobox.width + "px";
        this.top = combobox.top + 35;
        let left = combobox.left;
        // heigth,width của itembox
        let clientHeight = this.$refs.itemBox.clientHeight;
        let clientWidth = this.$refs.itemBox.clientWidth;
        //kiểm tra nếu quá màn hình hiển thị thì cho itembox mở lên phía trên
        if (this.top + clientHeight > maxHeight) {
          this.top += -43 - clientHeight;
        } else {
          this.top = combobox.top + 35;
        }
        //kiểm tra có quá độ rộng màn hình hay k
        if (left + clientWidth > maxWidth) {
          left -= left + clientWidth - maxWidth + 10;
        }
        //set vị trí của item box theo giá trị của top
        this.$refs.itemBox.style.top = this.top + "px";
        //set vị trí của item box theo giá trị của left
        this.$refs.itemBox.style.left = left + "px";
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : set mở box item khi focus
     * ******* 1. mở box item
     * ******* 2. quay icon 180 độ
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    openItemByFocus() {
      try {
        //mở item box
        this.$refs.itemBox.style.display = "block";
        //quay icon 180độ
        this.$refs.icon.style.transform = "rotate(180deg)";
        //lấy độ cao,rộng max của màn hình
        let maxHeight = window.innerHeight;
        let maxWidth = window.innerWidth;
        //lấy vị trí của combobox
        let combobox = this.$refs.input.getBoundingClientRect();
        this.$refs.itemBox.style.minWidth = combobox.width + "px";
        this.top = combobox.top + 35;
        let left = combobox.left;
        // heigth,width của itembox
        let clientHeight = this.$refs.itemBox.clientHeight;
        let clientWidth = this.$refs.itemBox.clientWidth;
        //kiểm tra nếu quá màn hình hiển thị thì cho itembox mở lên phía trên
        if (this.top + clientHeight > maxHeight) {
          this.top += -43 - clientHeight;
        } else {
          this.top = combobox.top + 35;
        }
        //kiểm tra có quá độ rộng màn hình hay k
        if (left + clientWidth > maxWidth) {
          left -= left + clientWidth - maxWidth + 10;
        }
        //set vị trí của item box theo giá trị của top
        this.$refs.itemBox.style.top = this.top + "px";
        //set vị trí của item box theo giá trị của left
        this.$refs.itemBox.style.left = left + "px";
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : sự kiện click chọn item,
     * ******* 1. emit data cho component cha
     * ******* 2. set selected item cho combobox, thêm class selected
     * ******* 3. đóng box item
     * ******* 4. quay icon 180 độ
     * @param {*} item item được chọn
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 22:00 08/04/2022
     */
    selectItem(item) {
      try {
        //emit dữ liệu
        this.$emit("update:modelValue", item[this.idProp]);
        if (this.isObject) {
          let data = item;
          this.$emit("getSelected", data);
          // console.log('selectItem');
        }
        //set tên hiển thị
        this.selectedName = item[this.nameProp];
        //bỏ selected cho các item không chọn
        this.listItem.forEach((element) => {
          element.selected = false;
        });
        //thêm class selected cho item được chọn
        item.selected = true;
        this.closeItemBox();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : sự kiện click chọn item,
     * ******* 1. emit data cho component cha
     * ******* 2. set selected item cho combobox, thêm class selected
     * @param {*} item item được chọn
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 22:00 08/04/2022
     */
    selectItemByArrow(item) {
      try {
        //emit dữ liệu
        this.$emit("update:modelValue", item[this.idProp]);
        if (this.isObject) {
          let data = item;
          this.$emit("getSelected", data);
          // console.log('selectItemByArrow');
        }
        //set tên hiển thị
        this.selectedName = item[this.nameProp];
        //bỏ selected cho các item không chọn
        this.listItem.forEach((element) => {
          element.selected = false;
        });
        //thêm class selected cho item được chọn
        item.selected = true;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : đóng item box, quay  icon 180 độ
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    closeItemBox() {
      // đóng item box
      this.$refs.itemBox.style.display = "none";
      this.$refs.icon.style.transform = "unset";
    },
    pushNewObject(data){
      try {
        this.listItem.unshift(data);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : Hàm set item đc mặc định chọn khi mới render
     * ******* nếu đc truyền vào thì lấy giá trị đó ngược chọn item đầu tiên
     * Created by: Nguyễn Đức Toán - MF1095 (09/04/2022)
     */
    setDefaultSelected() {
      try {
        // //mặc định text hiển thị cho combobox là phần tử đầu tiên
        // this.selectedName = this.listItem[0][this.nameProp];
        // //mặc định id cho combobox là phần tử đầu tiên
        // this.listItem[0].selected = true;
        // //emit giá trị để thay đổi v-model ở component cha
        // this.$emit("update:modelValue", this.listItem[0][this.idProp]);
        //duyệt và set selected cho item được chọn
        this.listItem.forEach((element, index) => {
          if (element[this.idProp] == this.modelValue) {
            //set tên hiển thị
            this.selectedName = element[this.nameProp];
            //bỏ selected
            this.listItem.forEach((ele) => {
              ele.selected = false;
            });
            element.selected = true;
            this.$emit("update:modelValue", element[this.idProp]);
            if (this.isObject) {
              let data = element;
              this.$emit("getSelected", data);
              // console.log('setDefaultSelected');
            }
            this.indexOfSelectedItem = index;
          }
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lazyLoading khi scroll xuống cuối bản ghi thì load thêm data
     * Created by: Nguyễn Đức Toán - MF1095 (25/05/2022)
     */
    lazyLoading() {
      try {
        //nếu lazyLoading bằng true thì mới loading.
        if (this.lazyLoading) {
          //độ cao dropdown
          let dropdownHeight = this.$refs.itemBoxTable.clientHeight;
          //phần đã bị tràn khỏi top.
          let scrollTop = this.$refs.itemBoxTable.scrollTop;
          //toàn bộ độ cao có thể scroll
          let scrollHeight = this.$refs.itemBoxTable.scrollHeight;
          // console.log(dropdownHeight,scrollTop, scrollHeight);
          //vị trí hiện tại của con lăn
          var total = dropdownHeight + Math.floor(scrollTop);
          //nếu bằng scrollHeight thì là đang ở bottom
          if (total >= scrollHeight) {

            total = 0;
            //emit tới cha load tiếp thông tin, truyền vào dữ liệu filter nếu có để lazy load tiếp thông tin đó.
            // this.$emit("eLoadNext", this.selectedText);
            this.currentPage += 1;
            this.getDataCombo();
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : setfocus cho input lỗi
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    setErrorFocus(title) {
      try {
        this.$refs.input.style.border = "1px solid red";
        this.$refs.input.title = title;
        this.setFocus(true);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : set focus cho input this.focus = true thì set focus và ngược lại
     * @param {*} focus true/false xác định input có được focus không
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 10:26 05/04/2022
     */
    setFocus(focus) {
      try {
        // this.focus = focus;
        if (focus) {
          this.$refs.input.focus();
          // this.focus = false;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : xóa focus error khi input
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    removeError() {
      try {
        let target = this.$refs.input;
        if (!target) return;
        target.style = "";
        target.title = "";
      } catch (error) {
        console.log(error);
      }
    },
    selectItemByButton(e) {
      if (e.keyCode == 38) {
        this.openItemByFocus();
        this.indexOfSelectedItem -= 1;
        if (this.indexOfSelectedItem < 0) {
          this.indexOfSelectedItem = this.listItem.length - 1;
        }
        this.selectItemByArrow(this.listItem[this.indexOfSelectedItem]);
      }
      if (e.keyCode == 40) {
        this.openItemByFocus();
        this.indexOfSelectedItem += 1;
        if (this.indexOfSelectedItem > this.listItem.length - 1) {
          this.indexOfSelectedItem = 0;
        }
        this.selectItemByArrow(this.listItem[this.indexOfSelectedItem]);
      }
      if (e.keyCode == 13) {
        // this.openItemByFocus();
        if (this.$refs.itemBox.style.display == "none") {
          this.openItemByFocus();
        } else {
          this.closeItemBox();
        }
      }
    },
    /**
     * Mô tả : gán listdata cho combobox
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    setData(data) {
      try {
        this.listItem = data;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : gán listdata cho combobox
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    removeData() {
      try {
        this.listItem = [];
        this.selectedName = "";
      } catch (error) {
        console.log(error);
      }
  },
    /**
     * Mô tả : setfocus cho input lỗi
     * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
     */
    stopDraggable(e) {
      try {
        e.stopPropagation();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy data từ api
     * Created by: Nguyễn Đức Toán - MF1095 (25/05/2022)
     */
    async getDataCombo() {
      try {
        let me = this;
        // nếu api lấy danh sách item có được truyền tức là combobox muốn lấy dữ liệu từ api
        //thay thế cho danh sách mặc định
        if (me.apiData != "") {
          await axios({
            method: "GET",
            url: `${me.apiData}?currentPage=${me.currentPage}&pageSize=${me.pageSize}&filterText=${me.searchText}`,
            async: true,
          })
            .then((res) => {
              //gán giá trị cho danh sách item của item box
              if (res.data.List) {
                me.listItem = me.listItem.concat(res.data.List);
              }
              //set mặc định chọn
              me.setDefaultSelected();
            })
            .catch((res) => {
              console.log(res);
            });
        } else {
          //nếu không có api lấy dữ liệu  thì dùng dữ liệu mặc định và set mặc định chọn
          me.setDefaultSelected();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy data từ api
     * Created by: Nguyễn Đức Toán - MF1095 (25/05/2022)
     */
    async getFirstCombo() {
      try {
        let me = this;
        // nếu api lấy danh sách item có được truyền tức là combobox muốn lấy dữ liệu từ api
        //thay thế cho danh sách mặc định
        if (me.apiData != "") {
          let api = me.apiData.replace(/filter/g, "");
          axios({
            method: "GET",
            url: `${api}${me.modelValue}`,
            async: true,
          })
            .then((res) => {
              //gán giá trị cho danh sách item của item box
              if (res.data) {
                let count = this.listItem.filter((item) => {
                  item[this.idProp] == res.data[this.idProp];
                }).length;
                if (count != 0) me.listItem.push(res.data);
              }
              //set mặc định chọn
              me.setDefaultSelected();
            })
            .catch((res) => {
              console.log(res);
            });
        } else {
          //nếu không có api lấy dữ liệu  thì dùng dữ liệu mặc định và set mặc định chọn
          me.setDefaultSelected();
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  /**
   * Mô tả : hàm chạy khi component được tạo
   * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
   */
  async created() {
    try {
      await this.getDataCombo();
    } catch (error) {
      console.log(error);
    }
  },
  //theo dõi thay đổi của biến
  watch: {
    //khi modelValue thay dổi thì set lại item được chọn
    modelValue(newVal) {
      if (!newVal) {
        this.selectedName = "";
        return;
      }
      if (this.firstLoad) {
        this.getFirstCombo();
        this.firstLoad = false;
      }
      this.setSelectedItem(newVal);
      this.removeError();
    },
    listItem() {
      if (this.firstLoad) {
        this.getFirstCombo();
      }
      this.setSelectedItem(this.modelValue);
    },
  },
  /**
   * Mô tả : sau khi mount thì itembox được sinh ra ở vị trí top
   * Created by: Nguyễn Đức Toán - MF1095 (10/04/2022)
   */
  mounted() {
    try {
      this.$nextTick(() => {
        let combobox = this.$refs.combobox.getBoundingClientRect();
        this.top = combobox.top + 35;
        this.$refs.itemBox.style.top = top + "px";
        this.$refs.itemBox.style.minWidth = combobox.width + "px";
      });
    } catch (error) {
      console.log(error);
    }
  },
};
</script>

<style scoped>
@import url("@/css/components/combobox.css");

.m-input {
  font-family: Notosans-regular !important;
}
input::placeholder {
  font-family: Notosans-regular !important;
  font-style: italic !important;
}
.item.empty {
  pointer-events: none;
}
table {
  border-collapse: separate;
  border-spacing: 0;
  width: 100%;
}
.object {
  padding: 0;
  overflow: unset;
}
.table-combobox {
  overflow-x: hidden;
  overflow-y: auto;
  max-height: 160px;
}
.table-combobox::-webkit-scrollbar {
  width: 10px !important;
}
.table-combobox::-webkit-scrollbar-thumb {
  background-color: #b8bcc3;
}

.table-combobox::-webkit-scrollbar-thumb:hover {
  background-color: #a5a5a5;
}

.table-combobox::-webkit-scrollbar-track {
  background-color: #f1f1f1;
}

.table-combobox table thead {
  height: 32px;
  min-height: 32px;
}
.table-combobox table thead th {
  padding: 0 10px;
  position: sticky;
  top: 0px;
}
.table-combobox table thead th {
  padding: 0 10px;
  min-width: 100px;
  background-color: #eceef1;
  z-index: 15;
  text-align: left;
}
.table-combobox table tbody tr td {
  min-height: 32px;
  height: 32px;
  padding: 0 10px;
}
.table-combobox table tbody tr {
  cursor: pointer;
}
.table-combobox table tbody tr {
  min-height: 32px;
  height: 32px;
}
.table-combobox table tbody tr:hover td {
  background-color: #eceef1;
  color: #35bf22;
}
.table-combobox table tbody tr.selected td {
  background-color: #35bf22;
  color: #fff;
}
.add-btn {
  width: 30px;
  height: 30px;
  position: absolute;
  right: 30px;
  top: 1px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-right: 1px solid #babec5;
}
.add-btn:hover {
  background-color: #eceef1;
}
tbody > tr td {
  border-right: unset;
  border-bottom: unset;
}
thead > tr th {
  border-right: unset;
  border-bottom: unset;
}
.item .data-content {
  overflow-wrap: break-word;
}
</style>
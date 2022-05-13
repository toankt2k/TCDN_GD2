<template>
  <div class="dropdown-list" ref="dropdown">
    <div
      @click="select($event, item.key)"
      class="item"
      v-for="(item, index) in listItem"
      :key="index"
    >
      {{ item.name }}
    </div>
  </div>
</template>

<script>
export default {
  name: "DropdownList",
  components: {},
  props: {
    //giá trị css top của phần tử
    offsetX: {
      type: Number,
      default: 0,
    },
    //giá trị css left của phần tử
    offsetY: {
      type: Number,
      default: 0,
    },
    //độ cao của màn hình
    maxHeight: {
      type: Number,
      default: 0,
    },
    listItem: {
      type: Array,
      default: () => [],
    },
  },
  methods: {
    /**
     * Mô tả : chọn chức năng xóa
     * Created by: Nguyễn Đức Toán - MF1095 (14/04/2022)
     */
    select(e, key) {
      e.preventDefault();
      this.$emit("selected", key);
    },
    /**
     * Mô tả : click ra ngoài dropdown list
     * Created by: Nguyễn Đức Toán - MF1095 (10/04/2022)
     */
    clickOutSide() {
      this.$emit("outSide", false);
    },
  },
  /**
  * Mô tả : khi component được render thì sẽ xuất hiện theo vị trí chỉ định
  * Created by: Nguyễn Đức Toán - MF1095 (25/04/2022)
  */
  mounted() {
    //độ cao của item box
    let clientHeight = this.$refs.dropdown.clientHeight;
    //độ rộng của item box
    let clientWidth = this.$refs.dropdown.clientWidth;
    //position top của item box
    let top = this.offsetY;
    //nếu vị trí xuất hiện của itembox bị che bởi màn hình thì giảm top để ittembox xuất hiện trong màn hình
    if (top + clientHeight >= this.maxHeight) {
      top = top - clientHeight;
    }
    //set vị trí cho itembox
    this.$refs.dropdown.style.top = top + "px";
    //set vị trí cho item bõ hiện trong màn hình
    this.$refs.dropdown.style.left = this.offsetX - clientWidth + 30 + "px";
    //sự kiện click outside
    window.addEventListener("click", this.clickOutSide);
  },
  /**
   * Mô tả : trước khi tắt dropdown thì hủy sự kiện click outside
   * Created by: Nguyễn Đức Toán - MF1095 (11/04/2022)
   */
  beforeUnmount() {
    //sự kiện click outside
    window.removeEventListener("click", this.clickOutSide);
  },
  watch: {
    offsetY(val) {
      //độ cao của itembox
      let clientHeight = this.$refs.dropdown.clientHeight;
      //set vị trí cho item box
      let top = val;
      if (top + clientHeight >= this.maxHeight) {
        top = top - clientHeight;
      }
      this.$refs.dropdown.style.top = `${top + 10}px`;
    },
    offsetX(val) {
      //độ rộng của itembox
      let clientWidth = this.$refs.dropdown.clientWidth;
      // set lại vị trí
      this.$refs.dropdown.style.left = `${val - clientWidth + 30}px`;
    },
  },
};
</script>

<style scoped>
.dropdown-list {
  border: 1px solid #c7c7c7;
  z-index: 21;
  position: fixed;
  width: auto;
  top: 0px;
  left: 0px;
  min-width: 100px;
  background-color: #fff;
  padding: 2px 1px;
}

.dropdown-list .item {
  height: 28px;
  line-height: 28px;
  padding: 0 10px;
}
.dropdown-list .item:hover {
  background-color: #e8e9ec;
  color: #08bf1e;
  cursor: pointer;
  transition: color 0.4s;
}
</style>
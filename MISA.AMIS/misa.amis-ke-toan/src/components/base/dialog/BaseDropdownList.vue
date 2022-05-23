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
    listItem: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      //giá trị css top của phần tử
      offsetX: 0,
      //giá trị css left của phần tử
      offsetY: 0,
      //độ cao màn hình
      maxHeight: 0,
    };
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
    /**
     * Mô tả : set position left cho component
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    setX(value) {
      try {
        this.offsetX = value;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : set position top cho component
     * Created by: Nguyễn Đức Toán - MF1095 (20/05/2022)
     */
    setY(value) {
      try {
        this.offsetY = value;
      } catch (error) {
        console.log(error);
      }
    },
  },
  /**
   * Mô tả : khi component được render thì sẽ xuất hiện theo vị trí chỉ định
   * Created by: Nguyễn Đức Toán - MF1095 (25/04/2022)
   */
  mounted() {
    //sự kiện click outside
    window.addEventListener("click", this.clickOutSide);
    this.maxHeight = document.body.clientHeight;
    this.maxWidth = document.body.clientWidth;
  },
  /**
   * Mô tả : trước khi tắt dropdown thì hủy sự kiện click outside
   * Created by: Nguyễn Đức Toán - MF1095 (11/04/2022)
   */
  beforeUnmount() {
    //sự kiện click outside
    window.removeEventListener("click", this.clickOutSide);
  },

  created() {},
  watch: {
    offsetY(val) {
      this.$nextTick(() => {
        let client = this.$refs['dropdown'].getBoundingClientRect();
        let top = val;
        if(client.height + top > this.maxHeight){
          top -= client.height;
        }
        this.$refs.dropdown.style.top = `${top + 10}px`;
      });
    },
    offsetX(val) {
      this.$nextTick(() => {
        let client = this.$refs['dropdown'].getBoundingClientRect();
        let left = val;
        if(client.width + left > this.maxWidth){
          left -= client.width;
        }
        this.$refs.dropdown.style.left = `${left}px`;
      });
      
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
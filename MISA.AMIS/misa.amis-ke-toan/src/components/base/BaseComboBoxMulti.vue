<template>
  <div class="combobox-container"  ref="combobox" :title="titleError" :style="{'background-color':disabled?'#3b3b3b0f':''}">
    <div class="value-combobox d-flex alignt-center">
      <div
        class="select-item"
        v-for="(value, index) in valueChecked"
        :key="index"
      >
        <div class="d-flex alignt-center">
          <div class="item-text">
            {{ value[code] }}
          </div>
          <div class="item-icon mi mi-16" :title="value[code]" @click="handleSelect(value)"></div>
        </div>
      </div>
      <input
      ref="input"
        type="text"
        @keyup="deleteEvent($event)"
        v-model="valueCB"
        @keydown="handleDeleteEvent($event)"
        @input="handleInput"
        @focus="setFocus(true)"
        :disabled="disabled"
      />
    </div>
    <div class="add">
      <button :style="{'background-color':disabled?'#3b3b3b00':''}">
        <div class="mi mi-16"></div>
      </button>
    </div>
    <div class="action" style="border-left:1px solid #bbb; border-radius-top-left:0;border-radius-bottom-left:0">
      <button @click="!this.disabled?isShow = !isShow:isShow=isShow" :style="{'background-color':disabled?'#3b3b3b00':''}">
        <div class="mi mi-16"></div>
      </button>
    </div>
    <div
      class="combobox-option"
      v-show="isShow"
      :class="{ dropUp: !isDropDown }"
    >
      <table :class="{ 'table-one-column': isOneColumn }">
        <thead>
          <tr>
            <th style="font-weight: 600">Mã nhóm NCC</th>
            <th style="font-weight: 600">Tên nhóm NCC</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(value, index) in values"
            :key="index"
            @click="handleSelect(value)"
            :class="{ active: value[id] == modelValue }"
          >
            <td>{{ value[code] }}</td>
            <td>{{ value[name] }}</td>
            <td class="selected-container">
              <div
                class="selected-icon mi mi-16"
                v-if="checkValueChecked(value)"
              ></div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
/**
 * Mô tả:  Combobox
 * Created by: Nguyễn Đức Toán - MF1095 (18/05/2022)
 * Created date: 14/04/2022
 */
import axios from "axios";
export default {
  name: "MComboBoxMulti",
  data() {
    return {
      isShow: false,
      valueCB: "",
      values: [],
      titleError: "",
      error: false,

      //Giá trị id các giá trị được chọn
      valueChecked: [],
      objectChecked: [],

      timeOut: null,
      model: [],
      onFocus:false,
    };
  },
  props: {
    Api: {
      type: String,
      default: "",
    },
    modelValue: {
      type: String,
      default: "",
    },
    id: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
    code: {
      type: String,
      defaule: "",
    },
    componentDes: {
      type: String,
      default: "",
    },
    isDropDown: {
      type: Boolean,
      default: true,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    valueOption: {
      type: Array,
      default: () => [],
    },
    isOneColumn: {
      type: Boolean,
      default: false,
    },
    timer: {
      type: Number,
      default: 1000,
    },
  },
  created() {
    try {
      //Lấy dữ liệu combobox
      //Nếu Api có, gọi Api lấy giữ liệu
      if (this.modelValue) {
        this.valueChecked = JSON.parse(this.modelValue);
      }
      if (this.Api != "") {
        axios
          .get(`${this.Api}/filter?currentPage=1&pageSize=10&filterText=`)
          .then((response) => {
            if (response.status === 200) {
              this.values = response.data.List;
            }
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        this.values = this.valueOption;
        /**
         * Nếu Api không có, lấy mảng trong prop
         * Nếu modelValue khác rỗng và số giá trị mảng option combobox
         * lớn hơn không thì gán giá trị input bằng giá trị value tương ứng
         */
        if (this.modelValue !== "" && this.values.length > 0) {
          var value = this.values.find(
            (item) => item[this.id] == this.modelValue
          );
          if (value != undefined) {
            this.valueCB = value[this.name];
          }
        }
      }
    } catch (error) {
      console.log(error);
    }
  },
  watch: {
    /**
     * Mô tả:  Lăng nghe thay đổi giá trị model
     * Nếu model rỗng thì gán giá trị input bằng rỗng
     * Created by: Nguyễn Đức Toán - MF1095 (18/05/2022)
     * Created date: 14/04/2022
     */
    modelValue(newVal,oldVal) {
      try {
        if(newVal==oldVal)return;
        if (newVal == "") {
          this.valueCB = "";
        }else{
          this.valueChecked = JSON.parse(newVal)
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  methods: {
    /**
     * Mô tả : load laij danh sach các lựa chọn
     * Created by: Nguyễn Đức Toán - MF1095 (18/05/2022)
     */
    reloadOption() {
      try {
        if (this.Api != "") {
          var apiSearch;
          if (this.valueCB.trim() === "") {
            apiSearch = `${this.Api}/filter?currentPage=1&pageSize=10&filterText=`;
          } else apiSearch = `${this.Api}/filter?currentPage=1&pageSize=10&filterText=${this.valueCB}`;
          axios
            .get(apiSearch)
            .then((response) => {
              if (response.status === 200) {
                this.values = response.data.List;
                this.isShow = true;

              }
            })
            .catch((e) => {
              console.log(e);
            });
        } else {
          this.values = this.valueOption;
          /**
           * Nếu Api không có, lấy mảng trong prop
           * Nếu modelValue khác rỗng và số giá trị mảng option combobox
           * lớn hơn không thì gán giá trị input bằng giá trị value tương ứng
           */
          if (this.modelValue !== "" && this.values.length > 0) {
            var value = this.values.find(
              (item) => item[this.id] == this.modelValue
            );
            if (value != undefined) {
              this.valueCB = value[this.name];
            }
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    handleInput() {
      try {
        if (this.timer === 0) {
          this.reloadOption();
        } else {
          clearTimeout(this.timeOut);
          this.timeOut = setTimeout(() => {
            this.reloadOption();
          }, this.timer);
        }
      } catch (error) {
        console.log(error);
      }
    },
    deleteEvent(e) {
      e.preventDefault();
    },
    handleDeleteEvent(e) {
      try {
        if (!this.valueCB) {
          if (e.keyCode == 8 && this.valueChecked.length > 0) {
            this.valueChecked.pop();
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    checkValueChecked(value) {
      try {
        if(!this.valueChecked)return false;
        var checked = this.valueChecked.find((x) => x[this.id] == value[this.id]);
        if (checked) return true;
        return false;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả: Xử lý khi chọn combobox
     * Created by: Nguyễn Đức Toán - MF1095 (18/05/2022)
     * Created date: 14/04/2022
     */
    handleSelect(value) {
      try {
        if(this.disabled)return;
        var checked = this.valueChecked.find((x) => x[this.id] == value[this.id]);
        if (!checked) {
          this.valueChecked.push(value);
        } else {
          this.valueChecked = this.valueChecked.filter(
            (x) => x[this.id] != value[this.id]
          );
        }
        var id = this.id;
        var code = this.code;
        var name = this.name;
        var modelValue = this.valueChecked.map((x) => {
          var obj = {};
          obj[id] = x[id];
          obj[code] = x[code];
          obj[name] = x[name];
          return obj;
        });
        this.$emit("update:modelValue", JSON.stringify(modelValue));
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Thêm class lỗi vào combobox
     * Created by: Nguyễn Đức Toán - MF1095 (18/05/2022)
     * Created date: 14/04/2022
     */
    addError(className, errorMsg) {
      try {
        this.error = true;
        this.$refs["combobox"].classList.add(className);
        if (errorMsg != undefined) {
          this.titleError = `${this.componentDes} ${errorMsg}`;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Xáo class lỗi khỏi combobox
     * Created by: Nguyễn Đức Toán - MF1095 (18/05/2022)
     * Created date: 14/04/2022
     */
    removeClass(className) {
      try {
        this.$refs["combobox"].classList.remove(className);
        this.error = false;
        this.titleError = "";
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : set focus cho combobox
     * Created by: Nguyễn Đức Toán - MF1095 (18/05/2022)
     */
    setFocus(focus) {
      try {
        this.onFocus = true;
        if (focus) {
          this.$refs.input.focus();
          this.focus = false;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
.combobox-container {
  border: 1px solid #afafaf;
  border-radius: 2px;
  display: flex;

  position: relative;
  font-size: 13px;
}
.error {
  border: 1px solid red;
}
.combobox-container:focus-within {
  border: 1px solid #2ca01c;
}

.value-combobox {
  flex: 1;
  height: 100%;
  padding: 3px 12px;
  flex-wrap: wrap;
  min-height: 30px;
}
.select-item {
  background-color: #e0e0e0;
  min-width: 20px;
  padding: 3px 7px;
  position: relative;
  border-radius: 3px;
  border: 1px solid #ccc;
  font-size: 11px;
}
.select-item {
  margin-right: 4px;
  margin-bottom: 4px;
}
.select-item .item-text {
  margin-right: 5px;
}
.select-item .item-icon {
  background-position: -80px -312px;
}

.value-combobox input {
  width: 100%;
  border: none;
  outline: none;
  border-radius: 2px;
}
.add,
.action {
  width: 30px;
  border-radius: 2px;
  border: none;
  position: relative;
}
.add button,
.action button {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  border: none;
  outline: none;
  border-radius: 2px;
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #fff;
}
.add button:hover,
.action button:hover {
  background-color: #e0e0e0;
  border-color: #e0e0e0;
}
.action button div {
  background-position: -560px -359px;

  margin: 0;
}
.add button div {
  background-position: -32px -312px;
}
.combobox-option {
  position: absolute;
  left: -1px;
  right: -1px;
  top: calc(100% + 5px);
  background-color: #fff;
  border: 1px solid #afafaf;
  border-radius: 2px;
  z-index: 100;
  max-height: 160px;
  overflow-y: auto;
}
.dropUp {
  top: unset;
  bottom: 130%;
}
.combobox-option table {
  width: 100%;
  border-spacing: 0;
}
.combobox-option table td,
.combobox-option table th {
  border: none;
  text-align: left;
  min-width: 30px;
  height: 32px;
  font-weight: 400;
}
.combobox-option table thead {
  background-color: #f4f5f8;
  color: #111;
  cursor: default;
  position: sticky;
  top: 0;
}
.table-one-column thead {
  display: none;
}
.table-one-column tbody tr td:first-child {
  display: none;
}
td,
th {
  text-align: left;
  padding: 0 14px 0 10px;
}
.combobox-option table tbody tr {
  cursor: pointer;
}
.combobox-option table tbody tr:hover td {
  background-color: #ebedf0;
  color: #35bf22;
}
.combobox-option table tbody tr .selected-container .selected-icon {
  background-position: -896px -312px;
}
.active {
  color: #fff !important;
  background-color: #2ca01c !important;
}

.active:hover > td {
  color: #fff !important;
  background-color: #2ca01c !important;
}
.parent {
  font-weight: 700;
  font-family: MISA notosans bold;
}
</style>
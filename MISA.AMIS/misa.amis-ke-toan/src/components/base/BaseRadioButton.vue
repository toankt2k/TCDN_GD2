<template>
  <div class="group-button">
    <div class="m-radio-button" v-for="(val, index) in buttonList" :key="index">
      <input
        type="radio"
        :id="`rb${val[id]}`"
        :name="id"
        :checked="modelValue == val[id]"
        @change="$emit('update:modelValue', val[id])"
      />
      <label :for="`rb${val[id]}`">
        <span class="check"></span>
        <span class="text">{{ val[name] }}</span>
      </label>
    </div>
  </div>
</template>

<script>
export default {
  name: "RadioButton",
  components: {},
  /**
   * Mô tả : khởi tạo các props cho component
   * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
   */
  props: {
    /**
     * Mô tả : danh sách gồm name, id , label của group radiobutton
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    button: {
      type: Array,
      default: () => [
        {
          GenderId: "1",
          GenderName: "Nam",
        },
        {
          GenderId: "0",
          GenderName: "Nữ",
        }
      ],
    },
    /**
     * Mô tả : id là tên của thuộc tính id trong mảng dữ liệu vd id="GenderId" để gán giá trị cho button
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    id: {
      type: String,
      default: "GenderId",
    },
    /**
     * Mô tả : id là tên của thuộc tính name trong mảng dữ liệu vd mame="GenderName" để hiển thị lên label
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    name: {
      type: String,
      default: "GenderName",
    },
    /**
     * Mô tả : binding dữ liệu với v-model của compoennt cha
     * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
     */
    modelValue: {
      type: String,
      default: "",
    },
  },
  /**
   * Mô tả : khai báo và định nghĩa các hàm dùng trong component
   * Created by: Nguyễn Đức Toán - MF1095 (08/04/2022)
   */
  methods: {
    getData() {},
  },
  data() {
    return {
      buttonList:this.button,
    }
  },
};
</script>

<style scoped>
@import url("@/css/components/radio-button.css");
</style>
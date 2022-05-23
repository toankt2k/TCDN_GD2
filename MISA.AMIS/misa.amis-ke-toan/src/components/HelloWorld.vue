<template>
  <div class="hello-world">
    <MButton
      :text="'Tùy chỉnh giao diện'"
      @click="openSetting"
      :buttonType="'primary'"
      :isDropdown="true"
    />
    <MButton
      :text="'dialog vendor'"
      @click="openVendor"
      :buttonType="'primary'"
      :isDropdown="true"
    />
    <component :is="theComponent" v-bind="somethingWeWantToPass"></component>
    <MSetting v-if="isSetting" @exitSetting="exitSetting"/>
    <VendorDetail v-if="vendorOpen" @exitForm="exitVendor"/>
    <MTextArea :placeholder="'VD: Số 82 Duy Tân, Dịch Vọng, Cầu Giấy, Hà Nội'" />

    

  </div>
</template>

<script>
// import ThePaymentDetail from '@/view/ca/ThePaymentDetail.vue'
import MButton from "@/components/base/button/BaseButton.vue";
import MSetting from "@/components/base/dialog/BaseCustomSetting.vue";
import VendorDetail from "@/view/vendor/VendorDetail.vue";
import MTextArea from "@/components/base/input/BaseTextArea.vue";
export default {
  name: "HelloWorld",
  components: {
    // ThePaymentDetail,
    MButton,
    MSetting,
    VendorDetail,
    MTextArea,
  },
  props: {},
  data() {
    return {
      theComponent:'MButton',
      somethingWeWantToPass:{
        text:"Tùy chỉnh giao diện",
        // @click="openSetting"
        buttonType:"primary",
        isDropdown:"true",
      },
      isSetting: false,
      vendorOpen: false,
    };
  },
  methods: {
    openSetting() {
      this.isSetting = true;
    },
    exitSetting(){
      this.isSetting=false;
    },
    openVendor() {
      this.vendorOpen = true;
    },
    exitVendor(){
      this.vendorOpen=false;
    }
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>

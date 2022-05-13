<template>
  <div id="cash" style="padding-left:20px">
    <TheTabbar :tabList="this.dataStorage.CATab" />
    <router-view></router-view>
  </div>
</template>

<script>
import TheTabbar from "@/components/layout/TheTabbar.vue";
export default {
  components: {
    TheTabbar,
  },
};
</script>

<style>
</style>
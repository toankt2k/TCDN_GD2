<template>
  <div class="m-content">
    <MHeader />
    <router-view></router-view>
  </div>
</template>

<script>
import MHeader from "@/components/layout/content/TheHeader.vue";
export default {
  name: "TheContent",
  components: {
    MHeader,
  },
};
</script>

<style scoped>
@import url("@/css/components/content.css");
</style>
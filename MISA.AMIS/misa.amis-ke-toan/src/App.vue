<template>
  <div class="app" id="app">
    <TheNavbar />
    <TheContent />
  </div>
</template>

<script>
import TheNavbar from '@/components/layout/TheNavbar.vue';
import TheContent from '@/components/layout/content/TheContent.vue';
export default {
  name: 'App',
  components: {
    TheNavbar,
    TheContent
  },
}
</script>

<style>
@import url("@/css/common/main.css");
</style>

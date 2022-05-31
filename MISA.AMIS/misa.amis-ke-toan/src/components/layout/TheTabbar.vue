<template>
  <div class="m-tab-bar">
    <router-link :to="val.link" active-class="selected" class="tab-item" v-for="val in tabList" :key="val.id">
      {{ val.text }}
    </router-link>
  </div>
</template>

<script>
export default {
  name: "TheTabbar",
  props: {
    tabList: {
      type: Array,
      default: () => [],
    },
  },
};
</script>

<style scoped>
.m-tab-bar {
  margin-top: 14px;
  height: 50px;
  width: 100%;
  display: flex;
  align-items: center;
  border-bottom: 1px solid #eceef1;
}
.m-tab-bar .tab-item {
  padding: 15px 20px;
  font-weight: 400;
}
a{
  color: #111;
}
.m-tab-bar .tab-item.selected {
  padding: 15px 20px;
  border-bottom: 4px solid #2ca01c;
  font-weight: 700;
  font-family: Notosans-bold;
}
.m-tab-bar .tab-item:hover {
  color: #2ca01c !important;
  cursor: pointer;
}

</style>
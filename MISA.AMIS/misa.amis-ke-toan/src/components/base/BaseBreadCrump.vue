<template>
  <div class="breadcump-area">
    <div class="breadcump">
      <div class="icon"></div>
      <a class="bread-link">Tất cả danh mục</a>
    </div>
  </div>
</template>

<script>
export default {
    name:"BreadCump",
    components:{}
};
</script>

<style scoped>
.breadcump-area{
    height: 18px;

}
.breadcump{
    display: flex;
    align-items: center;
}
.breadcump-area .breadcump .icon{
    height: 16px;
    width: 16px;
    background: url('@/assets/img/Sprites.64af8f61.svg') no-repeat;
    background-position: -224px -360px;
}
.breadcump-area .breadcump a{
    cursor: pointer;
    color: #0075c0;
}



</style>
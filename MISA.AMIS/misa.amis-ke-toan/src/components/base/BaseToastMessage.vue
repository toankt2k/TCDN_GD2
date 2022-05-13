<template>
  <div class="m-toast"  :class="type">
    <div class="m-toast-messenger">
      <div class="toast-img"></div>
      <div class="toast-text">{{message}}</div>
    </div>
  </div>    
</template>

<script>
export default {
    name:"ToasMessage",
    props:{
        //kiểu toastmessage : success, warnignng, error, info
        type:{
            type:String,
            default:""
        },
        //thông báo hiển thị theo toast
        message:{
            type:String,
            default:""
        }
    }
}
</script>

<style scoped>


/* error */
.m-toast {
 min-width: 300px;
}

.m-toast.error {
    position: relative;
    padding :10px;
    min-width: 300px;
    background-color: #fff;
    border-radius: 10px;
    font-size: 13px;
    margin-top: 16px;
    padding-right: 64px;
    box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.171);
    border: 1px solid #ff7777;
    animation: slidein 1s;
    /* , slideout 1s 3s forwards */
}

.m-toast .m-toast-messenger {
    cursor: pointer;
    display: flex;
    height: 100%;
    width: 100%;
}

.m-toast.error .m-toast-messenger .toast-img {
    background-image: url('@/assets/img/Sprites.64af8f61.svg');
    background-position: -746px -456px;
    background-repeat: no-repeat;
    width: 48px;
    height: 48px;
    min-width: 48px;
    min-height: 48px;
}
.m-toast.error .m-toast-messenger .toast-text {
    font-size: 13px;
    padding: 13px 10px 10px 10px;
}
/* success */
.m-toast.success {
    position: relative;
    padding :10px;
    min-width: 300px;
    background-color: #fff;
    border-radius: 10px;
    font-size: 13px;
    margin-top: 16px;
    padding-right: 64px;
    box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.171);
    border: 1px solid #2ca01c;
    animation: slidein 1s;
    /* , slideout 1s 3s forwards */
}


.m-toast.success .m-toast-messenger .toast-img {
    background-image: url('@/assets/img/Sprites.64af8f61.svg');
    background-position: -984px -456px;
    background-repeat: no-repeat;
    width: 48px;
    height: 48px;
    min-width: 48px;
    min-height: 48px;
}
.m-toast.success .m-toast-messenger .toast-text {
    font-size: 13px;
    padding: 13px 10px 10px 10px;
}
/* info */
.m-toast.info {
    position: relative;
    padding :10px;
    min-width: 300px;
    background-color: #fff;
    border-radius: 10px;
    font-size: 13px;
    margin-top: 16px;
    padding-right: 64px;
    box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.171);
    border: 1px solid #0075c0;
    animation: slidein 1s;
    /* , slideout 1s 3s forwards */
}

.m-toast.info .m-toast-messenger .toast-img {
    background-image: url('@/assets/img/Sprites.64af8f61.svg');
    background-position: -666px -456px;
    background-repeat: no-repeat;
    width: 48px;
    height: 48px;
    min-width: 48px;
    min-height: 48px;
}
.m-toast.info .m-toast-messenger .toast-text {
    font-size: 13px;
    padding: 13px 10px 10px 10px;
}
/* warning */
.m-toast.warning {
    position: relative;
    padding :10px;
    min-width: 300px;
    background-color: #fff;
    border-radius: 10px;
    font-size: 13px;
    margin-top: 16px;
    padding-right: 64px;
    box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.171);
    border: 1px solid #fce100;
    animation: slidein 1s;
    /* , slideout 1s 3s forwards */
}

.m-toast.warning .m-toast-messenger .toast-img {
    background-image: url('@/assets/img/Sprites.64af8f61.svg');
    background-position: -592px -456px;
    background-repeat: no-repeat;
    width: 48px;
    height: 48px;
    min-width: 48px;
    min-height: 48px;
}
.m-toast.warning .m-toast-messenger .toast-text {
    font-size: 13px;
    padding: 13px 10px 10px 10px;
}
</style>
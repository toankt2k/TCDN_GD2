<template>
    <div ref="toolTip" class="tooltip" :style="`top: ${top};left: ${left}`" >
        <span >{{valueTooltip}}</span>
    </div>
</template>

<script>
/**
* Mô tả:  Tooltip
* Created by: Đinh Văn Khánh - MF1112
* Created date: 18/04/2022
*/
export default {
    name:"ToolTip",
    data() {
        return {
        }
    },
    props:{
        //text hiển thị được truyền vào
        valueTooltip:{
            type:String,
            default:'Tooltip'
        },
        //vị trí hiển thị của tooltip:top
        top:{
            type:String,
            default:"0px"
        },
        //vị trí hiển thị của tooltip:left
        left:{
            type:String,
            default:"0px"
        },
    }
}
</script>
<style scoped>
.tooltip{
    position: fixed;
    color: #fff;
    background-color: rgb(81, 81, 81);
    z-index: 100;
    padding: 5px;
    font-size: 12px;
    
}
.tooltip span{
    height: 100px;
   
}
</style>
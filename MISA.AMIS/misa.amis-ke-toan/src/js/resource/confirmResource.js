const confirmResource = {
    //thông tin confirm cho diaolg confirm tính năng đag phát triển
    funcImprov: {
        button: [{
            id: 1,
            name: "Đóng",
            type: "primary",
            align: "left",
        }, ],
        getText() {
            return 'Tính năng đang phát triển. Vui lòng thử lại sau.'
        },
    }
}
export default confirmResource;
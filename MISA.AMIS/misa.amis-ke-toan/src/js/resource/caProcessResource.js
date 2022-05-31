const processResource = {
    /**
     * Mô tả : danh sách các utility btn trong trang process
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    utilityBtn: [{
            id: "1",
            text: "Nhà cung cấp",
            classIcon: "ic-process-provider",
            funcImprovConfirm: false,
            link: '/VendorList'
        },
        {
            id: "2",
            text: "Hệ thông tài khoản",
            classIcon: "ic-process-caculator",
            funcImprovConfirm: true,

        },
    ]
}
export default processResource;
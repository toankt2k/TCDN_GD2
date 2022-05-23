const resources = {
    confirmDialogData: {
        /**
         * Mô tả : danh sach các button cho confirm xóa nhiều
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        deleteMultiple: [{
                id: 1,
                name: "Không",
                type: "default",
                align: "left",
            },
            {
                id: 2,
                name: "Có",
                type: "primary",
                align: "right",
            },
        ],
        /**
         * Mô tả : danh sach các button cho confirm xóa
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        delete: [{
                id: 1,
                name: "Không",
                type: "default",
                align: "left",
            },
            {
                id: 2,
                name: "Có",
                type: "primary",
                align: "right",
            },
        ],

        /**
         * Mô tả : danh sách các thông tin diển thị lên confirm khi lỗi nhập liệu
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        errorInputConfirm: [{
            id: 1,
            name: "Đóng",
            type: "primary",
            align: "center",
        }, ],
        /**
         * Mô tả : danh sách các button cho confirm thoát form
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        exitConfirm: [{
                id: 1,
                name: "Hủy",
                type: "default",
                align: "left",
            },
            {
                id: 3,
                name: "Có",
                type: "primary",
                align: "right",
            },
            {
                id: 2,
                name: "Không",
                type: "default",
                align: "right",
            },
        ],
        /**
         * Mô tả : danh sách các button cho confirm khi có response trả về
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        errorResponse: [{
            id: 1,
            name: "Đồng ý",
            type: "primary",
            align: "right",
        }, ],
        /**
         * Mô tả : danh sách các thông tin diển thị lên confirm khi lỗi nhập liệu
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        errorInputMsg: {
            email: {
                key: "Email",
                name: "Email không đúng định dạng",
            },
            phoneNumber: {
                key: "PhoneNumber",
                name: "Số điện thoại không đúng định dạng",
            },
            departmentId: {
                key: "DepartmentId",
                name: "Mã phòng ban không được để trống",
            },
            employeeName: {
                key: "EmployeeName",
                name: "Tên nhân viên không được để trống",
            },
            employeeCode: {
                key: "EmployeeCode",
                name: "Mã nhân viên không được để trống",
            },

        },

        /**
         * Mô tả : trả về text confirm cho confirm xóa nhiều
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        getMultiDeleteConfirm() {
            return `Bạn có thực sự muốn xóa những nhân viên đã chọn không?`
        },
        /**
         * Mô tả : trả về text confirm cho confirm xóa 1 người
         * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
         */
        getDeleteConfirm(employeeCode) {
            return `Bạn có thực sự muốn xóa nhân viên <${employeeCode}> không?`
        }
    },
    /**
     * Mô tả : resource cho dropdown dialog
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    dropdownData: {
        //các item cho chức năng xóa hàng loạt
        multipleFunction: [
            { name: "Xóa hàng loạt", key: "multiDelete" },
        ],
        //các item cho chức năng của bảng vendor
        tableVendorFunction: [
            { name: "Sửa", key: "edit" },
            { name: "Xem", key: "view" },
            { name: "Xóa", key: "delete" },
        ],
        //các item cho chức năng của bảng payment
        tablePaymentFunction: [
            { name: "Xem", key: "view" },
            { name: "Xóa", key: "delete" },
            { name: "Nhân bản", key: "duplicate" },
        ],
    },
    /**
     * Mô tả : resource cho toastmessage
     * Created by: Nguyễn Đức Toán - MF1095 (27/04/2022)
     */
    toastMessage: {
        addSuccess: {
            type: "success",
            message: `Bạn vừa thêm nhân viên thành công`,
        },
        editSuccess: {
            type: "success",
            message: `Bạn vừa sửa nhân viên thành công`,
        },
        deleteSuccess: {
            type: "success",
            message: `Bạn vừa xóa nhân viên thành công`,
        }

    }
}
export default resources;
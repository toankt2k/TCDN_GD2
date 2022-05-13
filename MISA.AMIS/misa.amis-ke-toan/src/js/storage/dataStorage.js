 /**
  * Mô tả : khởi tạo data dùng chũng cho app
  * ******* 1. list item cho menu menuData
  * ******* 2. 
  * @param
  * @return
  * Created by: Nguyễn Đức Toán - MF1095
  * Created date: 13:37 04/04/2022
  */
 const dataStorage = {
     /**
      * Mô tả : danh sách cá item navbar
      * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
      */
     menuData: [{
             classLogoLink: "overview",
             text: "Tổng quan",
             link: "/"
         },
         {
             classLogoLink: "money",
             text: "Tiền mặt",
             link: "/CA"
         },
         {
             classLogoLink: "bank",
             text: "Gửi tiền",
             link: "/",
         },
         {
             classLogoLink: "buy",
             text: "Mua hàng",
             link: "/"
         },
         {
             classLogoLink: "sell",
             text: "Bán hàng",
             link: "/"
         },
         {
             classLogoLink: "invoice",
             text: "Quản lý hóa đơn ",
             link: "/"

         },
         {
             classLogoLink: "stock",
             text: "Kho",
             link: "/"
         },
         {
             classLogoLink: "tools",
             text: "Công cụ dụng cụ ",
             link: "/"
         },
         {
             classLogoLink: "fix-asset",
             text: "Tài sản cố định ",
             link: "/"
         },
         {
             classLogoLink: "tax",
             text: "Thuế",
             link: "/"
         },
         {
             classLogoLink: "price",
             text: "Giá thành",
             link: "/"
         },
         {
             classLogoLink: "general",
             text: "Tổng hợp",
             link: "/"
         },
         {
             classLogoLink: "budget",
             text: "Ngân sách",
             link: "/"
         },
         {
             classLogoLink: "report",
             text: "Báo cáo",
             link: "/"
         },
         {
             classLogoLink: "finace",
             text: "Phân tích tài chính ",
             link: "/"
         },
     ],
     /**
      * Mô tả : dữ liệu cho input giói tính
      * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
      */
     genderCombobox: {
         name: "GenderName",
         id: "Gender",
         data: [{
                 Gender: "0",
                 GenderName: "Nam",
             },
             {
                 Gender: "1",
                 GenderName: "Nữ",
             },
         ],
     },
     departmentCombobox: {
         name: "DepartmentName",
         id: "DepartmentId",
         data: [],
         api: "http://localhost:5093/api/v1/Departments",
     },
     /**
      * Mô tả : các option để phân trang để phân trang
      * Created by: Nguyễn Đức Toán - MF1095 (20/04/2022)
      */
     paging: {
         defaultCurrentPage: 1,
         defaultPageSize: 20,
         pageSize: [{
                 id: "10",
                 name: "10 bản ghi trên 1 trang",
             },
             {
                 id: "20",
                 name: "20 bản ghi trên 1 trang",
             },
             {
                 id: "30",
                 name: "30 bản ghi trên 1 trang",
             },
             {
                 id: "50",
                 name: "50 bản ghi trên 1 trang",
             },
             {
                 id: "100",
                 name: "100 bản ghi trên 1 trang",
             },
         ],
     },
     api: {
         addEmployee: "http://localhost:5093/api/v1/Employees",
         updateEmployee: "http://localhost:5093/api/v1/Employees",
         getById: "http://localhost:5093/api/v1/Employees",
         getNewEmployeeCode: "http://localhost:5093/api/v1/Employees/NewEmployeeCode",
         deleteEmployee: "http://localhost:5093/api/v1/Employees",
         deleteMultiEmployee: "http://localhost:5093/api/v1/Employees/Multiple",
         filter: "http://localhost:5093/api/v1/Employees/Filter",
         export: "http://localhost:5093/api/v1/Employees/Export",
     },
     /**
      * Mô tả : danh sách các item tab cho trang tiền mặt
      * Created by: Nguyễn Đức Toán - MF1095 (04/05/2022)
      */
     CATab: [{
             text: "Quy trình",
             id: "1",
             link: "/CA/CAProcess",
         },
         {
             text: "Thu, chi tiền",
             id: "2",
             link: "/CA/CAReceipt",
         },
     ],
     CAProcessUtilityBtn: [],
     vendorDetail: {
         radioButtonTypeVendor: {
             button: [{
                     id: "1",
                     name: "Tổ chức",
                 },
                 {
                     id: "0",
                     name: "Cá nhân",
                 }
             ],
             id: 'id',
             name: 'name'
         }
     }
 }
 export default dataStorage;
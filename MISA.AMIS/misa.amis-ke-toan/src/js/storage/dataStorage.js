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
         //  addEmployee: "http://localhost:5093/api/v1/Employees",
         //  updateEmployee: "http://localhost:5093/api/v1/Employees",
         //  getById: "http://localhost:5093/api/v1/Employees",
         //  getNewEmployeeCode: "http://localhost:5093/api/v1/Employees/NewEmployeeCode",
         //  deleteEmployee: "http://localhost:5093/api/v1/Employees",
         //  deleteMultiEmployee: "http://localhost:5093/api/v1/Employees/Multiple",
         //  filter: "http://localhost:5093/api/v1/Employees/Filter",
         //  export: "http://localhost:5093/api/v1/Employees/Export",
         employee: {
             add: "http://localhost:5093/api/v1/Employees/",
             update: "http://localhost:5093/api/v1/Employees/",
             delete: "http://localhost:5093/api/v1/Employees/",
             multi: "http://localhost:5093/api/v1/Employees/Multiple",
             filter: "http://localhost:5093/api/v1/Employees/filter",
             export: "http://localhost:5093/api/v1/Employees/export",
             getAll: "http://localhost:5093/api/v1/Employees",
             getById: "http://localhost:5093/api/v1/Employees",
             getNewCode: "http://localhost:5093/api/v1/Employees/NewEmployeeCode",
         },
         payment: {
             add: "http://localhost:5093/api/v1/Payments",
             update: "http://localhost:5093/api/v1/Payments",
             delete: "http://localhost:5093/api/v1/Payments",
             multi: "http://localhost:5093/api/v1/Payments/Multiple",
             filter: "http://localhost:5093/api/v1/Payments/filter",
             export: "http://localhost:5093/api/v1/Payments/export",
             getAll: "http://localhost:5093/api/v1/Payments",
             getById: "http://localhost:5093/api/v1/Payments",
             getNewCode: "http://localhost:5093/api/v1/Payments/NewPaymentCode",
         },
         paymentDetail: {
             add: "http://localhost:5093/api/v1/PaymentDetails",
             update: "http://localhost:5093/api/v1/PaymentDetails",
             delete: "http://localhost:5093/api/v1/PaymentDetails",
             multi: "http://localhost:5093/api/v1/PaymentDetails/Multiple",
             filter: "http://localhost:5093/api/v1/PaymentDetails/filter",
             export: "http://localhost:5093/api/v1/PaymentDetails/export",
             getAll: "http://localhost:5093/api/v1/PaymentDetails",
             getById: "http://localhost:5093/api/v1/PaymentDetails",
             getNewCode: "http://localhost:5093/api/v1/PaymentDetails/NewPaymentCode",
             getByPayment: "http://localhost:5093/api/v1/PaymentDetails/ByPayment"
         },
         vendor: {
             add: "http://localhost:5093/api/v1/Vendors",
             update: "http://localhost:5093/api/v1/Vendors",
             delete: "http://localhost:5093/api/v1/Vendors",
             multi: "http://localhost:5093/api/v1/Vendors/Multiple",
             filter: "http://localhost:5093/api/v1/Vendors/filter",
             export: "http://localhost:5093/api/v1/Vendors/export",
             getAll: "http://localhost:5093/api/v1/Vendors",
             getById: "http://localhost:5093/api/v1/Vendors",
             getNewCode: "http://localhost:5093/api/v1/Vendors/NewVendorCode",
         },
         vendorGroup: {
             add: "",
             update: "",
             delete: "",
             multi: "http://localhost:5093/api/v1/vendorGroups/Multiple",
             filter: "http://localhost:5093/api/v1/vendorGroups/filter",
             export: "http://localhost:5093/api/v1/vendorGroups/export",
             getAll: "http://localhost:5093/api/v1/VendorGroups",
             getById: "",
             getNewCode: "",
         },
         tableOption: {
             updateByCode: "http://localhost:5093/api/v1/TableOptions/ByCode",
             getByCode: "http://localhost:5093/api/v1/TableOptions/ByCode",
         },
         address: {
             getAllCountries: "http://saugi.xyz:5000/api/v1/Countries",
             getAllProvinces: "http://saugi.xyz:5000/api/v1/Provinces",
             getProvince: "http://saugi.xyz:5000/api/v1/Provinces",
             getByCountry: "http://saugi.xyz:5000/api/v1/Provinces/ByCountry",
             getAllDistricts: "http://saugi.xyz:5000/api/v1/Districts",
             getDistrict: "http://saugi.xyz:5000/api/v1/Districts",
             getByProvince: "http://saugi.xyz:5000/api/v1/Districts/ByProvince",
             getAllWards: "http://saugi.xyz:5000/api/v1/Wards",
             getWard: "http://saugi.xyz:5000/api/v1/Wards",
             getByDistrict: "http://saugi.xyz:5000/api/v1/Wards/ByDistrict",
         },
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
         vendorProp: [{
                 id: "VendorCode",
                 name: "Mã đối tượng",
             },
             {
                 id: "VendorName",
                 name: "Đối tượng",
             },
             {
                 id: "Address",
                 name: "Địa chỉ",
             },
             {
                 id: "TaxCode",
                 name: "Mã số thuế",
             },
             {
                 id: "PhoneNumber",
                 name: "Số điện thoại",
             },
         ],
         radioButtonTypeVendor: {
             button: [{
                     id: "0",
                     name: "Tổ chức",
                 },
                 {
                     id: "1",
                     name: "Cá nhân",
                 }
             ],
             id: 'id',
             name: 'name'
         },
         prefixNameCombobox: {
             button: [{
                     id: "0",
                     name: "Anh",
                 },
                 {
                     id: "1",
                     name: "Bà",
                 },
                 {
                     id: "3",
                     name: "Bạn",
                 },
                 {
                     id: "4",
                     name: "Chị",
                 },
                 {
                     id: "5",
                     name: "Mr",
                 },
                 {
                     id: "6",
                     name: "Mrs",
                 },
                 {
                     id: "7",
                     name: "Miss",
                 },
                 {
                     id: "8",
                     name: "Ông",
                 },
             ],
             id: 'id',
             name: 'name'
         },
         contractCombobox: [{
                 ContractId: 1,
                 ContractName: "Điều khoản 1",
                 ContractCode: "DK1"
             },
             {
                 ContractId: 2,
                 ContractName: "Điều khoản 2",
                 ContractCode: "DK2"
             },
             {
                 ContractId: 3,
                 ContractName: "Điều khoản 3",
                 ContractCode: "DK3"
             },
         ],
         debitAccount: [{
                 DebitPaymentAccountId: 1,
                 DebitPaymentAccountCode: "1111",
                 DebitPaymentAccountName: "Tài khoản của người bán"
             },
             {
                 DebitPaymentAccountId: 2,
                 DebitPaymentAccountCode: "11112",
                 DebitPaymentAccountName: "Tài khoản của người mua"
             },
             {
                 DebitPaymentAccountId: 3,
                 DebitPaymentAccountCode: "11113",
                 DebitPaymentAccountName: "Tài khoản của người lao động"
             },

         ],
         accountDebit: [{
                 DebitAccountId: 1,
                 DebitAccountCode: "1111",
                 DebitAccountName: "Tài khoản của người bán"
             },
             {
                 DebitAccountId: 2,
                 DebitAccountCode: "11112",
                 DebitAccountName: "Tài khoản của người bán"
             },
             {
                 DebitAccountId: 3,
                 DebitAccountCode: "11113",
                 DebitAccountName: "Tài khoản của người bán"
             },
             {
                 DebitAccountId: 4,
                 DebitAccountCode: "11114",
                 DebitAccountName: "Tài khoản của người bán"
             },
             {
                 DebitAccountId: 5,
                 DebitAccountCode: "11115",
                 DebitAccountName: "Tài khoản của người bán"
             },
             {
                 DebitAccountId: 6,
                 DebitAccountCode: "11116",
                 DebitAccountName: "Tài khoản của người bán"
             },

         ],
         accountDebitProp: [{
                 id: "DebitAccountCode",
                 name: "Số tài khoản",
             },
             {
                 id: "DebitAccountName",
                 name: "Tên Tài khoản",
             },
         ],
         accountCreadit: [{
                 CreditAccountId: 1,
                 CreditAccountCode: "1213",
                 CreditAccountName: "Tài khoản của người bán"
             },
             {
                 CreditAccountId: 2,
                 CreditAccountCode: "1113",
                 CreditAccountName: "Tài khoản của người bán"
             },
             {
                 CreditAccountId: 3,
                 CreditAccountCode: "11231",
                 CreditAccountName: "Tài khoản của người bán"
             },
             {
                 CreditAccountId: 4,
                 CreditAccountCode: "11233",
                 CreditAccountName: "Tài khoản của người bán"
             },

         ],
         accountCreditProp: [{
                 id: "CreditAccountCode",
                 name: "Số tài khoản",
             },
             {
                 id: "CreditAccountName",
                 name: "Tên Tài khoản",
             },
         ],

     },
     paymentDetail: {
         currencyCombobox: [{
                 id: "1",
                 code: "VND",
                 name: "Việt Nam đồng",
                 exchange: 1
             },
             {
                 id: "2",
                 code: "USD",
                 name: "Đô la Mỹ",
                 exchange: 23150.00
             }
         ],
         paymentTypeCombobox: [{
                 id: "1",
                 name: "1. Trả tiền nhà cung cấp (không theo hóa đơn)",
             },
             {
                 id: "2",
                 name: "2. Tạm ứng cho nhân viên",
             },
             {
                 id: "3",
                 name: "3. Chi mua ngoài có hóa đơn",
             },
             {
                 id: "4",
                 name: "4. Trả lương nhân viên",
             },
             {
                 id: "5",
                 name: "5. Chuyển tiền cho chi nhánh khác",
             },
             {
                 id: "6",
                 name: "6. Gửi tiền vào ngân hàng",
             },
             {
                 id: "7",
                 name: "7. Chi khác",
             },
         ],
         currencyProp: [{
                 id: "code",
                 name: "Mã loại tiền",
             },
             {
                 id: "name",
                 name: "Tên loại tiền",
             }
         ],
         accountDebit: [{
                 DebitAccountId: 1,
                 DebitAccountCode: "1111",
                 DebitAccountName: "Nguyễn Đức Toán"
             },
             {
                 DebitAccountId: 2,
                 DebitAccountCode: "11112",
                 DebitAccountName: "Tài khoản sao kê"
             },
             {
                 DebitAccountId: 3,
                 DebitAccountCode: "11113",
                 DebitAccountName: "Tiền Việt Nam"
             },
             {
                 DebitAccountId: 4,
                 DebitAccountCode: "11114",
                 DebitAccountName: "Ngoại tệ"
             },
             {
                 DebitAccountId: 5,
                 DebitAccountCode: "11115",
                 DebitAccountName: "Cổ phiếu"
             },
             {
                 DebitAccountId: 6,
                 DebitAccountCode: "11116",
                 DebitAccountName: "Tiền gửi"
             },

         ],
         accountDebitProp: [{
                 id: "DebitAccountCode",
                 name: "Số tài khoản",
             },
             {
                 id: "DebitAccountName",
                 name: "Tên Tài khoản",
             },
         ],
         accountCreadit: [{
                 CreditAccountId: 1,
                 CreditAccountCode: "1111",
                 CreditAccountName: "Công ty cổ phần MISA"
             },
             {
                 CreditAccountId: 2,
                 CreditAccountCode: "11111",
                 CreditAccountName: "Tài khoản thu hồi"
             },
             {
                 CreditAccountId: 3,
                 CreditAccountCode: "11112",
                 CreditAccountName: "Quỹ thu chi"
             },
             {
                 CreditAccountId: 4,
                 CreditAccountCode: "11113",
                 CreditAccountName: "Tiền tài trợ"
             },

         ],
         accountCreditProp: [{
                 id: "CreditAccountCode",
                 name: "Số tài khoản",
             },
             {
                 id: "CreditAccountName",
                 name: "Tên Tài khoản",
             },
         ],

     },

 }
 export default dataStorage;
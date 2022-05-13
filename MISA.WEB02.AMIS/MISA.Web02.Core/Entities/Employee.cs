using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web02.Core.Enums;
using MISA.Web02.Core.Resourses;

namespace MISA.Web02.Core.Entities
{
    /// <summary>
    /// class Employee chứa các trường thông tin về nhân viên
    /// </summary>
    /// Author: Nguyễn Đức Toán - (09/04/2022)
    public class Employee : BaseEntity
    {
        #region Properties
        /// <summary>
        /// mã id nhân viên
        /// </summary>
        [MISADisplay(Name = "Id nhân viên")]
        [MISARequired]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// tên nhân viên
        /// </summary>
        [MISADisplay(Name = "Tên nhân viên")]
        [MISARequired]
        public string? EmployeeName { get; set; }
        /// <summary>
        /// tên nhân viên
        /// </summary>
        [NotMap]
        public string? FirstName
        {
            get
            {
                //lấy index cuối của ký tự ' '
                var lastIndexOfSpace = EmployeeName == null ? -1 : EmployeeName.LastIndexOf(" ");
                //cắt chuỗi từ vị trí đầu đến vị trí cuối của ký tự ' '
                var name = EmployeeName;
                if (lastIndexOfSpace != -1)
                {
                    name = EmployeeName.Substring(0, lastIndexOfSpace).Trim();
                }
                return name;
            }
        }
        /// <summary>
        /// tên nhân viên
        /// </summary>
        [NotMap]
        public string? LastName
        {
            get
            {
                //lấy index cuối của ký tự ' '
                var lastIndexOfSpace = EmployeeName == null ? -1 : EmployeeName.LastIndexOf(" ");
                //cắt chuỗi từ vị trí cuối của ký tự ' ' đến hết
                var name = EmployeeName;
                if (lastIndexOfSpace != -1)
                {
                    name = EmployeeName.Substring(lastIndexOfSpace).Trim();
                }
                return name;

            }
        }

        /// <summary>
        /// mã code nhân viên
        /// </summary>
        [MISADisplay(Name = "Mã nhân viên")]
        [MISARequired]
        public string? EmployeeCode { get; set; }
        /// <summary>
        /// mã giới tính 0-nữ,1-nam,2-khác
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// tên hiển thị của giới tính
        /// </summary>
        [NotMap]
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case (int)GenderEnum.Female:
                        return EnumResource.VN_GenderFemale;
                    case (int)GenderEnum.Male:
                        return EnumResource.VN_GenderMale;
                    case (int)GenderEnum.Other:
                        return EnumResource.VN_GenderDifferent;
                    default: return "";
                }
            }
        }
        /// <summary>
        /// số CMND/CCCD
        /// </summary>
        [MISADisplay(Name = "Số cmnd/cccd")]
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// ngày cấp CMND/CCCD
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// nơi cấp CMND/CCCD
        /// </summary>
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// địa chỉ email của nhân viên
        /// </summary>
        [MISADisplay(Name = "Email")]
        public string? Email { get; set; }
        /// <summary>
        /// Số điện thoại di động của nhân viên
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại cố định của nhân viên
        /// </summary>
        public string? LandlineNumber { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// tiền lương của nhân viên
        /// </summary>
        public double? Salary { get; set; }
        /// <summary>
        /// mã số thuế của nhân viên
        /// </summary>
        public string? TaxCode { get; set; }
        /// <summary>
        /// mã id của bảng phòng ban
        /// </summary>
        [MISADisplay(Name = "Id phòng ban")]
        [MISARequired]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// tên của bảng phòng ban
        /// </summary>
        [NotMap]
        public string? DepartmentName { get; set; }
        /// <summary>
        /// tên của vị trí
        /// </summary>
        public string? PositionName { get; set; }
        /// <summary>
        /// trạng thái làm việc (0-đã nghỉ việc,1-đang làm việc,2-tạm dừng)
        /// </summary>
        public int WorkStatus { get; set; }
        /// <summary>
        /// trạng thái làm việc (0-đã nghỉ việc,1-đang làm việc,2-tạm dừng)
        /// </summary>
        [NotMap]
        public string WorkStatusName
        {
            get
            {
                switch (WorkStatus)
                {
                    case (int)WorkStatusEnum.Working:
                        return EnumResource.VN_WorkStatusWorking;
                    case (int)WorkStatusEnum.Stop:
                        return EnumResource.VN_WorkStatusStop;
                    case (int)WorkStatusEnum.Pause:
                        return EnumResource.VN_WorkStatusPause;
                    default: return "";
                }
            }
        }
        /// <summary>
        /// số tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }
        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// chi nhánh ngân hàng
        /// </summary>
        public string? BankBranch { get; set; }
        /// <summary>
        /// ngày sinh của nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// ngày nhân viên được nhận vào cty
        /// </summary>
        public DateTime? JoinDate { get; set; }
        #endregion
        #region Methods
        #endregion
    }
}

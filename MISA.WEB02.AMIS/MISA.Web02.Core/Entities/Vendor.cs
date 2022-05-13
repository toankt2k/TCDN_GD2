using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    /// <summary>
    /// Thông tin cá nhân của teacher
    /// Created by: Thai(13/1/2022)
    public class Vendor : BaseEntity
    {
        /// <summary>
        /// Id Bảng nhà cung cấp
        /// </summary>
        [MISARequired]
        public Guid Id { get; set; }

        /// <summary>
        /// Mã Nhà cung cấp
        /// </summary>
        [MISARequired]
        public string? VendorCode { get; set; }

        /// <summary>
        /// Tên đầy đủ Nhà cung cấp = tên prefix + tên nhà cung cấp
        /// </summary>
        public string? VendorFullName
        {
            get
            {
                return $"{PrefixName} {VendorName}";
            }
        }
        /// <summary>
        /// Tên Nhà cung cấp
        /// </summary>
        [MISARequired]
        public string? VendorName { get; set; }
        /// <summary>
        /// Tên tiền tố(xưng hô)
        /// </summary>
        public string? PrefixName { get; set; }


        /// <summary>
        /// mã số thuế
        /// </summary>
        public string? VendorTaxCode { get; set; }
        /// <summary>
        /// số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// số điện thoại bàn
        /// </summary>
        public string? LandLineNumber { get; set; }
        //public string? ContactAddress { get; set; }
        public string? Website { get; set; }
        /// <summary>
        /// Loại nhà cung cấp: 0 - tổ chức; 1 - cá nhân
        /// </summary>
        public int VendorType { get; set; }
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Xưng hô ( người liên hệ)
        /// </summary>
        public string? ContactPronoun { get; set; }
        public string? ContactName { get; set; }
        public string? ContactLegalRep { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhoneNumber { get; set; }

        #region Điều khoản
        public Guid? ContractId { get; set; }

        /// <summary>
        /// Số ngày nợ tối đa
        /// </summary>
        public int? MaxDebitDate { get; set; }

        /// <summary>
        /// Số nợ tối đa
        /// </summary>
        public float? MaxDebitAmount { get; set; }

        /// <summary>
        /// Tài khoản công nợ nhận
        /// </summary>
        public Guid? DebitReceiptAccount { get; set; }


        /// <summary>
        /// Tài khoản công nợ trả
        /// </summary>
        public Guid? DebitPaymentAccount { get; set; }

        #endregion

        #region Tài khoản ngân hàng
        public List<Guid>? BankAccounts { get; set; }
        #endregion

        #region Địa chỉ khác
        ///// <summary>
        ///// địa chỉ khác: lưu dạng Json
        ///// các thuộc tính gồm: Country,City,District,Ward,DeliveryAddresses
        ///// </summary>
        //public string? OtherAddress { get; set; }
        /// <summary>
        /// tên quốc gia
        /// </summary>
        public string? CountryName { get; set; }
        /// <summary>
        /// tên tỉnh
        /// </summary>
        public string? ProvinceName { get; set; }
        /// <summary>
        /// tên huyện
        /// </summary>
        public string? DistrictName { get; set; }
        /// <summary>
        /// tên xã
        /// </summary>
        public string? WardName { get; set; }
        /// <summary>
        /// danh sách các địac hỉ giap hàng
        /// </summary>
        public List<string>? DeliveryAddresses { get; set; }
        #endregion

        #region Ghi chú
        /// <summary>
        /// ghi chú
        /// </summary>
        public string? TextNote { get; set; }
        #endregion

        #region Cá nhân (thêm)
        /// <summary>
        /// ngày cấp chứng minh nhân dân
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// nơi cấp chứng minh nhân dân
        /// </summary>
        public string? IdentityPlace { get; set; }

        #endregion

    }
}


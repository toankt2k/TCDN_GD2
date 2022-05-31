using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    /// <summary>
    /// Chi tiết từng hạch toán trong phiếu chi
    /// </summary>
    public class PaymentDetail:BaseEntity
    {
        /// <summary>
        /// id accounting
        /// </summary>
        public Guid? PaymentDetailId { get; set; }

        /// <summary>
        /// diễn giải cho phiếu chi
        /// </summary>
        public string? DescriptionPaymentDetail { get; set; }

        /// <summary>
        /// khóa ngoại bảng Payment
        /// </summary>
        public Guid? PaymentId { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public int? DebitAccountId { get; set; }

        /// <summary>
        /// số tk nợ
        /// </summary>
        [NotMap]
        public string? DebitAccountNumber { get; set; }

        /// <summary>
        /// Tài khoản credit
        /// </summary>
        public int? CreditAccountId { get; set; }

        /// <summary>
        /// số tk có
        /// </summary>
        [NotMap]
        public string? CreditAccountNumber { get; set; }

        /// <summary>
        /// số tiền chi
        /// </summary>
        public float? CashAmount { get; set; }

        /// <summary>
        /// khóa ngoại cho đối tượng(Vendor, employee,khách hàng ...)
        /// </summary>
        public Guid? VendorId { get; set; }

        /// <summary>
        /// tên đối tượng(Vendor, employee,khách hàng ...)
        /// </summary>
        [NotMap]
        public string? VendorName { get; set; }
    }
}

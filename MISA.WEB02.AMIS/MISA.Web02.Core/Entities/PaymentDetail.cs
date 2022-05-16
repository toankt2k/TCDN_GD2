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
        public Guid PaymentDetailId { get; set; }

        /// <summary>
        /// diễn giải cho phiếu chi
        /// </summary>
        public string? DescriptionPayment { get; set; }

        /// <summary>
        /// khóa ngoại bảng Payment
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public Guid? DebitAccountId { get; set; }

        /// <summary>
        /// số tk nợ
        /// </summary>
        [NotMap]
        public Guid? DebitAccountNUmber { get; set; }

        /// <summary>
        /// Tài khoản credit
        /// </summary>
        public Guid? CreditAccountId { get; set; }

        /// <summary>
        /// số tk có
        /// </summary>
        [NotMap]
        public Guid? CreditAccountNumber { get; set; }

        /// <summary>
        /// số tiền chi
        /// </summary>
        public float CashAmount { get; set; }

        /// <summary>
        /// khóa ngoại cho đối tượng(Vendor, employee,khách hàng ...)
        /// </summary>
        public Guid AccountObjectId { get; set; }

        /// <summary>
        /// tên đối tượng(Vendor, employee,khách hàng ...)
        /// </summary>
        [NotMap]
        public string? AccountObjectName { get; set; }
    }
}

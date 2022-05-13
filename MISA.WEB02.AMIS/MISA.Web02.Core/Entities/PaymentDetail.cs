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
        public string? DebitAccount { get; set; }
        /// <summary>
        /// Tài khoản credit
        /// </summary>
        public string? CreditAccount { get; set; }
        /// <summary>
        /// số tiền chi
        /// </summary>
        public float CashAmount { get; set; }
        /// <summary>
        /// khóa ngoại cho đối tượng(Vendor, employee,khách hàng ...)
        /// </summary>
        public Guid ObjectId { get; set; }
        /// <summary>
        /// tên đối tượng(Vendor, employee,khách hàng ...)
        /// </summary>
        [NotMap]
        public string? ObjectName { get; set; }
    }
}

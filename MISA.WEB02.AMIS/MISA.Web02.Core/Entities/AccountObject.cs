using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class AccountObject:BaseEntity
    {
        /// <summary>
        /// id đói tượng
        /// </summary>
        public Guid? AccountObjectId { get; set; }

        /// <summary>
        /// mã đối tượng
        /// </summary>
        [NotMap]
        public string? AccountObjectCode { get; set; }

        /// <summary>
        /// tên đối tượng
        /// </summary>
        [NotMap]
        public string? AccountObjectName { get; set; }

        /// <summary>
        /// mã số thuế
        /// </summary>
        [NotMap]
        public string? Taxcode { get; set; }

        /// <summary>
        /// khóa ngoại bảng vendor
        /// </summary>
        public Guid? VendorId { get; set; }

        /// <summary>
        /// khóa ngoại bảng nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// khóa ngoại bảng customer
        /// </summary>
        public Guid? CustomerId { get; set; }
    }
}

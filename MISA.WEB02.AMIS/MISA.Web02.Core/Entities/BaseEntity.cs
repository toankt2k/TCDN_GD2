using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// người tạo dữ liệu
        /// </summary>
        [NotMap]
        public string? CreatedBy { get; set; }
        /// <summary>
        /// ngày tạo dữ liệu
        /// </summary>
        [NotMap]
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// người thay đổi dữ liệu
        /// </summary>
        [NotMap]
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// ngày thay đổi dữ liệu
        /// </summary>
        [NotMap]
        public DateTime? ModifiedDate { get; set; }
    }
}

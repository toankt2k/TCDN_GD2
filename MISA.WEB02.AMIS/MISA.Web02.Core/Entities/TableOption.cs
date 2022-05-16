using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class TableOption:BaseEntity
    {
        /// <summary>
        /// id của bảng
        /// </summary>
        public Guid TableOptionId { get; set; }
        /// <summary>
        /// mã code của bảng xác định option của bảng nào
        /// </summary>
        public string? TableCode { get; set; }
        /// <summary>
        /// danh sách các cột hiển thị trong bảng
        /// </summary>
        public string? ListColumns { get; set; }
        /// <summary>
        /// tên bảng
        /// </summary>
        public string? Name { get; set; }
    }
}

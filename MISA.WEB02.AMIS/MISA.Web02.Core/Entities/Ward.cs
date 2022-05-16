using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class Ward:BaseEntity
    {
        /// <summary>
        /// id bảng phường/xã
        /// </summary>
        [MISARequired]
        [MISADisplay(Name ="Mã phường/xã")]
        public Guid WardId { get; set; }
        /// <summary>
        /// tên xã/phường
        /// </summary>
        [MISARequired]
        [MISADisplay(Name ="Tên phường/xã")]
        public string? WardName { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        public string? WardDescription { get; set; }
        /// <summary>
        /// max code phường/xã
        /// </summary>
        public string? WardCode { get; set; }
        /// <summary>
        /// khóa ngoại bảng District
        /// </summary>
        [MISADisplay(Name ="Id quận/huyện")]
        public Guid DistrictId { get; set; }
    }
}

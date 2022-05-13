using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class District:BaseEntity
    {
        /// <summary>
        /// id bảng Province
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Id quận/huyện")]
        public Guid DistrictId { get; set; }

        /// <summary>
        /// tên Tỉnh/ thành phố
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Tên quận.huyện")]
        public string? DistrictName { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string? DistrictDescription { get; set; }

        /// <summary>
        /// max code quận/huyện
        /// </summary>
        public string? DistrictCode { get; set; }

        /// <summary>
        /// khóa ngoại bảng Province
        /// </summary>
        [MISARequired]
        [MISADisplay(Name ="Mã tỉnh/thành phố")]
        public Guid ProvinceId { get; set; }
    }
}

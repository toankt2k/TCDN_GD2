using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class Province:BaseEntity
    {
        /// <summary>
        /// id bảng Province
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Id thành phố")]
        public Guid ProvinceId { get; set; }

        /// <summary>
        /// tên Tỉnh/ thành phố
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Tên thành phố")]
        public string? ProvinceName { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string? ProvinceDescription { get; set; }

        /// <summary>
        /// max code phường/xã
        /// </summary>
        public string? ProvinceCode { get; set; }

        /// <summary>
        /// khóa ngoại bảng Country
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Mã quốc gia")]
        public Guid CountryId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class Country:BaseEntity
    {
        /// <summary>
        /// id bảng Country
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Id quốc gia")]
        public Guid CountryId { get; set; }
        /// <summary>
        /// Tên nước
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Tên quốc gia")]
        public string? CountryName { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        public string? CountryDescription { get; set; }
        /// <summary>
        /// max code phường/xã
        /// </summary>
        public string? CountryCode { get; set; }
    }
}

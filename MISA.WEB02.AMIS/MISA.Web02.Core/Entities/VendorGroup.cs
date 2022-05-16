using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class VendorGroup:BaseEntity
    {
        /// <summary>
        /// id nhóm nhà cung cấp
        /// </summary>
        public Guid VendorGroupId { get; set; }

        /// <summary>
        /// tên nhóm nhà cung cấp
        /// </summary>
        public string? VendorGroupName { get; set; }

        /// <summary>
        /// mã nhóm nhà cung cấp
        /// </summary>
        public string? VendorGroupCode { get; set; }

    }
}

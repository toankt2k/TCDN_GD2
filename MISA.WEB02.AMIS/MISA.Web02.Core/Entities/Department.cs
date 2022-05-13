using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class Department: BaseEntity
    {
        #region Properties
        /// <summary>
        /// mã id phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        #endregion

    }
}

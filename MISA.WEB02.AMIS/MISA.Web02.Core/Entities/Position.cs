using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class Position: BaseEntity
    {
        #region Attribute
        /// <summary>
        /// mã id vị trí
        /// </summary>
        public Guid PositionId { get; set; }
        /// <summary>
        /// tên vị trí
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// mã vị trí
        /// </summary>
        public string PositionCode { get; set; }
        #endregion

    }
}

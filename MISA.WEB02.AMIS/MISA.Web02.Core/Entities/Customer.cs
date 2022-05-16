using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class Customer:BaseEntity
    {
        /// <summary>
        /// id khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// tên khách hàng
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// mã khách hàng
        /// </summary>
        public string? CustomerCode { get; set; }
    }
}

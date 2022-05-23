using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Repository
{
    public interface IPaymentRepository:IBaseRepository<Payment>
    {
        /// <summary>
        /// thêm 1 phiếu chi 
        /// </summary>
        /// <param name="paymenObject"> gồm bay ment và các paymentdetail liên quan</param>
        /// <returns></returns>
        public int AddPayment(Payment payment);
    }
}

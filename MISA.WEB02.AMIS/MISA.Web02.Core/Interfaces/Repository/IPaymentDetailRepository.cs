using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Repository
{
    public interface IPaymentDetailRepository:IBaseRepository<PaymentDetail>
    {
        public IEnumerable<PaymentDetail> GetByPayment(Guid id);
    }
}

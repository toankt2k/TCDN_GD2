using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Interfaces.Repository;
using MISA.Web02.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Services
{

    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository) : base(paymentRepository)
        {
            _paymentRepository=paymentRepository;

        }

        public int AddPayment(Payment payment)
        {
            var res = _paymentRepository.AddPayment(payment);
            return res;
        }

    }
}

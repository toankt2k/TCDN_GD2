using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class PaymentObject
    {
        public Payment? Payment { get; set; }
        public List<PaymentDetail>? PaymentDetail { get; set; }

    }
}

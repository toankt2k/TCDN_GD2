using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Exceptions;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Interfaces.Repository;
using MISA.Web02.Core.Interfaces.Service;
using MISA.Web02.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(payment);
            if (validateEmptyResult.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //lấy code của entity truyền vào
            var code = payment.PaymentCode;
            //code đúng format
            var rx = new Regex(@"[a-zA-Z0-9]+$");
            if (!rx.IsMatch(code.ToUpper()))
            {
                errorMsg.Add($"PaymentCode", string.Format(MISAMessageResource.VN_InvalidFormat, "Số chứng từ"));
            }
            //kiểm tra mã code trùng
            var data = _paymentRepository.FindByCode(code);
            //nếu có bản ghi trùng với mã hiện tại
            if (data != null)
            {
                errorMsg.Add($"PaymentCode", String.Format(MISAMessageResource.VN_DataDuplicate, $"Số chứng từ <{code}> ")+ "Bạn có muốn chương trình tự động tăng số chứng từ không?");
                errorMsg.Add($"NewCode", GetNewCodeService());
            }
            if (errorMsg.Count() > 0)//nếu danh sách lỗi có lỗi thì throw exception
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //thêm mới database
            var res = _paymentRepository.AddPayment(payment);
            return res;
        }

        public override string GetNewCodeService()
        {
            var result = $"PC{base.GetNewCodeService().PadLeft(6, '0')}";
            return result;
        }
    }
}

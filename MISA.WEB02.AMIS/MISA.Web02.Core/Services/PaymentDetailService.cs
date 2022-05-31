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
    public class PaymentDetailService : BaseService<PaymentDetail>, IPaymentDetailService
    {
        IPaymentDetailRepository _paymentDetailRepository;
        public PaymentDetailService(IPaymentDetailRepository paymentDetailRepository) : base(paymentDetailRepository)
        {
            _paymentDetailRepository = paymentDetailRepository;
        }

        /// <summary>
        /// overide base method vowis cas nghieepj vuj
        /// </summary>
        /// <param name="paymentDetail"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MISAExceptions"></exception>
        public override int UpdateService(PaymentDetail paymentDetail, Guid id)
        {

            var entityName = "PaymentDetail";
            //gán lại id cho entity mới
            paymentDetail.PaymentDetailId = id;
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(paymentDetail);
            if (validateEmptyResult.Count() > 0)//nếu dữ liệu bị trống
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //lấy giá trị cũ trong data base
            var oldEntity = _paymentDetailRepository.GetById(id);
            //nếu không tìm thấy bản ghi / bản ghi đã bị xóa trước khi sửa 
            if (oldEntity == null)
            {
                errorMsg.Add($"{entityName}NotFound", MISAMessageResource.VN_DataNotFound);
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //set lại data
            //lấy các prop trong entity
            var props = paymentDetail.GetType().GetProperties();
            foreach (var prop in props)
            {
                //lấy giá trị của entity đó
                var entityValue = prop.GetValue(paymentDetail);
                //lấy giá trị của data
                var dataValue = prop.GetValue(oldEntity);
                //nếu các trường không được nhập vào thì người dùng không muốn thay đổi
                //nên giữ nguyên giá trị cũ trong database
                if (entityValue == null)
                {
                    prop.SetValue(paymentDetail, dataValue);
                }
            }
            // gọi update đến repository
            var result = _paymentDetailRepository.Update(paymentDetail);
            return result;
        }
        /// <summary>
        /// overide base method
        /// </summary>
        /// <param name="paymentDetail"></param>
        /// <returns>
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public override int InsertService(PaymentDetail paymentDetail)
        {
            var entityName = "PaymentDetail";
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(paymentDetail);
            if (validateEmptyResult.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            if (errorMsg.Count() > 0)//nếu danh sách lỗi có lỗi thì throw exception
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //thêm mới database
            var result = _paymentDetailRepository.Insert(paymentDetail);
            return result;
        }

        public IEnumerable<PaymentDetail> GetByPaymentService(Guid id)
        {
            var res = _paymentDetailRepository.GetByPayment(id);
            return res;
        }
    }
}

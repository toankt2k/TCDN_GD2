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
    public class VendorService : BaseService<Vendor>, IVendorService
    {
        private IVendorRepository _vendorRepository;
        public VendorService(IVendorRepository vendorRepository) : base(vendorRepository)
        {
            this._vendorRepository = vendorRepository;
        }
        /// <summary>
        /// tra về mã nhà cung cấp mới nhất thêm tiền tố NCC
        /// </summary>
        /// <returns></returns>
        public override string GetNewCodeService()
        {
            var res = _vendorRepository.GetNewCode();
            res = $"NCC{res.PadLeft(6, '0')}";
            return res;
        }

        public override int DeleteService(Guid vendorId)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra bản ghi đã tồn tại
            var oldVendor = _vendorRepository.GetById(vendorId);
            if (oldVendor == null)//nếu bản ghi không tồn tại tức là bản ghi đã bị xóa trước đó
            {
                //thêm lỗi vào errorMsg
                errorMsg.Add($"VendorEmpty", MISAMessageResource.VN_DataNotFound);
            }
            else
            {
                var check = _vendorRepository.CheckHasPayment(vendorId);
                if (check)
                {
                    errorMsg.Add($"VendorHasPayment", string.Format(MISAMessageResource.VN_VendorHasPayment, $"<{oldVendor.VendorCode}>"));
                }
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            var result = _vendorRepository.Delete(vendorId);
            return result;
        }

        public override int MultiDelete(List<Guid> listId)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            var listVendorError = $"";
            foreach (var id in listId)
            {
                var check = _vendorRepository.CheckHasPayment(id);
                if (check)
                {
                    var oldVendor = _vendorRepository.GetById(id);
                    listVendorError += $"<{oldVendor.VendorCode}> ";

                }
            }
            if (listVendorError != "")
            {
                errorMsg.Add($"VendorHasPayment", string.Format(MISAMessageResource.VN_VendorHasPayment, listVendorError));
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            return base.MultiDelete(listId);
        }

        public override int InsertService(Vendor vendor)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(vendor);
            if (validateEmptyResult.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, validateEmptyResult);
            }
            //lấy code của entity truyền vào
            var code = vendor.VendorCode;
            //code đúng format
            var rx = new Regex(@"[a-zA-Z]{3}[0-9]+$");
            if (!rx.IsMatch(code.ToUpper()))
            {
                errorMsg.Add($"VendorCode", MISAMessageResource.VN_CodeInvalid);
            }
            //kiểm tra mã code trùng
            var data = _vendorRepository.FindByCode(code);
            //nếu có bản ghi trùng với mã hiện tại
            if (data != null)
            {
                errorMsg.Add($"VendorCode", MISAMessageResource.VN_CodeDuplicate);
            }
            if (errorMsg.Count() > 0)//nếu danh sách lỗi có lỗi thì throw exception
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }

            return _vendorRepository.Insert(vendor);
        }
    }
}

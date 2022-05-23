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
    public class VendorService : BaseService<Vendor> , IVendorService
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
            res = $"NCC{res}";
            return res;
        }
    }
}

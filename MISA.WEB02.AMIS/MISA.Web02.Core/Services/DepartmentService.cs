using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces;
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
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {

        }

        #endregion
        #region ApiController
        /// <summary>
        /// lấy mã mới nhất của nhân viên
        /// </summary>
        /// <returns>
        /// chuỗi 5 chữ số
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095(15/04/2022)
        public override string GetNewCodeService()
        {
            var result = $"DP-{base.GetNewCodeService().PadLeft(2, '0')}";
            return result;
        }
        #endregion

    }
}

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
    public class TableOptionService : BaseService<TableOption>, ITableOptionService
    {
        private ITableOptionRepository _tableOptionRepositor;
        public TableOptionService(ITableOptionRepository tableOptionRepository) : base(tableOptionRepository)
        {
            _tableOptionRepositor = tableOptionRepository;
        }

        public TableOption GetByCode(string code)
        {
            var res = _tableOptionRepositor.FindByCode(code);
            return res;
        }

        public int PutByCode(TableOption table)
        {
            var res = _tableOptionRepositor.PutByCode(table);
            return res;
        }
    }
}

using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Service
{
    public interface ITableOptionService:IBaseService<TableOption>
    {
        public TableOption GetByCode(string code);
        public int PutByCode(TableOption table);

    }
}

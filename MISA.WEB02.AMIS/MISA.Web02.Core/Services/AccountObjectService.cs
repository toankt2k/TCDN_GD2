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
    public class AccountObjectService : BaseService<AccountObject>, IAccountObjectService
    {
        public AccountObjectService(IAccountObjectRepository accountObjectRepository) : base(accountObjectRepository)
        {
        }
    }
}

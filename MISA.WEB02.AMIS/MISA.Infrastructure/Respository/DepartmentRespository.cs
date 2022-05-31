using Microsoft.Extensions.Configuration;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces;
using MISA.Web02.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Respository
{
    public class DepartmentRespository : BaseRespository<Department>, IDepartmentRepository
    {
        public DepartmentRespository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}

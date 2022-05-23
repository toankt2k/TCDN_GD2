using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using Npgsql;
using System.Text.RegularExpressions;
using MISA.Infrastructure.Helpers;
using System.Data;
using MISA.Web02.Core.Interfaces.Repository;

namespace MISA.Infrastructure.Respository
{
    public class EmployeeRepository : BaseRespository<Employee>, IEmployeeRepository
    {
        public override IEnumerable<Employee> Get()
        {
            string commandText = $"SELECT e.*,d.department_name FROM employee as e join department as d on e.department_id = d.department_id";
            //dữ liệu trả về gồm các propperty của T
            var data = BindingEntity.Query<Employee>(commandText, _sqlPostgreslqString, CommandType.Text);
            //trả về kết quả
            return data.Result;
        }
    }
}

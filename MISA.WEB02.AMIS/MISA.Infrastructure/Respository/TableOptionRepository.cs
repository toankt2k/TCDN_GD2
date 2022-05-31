using Microsoft.Extensions.Configuration;
using MISA.Infrastructure.Helpers;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Respository
{
    public class TableOptionRepository : BaseRespository<TableOption>, ITableOptionRepository
    {
        public TableOptionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int PutByCode(TableOption table)
        {
            //sử dụng func update
            string commandText = $"UPDATE table_option set list_columns = @list WHERE table_option_code = @code RETURNING * ;";
            //tạo tham số
            var param = new List<NpgsqlParameter>();
            //lấy danh sách property của entity
            param.Add(new NpgsqlParameter($"@code", table.TableOptionCode == null ? DBNull.Value : table.TableOptionCode.ToString()));
            param.Add(new NpgsqlParameter($"@list", table.ListColumns == null ? DBNull.Value : table.ListColumns.ToString()));
            //update
            var res = BindingEntity.Execute(commandText, _sqlPostgreslqString, CommandType.Text, param);
            //trả về kết quả
            return res==null?0:1;
        }
    }
}

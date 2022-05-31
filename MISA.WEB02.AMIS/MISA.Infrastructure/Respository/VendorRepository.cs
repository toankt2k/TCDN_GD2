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
    public class VendorRepository : BaseRespository<Vendor>, IVendorRepository
    {
        public VendorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool CheckHasPayment(Guid vendorId)
        {
            string commandText = $"SELECT count(1) FROM payment as p where p.vendor_id = @vendor_id";

            var param = new List<NpgsqlParameter>();
            param.Add(new NpgsqlParameter("@vendor_id", vendorId.ToString()));
            //dữ liệu trả về gồm các propperty của T
            var data = BindingEntity.Query<int>(commandText, _sqlPostgreslqString, CommandType.Text,param);
            //trả về kết quả
            return data.Result.FirstOrDefault()!=0;
        }
    }
}

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
    public class PaymentDetailRepository : BaseRespository<PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<PaymentDetail> GetByPayment(Guid id)
        {
            //lấy dữ liệu
            string commandText = $"SELECT * FROM payment_detail as e WHERE payment_id = @m_payment_id";
            //khởi tạo tham số
            var param = new List<NpgsqlParameter>();
            param.Add(new NpgsqlParameter($"@m_payment_id", id.ToString()));
            //dữ liệu trả về gồm các propperty của T
            var data = BindingEntity.Query<PaymentDetail>(commandText, _sqlPostgreslqString, CommandType.Text, param);
            var result = data.Result;
            //trả về kết quả
            return result;
        }
    }
}

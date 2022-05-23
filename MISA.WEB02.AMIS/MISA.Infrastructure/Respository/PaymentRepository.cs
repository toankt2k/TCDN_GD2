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
    public class PaymentRepository : BaseRespository<Payment>, IPaymentRepository
    {
        public int AddPayment(Payment payment)
        {
            //sử dụng func update
            var commandAddPayment = $"func_insert_payment";
            var commandAddPaymentDetail = "func_insert_payment_detail";

            var conn = new NpgsqlConnection(_sqlPostgreslqString);
            conn.Open();
           
            using (NpgsqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    
                    var result = new List<int>();
                    using (var cmd = new NpgsqlCommand("", conn))
                    {
                        //thêm payment
                        cmd.CommandType = CommandType.StoredProcedure;
                        var listProperties = typeof(Payment).GetProperties();
                        foreach (var prop in listProperties)
                        {
                            //chuyển qua snake case
                            var tableColunm = BindingEntity.ToSnakeCase(prop.Name);
                            //xem prop có cho phép map vào database không
                            var isNotMap = prop.IsDefined(typeof(NotMap), false);
                            if (!isNotMap)
                            {
                                //giá trị mới được truyền vào
                                var value = payment.GetType().GetProperty(prop.Name).GetValue(payment);
                                cmd.Parameters.Add(new NpgsqlParameter($"@m_{tableColunm}", value == null ? DBNull.Value : value.ToString()));
                            }
                        }
                        cmd.CommandText = commandAddPayment;
                        var rs = cmd.ExecuteScalarAsync();
                        //thêm payent detail
                        foreach(var pd in payment.PaymentDetail)
                        {
                            cmd.Parameters.Clear();
                            var listProps = typeof(PaymentDetail).GetProperties();
                            foreach (var prop in listProps)
                            {
                                //chuyển qua snake case
                                var tableColunm = BindingEntity.ToSnakeCase(prop.Name);
                                //xem prop có cho phép map vào database không
                                var isNotMap = prop.IsDefined(typeof(NotMap), false);
                                if (!isNotMap)
                                {
                                    //giá trị mới được truyền vào
                                    var value = pd.GetType().GetProperty(prop.Name).GetValue(pd);
                                    cmd.Parameters.Add(new NpgsqlParameter($"@m_{tableColunm}", value == null ? DBNull.Value : value.ToString()));
                                }
                            }
                            cmd.CommandText = commandAddPaymentDetail;
                            var rs1 = cmd.ExecuteScalarAsync();
                        }
                    }
                    conn.Close();
                    trans.CommitAsync();
                    return 1;
                }
                catch (NpgsqlException e)
                {
                    trans.RollbackAsync();
                    return 0;
                }
            }
        }



    }
}

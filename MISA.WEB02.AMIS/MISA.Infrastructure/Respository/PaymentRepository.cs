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
using System.Transactions;

namespace MISA.Infrastructure.Respository
{
    public class PaymentRepository : BaseRespository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int AddPayment(Payment payment)
        {
            //sử dụng func update
            var commandAddPayment = $"func_insert_payment";
            var commandAddPaymentDetail = "func_insert_payment_detail";
           
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {   
                    using (var conn1 = new NpgsqlConnection(_sqlPostgreslqString))
                    {
                        conn1.Open();
                        var cmd = new NpgsqlCommand(commandAddPayment, conn1);
                        //thêm payment
                        cmd.CommandType = CommandType.StoredProcedure;
                        payment.PaymentId = Guid.NewGuid();

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
                        var rs = cmd.ExecuteScalar();
                        conn1.Close();
                        using(var conn2 = new NpgsqlConnection(_sqlPostgreslqString))
                        {
                            conn2.Open();
                            var cmd2 = new NpgsqlCommand(commandAddPaymentDetail, conn2);
                            //thêm payment
                            cmd2.CommandType = CommandType.StoredProcedure;
                            //thêm payent detail
                            if (payment.PaymentDetail != null)
                            {
                                foreach (var pd in payment.PaymentDetail)
                                {
                                    cmd2.Parameters.Clear();
                                    pd.PaymentId = payment.PaymentId;
                                    pd.PaymentDetailId = Guid.NewGuid();
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
                                            cmd2.Parameters.Add(new NpgsqlParameter($"@m_{tableColunm}", value == null ? DBNull.Value : value.ToString()));
                                        }
                                    }
                                    var rs1 = cmd2.ExecuteScalar();
                                }
                            }
                            conn2.Close();
                        }
                    }
                    scope.Complete();
                    return 1;
                }
                catch (NpgsqlException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }



    }
}

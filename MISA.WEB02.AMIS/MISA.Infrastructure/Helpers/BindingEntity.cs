using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Helpers
{
    public class BindingEntity
    {
        /// <summary>
        /// dùng khi muốn lấy dữ liêụ trả về từ database
        /// </summary>
        /// <param name="connectionString">kết nối database</param>
        /// <param name="commandType">kiểu truy vấn là store hay text</param>
        /// <param name="commandText">câu query hoặc tên function</param>
        /// <param name="param">list các tham số cho câu query</param>
        /// <returns>
        /// danh sách kết quả cho đối tượng cần lấy
        /// </returns>
        /// Author: Nguyễn Đức Toán - 11/05/2022
        public async static Task<IEnumerable<T>> Query<T>(string commandText, string connectionString, CommandType commandType, List<NpgsqlParameter> param)
        {
            using (NpgsqlConnection? conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var result = new List<T>();
                using (var cmd = new NpgsqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(param.ToArray());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dynamic entity = new ExpandoObject();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var propName = ToPascalCase(reader.GetName(i));
                                var propValue = reader.GetValue(i);
                                if (typeof(T).IsPrimitive || typeof(T).Name == "String")
                                {
                                    entity = propValue;
                                    continue;
                                }
                                ((IDictionary<string, object>)entity).Add(propName, propValue);
                            }
                            var entityData = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(entity));
                            result.Add(entityData);
                        }
                    }
                }
                conn.Close();
                return result;
            }

        }

        /// <summary>
        /// dùng khi muốn lấy dữ liêụ trả về từ database
        /// </summary>
        /// <param name="connectionString">kết nối database</param>
        /// <param name="commandType">kiểu truy vấn là store hay text</param>
        /// <param name="commandText">câu query hoặc tên function</param>
        /// <returns>
        /// danh sách kết quả cho đối tượng cần lấy
        /// </returns>
        /// Author: Nguyễn Đức Toán - 11/05/2022
        public async static Task<IEnumerable<T>> Query<T>(string commandText, string connectionString, CommandType commandType)
        {
            using (NpgsqlConnection? conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var result = new List<T>();
                using (var cmd = new NpgsqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dynamic entity = new ExpandoObject();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var propName = ToPascalCase(reader.GetName(i));
                                var propValue = reader.GetValue(i);
                                if (typeof(T).IsPrimitive || typeof(T).Name == "String")
                                {
                                    entity = propValue;
                                    continue;
                                }
                                ((IDictionary<string, object>)entity).Add(propName, propValue);
                            }

                            var entityData = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(entity));

                            result.Add(entityData);
                        }
                    }
                }
                conn.Close();
                return result;
            }

        }

        /// <summary>
        /// dùng khi muốn lấy dữ liêụ trả về từ database
        /// </summary>
        /// <param name="connectionString">kết nối database</param>
        /// <param name="commandType">kiểu truy vấn là store hay text</param>
        /// <param name="commandText">câu query hoặc tên function</param>
        /// <param name="param">list param cho câu query</param>
        /// <returns>
        /// danh sách kết quả cho đối tượng cần lấy
        /// </returns>
        /// Author: Nguyễn Đức Toán - 11/05/2022
        public async static Task<int> Execute(string commandText, string connectionString, CommandType commandType, List<NpgsqlParameter> param)
        {
            using (NpgsqlConnection? conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var result = new List<int>();
                using (var cmd = new NpgsqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(param.ToArray());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetInt32(0));
                        }
                    }
                }
                conn.Close();
                return result.FirstOrDefault();
            }

        }


        /// <summary>
        /// dùng khi goị function k trả về query 
        /// </summary>
        /// <param name="connectionString">kết nối database</param>
        /// <param name="commandType">kiểu truy vấn là store hay text</param>
        /// <param name="commandText">câu query hoặc tên function</param>
        /// <returns>
        /// số lượng bản ghi thay đổi 0 là lỗi hoặc k có thay đổi
        /// </returns>
        /// Author: Nguyễn Đức Toán - 11/05/2022
        public async static Task<int> Execute(string commandText, string connectionString, CommandType commandType)
        {
            using (NpgsqlConnection? conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var result = new List<int>();
                using (var cmd = new NpgsqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetInt32(0));
                        }
                    }
                }
                conn.Close();
                return result.FirstOrDefault();
            }

        }

        /// <summary>
        /// chuyển key từ pascal case to snake
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToSnakeCase(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length < 2)
            {
                return key;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(key[0]));
            for (int i = 1; i < key.Length; ++i)
            {
                char c = key[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// convert key to pascal case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPascalCase(string str)
        {
            string result = Regex.Replace(str, "_[a-z]", delegate (Match m)
            {
                return m.ToString().TrimStart('_').ToUpper();
            });

            result = char.ToUpper(result[0]) + result.Substring(1);

            return result;
        }
    }
}

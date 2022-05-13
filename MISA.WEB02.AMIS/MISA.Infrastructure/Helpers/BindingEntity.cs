using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Helpers
{
    public class BindingEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">class cần trả về</typeparam>
        /// <param name="reader">kết quả trả về từ database</param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán - 11/05/2022
        public async static Task<IEnumerable<T>> Query<T>(NpgsqlDataReader reader)
        {
            var data = new List<T>();
            var listColunmsName = reader.GetColumnSchemaAsync().Result.Select(col => col.ColumnName.ToString()).ToList();
            while (reader.Read())
            {
                var item = (T)Activator.CreateInstance(typeof(T));
                var props = typeof(T).GetProperties();
                foreach (var prop in props)
                {
                    if (!listColunmsName.Contains(ToSnakeCase(prop.Name))) continue;
                    var value = reader[ToSnakeCase(prop.Name)];
                    prop.SetValue(item, value, null);
                }
                data.Add(item);
            }
            return data;
        }
        /// <summary>
        /// chuyển key từ pascal case to snake
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static string ToSnakeCase(string key)
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
    }
}

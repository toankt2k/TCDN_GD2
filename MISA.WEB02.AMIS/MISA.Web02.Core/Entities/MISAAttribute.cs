using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    /// <summary>
    /// chỉ định thuộc tính không được lưu vào database
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMap: Attribute
    {

    }
    /// <summary>
    /// đặt tên để hiển thị cho thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISADisplay: Attribute
    {
        public string? Name { get; set; }
    }
    /// <summary>
    /// chỉ định thuộc tính không được để null hoặc bỏ trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired: Attribute
    {
    }


}

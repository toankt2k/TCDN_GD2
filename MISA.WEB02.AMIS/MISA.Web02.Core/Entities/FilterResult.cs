using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Entities
{
    public class FilterResult<T>
    {
        public List<T> List { get; set; }
        public long? TotalRecord { get; set; }
        public long? TotalPage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Enums
{
    /// <summary>
    /// enum giới tính 
    /// </summary>
    /// Author: Nguyễn Đức Toán - (09/04/2022)
    public enum GenderEnum
    {
        Male = 1,//nam
        Female = 0,//nữ
        Other = 2,//khác
    }
    /// <summary>
    /// enum trạng thái làm việc
    /// </summary>
    /// Author: Nguyễn Đức Toán - (09/04/2022)
    public enum WorkStatusEnum
    {
        Working = 1,//đang làm việc
        Stop = 0,//đã nghỉ việc
        Pause = 2,//tạm dừng 
    }



}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XiDeng.IService
{
    public interface IVideoPickerService
    {
        Task<string> GetVideoFileNameAsync();
    }
}

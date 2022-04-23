using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using XiDeng.Views;

namespace XiDeng.Common
{
    public static class VersionHelper
    {
        
        public static async Task CheckUpdate(bool notice = true)
        {
            var response = await Utility.GetStringAsync("https://gitee.com/AC200/turn-off-the-lights/raw/master/CurrentVerion");
            if (!response.IsSuccessStatusCode)
            {
                if (notice)
                {
                    await "获取版本信息失败".Message();
                }
                return;
            }
            var model = response.Content.To<VersionModel>();
            if (model == null || model.VersionNumber == VersionTracking.CurrentVersion)
            {
                if (notice)
                {
                    await Shell.Current.DisplayToastAsync("已是最新版本！");
                }
                return;
            }

            var currentVersion = VersionTracking.CurrentVersion.Split('.').Select(x => int.Parse(x)).ToList();
            var newVersion = model.VersionNumber.Split('.').Select(x => int.Parse(x)).ToList();

            for (int i = 0; i < currentVersion.Count; i++)
            {
                if (currentVersion[i] == newVersion[i])
                {
                    continue;
                }
                if (currentVersion[i] > newVersion[i])
                {
                    if (notice)
                    {
                        await Shell.Current.DisplayToastAsync("已是最新版本！");
                    }
                    return;
                }
                else
                {
                    await Shell.Current.Navigation.PushPopupAsync(new UpdatePopupPage(model));
                    return;
                }
            }
            if (notice)
            {
                await Shell.Current.DisplayToastAsync("已是最新版本！");
            }

        }

    }
    public class VersionModel
    {
        public string VersionNumber { get; set; }
        public string DownloadAddress { get; set; }
        public DateTime ReleaseTime { get; set; }
        public string Description { get; set; }
    }
}

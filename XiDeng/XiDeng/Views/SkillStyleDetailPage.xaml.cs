using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Core;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile),QueryProperty("SkillID","SkillID"), QueryProperty("SkillStyleID", "SkillStyleID")]
    public partial class SkillStyleDetailPage : ContentPage
    {
        public string SkillID { get; set; }
        public string SkillStyleID { set { BindingContext = new SkillStyleDetailPageViewModel(Guid.Parse(SkillID), Guid.Parse(value)); } }

        public SkillStyleDetailPage()
        {
            InitializeComponent();
            //this.SizeChanged += SkillStyleDetailPage_SizeChanged;
        }

        //private void SkillStyleDetailPage_SizeChanged(object sender, EventArgs e)
        //{
        //    if (this.Width > Height)
        //    {
        //        meVideo.HeightRequest = this.HeightRequest;
        //    }
        //    else
        //    {
        //        meVideo.HeightRequest = 200;
        //    }
        //}

        //private void MediaElement_MediaFailed(object sender, EventArgs e)
        //{
        //    DisplayAlert("错误","获取视频失败!请重新指示地址或文件","OK");
        //}

        //private void MediaElement_Unfocused(object sender, FocusEventArgs e)
        //{
        //    try
        //    {
        //        if (meVideo.CurrentState != MediaElementState.Closed)
        //            (sender as MediaElement).Pause();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void meVideo_Focused(object sender, FocusEventArgs e)
        //{
        //    try
        //    {
        //        if (meVideo.CurrentState != MediaElementState.Closed)
        //            (sender as MediaElement).Play();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void Button_Focused(object sender, FocusEventArgs e)
        //{
        //    try
        //    {
        //        if (meVideo.CurrentState != MediaElementState.Closed)
        //            meVideo.Pause();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (meVideo.CurrentState != MediaElementState.Closed)
        //        {
        //            meVideo.Pause();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
    }
}
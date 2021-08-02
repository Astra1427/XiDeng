using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThanksPage : ContentPage
    {
        public ImageSource zm { get; set; }
        public ImageSource xz { get; set; }
        public ThanksPage()
        {
            InitializeComponent();
            zm = Utility.GetImage("zm");
            xz = Utility.GetImage("xz");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();

        }
        public ObservableCollection<ThankUser> Users { get; set; }
        /// <summary>
        /// Init data and controls
        /// </summary>
        private async void Init()
        {
            try
            {
                ai.IsRunning = true;
                var httpClient = new HttpClient() { Timeout = new TimeSpan(0, 0, 6) };
                var rs = await httpClient.GetStringAsync("https://gitee.com/AC200/turn-off-the-lights/raw/master/ThankList.json");

                Users = Newtonsoft.Json.JsonConvert.DeserializeObject< ObservableCollection<ThankUser>>(rs) ;
                for (int i = 0; i < Users.Count; i++)
                {
                    try
                    {
                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post,Users[i].imgUrl);
                        var request = HttpWebRequest.Create(Users[i].imgUrl);
                        request.Method = "GET";
                        var response = request.GetResponse();
                        var Stream = response.GetResponseStream();
                        Users[i].ImgSource = ImageSource.FromStream(() => { return Stream; });
                    }
                    catch (Exception ex2)
                    {

                    }
                }
                cvUsers.ItemsSource = Users;
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Tips","获取数据失败，请检查网络连接！","OK");
            }
            ai.IsRunning = false;

        }
    }

    public class ThankUser
    {
        public string name { get; set; }
        public string qq { get; set; }
        public string imgUrl { get; set; }
        public string ThankText { get; set; }
        public ImageSource ImgSource { get; set; }
    }
}
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.ViewModel;
using XiDeng.Views;

namespace XiDeng
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }


        
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.UserAppTheme = App.Current.UserAppTheme == OSAppTheme.Dark ? OSAppTheme.Light : OSAppTheme.Dark;
        }

        private void Skill_Tapped(object sender, EventArgs e)
        {
            try
            {
                var grid = sender as Grid;
                var label = grid.Children[0] as Label;
                int ID = int.Parse(label.Text);

            }
            catch (Exception ex)
            {

            }
        }

        private async void Setting_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("SettingPage");
        }

        private async void Thanks_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ThanksPage");
        }

        private async void About_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AboutPage");
        }

        private async void ExerciseLog_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ExerciseLogPage");
        }

        private async void Donation_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("DonationPage");
        }

        private async void BackupDatabase_Tapped(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareFileRequest(new ShareFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "xdDB.db3"))));
        }

        //private async void BackupData_Tapped(object sender, EventArgs e)
        //{
        //    bool isSuccess = await CrossFilePicker.Current.SaveFile(new FileData("data", $"XiDengBackUp_Skills.txt", () =>
        //    {
        //        return new MemoryStream(Encoding.UTF8.GetBytes(DataCommon.Skills.ToJson()));
        //    }));

        //    await this.Message(isSuccess.ToString());

        //}
    }
}

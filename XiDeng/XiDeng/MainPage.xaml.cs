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
using XiDeng.Models.SkillModels;
using XiDeng.ViewModel;
using XiDeng.Views;
using XiDeng.Views.ExerciseLogViews;
namespace XiDeng
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
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
            await Shell.Current.GoToAsync(nameof(SettingPage));
        }
        private async void Thanks_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ThanksPage));
        }
        private async void About_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AboutPage));
        }
        private async void Donation_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DonationPage));
        }
        private async void BackupDatabase_Tapped(object sender, EventArgs e)
        {
            //await App.Database.BackupAsync();
            //logger file
            string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+"/logs/";
            await Share.RequestAsync(new ShareFileRequest(new ShareFile(Path.Combine(logFilePath, DateTime.Now.ToString("yyyy-MM-dd") + ".log.csv"))));
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

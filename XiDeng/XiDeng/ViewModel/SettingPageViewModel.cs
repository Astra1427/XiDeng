using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Common;
using XiDeng.Data;
namespace XiDeng.ViewModel
{
    class SettingPageViewModel:BaseViewModel
    {
        private ImageSource traningIcon;
        public ImageSource TraningIcon
        {
            get { return traningIcon; }
            set { traningIcon = value;this.RaisePropertyChanged("TraningIcon"); }
        }
        private int second;
        public int SleepSecond
        {
            get { return second; }
            set { second = value; RaisePropertyChanged("SleepSecond"); }
        }
        private int numberSecond;
        public int NumberSecond
        {
            get { return numberSecond; }
            set { numberSecond = value; RaisePropertyChanged("NumberSecond"); }
        }
        private int downNumberSecond;
        public int DownNumberSecond
        {
            get { return downNumberSecond; }
            set { downNumberSecond = value;RaisePropertyChanged("DownNumberSecond"); }
        }
        private int upNumberSecond;
        public int UpNumberSecond
        {
            get { return upNumberSecond; }
            set { upNumberSecond = value;RaisePropertyChanged("UpNumberSecond"); }
        }
        private bool isOffline;
        public bool IsOffline
        {
            get { return isOffline; }
            set
            {
                if (Utility.LoggedAccount == null || Utility.LoggedAccount.JwtToken.IsEmpty())
                {
                    this.Message("请登录之后再使用此功能。");
                }
                else
                {
                    isOffline = value;
                    Config.IsOffline = value;
                    FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config));
                }
                this.RaisePropertyChanged(nameof(IsOffline));
            }
        }
        /// <summary>
        /// 组间休息时间
        /// </summary>
        public DelegateCommand ChangeSleepSecond { get => new DelegateCommand { ExecuteAction = new Action<object>(ChangeSleepFunc)}; }
        private async void ChangeSleepFunc(object obj)
        {
            string rs = await App.Current.MainPage.DisplayPromptAsync("输入","输入秒数","确定","取消",null,3,Keyboard.Numeric,"");
            try
            {
                int s = int.Parse(rs);
                if (s < 3)
                {
                    await App.Current.MainPage.DisplayAlert("错误","不能小于3","好的");
                    return;
                }
                Config.SleepSecond = s;
                FileHelper.WriteFile(FileHelper.SettingFile,JsonConvert.SerializeObject(Config));
                SleepSecond = s;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("错误","请输入正整数","好的");
            }
        }
        #region 动作时间
        /// <summary>
        /// 动作间隔时间
        /// </summary>
        public DelegateCommand ChangeNumberSecondCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(ChangeNumberSecondFunc) }; }
        private async void ChangeNumberSecondFunc(object obj)
        {
            string rs = await App.Current.MainPage.DisplayPromptAsync("输入", "输入毫秒数", "确定", "取消", "1000 为 1秒，1200 为 1.2秒", 5, Keyboard.Numeric, "");
            try
            {
                int s = int.Parse(rs);
                if (s < 1)
                {
                    await App.Current.MainPage.DisplayAlert("错误", "不能小于1", "好的");
                    return;
                }
                Config.NumberSecond = s;
                FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config));
                NumberSecond = s;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("错误", "请输入正整数", "好的");
            }
        }
        public DelegateCommand ChangeActionNumberSecondCommand { get => new DelegateCommand() { ExecuteAction = new Action<object>(async obj=> {
            var o = obj.ToString();
            var rs = await GetInput("输入", "输入毫秒数", "确定", "取消", "1000 为 1秒，1200 为 1.2秒", 5, Keyboard.Numeric, "", 1);
            if (rs == -1)
            {
                return;
            }
            if (o == "1")
            {
                Config.DownNumberSecond = rs;
                if (FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config)))
                {
                    DownNumberSecond = rs;
                }else
                    await App.Current.MainPage.DisplayAlert("Tips", "修改失败", "OK");
            }
            else if(o == "2")
            {
                Config.UpNumberSecond = rs;
                if (FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config)))
                {
                    UpNumberSecond = rs;
                }else
                    await App.Current.MainPage.DisplayAlert("Tips", "修改失败", "OK");
            }
        })}; }
        #endregion
        private async Task<int> GetInput(string Title,string text,string ok = "OK",string cancel = "Cancel",string placeholder = null,int maxLength = -1,Keyboard keyboard = null,string initValue = "",int minNum = 1)
        {
            string rs = await App.Current.MainPage.DisplayPromptAsync(Title, text, ok, cancel, placeholder, maxLength, keyboard, initValue);
            try
            {
                int s = int.Parse(rs);
                if (s < minNum)
                {
                    await App.Current.MainPage.DisplayAlert("错误", $"不能小于{minNum}", "好的");
                    return -1;
                }
                return s;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("错误", "请输入正整数", "好的");
                return -1;
            }
        }
        /// <summary>
        /// 呼吸律动
        /// </summary>
        public DelegateCommand ChangeRespiratoryRhythmCommand { get => new DelegateCommand {ExecuteAction = new Action<object>(ChangeRespiratoryRhythmFunc) }; }
        private void ChangeRespiratoryRhythmFunc(object obj)
        {
            try
            {
                Config.IsRespiratoryRhythm = 1;
                IsRespiratoryRhythm = Config.IsRespiratoryRhythm == 1 ? "开启" : "关闭";
                FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config));
            }
            catch (Exception ex)
            {
            }
        }
        private string isRespiratoryRhythm;
        public string IsRespiratoryRhythm
        {
            get { return isRespiratoryRhythm; }
            set { isRespiratoryRhythm = value; RaisePropertyChanged("isRespiratoryRhythm"); }
        }
        private int startContinueSecond;
        public int StartContinueSecond
        {
            get { return startContinueSecond; }
            set { startContinueSecond = value; RaisePropertyChanged("StartContinueSecond"); }
        }
        /// <summary>
        /// 开始/继续 倒计时间
        /// </summary>
        public DelegateCommand ChangeStartContinueSecondCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(ChangeStartContinueSecondFunc) }; }
        private async void ChangeStartContinueSecondFunc(object obj)
        {
            string rs = await App.Current.MainPage.DisplayPromptAsync("输入", "输入秒数", "确定", "取消", "1 为 1秒，3 为 3秒", 5, Keyboard.Numeric, "");
            try
            {
                int s = int.Parse(rs);
                if (s < 1)
                {
                    await App.Current.MainPage.DisplayAlert("错误", "不能小于1", "好的");
                    return;
                }
                Config.StartContinueSecond = s;
                FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config));
                StartContinueSecond = s;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("错误", "请输入正整数", "好的");
            }
        }
        public ConfigModel Config { get; set; }
        /// <summary>
        /// ctor
        /// </summary>
        public SettingPageViewModel()
        {
            TraningIcon = Utility.GetImage("ic_query_builder_grey_600_36dp");
            BackCommand = new Command<object>(async obj=> {
                App.Config = this.Config;
                await this.GoAsync("../");
            });
            //read second
            try
            {
                Config = JsonConvert.DeserializeObject<ConfigModel>(FileHelper.ReadFile(FileHelper.SettingFile));
                SleepSecond = Config.SleepSecond;
                NumberSecond = Config.NumberSecond;
                StartContinueSecond = Config.StartContinueSecond;
                UpNumberSecond = Config.UpNumberSecond;
                DownNumberSecond = Config.DownNumberSecond;
                isOffline = Config.IsOffline;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("错误", "读取组间休息时间失败，请重新设置", "好的");
            }
        }
        public new Command<object> BackCommand { get; set; }
    }
}

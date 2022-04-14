using System;
using System.Collections.Generic;
using System.Text;
using XiDeng.Data;
using System.Linq;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Command;
using Newtonsoft.Json;
using System.Web;
using XiDeng.IService;
using Xamarin.Essentials;
using XiDeng.Models.SkillModels;
using Xamarin.CommunityToolkit.ObjectModel;
using XiDeng.Views;

namespace XiDeng.ViewModel
{
    class SkillStyleDetailPageViewModel:BaseViewModel
    {

        #region Image
        private ImageSource audioIcon;

        public ImageSource AudioIcon
        {
            get { return audioIcon; }
            set { audioIcon = value;this.RaisePropertyChanged("AudioIcon"); }
        }

        private ImageSource settingIcon;

        public ImageSource SettingIcon
        {
            get { return settingIcon; }
            set { settingIcon = value; this.RaisePropertyChanged("SettingIcon"); }
        }

        #endregion

        #region UI 
        private bool isShowLayout;

        public bool IsShowLayout
        {
            get { return isShowLayout; }
            set { isShowLayout = value; this.RaisePropertyChanged("IsShowLayout"); }
        }

        public DelegateCommand HideLayoutCommand { get { return new DelegateCommand { ExecuteAction = new Action<object>(HideLayoutFunc) }; } }

        private void HideLayoutFunc(object obj)
        {
            IsShowLayout = false;
            IsChooseStandard = false;
            IsChangeAudioVolume = false;
        }

        public DelegateCommand GoTraningCommand { get { return new DelegateCommand { ExecuteAction = new Action<object>(GoTraningFunc) }; } }

        private void GoTraningFunc(object obj)
        {
            IsShowLayout = true;
            IsChooseStandard = true;
        }


        private bool isCustomTraning;

        public bool IsCustomTraning
        {
            get { return isCustomTraning; }
            set { isCustomTraning = value;this.RaisePropertyChanged("IsCustomTraning"); }
        }
        private bool isDefaultShow;

        public bool IsDefaultShow
        {
            get { return isDefaultShow; }
            set { isDefaultShow = value; RaisePropertyChanged("IsDefaultShow"); }
        }

        private string showInfo;

        public string ShowInfo
        {
            get { return showInfo; }
            set { showInfo = value; RaisePropertyChanged("ShowInfo"); }
        }

        private bool isChooseStandard;

        public bool IsChooseStandard
        {
            get { return isChooseStandard; }
            set { isChooseStandard = value; RaisePropertyChanged("IsChooseStandard"); }
        }

        private bool isChangeAudioVolume;

        public bool IsChangeAudioVolume
        {
            get { return isChangeAudioVolume; }
            set { isChangeAudioVolume = value; RaisePropertyChanged("IsChangeAudioVolume"); }
        }


        #endregion

        private ConfigModel config;

        public ConfigModel Config
        {
            get { return config; }
            set { config = value; RaisePropertyChanged("Config"); }
        }

        #region Audio Volume
        public DelegateCommand AudioVolumePanelCommand
        {
            get => new DelegateCommand
            {
                ExecuteAction = new Action<object>((obj) => {
                    this.IsShowLayout = this.IsChooseStandard ? true : !this.IsShowLayout;
                    this.IsChangeAudioVolume = !this.IsChangeAudioVolume;
                })
            };
        }

        private double backAudioVolume;

        public double BackAudioVolume
        {
            get { return backAudioVolume; }
            set { backAudioVolume = value; RaisePropertyChanged("BackAudioVolume"); }
        }

        public DelegateCommand BackAudioVolumeValueChangedCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(BackAudioVolumeValueChangedFunc) }; }

        private void BackAudioVolumeValueChangedFunc(object obj)
        {
            Config.BackAudioVolume = BackAudioVolume;
            FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config));
        }


        private double personAudioVolume;

        public double PersonAudioVolume
        {
            get { return personAudioVolume; }
            set { personAudioVolume = value; RaisePropertyChanged("PersonAudioVolume"); }
        }

        public DelegateCommand PersonAudioVolumeValueChangedCommand { get => new DelegateCommand {ExecuteAction = new Action<object>((p)=> {
            Config.PersonAudioVolume = PersonAudioVolume;
            FileHelper.WriteFile(FileHelper.SettingFile, JsonConvert.SerializeObject(Config));
        }) }; }

        #endregion

        private StandardDTO currentStandard;

        public StandardDTO CurrentStandard
        {
            get { return currentStandard ?? SkillStyle.Standards[0]; }
            set { currentStandard = value; this.RaisePropertyChanged("CurrentStandard"); }
        }

        private int customGroupNumber = 1;

        public int CustomGroupNumber
        {
            get { return customGroupNumber; }
            set { customGroupNumber = value;this.RaisePropertyChanged("CustomGroupNumber"); }
        }

        private int customNumber = 1;

        public int CustomNumber
        {
            get { return customNumber; }
            set { customNumber = value;RaisePropertyChanged("CustomNumber"); }
        }
        



        private int traningLevel;

        public int TraningLevel
        {
            get { return traningLevel; }
            set { traningLevel = value;
                this.RaisePropertyChanged("TraningLevel");
                IsDefaultShow = true;
                IsCustomTraning = false;
                if (TraningLevel == 0)
                {
                    CurrentStandard = SkillStyle.Standards[0];
                }
                else if (TraningLevel == 1)
                {
                    CurrentStandard = SkillStyle.Standards[1];

                }
                else if (TraningLevel == 2)
                {
                    CurrentStandard = SkillStyle.Standards[2];

                }
                else if (TraningLevel == 3)
                {
                    //custom
                    CurrentStandard = new StandardDTO {GroupNumber = 1,Number = 1, StyleId = SkillStyle.Id, Style = SkillStyle };//TraningType=SkillStyle.Standards[1].TraningType,IsSingle = SkillStyle.Standards[0].IsSingle
                    IsCustomTraning = true;
                    IsDefaultShow = false;
                }
            }
        }


        private SkillStyleDTO skillStyle;

        public SkillStyleDTO SkillStyle
        {
            get { return skillStyle; }
            set { skillStyle = value;this.RaisePropertyChanged("SkillStyle"); }
        }

        public DelegateCommand GotoTraningCommand { get { return new DelegateCommand { ExecuteAction = new Action<object>(GotoTraningFunc) }; } }

        private async void GotoTraningFunc(object obj)
        {
            try
            {
                //$"SkillStyleDetailPage?SkillID=1&SkillStyleID=1"
                if (IsCustomTraning)
                {
                    CurrentStandard = new StandardDTO { GroupNumber = CustomGroupNumber,Number = CustomNumber, StyleId = SkillStyle.Id, Style = SkillStyle };//TraningType = SkillStyle.Standards[1].TraningType, IsSingle = SkillStyle.Standards[0].IsSingle
                }
                string routuri = $"TraningPage?SkillID={this.SkillStyle.SkillId}&StyleID={this.SkillStyle.Id}&StandardJson={Uri.EscapeDataString(JsonConvert.SerializeObject(CurrentStandard))}";
                await this.GoAsync(routuri,false);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Tips", "跳转失败", "OK");
            }
        }
        public DelegateCommand PickerLocalVideoCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(PickerLocalVideoFunc)}; }

        private async void PickerLocalVideoFunc(object obj)
        {
            try
            {
                var fr = await MediaPicker.PickVideoAsync();
                if (fr == null)
                {
                    return;
                }
                var style = SkillDataCommon.Skills.Where(a => a.Id == this.SkillStyle.SkillId).FirstOrDefault().SkillStyles.Where(a => a.Id == this.SkillStyle.Id).First();

                style.VideoUrl = fr.FullPath;
                this.SkillStyle.VideoUrl = fr.FullPath;
                style.VideoUrl = fr.FullPath;
                this.SkillStyle.VideoUrl = fr.FullPath;
                FileHelper.WriteFile(FileHelper.VideoUriFile, JsonConvert.SerializeObject(SkillDataCommon.Skills));
            }
            catch (Exception ex)
            {

            }
        }

        public DelegateCommand UndoVideoCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(UndoVideoFunc)}; }

        private void UndoVideoFunc(object obj)
        {
            try
            {
                var style = SkillDataCommon.Skills.Where(a => a.Id == this.SkillStyle.SkillId).FirstOrDefault().SkillStyles.Where(a => a.Id == this.SkillStyle.Id).First();
                style.VideoUrl = style.VideoUrl;
                style.VideoUrl = style.VideoUrl;
                FileHelper.WriteFile(FileHelper.VideoUriFile, JsonConvert.SerializeObject(SkillDataCommon.Skills));
                App.Current.MainPage.DisplayAlert("Tips", "操作成功！\n请重新进入该界面", "好的");
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Tips","Error!","OK");
            }

        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="SkillID"></param>
        /// <param name="SkillStyleID"></param>
        public SkillStyleDetailPageViewModel(Guid SkillID,Guid SkillStyleID)
        {
            //Temp 01
            SkillStyle = SkillDataCommon.Skills.Where(a => a.Id == SkillID).FirstOrDefault().SkillStyles.Where(a => a.Id == SkillStyleID).FirstOrDefault();

            AppearingCommand = new AsyncCommand(async ()=> {
                try
                {
                    //Init data
                    
                    Config = App.Config;
                    BackAudioVolume = Config.BackAudioVolume;
                    PersonAudioVolume = Config.PersonAudioVolume;
                    //Temp 01

                    if (SkillStyle == null)
                    {
                        await this.Message("获取数据失败,请检查数据是否存在！");

                        return;
                    }
                }
                catch (Exception ex)
                {
                    await this.Message("获取数据失败,请检查数据是否存在！");
                }

                InitImage();
                if (DataCommon.ExerciseLogs == null)
                {
                    await DataCommon.LoadLog();
                }
                if (DataCommon.ExerciseLogs == null)
                {
                    await this.Message("获取上一次训练的记录失败！");
                }
                InitStandard();
                ShowInfo = SkillStyle.TraningType ? "秒" : "次";
            });
            GotoVideoPageCommand = new AsyncCommand(async delegate {
                //await this.Message(this.SkillStyle.Id.ToString());
                
                if (this.skillStyle.VideoUrl == null || this.SkillStyle.SkillName == "倒立撑")
                {
                    await this.Message("暂无视频！");
                    return;
                }
                await this.GoAsync(nameof(WebViewPage) + $"?WebUrl={Uri.EscapeDataString(this.SkillStyle.VideoUrl)}");
            });
        }

        private void InitImage()
        {
            AudioIcon = Utility.GetImage("ic_audiotrack_white_96"); 
            SettingIcon = Utility.GetImage("ic_settings_white_96"); 

        }

        private void InitStandard()
        {
            //记忆上次训练的组数和次数并自动填充
            var Last = DataCommon.ExerciseLogs?.LastOrDefault(a => a.Style.SkillId == this.SkillStyle.SkillId && a.StyleId == this.SkillStyle.Id);
            if (Last == null)
            {
                return;
            }
            StandardDTO standard = new StandardDTO() { 
                GroupNumber = Last.GroupNumber,
                Number = Last.Number,
                StyleId = Last.StyleId
            };
            if (standard == null)
            {
                return;
            }
            TraningLevel = 3;
            CustomGroupNumber = standard.GroupNumber;
            CustomNumber = standard.Number;

            CurrentStandard = new StandardDTO { GroupNumber = standard.GroupNumber, Number = standard.Number,StyleId = SkillStyle.Id,Style = SkillStyle };//TraningType = SkillStyle.Standards[1].TraningType, IsSingle = SkillStyle.Standards[1].IsSingle

        }

        public new AsyncCommand AppearingCommand { get; set; }
        public AsyncCommand GotoVideoPageCommand { get; set; }
    }
}

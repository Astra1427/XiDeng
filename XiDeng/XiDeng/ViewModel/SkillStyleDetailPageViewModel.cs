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

namespace XiDeng.ViewModel
{
    class SkillStyleDetailPageViewModel:NotificationObject
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

        private Standard currentStandard;

        public Standard CurrentStandard
        {
            get { return currentStandard ?? SkillStyle.PrimaryStandard; }
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
                    CurrentStandard = SkillStyle.PrimaryStandard;
                }
                else if (TraningLevel == 1)
                {
                    CurrentStandard = SkillStyle.IntermediateStandard;

                }
                else if (TraningLevel == 2)
                {
                    CurrentStandard = SkillStyle.UpgradeStandard;

                }
                else if (TraningLevel == 3)
                {
                    //custom
                    CurrentStandard = new Standard {GroupsNumber = 1,Number = 1,TraningType=SkillStyle.IntermediateStandard.TraningType,IsSingle = SkillStyle.PrimaryStandard.IsSingle };
                    IsCustomTraning = true;
                    IsDefaultShow = false;
                }
            }
        }


        private SkillStyle skillStyle;

        public SkillStyle SkillStyle
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
                    CurrentStandard = new Standard { GroupsNumber = CustomGroupNumber,Number = CustomNumber, TraningType = SkillStyle.IntermediateStandard.TraningType, IsSingle = SkillStyle.PrimaryStandard.IsSingle };
                }
                string routuri = $"TraningPage?SkillID={this.SkillStyle.SkillID}&StyleID={this.SkillStyle.ID}&StandardJson={Uri.EscapeDataString(JsonConvert.SerializeObject(CurrentStandard))}";
                await Shell.Current.GoToAsync(routuri,false);
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
                var style = DataCommon.Skills.Where(a => a.ID == this.SkillStyle.SkillID).FirstOrDefault().Styles.Where(a => a.ID == this.SkillStyle.ID).First();

                style.DisplayVideoUri = fr.FullPath;
                this.SkillStyle.DisplayVideoUri = fr.FullPath;
                style.LocalVideoUri = fr.FullPath;
                this.SkillStyle.LocalVideoUri = fr.FullPath;
                FileHelper.WriteFile(FileHelper.VideoUriFile, JsonConvert.SerializeObject(DataCommon.Skills));
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
                var style = DataCommon.Skills.Where(a => a.ID == this.SkillStyle.SkillID).FirstOrDefault().Styles.Where(a => a.ID == this.SkillStyle.ID).First();
                style.DisplayVideoUri = style.VideoUri;
                style.LocalVideoUri = style.VideoUri;
                FileHelper.WriteFile(FileHelper.VideoUriFile, JsonConvert.SerializeObject(DataCommon.Skills));
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
        public SkillStyleDetailPageViewModel(int SkillID,int SkillStyleID)
        {
            try
            {
                //Init data
                Config = JsonConvert.DeserializeObject<ConfigModel>(FileHelper.ReadFile(FileHelper.SettingFile));
                BackAudioVolume = Config.BackAudioVolume;
                PersonAudioVolume = Config.PersonAudioVolume;

                SkillStyle = DataCommon.Skills.Where(a => a.ID == SkillID).FirstOrDefault().Styles.Where(a => a.ID == SkillStyleID).FirstOrDefault();
                if (SkillStyle == null)
                {
                    App.Current.MainPage.DisplayAlert("Error", "获取数据失败,请检查数据是否存在！", "OK");
                    
                    return;
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "获取数据失败,请检查数据是否存在！", "OK");
            }

            InitImage();
            InitStandard();
            ShowInfo = SkillStyle.PrimaryStandard.TraningType == 0 ? "次"  : "秒";
        }

        private void InitImage()
        {
            AudioIcon = Utility.GetImage("ic_audiotrack_white_96"); 
            SettingIcon = Utility.GetImage("ic_settings_white_96"); 

        }

        private void InitStandard()
        {
            var Last = DataCommon.ExerciseLogs.LastOrDefault(a => a.SkillID == this.SkillStyle.SkillID && a.StyleID == this.SkillStyle.ID);
            if (Last == null)
            {
                return;
            }
            var standard = Last.ExerciseStandard;
            if (standard == null)
            {
                return;
            }
            TraningLevel = 3;
            CustomGroupNumber = standard.GroupsNumber;
            CustomNumber = standard.Number;
            CurrentStandard = new Standard { GroupsNumber = standard.GroupsNumber, Number = standard.Number, TraningType = SkillStyle.IntermediateStandard.TraningType, IsSingle = SkillStyle.PrimaryStandard.IsSingle };
            
        }
    }
}

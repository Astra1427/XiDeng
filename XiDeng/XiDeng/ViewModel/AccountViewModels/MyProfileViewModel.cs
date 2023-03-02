﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models;
using XiDeng.Models.AccountModels;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;
using XiDeng.Views;
using XiDeng.Views.AccountViews;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class MyProfileViewModel:BaseViewModel
    {
        private AccountDTO account;

        public AccountDTO Account
        {
            get { return account; }
            set { 
                account = value;
                RaisePropertyChanged(nameof(Account));
                IsLogged = value != null;
            }
        }
        public ImageSource ImgRight => Utility.GetImage("arrow_63_240");
        private bool isLogged;
        public bool IsLogged
        {
            get { return isLogged; }
            set
            {
                isLogged = value;
                this.RaisePropertyChanged(nameof(IsLogged));
            }
        }


        public MyProfileViewModel()
        {
            PrepareAccountDataCommand = new AsyncCommand(async delegate {

                await base.Appearing(null);

                this.Account = Utility.LoggedAccount;
                if (this.Account == null || this.Account.JwtToken == null)
                {
                    //try load login info
                    string loginInfoJson = FileHelper.ReadFile(FileHelper.LoginInfoFile);
                    this.Account = loginInfoJson.To<AccountDTO>() ?? Account;
                    Utility.LoggedAccount = this.Account;
                    if (Utility.LoggedAccount == null || Utility.LoggedAccount.JwtToken.IsEmpty())
                    {
                        IsLogged = false;
                    }
                }


            });

            ViewMyProfileCommand = new AsyncCommand<object>(async obj => {
                //check login info is valid in server
                if (this.Account == null || this.Account.JwtToken == null)
                {
                    //goto login page
                    
                    await this.GoAsync(nameof(LoginPage));
                    return;
                }

                //goto user info page
                await this.GoAsync(nameof(PersonalInfoPage));
            });
            GotoMyPlanPageCommand = new AsyncCommand<object>(async obj=> {
                await this.GoAsync(nameof(MyPlanPage));
            });

            GotoSettingsPage = new AsyncCommand<object>(async delegate {
                await this.GoAsync(nameof(SettingPage));
            });
            TapTableItemCommand = new AsyncCommand<object>(async tag=> {
                if (tag == null || tag.ToString().IsEmpty())
                {
                    return;
                }
                await this.GoAsync(tag.ToString());
            });

            SynchronizationCommand = new AsyncCommand<object>(async obj=> {
                string selected = await Shell.Current.DisplayActionSheet("同步", "取消", "", "本地数据上传至云端", "云端数据覆盖至本地");

                await this.Try(async o=> {
                    if (selected == "本地数据上传至云端")
                    {
                        await SynchronizationHelper.LocalToCloud();
                    }
                    else if (selected == "云端数据覆盖至本地")
                    {
                        await SynchronizationHelper.CloudToLocal();
                    }
                    else
                    {

                    }

                    //Set data is updated
                    await App.Database.SetUpdateTable<ExercisePlanDTO>();
                    await App.Database.SetUpdateTable<PlanEachDayDTO>();
                    await App.Database.SetUpdateTable<SkillDTO>();
                    await App.Database.SetUpdateTable<SkillStyleDTO>();
                    await App.Database.SetUpdateTable<StandardDTO>();

                },obj,true);
            });
        }

        

        public AsyncCommand PrepareAccountDataCommand { get; set; }
        public AsyncCommand<object> ViewMyProfileCommand { get; set; }
        public AsyncCommand<object> GotoMyPlanPageCommand { get; set; }
        public AsyncCommand<object> GotoSettingsPage { get; set; }
        public AsyncCommand<object> SynchronizationCommand { get; set; }
        public AsyncCommand<object> TapTableItemCommand { get; set; }



    }
}

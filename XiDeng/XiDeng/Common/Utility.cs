﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Models.AccountModels;
using XiDeng.Models;
using System.Collections.ObjectModel;
using XiDeng.Views.AccountViews;
using System.Threading;
using XiDeng.ViewModel;
using NLog;
using XiDeng.Views;
using XiDeng.Views.ExerciseLogViews;
using System.Linq;
using Android.App;
using XiDeng.Views.ThemeSettingViews;

namespace XiDeng.Common
{
    public static class Utility
    {

        public static ImageSource GetImage(string name)
        {
            try
            {
                return ImageSource.FromStream(() => { return new MemoryStream((byte[])Properties.Resources.ResourceManager.GetObject(name)); });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static AccountDTO LoggedAccount { get; set; } = new AccountDTO();

        #region Extension
        public static bool IsEmpty(this String str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
        #endregion
        #region Message
        public static async Task Message(this Object obj,string message,string title = "提示",string button = "好的")
        {
            
            await Shell.Current.DisplayAlert(title,message,button);
        }
        public static async Task<bool> YesMessage(this Object obj, string message, string title = "提示", string accept = "确定",string cancel = "取消")
        {

            return await Shell.Current.DisplayAlert(title, message, accept,cancel);
        }
        public static async Task Message(this string message,string title = "提示",string button = "好的")
        {
            await Shell.Current.DisplayAlert(title,message,button);
        }
        #endregion

        #region HttpClient
        private static string DebugUrl = "http://192.168.31.131:8001/api/";
        private static string ReleaseUrl = "http://101.33.206.168:8001/api/";
        private static int DebugTimeout = 120;
        private static int ReleaseTimeout = 10;

        //HttpClientHandler及其派生类使开发人员能够配置各种选项, 包括从代理到身份验证。
        private static HttpClientHandler HttpClientHandler = new HttpClientHandler() {
            //如果服务器有 https 证书，但是证书不安全，则需要使用下面语句
            //也就是说，不校验证书，直接允许
            ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true,
            UseCookies = true,
            UseProxy = false,
        };
        //将HttpClientHandler放进HttpClient 构造函数中即可
        public static readonly HttpClient Client = new HttpClient(HttpClientHandler) { Timeout = TimeSpan.FromSeconds(ReleaseTimeout),BaseAddress = new Uri(ReleaseUrl) };


        public static async Task<ResponseModel> GetStringAsync(this string action,string token = null,params string[] paras)
        {
            
            try
            {
                if (paras != null && paras.Length > 0)
                {
                    paras.ForEach(x=> {
                        action += $"/{x}";
                    });
                }
                //check network state
                if (App.Config.IsOffline)
                {
                    return new ResponseModel( System.Net.HttpStatusCode.SeeOther,null,"当前处于离线模式",false);
                }

                if (token is null)
                {
                    token = LoggedAccount?.JwtToken;
                }
                //token
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await Client.GetAsync(action);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //try authorization
                    if (await RefreshToken())
                    {
                        return await action.GetStringAsync(Utility.LoggedAccount.JwtToken);
                    }
                    Utility.LoggedAccount.JwtToken = null;
                    FileHelper.WriteFile(FileHelper.LoginInfoFile, LoggedAccount.ToJson());
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    return new ResponseModel(response.StatusCode, null, "登录信息已过期。\n请重新登录!",response.IsSuccessStatusCode);

                }

                return await ResponseHandler(response, action);

            }
            catch (Exception ex)
            {
                //Logger
                //throw ex;
                return new ResponseModel(System.Net.HttpStatusCode.RequestTimeout, null,"连接超时！",false);
            }
        }

        public static async Task<T> GetAsync<T>(this string action,string token = null)
        {
            try
            {
                if (token is null)
                {
                    token = LoggedAccount?.JwtToken;
                }
                //token
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await action.GetStringAsync(token);
                if (!response.IsSuccessStatusCode)
                {
                    return default;
                }
                return response.Content.To<T>();
            }
            catch (Exception ex)
            {
                //logger
                return default;
            }
        }


        public static async Task<ResponseModel> PostAsync(this string action,string json = "",string token = null)
        {
            return await action.PostAsync(new StringContent(json,Encoding.UTF8,"application/json"),token);
        }

        public static async Task<ResponseModel> PostAsync(this string action,HttpContent content,string token = null)
        {
            try
            {
                //check network state
                if (App.Config.IsOffline)
                {
                    return new ResponseModel(System.Net.HttpStatusCode.SeeOther, null, "当前处于离线模式", false);
                }


                if (token is null)
                {
                    token = LoggedAccount?.JwtToken;
                }
                //token
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                //Client.DefaultRequestHeaders.From.cookie.GetValues("refreshToken");
                var response = await Client.PostAsync(action, content);
                
                return await ResponseHandler(response, action, content);
            }
            catch (Exception ex)
            {
                return new ResponseModel( System.Net.HttpStatusCode.RequestTimeout,null,"连接超时",false);
            }
        }

        /// <summary>
        /// Return the corresponding result according to the status code
        /// </summary>
        /// <param name="response"></param>
        /// <param name="action"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<ResponseModel> ResponseHandler(HttpResponseMessage response,string action = null , HttpContent content = null)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                return new ResponseModel(response.StatusCode, responseContent, null, response.IsSuccessStatusCode);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //try authorization
                if (await RefreshToken())
                {
                    return await action.PostAsync(content, Utility.LoggedAccount.JwtToken);
                }
                Utility.LoggedAccount.JwtToken = null;
                FileHelper.WriteFile(FileHelper.LoginInfoFile, LoggedAccount.ToJson());
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return new ResponseModel(response.StatusCode, null, "登录信息已过期。\n请重新登录", response.IsSuccessStatusCode);
            }
            else
            {
                LogManager.GetLogger(action).Error(response.StatusCode.ToString());

                return new ResponseModel(response.StatusCode, null, responseContent.IsEmpty() ? response.StatusCode.ToString() : responseContent.To<HttpReponseMessageModel>().Message, response.IsSuccessStatusCode);

            }
        }

        /// <summary>
        /// Refresh the user token according to refreshtoken, which is applicable when the user token expires
        /// </summary>
        /// <returns>If true is returned, the refresh is successful. If false is returned, the refresh fails. The failure reason is generally the failure of refreshtoken</returns>
        public static async Task<bool> RefreshToken()
        {
            ResponseModel response = await ActionNames.Account.RefreshToken.PostAsync(
                new StringContent(
                        new {
                            accountId = Utility.LoggedAccount?.Id, 
                            refreshToken = Utility.LoggedAccount?.RefreshToken 
                        }.ToJson(),
                        Encoding.UTF8,
                        "application/json"
                    )
                );

            if (response.IsSuccessStatusCode)
            {
                Utility.LoggedAccount = response.Content.To<AccountDTO>();
                //save info 
                FileHelper.WriteFile(FileHelper.LoginInfoFile, Utility.LoggedAccount.ToJson());
                return true;
            }

            return false;
        }

        #endregion

        #region Json Convert
        public static T To<T>(this string json)
        {
            try
            {
                
                return json == null ? default : JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        public static string ToJson(this object obj)
        {
            try
            {

                return obj == null ? null : JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region List Convert
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> ts)
        {
            return ts == null ? null : new ObservableCollection<T>(ts);
        }
        #endregion


        #region ObservableCollection Extensions
        public static void ForEach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            if (ts == null)
            {
                return;
            }
            foreach (var item in ts)
            {
                action?.Invoke(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> ts, Func<T,Task> action)
        {
            if (ts == null)
            {
                return;
            }
            foreach (var item in ts)
            {
                await action?.Invoke(item);
            }
        }

        public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> ts,IEnumerable<T> ts2)
        {
            if (ts2 == null)
            {
                return ts;
            }

            foreach (var item in ts2)
            {
                ts.Add(item);
            }
            return ts;
        }

        #endregion

        #region Task Extensions
        public static async Task CancellationDelay(int millisecondsDelay, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(millisecondsDelay, cancellationToken);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Shell Extensions
        public static string[] IgnorePreventNavigationKeyWords = new string[] {nameof(LoginPage),"../","..",nameof(ForgotPasswordPage), nameof(RegisterPage),nameof(StylePage),nameof(SkillStyleDetailPage),nameof(TraningPage), nameof(ThanksPage),nameof(DonationPage),nameof(AboutPage),nameof(StatisticsPage), nameof(ExerciseCalendarLogPage),nameof(StretchGuidancePage),nameof(FeedbackEmailPage),nameof(FeedbackPage),nameof(SettingPage), nameof(UserAgreementPage),nameof(ThemeListPage),nameof(AddThemePage),nameof(ThemeSettingPage) };
        public static async Task GoAsync(this BaseViewModel vm,string navigationState,bool animation = true)
        {
            vm.IsE = false;
            Shell.Current.IsEnabled = false;

            try
            {
                string pageName = navigationState.Split('?')[0];
                if ((Utility.LoggedAccount == null || Utility.LoggedAccount.JwtToken.IsEmpty()) && !IgnorePreventNavigationKeyWords.Any(x=>x.Contains(pageName)))
                {
                    await "请登录".Message();
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    return;
                }
                await Shell.Current.GoToAsync(navigationState, animation);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex);
            }
            finally
            {
                vm.IsE = true;
                Shell.Current.IsEnabled = true;
            }


        }
        #endregion


    }
}

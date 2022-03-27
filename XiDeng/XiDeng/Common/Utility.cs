using Newtonsoft.Json;
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
using Microsoft.EntityFrameworkCore;

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
        public static async Task Message(this string message,string title = "提示",string button = "好的")
        {
            await Shell.Current.DisplayAlert(title,message,button);
        }
        #endregion

        #region HttpClient
        private static string DebugUrl = "http://192.168.31.131:8001/api/";
        private static string ReleaseUrl = "http://192.168.137.7:8001/api/";
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
        public static readonly HttpClient Client = new HttpClient(HttpClientHandler) { Timeout = TimeSpan.FromSeconds(DebugTimeout),BaseAddress = new Uri(DebugUrl) };
        
        public static async Task<ResponseModel> GetStringAsync(this string action,string token = null)
        {
            
            try
            {

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

                return JsonConvert.DeserializeObject<T>(json);
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

                return JsonConvert.SerializeObject(obj);
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
            foreach (var item in ts)
            {
                action?.Invoke(item);
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

        #region DbSet Extensions
        public static async Task<int> AddOrUpdateAsync<TEntity>(this DbSet<TEntity> set, TEntity model) where TEntity : ModelBase, new ()
        {
            App.Database.DetachAllEntities();
            if (await set.AnyAsync(x => x.Id == model.Id))
            {
                set.Update(model);
            }
            else
            {
                await set.AddAsync(model);
            }
            return await App.Database.SaveChangesAsync();
        }

        public static async Task<int> AddOrUpdateRangeAsync<TEntity>(this DbSet<TEntity> set, IEnumerable<TEntity> models) where TEntity : ModelBase, new()
        {
            App.Database.DetachAllEntities();
            foreach (var item in models)
            {
                await set.AddOrUpdateAsync(item);
                //if (await set.AnyAsync(x => x.Id == item.Id))
                //{
                //    App.Database.Entry(item).State = EntityState.Modified;
                //}
                //else
                //{
                //    await set.AddAsync(item);
                //}
            }
            return await App.Database.SaveChangesAsync();
        }

        public static async Task<bool> CheckConflictAsync<TEntity>(this DbSet<TEntity> set, TEntity t) where TEntity : ModelBase, new()
        {
            if (t == null)
            {
                return false;
            }

            return await set.AnyAsync(x => x.Id == t.Id && (!x.Updated || x.IsRemoved));
        }

        public static async Task SetDataUpdated<TEntity>(this DbSet<TEntity> set) where TEntity : ModelBase, new()
        {
            (await set.ToListAsync())?.ForEach(x=>x.Updated = true);
            await App.Database.SaveChangesAsync();
        }
        #endregion

    }
}

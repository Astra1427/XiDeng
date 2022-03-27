using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.AccountModels;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class ForgotPasswordPageViewModel:BaseViewModel
    {

        private string email = "1978855028@qq.com";
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                this.RaisePropertyChanged(nameof(Email));
            }
        }

        private string password = "123";
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.RaisePropertyChanged(nameof(Password));
            }
        }
        private string confirmPassword = "123";
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                this.RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }

        private string verifyCode;
        public string VerifyCode
        {
            get { return verifyCode; }
            set
            {
                verifyCode = value;
                this.RaisePropertyChanged(nameof(VerifyCode));
            }
        }

        private static int sendEmailCountDown = 60;
        public int SendEmailCountDown
        {
            get { return sendEmailCountDown; }
            set
            {

                sendEmailCountDown = value == 0 ? 60 : value;

                this.RaisePropertyChanged(nameof(SendEmailCountDown));
                this.RaisePropertyChanged(nameof(SendEmailButtonText));
            }
        }
        private bool canExecuteSendEmail = true;
        public bool CanExecuteSendEmail
        {
            get { return canExecuteSendEmail; }
            set
            {
                canExecuteSendEmail = value;
                this.RaisePropertyChanged(nameof(CanExecuteSendEmail));
            }
        }


        private string sendEmailButtonText = "发送验证码到邮箱";
        public string SendEmailButtonText
        {
            get { return sendEmailButtonText; }
            set
            {
                sendEmailButtonText = SendEmailCountDown == 60 ? "发送验证码到邮箱" : value;
                this.RaisePropertyChanged(nameof(SendEmailButtonText));
            }
        }

        public ForgotPasswordPageViewModel()
        {

            SendCodeToEmailCommand = new Command<object>(async obj => {


                //this.IsRefresh = true;
                CanExecuteSendEmail = false;
                await this.Try(async o => {
                    //check email valid
                    if (Email.IsEmpty() || !Regex.IsMatch(Email, @"^\w+@[a-zA-Z0-9]+((\.[a-z0-9A-Z]{1,})+)$"))
                    {
                        await this.Message("邮箱格式有误!");
                        return;
                    }

                    //Send forgot password verify code to email
                    var response = await ActionNames.Account.SendForgotPasswordEmail.PostAsync(new StringContent(new { this.Email }.ToJson(), Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        await this.Message($"失败：\n{response.Message}");
                        return;
                    }

                    SetCountDown();



                }, obj, true);

                //this.IsRefresh = false ;
            });

            RegisterCommand = new Command<object>(async obj => {

                await this.Try(async o => {
                    var response = await ActionNames.Account.ResetPassword.PostAsync(
                            new StringContent(new ResetPasswordModel {  Password = Password, ConfirmPassword = ConfirmPassword, Email = Email, ResetPasswordCode = VerifyCode }.ToJson(),
                            Encoding.UTF8,
                            "application/json"
                        ));
                    if (!response.IsSuccessStatusCode)
                    {
                        await this.Message($"失败:\n{response.Message}");
                    }
                    else
                    {
                        await this.Message("修改成功！");
                        this.BackCommand.Execute(null);
                    }

                }, obj, true);

            });
        }


        private void SetCountDown()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                SendEmailButtonText = $"{--SendEmailCountDown} 秒后可再次发送";
                if (SendEmailCountDown == 0 || SendEmailCountDown == 60)
                {
                    CanExecuteSendEmail = true;
                    return false;
                }
                CanExecuteSendEmail = false;
                return true;
            });
        }



        public Command<object> SendCodeToEmailCommand { get; set; }
        public Command<object> RegisterCommand { get; set; }


    }
}

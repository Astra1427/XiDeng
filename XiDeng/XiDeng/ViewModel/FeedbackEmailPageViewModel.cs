using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using XiDeng.Common;
namespace XiDeng.ViewModel
{
    public class FeedbackEmailPageViewModel : BaseViewModel
    {
        private string emailSubject;
        public string EmailSubject
        {
            get { return emailSubject; }
            set
            {
                emailSubject = value;
                this.RaisePropertyChanged(nameof(EmailSubject));
            }
        }
        private string emailContent;
        public string EmailContent
        {
            get { return emailContent; }
            set
            {
                emailContent = value;
                this.RaisePropertyChanged(nameof(EmailContent));
            }
        }
        private DateTime exportDate = DateTime.Now;
        public DateTime ExportDate
        {
            get { return exportDate; }
            set
            {
                exportDate = value;
                this.RaisePropertyChanged(nameof(ExportDate));
            }
        }
        public List<string> EmailTo = new List<string>() { "xideng_xd@foxmail.com", "lingjunjie@foxmail.com" };
        public FeedbackEmailPageViewModel()
        {
            SendEmailCommand = new AsyncCommand(async () =>
            {
                await Email.ComposeAsync(subject:null,body:null,to:EmailTo[0]);
            });
            CopyEmailCommand = new AsyncCommand(async ()=> {
                await Clipboard.SetTextAsync("xideng_xd@foxmail.com");
                await Shell.Current.DisplayToastAsync("已复制邮箱");
            });
            ExportErrorLogCommand = new AsyncCommand(async ()=> {
                string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/logs/";
                string file = Path.Combine(logFilePath, ExportDate.ToString("yyyy-MM-dd") + ".log.csv");
                if (!File.Exists(file))
                {
                    await this.Message($"{ExportDate.ToLongDateString()} 没有生成错误日志");
                    return;
                }
                await Share.RequestAsync(new ShareFileRequest(new ShareFile(file)));
            });
        }
        public AsyncCommand SendEmailCommand { get; set; }
        public AsyncCommand CopyEmailCommand { get; set; }
        public AsyncCommand ExportErrorLogCommand { get; set; }
    }
}

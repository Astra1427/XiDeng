using System;
using System.Collections.Generic;
using System.Text;
using XiDeng.Common;

namespace XiDeng.Models.AccountModels
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; } = "请登录";

        [Newtonsoft.Json.JsonIgnore]
        private string photoUrl;
        public string PhotoUrl
        {
            get { return photoUrl.IsEmpty() ? "https://qlogo4.store.qq.com/qzone/2573019279/2573019279/100?1650630063" : photoUrl; }
            set { photoUrl = value; }
        }

        public string PhoneNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Gender { get; set; }
        public int AccountStatus { get; set; }
        public int AccountLevel { get; set; }
        public string Introduce { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

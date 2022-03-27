using System;

namespace XiDeng.Models.AccountModels
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; } = "请登录";
        public string PhotoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Gender { get; set; }
        public int AccountStatus { get; set; }
        public int AccountLevel { get; set; }
        public string Introduce { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

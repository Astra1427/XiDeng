using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Models.AccountModels
{
    public class ResetPasswordModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ResetPasswordCode { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel);
        public string Login(LoginModel loginModel);
        public string ForgotPassword(string EmailId);
        public string ResetPassword(string EmailId, string newPassword, string confirmPassword);
    }
}

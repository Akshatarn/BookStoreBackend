using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;



namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel);
        public string Login(LoginModel loginModel);
        public string ForgotPassword(string EmailId);
        public string ResetPassword(string EmailId, string newPassword, string confirmPassword);
    }
}

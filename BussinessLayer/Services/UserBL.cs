using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;


namespace BussinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Login(LoginModel loginModel)
        {
            try
            {
                return iuserRL.Login(loginModel);  
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel);
    }
}

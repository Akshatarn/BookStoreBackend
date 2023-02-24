using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;



namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel);

    }
}

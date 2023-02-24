using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.Model;

namespace RepositoryLayer.Services
{
    public class UserRL:IUserRL
    {
        private readonly IConfiguration iconfiguration;
        //public static string Key = "akshata_rn00";
        public UserRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=AKSHATA-123\\SQLEXPRESS;Initial Catalog=BookStrore;Integrated Security=True");
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                using (con)
                {
                    SqlCommand cmd = new SqlCommand("SPRegistration",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", userRegistrationModel.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", userRegistrationModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", userRegistrationModel.Password);
                    cmd.Parameters.AddWithValue("@PhoneNum", userRegistrationModel.PhoneNum);
                    
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 0) 
                    {
                        return userRegistrationModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

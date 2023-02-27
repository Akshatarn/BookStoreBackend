using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.Model;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.PortableExecutable;
using javax.security.auth.spi;

namespace RepositoryLayer.Services
{
    public class UserRL:IUserRL
    {
        private readonly IConfiguration iconfiguration;
        public static string Key = "akshata_rn00";
        public string connectionString;
        public UserRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public static string ConvertoEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string ConvertoDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData))
                return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
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
                    cmd.Parameters.AddWithValue("@Password", ConvertoEncrypt(userRegistrationModel.Password));
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

        public string Login(LoginModel loginModel)
        {
            try
            {
                using (con)
                {
                    SqlCommand cmd = new SqlCommand("SPLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", ConvertoEncrypt(loginModel.Password));

                    con.Open();

                     SqlDataReader result = cmd.ExecuteReader();
                    if (result.HasRows)
                    {
                        long UserId = 0;
                        while(result.Read())
                        {
                            UserId = result.IsDBNull("UserId") ? 0 : result.GetInt32("UserId");
                        }
                        string token = GenerateSecurityToken(loginModel.EmailId, UserId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            { 
                throw ex;
            }   
        }
        public string ForgotPassword(string EmailId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPForgotPass", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    con.Open();
                    SqlDataReader result = cmd.ExecuteReader();
                    if (result.HasRows)
                    {
                        long UserId = 0;
                        while(result.Read())
                        {
                            EmailId = Convert.ToString(result["EmailId"] == DBNull.Value ? default : result["EmailId"]);
                            UserId = result.IsDBNull("UserId") ? 0 : result.GetInt32("UserId");

                        }
                        var token = this.GenerateSecurityToken(EmailId,UserId);
                        MSMQ_Model msmq = new MSMQ_Model();
                        msmq.sendData2Queue(token);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }
       
        public string GenerateSecurityToken(string emailId,long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("EmailId",emailId),
                    new Claim("UserId",userId.ToString())
                }),
                Expires =DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

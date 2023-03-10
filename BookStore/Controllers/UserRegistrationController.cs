using BussinessLayer.Interface;
using CommonLayer.Model;
using javax.security.auth.spi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserRegistrationController(IUserBL iuserBL)
        {
                    this.iuserBL= iuserBL;
        }
        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);
                if(result!=null)
                {
                    return Ok(new { success = true, message = "Registration is Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration is UNSuccessful" });
                }

            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = iuserBL.Login(loginModel); 
                if(result!=null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Unsuccessful" });
                }
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string EmailId)
        {
            try
            {
                var result = iuserBL.ForgotPassword(EmailId);
                if (result != null)
                {
                    return Ok(new { success = true, message = " Reset Email sent" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string newPasswoerd,string confirmPassword)
        {
            try
            {
                var EmailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserBL.ResetPassword(EmailId,newPasswoerd,confirmPassword);
                if (result != null)
                {
                    return Ok(new { success = true, message = " Reset Email sent" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}

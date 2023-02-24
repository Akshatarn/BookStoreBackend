using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}

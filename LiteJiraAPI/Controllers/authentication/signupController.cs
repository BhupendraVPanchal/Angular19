using LiteJiraAPI.Business_Logic.authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LiteJiraAPI.Controllers.authentication
{
   
    public class signupController : BaseController
    {
        private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "files\\profile\\");
        signup_bl bl = new signup_bl();

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("API is working!");
        }

        [AllowAnonymous]
        [HttpPost]
        public JObject userSignUp([FromBody] JObject data)
        {
            JObject RtnObj = new JObject();
            try
            {
                return bl.userSignUp(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

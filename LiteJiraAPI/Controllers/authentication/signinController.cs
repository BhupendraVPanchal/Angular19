using LiteJiraAPI.Business_Logic.authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LiteJiraAPI.Controllers.authentication
{
    public class signinController : BaseController
    {
        private signin_bl _bl = new();

        [AllowAnonymous]
        [HttpPost]
        public JObject userSignIn([FromBody] JObject data)
        {
            try
            {
                return _bl.userSignIn(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during login.", ex);
            }
        }
    }
}

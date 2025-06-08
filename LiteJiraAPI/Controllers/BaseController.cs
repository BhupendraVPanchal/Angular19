using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiteJiraAPI.Controllers
{
    [ApiController]
    [Route("api/{controller}/{action}/{id?}")]
    public class BaseController : ControllerBase
    {
    }
}

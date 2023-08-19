using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HT366.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            return Guid.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        }
    }
}

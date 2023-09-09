using Microsoft.AspNetCore.Mvc;

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
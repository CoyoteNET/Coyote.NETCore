using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CoyoteNETCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : DefaultController
    {
        [HttpGet("LogIn")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "GitHub");
        }

        [HttpGet("LogOff")]
        public IActionResult LogOff()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" });
        }
    }
}
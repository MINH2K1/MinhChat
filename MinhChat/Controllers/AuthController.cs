using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhChat.Interface;
using MinhChat.Model;

namespace MinhChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> LoginUserAsync(
            [FromBody] UserLoginRequest request)
        {
            var result = await authService.LoginUserAsync(request);

            return result;
        }
    }
}

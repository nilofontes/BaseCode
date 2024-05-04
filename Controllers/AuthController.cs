using BaseCode.Services;
using BaseCode.Models;
using Microsoft.AspNetCore.Mvc;
using BaseCode.Interface;

namespace BaseCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) 
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var tokenResponse = _authService.AuthenticateASync(request.UserName, request.PassWordHash, request.ApiKey);

            return Ok(tokenResponse);
        }

        [HttpPost("token")]
        public IActionResult Token() 
        {
            var tokenResponse = _authService.ValidateJwt(HttpContext);
            
            if (tokenResponse.StatusCode == 200)
            {
                return Ok(tokenResponse);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("refreshToken")]
        public IActionResult AuthenticateRefreshToken() 
        {
            var currentTokenResponse = _authService.ValidateJwt(HttpContext);

            var tokenResponse = _authService.AuthenticateRefreshToken(currentTokenResponse.Token);
            if (currentTokenResponse.StatusCode == 200)
            {
                return Ok(tokenResponse);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

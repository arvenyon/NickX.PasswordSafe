using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NickX.PasswordSafe.WebAPI.Domain.Manager.Interfaces;
using NickX.PasswordSafe.WebAPI.Domain.Models;

namespace NickX.PasswordSafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IJwtAuthenticationManager _jwtAuthManager;
        public AuthenticationController(IJwtAuthenticationManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCredential userCredentials)
        {
            var token = _jwtAuthManager.Authenticate(userCredentials.Username, userCredentials.Password);

            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();

            return Ok(token);
        }

    }
}

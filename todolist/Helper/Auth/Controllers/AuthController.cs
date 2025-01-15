using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using todolist.Helper.Auth.Command;
using Microsoft.AspNetCore.Authorization;

namespace todolist.Helper.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            bool response = await _mediator.Send(command);

            if (response) { return Ok(); }

            return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
      
            string response = await _mediator.Send(command);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }
        [HttpPost("logout")]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
};

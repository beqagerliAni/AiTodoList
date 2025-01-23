using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_do_List.src.Modules.User.Command;
using todolist.src.Modules.User.Command;

namespace todolist.src.Modules.User.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(CreateUser command)
        {
            var result = await _mediator.Send(command);
            if (result)
                return Ok(new { message = "User created successfully" });
            else
                return BadRequest(new { message = "Failed to create user" });
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new FindAllCommand());
            if (users == null || users.Count == 0)
                return NotFound(new { message = "No users found" });

            return Ok(users);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get()
        {   
            var user = await _mediator.Send(new FindOneUserCommand());
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpPut]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
                return Ok(new { message = "User updated successfully" });
            else
                return BadRequest(new { message = "Failed to update user" });
        }

        [HttpDelete]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> Delete()
        {
            var result = await _mediator.Send(new UserDeleteCommand());
            if (result)
                return Ok(new { message = "User deleted successfully" });
            else
                return NotFound(new { message = "User not found" });
        }
    }
}

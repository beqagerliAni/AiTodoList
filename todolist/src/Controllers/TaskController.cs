using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using To_do_List.src.Modules.Task.Command;
using To_do_List.src.Modules.Task.Module;
using todolist.src.Modules.Task.Command;
using Microsoft.AspNetCore.Authorization;

namespace todolist.src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateTask command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Task created successfully");
            }
            return BadRequest("Failed to create task");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            Guid guid = Guid.Parse(id);
            var result = await _mediator.Send(new DeleteTaskCommand { Id = guid });
            if (result)
            {
                return Ok("Task deleted successfully");
            }
            return NotFound("Task not found");
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _mediator.Send(new FindAllCommand());
            return Ok(tasks);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTask command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Task updated successfully");
            }
            return NotFound("Task not found");
        }
    }
}

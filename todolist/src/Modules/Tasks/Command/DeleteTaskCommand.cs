using MediatR;

namespace todolist.src.Modules.Task.Command
{
    public class DeleteTaskCommand: IRequest<bool>
    {
        public Guid Guid { get; set; } 
    }
}

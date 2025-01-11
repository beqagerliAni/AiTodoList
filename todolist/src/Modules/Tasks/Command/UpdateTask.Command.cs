using MediatR;
using To_do_List.src.Modules.Task.Command;

namespace todolist.src.Modules.Task.Command
{
    public class UpdateTask: CreateTask
    {
        public required Guid Id { get; set; }   
    }
}

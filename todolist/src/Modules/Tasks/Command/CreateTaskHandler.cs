using MediatR;
using To_do_List.src.Modules.Task.Command;
using To_do_List.src.Modules.Tasks.Repository;

namespace todolist.src.Modules.Task.Command
{
    public class CreateTaskHandler : IRequestHandler<CreateTask, bool>
    {
        ITaskRepositroy _taskRepositroy { get; set; }
        public CreateTaskHandler(ITaskRepositroy repositroy)
        {

           _taskRepositroy = repositroy;
            
        }
        public Task<bool> Handle(CreateTask request, CancellationToken cancellationToken)
        {
            return _taskRepositroy.Create(request);
        }
    }
}

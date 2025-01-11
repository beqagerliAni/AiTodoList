using MediatR;
using To_do_List.src.Modules.Tasks.Repository;

namespace todolist.src.Modules.Task.Command
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTask, bool>
    {

        ITaskRepositroy _taskRepositroy { get; set; }
        public UpdateTaskHandler(ITaskRepositroy repositroy)
        {

            _taskRepositroy = repositroy;

        }
        public Task<bool> Handle(UpdateTask request, CancellationToken cancellationToken)
        {
            return _taskRepositroy.Update(request);
        }
    }
}

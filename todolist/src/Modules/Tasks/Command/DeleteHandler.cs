using MediatR;
using To_do_List.src.Modules.Tasks.Repository;
using todolist.src.Modules.User.Command;

namespace todolist.src.Modules.Task.Command
{
    public class DeleteHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        ITaskRepositroy _taskRepositroy { get; set; }
        public DeleteHandler(ITaskRepositroy repositroy)
        {

            _taskRepositroy = repositroy;

        }
        public Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
           return _taskRepositroy.Delete(request);
        }
    }
}

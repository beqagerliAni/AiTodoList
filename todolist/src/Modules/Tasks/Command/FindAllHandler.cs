using MediatR;
using To_do_List.src.Modules.Task.Module;
using To_do_List.src.Modules.Tasks.Repository;

namespace todolist.src.Modules.Task.Command
{
    public class FindAllHandler : IRequestHandler<FindAllCommand, List<TaskModule>>
    {
        ITaskRepositroy _taskRepositroy { get; set; }
        public FindAllHandler(ITaskRepositroy repositroy)
        {

            _taskRepositroy = repositroy;

        }
        public Task<List<TaskModule>> Handle(FindAllCommand request, CancellationToken cancellationToken)
        {
            return _taskRepositroy.FindAll();
        }
    }
}

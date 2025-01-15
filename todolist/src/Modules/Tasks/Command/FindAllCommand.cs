using MediatR;
using To_do_List.src.Modules.Task.Entity;
using To_do_List.src.Modules.Task.Module;

namespace todolist.src.Modules.Task.Command
{
    public class FindAllCommand: IRequest<List<TaskDto>>
    {

    }
}

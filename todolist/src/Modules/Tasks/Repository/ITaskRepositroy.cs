using To_do_List.src.Modules.Task.Command;
using To_do_List.src.Modules.Task.Module;
using todolist.src.Modules.Task.Command;

namespace To_do_List.src.Modules.Tasks.Repository
{
    public interface ITaskRepositroy
    {
        public Task<bool> Create(CreateTask module);
        public Task<bool>  Update(UpdateTask module);
        public Task<bool> Delete(DeleteTaskCommand module);
        public Task<List<TaskModule>> FindAll();
        public TaskModule FindOne(string module);
    }
}

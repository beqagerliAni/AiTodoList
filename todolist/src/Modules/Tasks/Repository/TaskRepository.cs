using To_do_List.src.Modules.Task.Command;
using To_do_List.src.Modules.Task.Entity;
using To_do_List.src.Modules.Task.Module;
using todolist.src.Modules.Task.Command;

namespace To_do_List.src.Modules.Tasks.Repository
{
    public class TaskRepository : ITaskRepositroy
    {
        private readonly appDbcontext _dbContext;

        public TaskRepository(appDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(CreateTask module)
        {
            Guid id = Guid.NewGuid();
            TaskEntity newTask = new TaskEntity
            {
                Title = module.Title,
                UserId = id,
            };
            _dbContext.Task.Add(newTask);
            if(module.text != null)
            {
                foreach (string text in module.text)
                {
                    TodoEntity toDo = new TodoEntity { TaskId = newTask.Id, text = text };
                    _dbContext.ToDo.Add(toDo);
                    await _dbContext.SaveChangesAsync();

                }
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(DeleteTaskCommand module)
        {
            TaskEntity? task = (from Task in _dbContext.Task
                        where Task.Id == module.Id
                        select new TaskEntity { Title = Task.Title, UserId = module.Id }).FirstOrDefault();

            if (task == null)
            {
                return false;
            }

            _dbContext.Task.Remove(task);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public  Task<List<TaskModule>> FindAll()
        {
            List<TaskModule> tasks = (from Task in _dbContext.Task
                                       select new TaskModule(Task)).ToList();
            return System.Threading.Tasks.Task.FromResult(tasks);

        }

        public TaskModule FindOne(string module)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(UpdateTask module)
        {
            var task = _dbContext.Task.FirstOrDefault(t => t.Id == module.Id);
            if (task == null)
            {
                return false;
            }

            task.Title = module.Title;
            
       
            _dbContext.Task.Update(task);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

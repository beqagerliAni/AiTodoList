using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ml_todolist.src.ML;
using To_do_List.src.Modules.Task.Command;
using To_do_List.src.Modules.Task.Entity;
using todolist.Helper.Jwt;
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
            JwtSecurityToken UserClaim = await JwtDecode.DecodeAuthToken("Authorization");

            string? idClaim = UserClaim.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (idClaim == null) { throw new NotImplementedException();  }

            TaskEntity newTask = new TaskEntity
            {
                Title = module.Title,
                UserId = Guid.Parse(idClaim),
            };
            _dbContext.Task.Add(newTask);
                if(module.text != null)
                {
                    foreach (string text in module.text)
                    {
                        string level = MLModel.predictDificulty(text);

                        Console.WriteLine(level);

                        TodoEntity toDo = new TodoEntity 
                        { 
                            TaskId = newTask.Id,
                            text = text,
                            dificultyLevel =  int.Parse(text)
                        };

                        _dbContext.ToDo.Add(toDo);
                        await _dbContext.SaveChangesAsync();

                    }
                }
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(DeleteTaskCommand command)
        {
            TaskEntity? task = (from Task in _dbContext.Task
                        where Task.Id == command.Guid
                        select new TaskEntity { Title = Task.Title, UserId = command.Guid }).FirstOrDefault();

            if (task == null)
            {
                return false;
            }

            _dbContext.Task.Remove(task);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public  Task<List<TaskDto>> FindAll()
        {
            List<TaskDto> tasksWithNestedTodo = (from task in _dbContext.Task
                                                 join todo in _dbContext.ToDo
                                                 on task.Id equals todo.TaskId
                                                 select new TaskDto
                                                 {
                                                     Id = task.Id,
                                                     Title = task.Title,
                                                     Todo = new TodoDto
                                                     {
                                                         TaskId = todo.TaskId,
                                                         Text = todo.text
                                                     }
                                                 }).ToList();


            return System.Threading.Tasks.Task.FromResult(tasksWithNestedTodo);
        }
        public async Task<bool> Update(UpdateTask module)
        {
            var task = _dbContext.Task.FirstOrDefault(t => t.Id == module.id);
            if (task == null)
            {
                return false;
            }
            if (module.updateTodo != null) { 
                foreach(updateTodo text in module.updateTodo)
                {
                    Guid id = Guid.Parse(text.Id);

                    TodoEntity? todo = _dbContext.ToDo.FirstOrDefault(t => t.Id == id);

                    if (todo == null)
                    {
                        return false;
                    }

                    todo.text = text.text;
                    _dbContext.ToDo.Update(todo);
                }
            }
            task.Title = module.Title;
            
       
            _dbContext.Task.Update(task);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

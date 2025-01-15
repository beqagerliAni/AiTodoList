using System.Text.Json.Serialization;
using To_do_List.src.Modules.Task.Entity;

namespace To_do_List.src.Modules.Task.Module
{
    public class TaskModule
    {
        public string Title { get; set; } = string.Empty;
        public TodoEntity todo { get; set; } = new TodoEntity();
        public TaskEntity  Entity { get; set; } = new TaskEntity();
        public TaskModule(TaskEntity entity, TodoEntity todoEntity)
        {
            Title = entity.Title;
            todo = todoEntity;
        }
        
   
    }
}
public class TodoDto
{
    public Guid TaskId { get; set; }
    public string Text { get; set; } = string.Empty;
}

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TodoDto Todo { get; set; } = new TodoDto();
}

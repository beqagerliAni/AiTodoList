using To_do_List.src.Modules.Task.Entity;

namespace To_do_List.src.Modules.Task.Module
{
    public class TaskModule
    {
        private string? Title { get; set; }
        private string?  todo { get; set; }
        private TaskEntity? Entity { get; set; }
        public TaskModule(TaskEntity entity, int i)
        {
            Title = entity.Title;
            todo = entity.Todo[i].text;
        }
        public TaskModule(TaskEntity entity)
        {
            Entity = entity;
        }

    }
}


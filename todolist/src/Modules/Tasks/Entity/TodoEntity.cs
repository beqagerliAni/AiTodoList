using To_do_List.Helper.Entity;

namespace To_do_List.src.Modules.Task.Entity
{
    public class TodoEntity : BaseEntity
    {
        public  string text { get; set; } = string.Empty;
        public  Guid TaskId { get; set; } = Guid.NewGuid();
        public TaskEntity? Taskentity { get; set; }
        public int dificultyLevel { get; set; }

    }
}

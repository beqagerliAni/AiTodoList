using To_do_List.Helper.Entity;

namespace To_do_List.src.Modules.Task.Entity
{
    public class TodoEntity : BaseEntity
    {
        public required string text { get; set; }
        public required Guid TaskId { get; set; }
        public TaskEntity? Taskentity { get; set; }

    }
}

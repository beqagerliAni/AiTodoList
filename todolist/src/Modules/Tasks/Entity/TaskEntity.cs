using To_do_List.Helper.Entity;
using To_do_List.src.Modules.User.Entity;

namespace To_do_List.src.Modules.Task.Entity
{
    public class TaskEntity : BaseEntity
    {
        public required string Title { get; set; }
        public List<TodoEntity>? Todo { get; set; }
        public UserEntity? User { get; set; } 
        public required Guid UserId { get; set; }
    }
}

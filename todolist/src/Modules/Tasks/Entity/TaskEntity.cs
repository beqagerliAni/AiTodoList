using To_do_List.Helper.Entity;
using To_do_List.src.Modules.User.Entity;

namespace To_do_List.src.Modules.Task.Entity
{
    public class TaskEntity : BaseEntity
    {
        public  string Title { get; set; } = string.Empty;
        public List<TodoEntity>? Todo { get; set; }
        public UserEntity? User { get; set; } 
        public Guid UserId { get; set; } =  new Guid();
    }
}

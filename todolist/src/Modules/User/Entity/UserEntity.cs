using To_do_List.Helper.Entity;
using To_do_List.src.Modules.Task.Entity;

namespace To_do_List.src.Modules.User.Entity
{
    public class UserEntity : BaseEntity
    {
        public required string name { get; set; }
        public required string password { get; set; }
        public required string email { get; set; }
        public List<TaskEntity>? taskEntity { get; set; } = [];

    }
}

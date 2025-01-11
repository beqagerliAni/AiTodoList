namespace To_do_List.Helper.Entity
{
    public class BaseEntity
    {
        public  Guid Id { get; set; } =  Guid.NewGuid();
        public  DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public  DateTime? DeletedAt {  get; set; }
        public  DateTime? UpdatedAt {  get; set; } 
    }
}

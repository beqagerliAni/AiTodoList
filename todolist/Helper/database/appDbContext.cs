using Microsoft.EntityFrameworkCore;
using To_do_List.Helper.Entity;
using To_do_List.src.database.configuration;
using To_do_List.src.Modules.Task.Entity;
using To_do_List.src.Modules.User.Entity;

public class appDbcontext : DbContext
{
    public required DbSet<TaskEntity> Task { get; set; }
    public  required DbSet<UserEntity> User { get; set; }
    public  required DbSet<TodoEntity> ToDo { get; set; }

    public appDbcontext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ToDoConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    baseEntity.CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    baseEntity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    baseEntity.DeletedAt = DateTime.UtcNow;
                }
            }
        }
        return base.SaveChanges();
    }

}


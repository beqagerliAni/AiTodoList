using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using To_do_List.src.Modules.Task.Entity;

namespace To_do_List.src.database.configuration
{
    public class ToDoConfiguration : IEntityTypeConfiguration<TodoEntity>
    {
        public void Configure(EntityTypeBuilder<TodoEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder
                .HasOne(t => t.Taskentity)
                .WithMany(x => x.Todo)
                .HasForeignKey(t => t.TaskId);
        }
    }
}

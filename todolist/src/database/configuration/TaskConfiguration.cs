using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using To_do_List.src.Modules.Task.Entity;

namespace To_do_List.src.database.configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Id)
                .IsUnique();

            builder
                .HasOne(t => t.User)
                .WithMany(u => u.taskEntity)
                .HasForeignKey(t => t.UserId);

        }
    }
}

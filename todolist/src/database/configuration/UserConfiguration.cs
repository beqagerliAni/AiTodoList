using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using To_do_List.src.Modules.User.Entity;

namespace To_do_List.src.database.configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasIndex(u => u.email)
                .IsUnique();

            builder
                .HasMany(u => u.taskEntity)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
        }
    }
}

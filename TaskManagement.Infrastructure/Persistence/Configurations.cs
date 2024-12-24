using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.Id); // Shared primary key

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }

    public class TaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.HasOne(t => t.TaskOwner)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.TaskOwnerId);

            builder.Property(t => t.TaskStatus)
                .HasConversion<string>();
        }
    }

    public class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
    {
        public void Configure(EntityTypeBuilder<SubTask> builder)
        {
            builder.HasOne(st => st.Task)
                .WithMany(t => t.Subtasks)
                .HasForeignKey(st => st.TaskId);
        }
    }
}

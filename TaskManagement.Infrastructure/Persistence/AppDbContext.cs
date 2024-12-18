using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Domain.Entities.UserTask> UserTasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new SubTaskConfiguration());

            // Global naming convetion
            SetGlobalNamingConventions(modelBuilder);
        }

        private static void SetGlobalNamingConventions(ModelBuilder modelBuilder)
        {
            // Table & column naming convention
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.GetColumnName()));
                }

                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    foreignKey.SetConstraintName(ToSnakeCase(foreignKey.GetConstraintName()));
                }
            }

        }

        private static string? ToSnakeCase(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var result = string.Concat(name.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? $"_{x}" : $"{x}")).ToLower();

            return result;
        }
    }
}

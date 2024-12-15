using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        }

        private string? ToSnakeCase(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var result = string.Concat(name.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? $"_{x}" : $"{x}")).ToLower();

            return result;
        }
    }
}

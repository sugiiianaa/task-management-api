using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManagement.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Get the current directory
            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = (Directory.GetParent(currentDirectory)?.FullName) 
                ?? throw new InvalidOperationException("Unable to determine the parent directory.");
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile(Path.Combine(parentDirectory, "TaskManagementAPI", "appsettings.json"))
                .AddJsonFile(Path.Combine(parentDirectory, "TaskManagementAPI", "appsettings.Development.json"), optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

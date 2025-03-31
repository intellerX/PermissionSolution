using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Permission.Infrastructure.Context;

namespace PermissionApi
{
    public class PersistenceContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
    {
        public PersistenceContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PersistenceContext>();
            var connectionString = configuration.GetConnectionString("database");

            optionsBuilder.UseSqlServer(connectionString);

            return new PersistenceContext(optionsBuilder.Options);
        }
    }
}

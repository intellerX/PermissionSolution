using Microsoft.EntityFrameworkCore;
using Permission.Domain.Entities;

namespace Permission.Infrastructure.Context
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options)
            : base(options) { }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionEntity>()
                .HasOne(p => p.PermissionType)
                .WithMany()
                .HasForeignKey(p => p.PermissionTypeId);

            modelBuilder.Entity<PermissionTypeEntity>();

            base.OnModelCreating(modelBuilder);
        }
    }
}

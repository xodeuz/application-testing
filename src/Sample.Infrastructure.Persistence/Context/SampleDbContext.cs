using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contacts;
using Sample.Domain.SeedWork;
#nullable disable

namespace Sample.Infrastructure.Persistence.Context
{
    public class SampleDbContext : DbContext, ISampleDbContext, IUnitOfWork
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PreSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void PreSaveChanges()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(e => e.Entity is TimeTrackableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .Select(e => e.Entity as TimeTrackableEntity);

            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    if (entity.Created == null)
                    {
                        entity.Created = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.Updated = DateTimeOffset.UtcNow;
                    }
                }
            }
        }
    }
}

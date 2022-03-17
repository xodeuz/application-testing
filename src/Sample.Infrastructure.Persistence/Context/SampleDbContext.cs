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
    }
}

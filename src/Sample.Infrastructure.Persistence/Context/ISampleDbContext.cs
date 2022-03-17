using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sample.Domain.Contacts;
using Sample.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;

namespace Sample.Infrastructure.Persistence.Context
{
    public interface ISampleDbContext : IUnitOfWork, IDisposable
    {
        DbSet<Contact> Contacts { get; }
        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
    }
}

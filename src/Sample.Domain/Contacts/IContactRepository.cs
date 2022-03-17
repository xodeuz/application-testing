using Sample.Domain.SeedWork;

namespace Sample.Domain.Contacts
{
    public interface IContactRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Contact>> GetAsync(CancellationToken ct);
        Task<Contact> GetByIdAsync(Guid id, CancellationToken ct);
        Contact Add(Contact contact);
        Contact Update(Contact contact);
        void Delete(Contact contact);
    }
}

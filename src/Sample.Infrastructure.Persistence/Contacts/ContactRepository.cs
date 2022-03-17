using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contacts;
using Sample.Domain.SeedWork;
using Sample.Infrastructure.Persistence.Context;

namespace Sample.Infrastructure.Persistence.Contacts
{
    public class ContactRepository : IContactRepository
    {
        public IUnitOfWork UnitOfWork => _context;

        private readonly ISampleDbContext _context;

        public ContactRepository(ISampleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAsync(CancellationToken ct)
        {
            return await _context.Contacts.ToListAsync(ct);
        }

        public async Task<Contact> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Contacts.SingleAsync(query => query.Id == id, ct);
        }

        public Contact Add(Contact contact)
        {
            return _context.Contacts.Add(contact).Entity;
        }

        public Contact Update(Contact contact)
        {
            return _context.Contacts.Update(contact).Entity;
        }

        public void Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);
        }
    }
}

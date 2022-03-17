namespace Sample.Domain.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _respository;
        public ContactService(IContactRepository respository)
        {
            _respository = respository;
        }

        public async Task<IEnumerable<ContactDto>> GetAllAsync(CancellationToken ct)
        {
            var entities = await _respository.GetAsync(ct);
            return entities.ToDtoCollection();
        }

        public async Task<ContactDto> GetByIdAsync(Guid id, CancellationToken ct)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentException("Id cannot be empty");
            }

            var entity = await _respository.GetByIdAsync(id, ct);
            return entity.ToDto();
        }

        public async Task<ContactDto> CreateAsync(string name, string email, string phoneNumber, CancellationToken ct)
        {
            var contact = Contact.New(name, email, phoneNumber);
            _respository.Add(contact);
            await _respository.UnitOfWork.SaveChangesAsync(ct);
            return contact.ToDto();
        }

        public async Task<bool> UpdateAsync(ContactDto dto, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            var entity = await _respository.GetByIdAsync(dto.Id, ct);

            entity.ChangePhoneNumber(dto.PhoneNumber);
            entity.ChangeEmailAddress(dto.Email);
            entity.ChangeName(dto.Name);

            _respository.Update(entity);
            return await _respository.UnitOfWork.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        {
            if(Guid.Empty == id)
            {
                throw new ArgumentException("Id cannot be empty");
            }

            var entity = await _respository.GetByIdAsync(id, ct);
            _respository.Delete(entity);
            return await _respository.UnitOfWork.SaveChangesAsync(ct) > 0;
        }
    }
}

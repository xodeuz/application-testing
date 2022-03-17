namespace Sample.Domain.Contacts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllAsync(CancellationToken ct);
        Task<ContactDto> GetByIdAsync(Guid id, CancellationToken ct);
        Task<ContactDto> CreateAsync(string name, string email, string phoneNumber, CancellationToken ct);
        Task<bool> UpdateAsync(ContactDto dto, CancellationToken ct);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    }
}

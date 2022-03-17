using Sample.Api.RequestResponse;
using Sample.Domain.Contacts;

namespace Sample.Api.Extensions
{
    public static class ContactRequestExtensions
    {
        public static ContactDto ToDto(this ContactRequest request, Guid id)
            => new () { Email = request.Email, Id = id, Name = request.Name, PhoneNumber = request.PhoneNumber };
    }
}

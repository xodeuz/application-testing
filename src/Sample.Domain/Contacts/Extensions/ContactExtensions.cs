namespace Sample.Domain.Contacts
{
    internal static class ContactExtensions
    {
        public static ContactDto ToDto(this Contact contact)
        {
            return new ContactDto
            {
                Email = contact.Email,
                Id = contact.Id,
                Name =  contact.Name,
                PhoneNumber = contact.PhoneNumber,
            };
        }

        public static IEnumerable<ContactDto> ToDtoCollection(this IEnumerable<Contact> contacts)
        {
            if(contacts is null) 
                return Enumerable.Empty<ContactDto>();
            return contacts.Select(contact => contact.ToDto()).ToList();
        }
    }
}

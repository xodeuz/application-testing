namespace Sample.Domain.Contacts
{
    public record ContactDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
